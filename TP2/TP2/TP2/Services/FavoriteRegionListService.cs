using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TP2.Models.Entities;
using TP2.Services.Interfaces;

namespace TP2.Services
{
    public class FavoriteRegionListService : IFavoriteRegionListService
    {
        public Collection<FavoriteRegion>  _favoriteRegion;

        public FavoriteRegionListService()
        {
            _favoriteRegion = new Collection<FavoriteRegion>();
        }

        public void AddUserFavoriteList(string login)
        {
            FavoriteRegion userToAdd = new FavoriteRegion(login, new Collection<Region>());
            _favoriteRegion.Add(userToAdd);
            AddRegion(login, new Region("quebec"));
            AddRegion(login, new Region("montreal"));
        }

        public Collection<Region> GetFavoriteRegionList(string login)
        {
            foreach(FavoriteRegion element in _favoriteRegion)
            {
                if (element.Login == login) return element.FavoriteRegionList;
            }

            return null;
        }

        public void AddRegion(string login, Region region)
        {
            foreach (FavoriteRegion element in _favoriteRegion)
            {
                if (element.Login == login) element.FavoriteRegionList.Add(region);
            }
        }

        public void RemoveRegion(string login, Region region)
        {
            foreach (FavoriteRegion element in _favoriteRegion)
            {
                if (element.Login == login) element.FavoriteRegionList.Remove(region);
            }
        }
    }
}
