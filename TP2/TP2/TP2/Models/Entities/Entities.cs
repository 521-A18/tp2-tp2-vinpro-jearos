using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TP2.Models.Entities
{
    public class Entity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
