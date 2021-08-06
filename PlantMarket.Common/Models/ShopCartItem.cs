using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantMarket.Common.Models
{
    public class ShopCartItem
    {

        [Key]
        public int Id { get; set; }

        public virtual Plant Plant { get; set; }

        public int PlantId { get; set; }

        public virtual ShopCart ShopCart { get; set; }

        public int ShopCartId { get; set; }
    }
}
