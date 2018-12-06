using System.Collections.ObjectModel;
using TP2.Models.Entities;

namespace TP2.Services.Interfaces
{
    public interface IFavoriteRegionListService
    {
        void AddUserFavoriteList(string login);
        Collection<Region> GetFavoriteRegionList(string login);
        void AddRegion(string login, Region region);
        void RemoveRegion(string login, Region region);
        bool CheckRegionInList(string login, Region region);
    }
}
