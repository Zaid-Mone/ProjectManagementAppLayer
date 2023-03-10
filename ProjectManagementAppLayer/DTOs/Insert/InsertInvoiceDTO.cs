using ProjectManagementBusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementAppLayer.DTOs.Insert
{
    public class InsertInvoiceDTO
    {
        [Display(Name ="PaymentTerms")]
        public List<Guid> PaymentTermIds { get; set; }
        
        [Display(Name = "Invoice Title")]
        [Required]
        public string InvoiceTitle { get; set; }
        [Display(Name = "Invoice Date")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime InvoiceDate { get; set; }
        [Display(Name = "Projects")]
        public Guid ProjectId { get; set; }
        public PaymentTerm  PaymentTerm { get; set; }

    }
}
