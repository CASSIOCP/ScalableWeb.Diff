using Microsoft.AspNetCore.Mvc;
using ScalableWeb.Diff.Model.Enums;

namespace ScalableWeb.Diff.Application.Services
{
    public interface IDiffService
    {
        string Get(int id);
        string SetContent(int id, string content, Side side = Side.Left);
    }
}