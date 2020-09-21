using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using SystemApp.Admin.IRepos;
using SystemApp.Admin.Repos;

namespace SystemApp.Models.DataModels
{
    public class OrderDetails : BaseModel, IPseudoDeletable
    {
        [Required]
        public Guid OrderHeaderId { get; set; }

        [ForeignKey("OrderHeaderId")]
        public virtual OrderHeader OrderHeader { get; set; }


        [Required]
        public Guid ProductServiceId { get; set; }

        [ForeignKey("ProductServiceId")]
        public virtual Product ProductService { get; set; }

        [Required]
        public string ServiceName { get; set; }

        [Required]
        public double Price { get; set; }
        public bool Delete { get; set; }
    }
}
