using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karadul.Data.Entities
{
    public class CategoryProduct : BaseEntity
    {
        public Product Product { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
    }
}
