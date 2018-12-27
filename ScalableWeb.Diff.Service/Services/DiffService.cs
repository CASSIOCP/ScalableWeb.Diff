using ScalableWeb.Diff.Application.Services;
using ScalableWeb.Diff.Data.Acess.Repositories;
using ScalableWeb.Diff.Model.Enums;
using ScalableWeb.Diff.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ScalableWeb.Diff.Provider.Services
{
    public class DiffService : IDiffService
    {     
        public string Get(int id)
        {
            using (var comparerRepository = new ComparerRepository())
            {
                var comparer = comparerRepository.Find(id);

                if (comparer is null)
                    throw new ArgumentNullException("Comparer not found");

                if (comparer.Left.Equals(comparer.Right))
                    return "Contents are the same.";

                if (!comparer.Left.Length.Equals(comparer.Right.Length))
                    return "Contents are not of the same size.";

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

        public string SetContent(int id, string content, Side side = Side.Left)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("null content");

            if (!IsBase64String(content))
                throw new ArgumentException("invalid content");

            using (var comparerRepository = new ComparerRepository())
            {
                var comparer = comparerRepository.Find(id);

                if (comparer is null)
                {
                    comparer = new Comparer()
                    {
                        Id = id
                    };

                    comparerRepository.Add(comparer);
                }

                if (side == Side.Left)
                    comparer.Left = content;
                else
                    comparer.Right = content;

                comparerRepository.Update(comparer);

                return $"{side}-side Content was set";
            }
        }

        /// <summary>
        /// Determine if the parameter is a valid base64 encoded string.
        /// </summary>
        /// <param name="s">entry string</param>
        private bool IsBase64String(string s)
        {
            s = s.Trim();
            return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
        }
    }
}