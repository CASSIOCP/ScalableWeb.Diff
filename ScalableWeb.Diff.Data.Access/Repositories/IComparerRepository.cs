using ScalableWeb.Diff.Model.Models;

namespace ScalableWeb.Diff.Data.Acess.Repositories
{
    public interface IComparerRepository
    {
        void Add(Comparer item);
        Comparer Find(int id);
        void Update(Comparer item);
    }
}
