using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementBusinessLayer.Entities
{
    public class Invoice
    {
        [Key]
        public Guid Id { get; set; }
        [Display(Name = "Invoice Title")]
        public string InvoiceTitle { get; set; }
        [Display(Name = "Invoice Date")]
        [DataType(DataType.Date)]
        public DateTime InvoiceDate { get; set; }
        public List<InvoicePaymentTerms> InvoicePaymentTerms { get; set; }
        [ForeignKey("ProjectId")]
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public string SerialNumber { get; set; } // auto Generated unique number
        public bool IsApproved { get; set; } = false;
        // (True )Approved in invoice mean you can't edit the invoice
        // (False )Pending in invoice mean you can edit the invoice
        public bool IsPaidInvoice { get; set; } = false;
        // if true means i can't send sms messages anymore and can't make it pending anymore too
        // add bool want to not pay it all 
        // decimal remeaning 
    }

}
