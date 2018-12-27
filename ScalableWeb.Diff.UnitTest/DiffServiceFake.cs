using ScalableWeb.Diff.Application.Services;
using ScalableWeb.Diff.Data.Acess.Repositories;
using ScalableWeb.Diff.Model.Enums;
using ScalableWeb.Diff.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ScalableWeb.Diff.UnitTest
{
    public class DiffServiceFake : IDiffService
    {
        public DiffServiceFake()
        {
            
        }

        public string Get(int id)
        {
            using (var comparerRepository = new ComparerRepositoryFake())
            {
                var comparer = comparerRepository.Find(id);

                if (comparer is null)
                    throw new ArgumentNullException("Content not found");

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
        public string SetContent(int id, string content, Side side = Side.Left) => throw new NotImplementedException();
    }
}
