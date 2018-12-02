using System;
using System.Collections.Generic;
using System.Text;

namespace TP2.Models.Entities
{
    public class Region
    {
        private string _name;

        public Region(string name)
        {
            _name = name;
        }

        public string Name
        {
            set => _name = value;
            get => _name;
        }
    }
}
