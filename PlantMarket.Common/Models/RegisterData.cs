using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantMarket.Common.Models
{
    public class RegisterData
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public User User { get; set; }
    }
}
