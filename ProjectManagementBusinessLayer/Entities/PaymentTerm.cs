using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementBusinessLayer.Entities
{
    public class PaymentTerm
    {
        [Key]
        public Guid Id { get; set; }
        [Display(Name = "PaymentTerm Title")]
        public string PaymentTermTitle { get; set; }
        [Display(Name = "PaymentTerm Amount")]
        public decimal PaymentTermAmount { get; set; }
        [ForeignKey("DeliverableId")]
        public Guid DeliverableId { get; set; }
        public Deliverable Deliverable { get; set; }
        public List<InvoicePaymentTerm> InvoicePaymentTerms { get; set; }

    }

}
