using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;
using SystemApp.Admin.IRepos;
using SystemApp.Admin.Repos;

namespace SystemApp.Models.DataModels
{
    public class OrderHeader : BaseModel, IPseudoDeletable
    {

        [Required(ErrorMessage ="Enter your fullname")]
        [Display(Prompt = "Customer's name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter your phone number")]
        [Display(Prompt = "Phone number")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Enter your email address")]
        [Display(Prompt = "Eamil address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter your primary address")]
        [Display(Prompt = "Address/Location")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Enter the city you are")]
        [Display(Prompt = "City")]
        public string City { get; set; }
        [Required(ErrorMessage = "Enter Zip code")]
        [Display(Prompt = "Zip code")]
        public string ZipCode { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        [Display(Prompt = "Short comment")]
        public string Comments { get; set; }
        public int ProductServiceCount { get; set; }

        [ScaffoldColumn(false)]
        public double Total { get; set; }

        [ScaffoldColumn(false)]
        public string PaymentTransactionId { get; set; }

        [ScaffoldColumn(false)]
        public bool HasBeenShipped { get; set; }

        public List<OrderDetails> OrderDetails { get; set; }
        public bool Delete { get; set; }

        public OrderHeader()
        {
            PaymentTransactionId = string.Format($"Txn-{Guid.NewGuid()}");
            HasBeenShipped = false;
            Delete = false;
        }
    }
}
