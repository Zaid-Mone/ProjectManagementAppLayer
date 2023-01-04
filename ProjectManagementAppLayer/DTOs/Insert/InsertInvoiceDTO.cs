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
        public string InvoiceTitle { get; set; }
        [Display(Name = "Invoice Date")]
        public DateTime InvoiceDate { get; set; }
        [Display(Name = "Projects")]
        public Guid ProjectId { get; set; }

    }
}
