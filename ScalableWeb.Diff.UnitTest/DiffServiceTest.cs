using ScalableWeb.Diff.Model.Models;
using System.IO;
using Xunit;

namespace ScalableWeb.Diff.UnitTest
{
    public class DiffServiceTest
    {
        [Fact]
        public void Test_Find_Comparer()
        {
            using (var comparerRepository = new ComparerRepositoryFake())
            {
                var comparer = comparerRepository.Find(1);

                Assert.NotNull(comparer);
                Assert.Equal(comparer.Left, File.ReadAllText(@"..\..\..\..\files\SameSize1.txt"));
                Assert.Equal(comparer.Right, File.ReadAllText(@"..\..\..\..\files\SameSize2.txt"));
            }
        }

        [Fact]
        public void Test_Not_Find_Comparer()
        {
            using (var comparerRepository = new ComparerRepositoryFake())
            {
                var comparer = comparerRepository.Find(-9999);

                Assert.Null(comparer);
            }
        }

        [Fact]
        public void Test_Add_Comparer1()
        {
            using (var comparerRepository = new ComparerRepositoryFake())
            {
                comparerRepository.Add(new Comparer()
                {
                    Id = 99,
                    Left = File.ReadAllText(@"..\..\..\..\files\SameSize1.txt"),
                    Right = File.ReadAllText(@"..\..\..\..\files\SameSize2.txt"),
                });

                comparerRepository.Find(99);

                Assert.NotNull(comparerRepository.Find(99));
            }
        }

        [Fact]
        public void Test_Add_Comparer2()
        {
            using (var comparerRepository = new ComparerRepositoryFake())
            {
                comparerRepository.Add(new Comparer()
                {
                    Id = 99,
                });

                comparerRepository.Find(99);

                Assert.NotNull(comparerRepository.Find(99));
            }
        }

        [Fact]
        public void Test_Update_Comparer1()
        {
            using (var comparerRepository = new ComparerRepositoryFake())
            {
                var comparer = comparerRepository.Find(1);

                string content = File.ReadAllText(@"..\..\..\..\files\SameSize2.txt");
                comparer.Left = content;

                comparerRepository.Update(comparer);

                Assert.Equal(comparerRepository.Find(1).Left, content);
            }
        }

        [Fact]
        public void Test_Update_Comparer2()
        {
            using (var comparerRepository = new ComparerRepositoryFake())
            {
                var comparer1 = comparerRepository.Find(1);
                var comparer2 = comparerRepository.Find(2);

                comparer1.Right = comparer2.Right;

                comparerRepository.Update(comparer1);

                comparer1 = comparerRepository.Find(1);

                Assert.Equal(comparer1.Right, comparer2.Right);
            }
        }
    }
}
