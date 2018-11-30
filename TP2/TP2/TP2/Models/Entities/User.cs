using System;
using System.Collections.Generic;
using System.Text;

namespace TP2.Models.Entities
{
    public class User : Entity
    {
        public string Login { get; set; }
        public string HashedPassword { get; set; }
        public string PasswordSalt { get; set; }
    }
}
