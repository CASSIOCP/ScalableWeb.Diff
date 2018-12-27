using Microsoft.AspNetCore.Mvc;
using ScalableWeb.Diff.Model.Enums;

namespace ScalableWeb.Diff.Application.Services
{
    public interface IDiffService
    {
        /// <summary>
        /// Compare left-side and right-side content to return if they are equal, different in size or provide insight into where diffs are.
        /// </summary>
        /// <param name="id">Id of the comparer</param>
        string Get(int id);

        /// <summary>
        /// Sets the content of a specific type to a comparer (if the comparer does not exist, a new one is created for that Id).
        /// </summary>
        /// <param name="id">Id of the comparer</param>
        /// <param name="content">Content that will be setted to the comparer</param>
        /// <param name="side">Side of the content that will be setted to the comparer</param>
        string SetContent(int id, string content, Side side = Side.Left);
    }
}