using LiteDB;
using ScalableWeb.Diff.Application.Services;
using ScalableWeb.Diff.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ScalableWeb.Diff.Provider.Services
{
    public class DiffService : IDiffService
    {
        public string Get(int id)
        {
            using (var db = new LiteDatabase(@"Diff.db"))
            {
                var comparers = db.GetCollection<Comparer>("comparers");
                var comparer = comparers.Find(a => a.Id == id).FirstOrDefault();

                if (comparer is null)
                    return string.Empty;

                if (comparer.Left.Equals(comparer.Right))
                    return "Content is the same.";

                if (!comparer.Left.Length.Equals(comparer.Right.Length))
                    return "Content is not of the same size.";

                string jsonLeft = Encoding.UTF8.GetString(Convert.FromBase64String(comparer.Left));
                string jsonRight = Encoding.UTF8.GetString(Convert.FromBase64String(comparer.Right));

                int length = 0;
                int? offset = null;
                List<DiffEntry> diffEntries = new List<DiffEntry>();

                for (int i = 0; i < jsonLeft.Length; i++)
                {
                    if (jsonLeft[i] != jsonRight[i])
                    {
                        if (length == 0)
                            offset = i;

                        length++;
                    }
                    else if (length > 0)
                    {
                        diffEntries.Add(new DiffEntry((int)offset, length));
                        length = 0;
                        offset = 0;
                    }
                }

                return Newtonsoft.Json.JsonConvert.SerializeObject(diffEntries);
            }
        }

        public string SetLeft(int id, string leftContent)
        {
            if (string.IsNullOrWhiteSpace(leftContent))
                throw new ArgumentException("invalid content");

            if (!IsBase64String(leftContent))
                throw new ArgumentException("wrong type content");

            using (var db = new LiteDatabase(@"Diff.db"))
            {
                var comparers = db.GetCollection<Comparer>("comparers");
                var comparer = comparers.Find(a => a.Id == id).FirstOrDefault();

                if (comparer is null)
                {
                    comparers.Insert(new Comparer()
                    {
                        Id = id,
                        Left = leftContent,
                    });
                }
                else
                {
                    comparer.Left = leftContent;
                    comparers.Update(comparer);
                }

                return "Left-side Content set";
            }
        }

        public string SetRight(int id, string rightContent)
        {
            if (string.IsNullOrWhiteSpace(rightContent))
                throw new ArgumentException("invalid content");

            if (!IsBase64String(rightContent))
                throw new ArgumentException("wrong type content");

            using (var db = new LiteDatabase(@"Diff.db"))
            {
                var comparers = db.GetCollection<Comparer>("comparers");
                var comparer = comparers.Find(a => a.Id == id).FirstOrDefault();

                if (comparer is null)
                {
                    comparers.Insert(new Comparer()
                    {
                        Id = id,
                        Right = rightContent,
                    });
                }
                else
                {
                    comparer.Right = rightContent;
                    comparers.Update(comparer);
                }

                return "Right-side Content set";
            }
        }

        private bool IsBase64String(string s)
        {
            s = s.Trim();
            return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);

        }
    }
}