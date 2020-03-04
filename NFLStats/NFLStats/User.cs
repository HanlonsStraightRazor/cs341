using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFLStats
{
    public class User
    {
        public string Username { get; set; }

        public int PasswordHash { get; set; }

        public string FavTeam { get; set; }

        public User(string name, int hash, string team)
        {
            Username = name;
            PasswordHash = hash;
            FavTeam = team;
        }
        public User()
        {
        }
    }
}
