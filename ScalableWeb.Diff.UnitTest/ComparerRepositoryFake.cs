using ScalableWeb.Diff.Data.Acess.Contexts;
using ScalableWeb.Diff.Data.Acess.Repositories;
using ScalableWeb.Diff.Model.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ScalableWeb.Diff.UnitTest
{
    public class ComparerRepositoryFake : DatabaseContext, IComparerRepository
    {
        private readonly List<Comparer> _comparers;

        public ComparerRepositoryFake()
        {
            _comparers = new List<Comparer>()
            {
                new Comparer()
                {
                    Id = 1,
                    Left = File.ReadAllText(@"..\..\..\..\files\SameSize1.txt"),
                    Right = File.ReadAllText(@"..\..\..\..\files\SameSize2.txt")
                },
                new Comparer()
                {
                    Id = 2,
                    Left = File.ReadAllText(@"..\..\..\..\files\SameSize1.txt"),
                    Right = File.ReadAllText(@"..\..\..\..\files\DifferentSize.txt")
                },
                new Comparer()
                {
                    Id = 3,
                    Left = File.ReadAllText(@"..\..\..\..\files\SameSize1.txt")
                },
                new Comparer()
                {
                    Id = 4,
                    Right = File.ReadAllText(@"..\..\..\..\files\SameSize1.txt")
                },
                new Comparer()
                {
                    Id = 5
                },
            };
        }

        public void Add(Comparer item)
        {
            _comparers.Add(item);
        }

        public Comparer Find(int id)
        {
            return _comparers.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Comparer> GetAll()
        {
            return _comparers;
        }

        public void Update(Comparer item)
        {
            var comparer = _comparers.FirstOrDefault(a => a.Id == item.Id);

            if (comparer is null)
                return;

            comparer = item;
        }
    }
}
