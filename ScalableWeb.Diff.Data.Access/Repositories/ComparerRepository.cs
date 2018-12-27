using ScalableWeb.Diff.Data.Acess.Contexts;
using ScalableWeb.Diff.Model.Models;

namespace ScalableWeb.Diff.Data.Acess.Repositories
{
    public class ComparerRepository : DatabaseContext, IComparerRepository
    {
        private const string Collection = "comparers";

        public void Add(Comparer item)
        {
            Database.GetCollection<Comparer>(Collection).Insert(item);
        }

        public Comparer Find(int id)
        {
            return Database.GetCollection<Comparer>(Collection).FindOne(a => a.Id == id);
        }

        public void Update(Comparer item)
        {
            Database.GetCollection<Comparer>(Collection).Update(item);
        }
    }
}