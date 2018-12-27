using ScalableWeb.Diff.Model.Models;
using System.Collections.Generic;

namespace ScalableWeb.Diff.Data.Acess.Repositories
{
    public interface IComparerRepository
    {
        void Add(Comparer item);
        IEnumerable<Comparer> GetAll();
        Comparer Find(int id);
        void Remove(int id);
        void Update(Comparer item);
    }
}
