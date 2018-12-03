using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TP2.Models.Entities;
using TP2.Services;
using TP2.UnitTests.Constante;
using Xunit;

namespace TP2.UnitTests.Services
{
    public class FavoriteRegionListServiceTest
    {
        private FavoriteRegionListService _favoriteRegionListService;

        public FavoriteRegionListServiceTest()
        {
            _favoriteRegionListService = new FavoriteRegionListService();
        }

        [Fact]
        public void AddUserFavoriteList_WhenNewUserisAdded_ShouldAddUserToList()
        {
            _favoriteRegionListService.AddUserFavoriteList(ConstanteTest.GOOD_EMAIL);

            Assert.Contains(ConstanteTest.GOOD_EMAIL, _favoriteRegionListService._favoriteRegion[0].Login);
        }


        [Fact]
        public void GetFavoriteRegionList_WhenUserOnList_ShouldReturnCollectionRegion()
        {
            _favoriteRegionListService.AddUserFavoriteList(ConstanteTest.GOOD_EMAIL);
            _favoriteRegionListService.AddRegion(ConstanteTest.GOOD_EMAIL, new Region("quebec"));
            Collection<Region> regionList = _favoriteRegionListService.GetFavoriteRegionList(ConstanteTest.GOOD_EMAIL);

            Assert.Contains("quebec", regionList[0].Name);
        }

        [Fact]
        public void GetFavoriteRegionList_WhenUserNotOnList_ShouldNull()
        {
            Assert.Null(_favoriteRegionListService.GetFavoriteRegionList(" "));
        }

        [Fact]
        public void AddRegion_WhenUserAddRegion_ShouldAddToCollectionRegion()
        {
            _favoriteRegionListService.AddUserFavoriteList(ConstanteTest.GOOD_EMAIL);
            _favoriteRegionListService.AddRegion(ConstanteTest.GOOD_EMAIL, new Region("quebec"));
            _favoriteRegionListService.AddRegion(ConstanteTest.GOOD_EMAIL, new Region("montreal"));
            Collection<Region> regionList = _favoriteRegionListService.GetFavoriteRegionList(ConstanteTest.GOOD_EMAIL);

            Assert.Contains("quebec", regionList[0].Name);
            Assert.Contains("montreal", regionList[1].Name);
        }

        [Fact]
        public void RemoveRegion_WhenUserRemoveRegion_ShouldRemoveToCollectionRegion()
        {
            _favoriteRegionListService.AddUserFavoriteList(ConstanteTest.GOOD_EMAIL);
            Region regionToRemove = new Region("quebec");
            _favoriteRegionListService.AddRegion(ConstanteTest.GOOD_EMAIL, regionToRemove);
            _favoriteRegionListService.RemoveRegion(ConstanteTest.GOOD_EMAIL, regionToRemove);
            Collection<Region> regionList = _favoriteRegionListService.GetFavoriteRegionList(ConstanteTest.GOOD_EMAIL);

            Assert.Empty(regionList);
        }

        [Fact]
        public void CheckRegionInList_WhenRegionIsInList_ShouldReturnTrue()
        {
            _favoriteRegionListService.AddUserFavoriteList(ConstanteTest.GOOD_EMAIL);
            Region regionToRemove = new Region("quebec");
            _favoriteRegionListService.AddRegion(ConstanteTest.GOOD_EMAIL, regionToRemove);

            Assert.True(_favoriteRegionListService.CheckRegionInList(ConstanteTest.GOOD_EMAIL, regionToRemove));
        }

        [Fact]
        public void CheckRegionInList_WhenRegionIsNotInList_ShouldReturnFalse()
        {
            _favoriteRegionListService.AddUserFavoriteList(ConstanteTest.GOOD_EMAIL);
            Region regionToRemove = new Region("quebec");

            Assert.False(_favoriteRegionListService.CheckRegionInList(ConstanteTest.GOOD_EMAIL, regionToRemove));
        }

        [Fact]
        public void CheckRegionInList_WhenUserIsNotInList_ShouldReturnFalse()
        {
            Region regionToRemove = new Region("quebec");

            Assert.False(_favoriteRegionListService.CheckRegionInList(ConstanteTest.GOOD_EMAIL, regionToRemove));
        }
    }
}
