using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PlantMarket.Common.Models
{
    public class OrderedPlant
    {

        [Key]
        public int Id { get; set; }

        public virtual Plant Plant { get; set; }

        public int PlantId { get; set; }

        public virtual Order Order { get; set; }

        public int OrderId { get; set; }
    }
}
