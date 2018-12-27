using ScalableWeb.Diff.Model.Models;

namespace ScalableWeb.Diff.Data.Acess.Repositories
{
    public interface IComparerRepository
    {
        /// <summary>
        /// Adds a new comparer to the repository.
        /// </summary>
        /// <param name="item">Comparer that will be added</param>
        void Add(Comparer item);

        /// <summary>
        /// Find a comparer for the specific Id, if not find retuns null object.
        /// </summary>
        /// <param name="id">Id of the comparer</param>
        Comparer Find(int id);

        /// <summary>
        /// Update a comparer for the specific Id.
        /// </summary>
        /// <param name="id">Comparer that will be updated</param>
        void Update(Comparer item);
    }
}