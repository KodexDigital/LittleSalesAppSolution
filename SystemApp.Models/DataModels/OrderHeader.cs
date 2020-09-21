﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SystemApp.Admin.IRepos;
using SystemApp.Admin.Repos;

namespace SystemApp.Models.DataModels
{
    public class OrderHeader : BaseModel, IPseudoDeletable
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string ZipCode { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        public int ProductServiceCount { get; set; }
        public bool Delete { get; set; }
    }
}
