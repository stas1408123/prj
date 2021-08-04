using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantMarket.Common.Models
{
    public class ShopCart
    {

        [Key]
        public int Id { get; set; }
        public double Price { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public List<ShopCartItem> ShopItems { get; set; }
    }
}
