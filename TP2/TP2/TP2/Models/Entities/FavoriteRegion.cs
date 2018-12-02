using System.Collections.ObjectModel;

namespace TP2.Models.Entities
{
    public class FavoriteRegion
    {
        public string Login { get; set; }
        public Collection<Region> FavoriteRegionList { get; set; }

        public FavoriteRegion(string login, Collection<Region> list)
        {
            Login = login;
            FavoriteRegionList = list;
        }
    }
}
