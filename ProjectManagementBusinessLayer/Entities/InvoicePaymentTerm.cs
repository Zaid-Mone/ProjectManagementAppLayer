using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementBusinessLayer.Entities
{
    public class InvoicePaymentTerms
    {
        [ForeignKey("InvoiceId")]
        public Guid InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        [ForeignKey("PaymentTermId")]
        public Guid PaymentTermId { get; set; }
        public PaymentTerm PaymentTerm { get; set; }

    }


}
