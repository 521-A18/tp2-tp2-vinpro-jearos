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
        private Collection<FavoriteRegion>  _favoriteRegion;

        public FavoriteRegionListService()
        {
            _favoriteRegion = new Collection<FavoriteRegion>();
        }

        public void AddUserFavoriteList(string login)
        {
            FavoriteRegion userToAdd = new FavoriteRegion(login, new Collection<Region>());
            Region region1 = new Region("quebec");
            Region region2 = new Region("montreal");
            userToAdd.FavoriteRegionList.Add(region1);
            userToAdd.FavoriteRegionList.Add(region2);
            _favoriteRegion.Add(userToAdd);
        }

        public Collection<Region> GetFavoriteRegionList(string login)
        {
            foreach(FavoriteRegion element in _favoriteRegion)
            {
                if (element.Login == login) return element.FavoriteRegionList;
            }

            return null;
        }
    }
}
