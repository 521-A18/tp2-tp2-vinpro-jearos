using System.Collections.ObjectModel;
using TP2.Models.Entities;

namespace TP2.Services.Interfaces
{
    public interface IFavoriteRegionListService
    {
        void AddUserFavoriteList(string login);
        Collection<Region> GetFavoriteRegionList(string login);
    }
}
