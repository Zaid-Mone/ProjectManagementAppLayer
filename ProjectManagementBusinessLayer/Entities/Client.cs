using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementBusinessLayer.Entities
{
    public class Client
    {
        [Key]
        public Guid Id { get; set; }
        [Display(Name = "Client Name")]
        public string Name { get; set; }

        [Display(Name = "Client Email")]
        [DataType(DataType.EmailAddress)]
        public string  Email { get; set; }
    }

}
