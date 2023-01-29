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
        [Required]
        public string PaymentTermTitle { get; set; }
        [Display(Name = "PaymentTerm Amount")]
        [Required]
        public decimal PaymentTermAmount { get; set; }
        [ForeignKey("DeliverableId")]
        public Guid DeliverableId { get; set; }
        public Deliverable Deliverable { get; set; }
        public List<InvoicePaymentTerms> InvoicePaymentTerms { get; set; }

        // add flag 
        // to check if it's paid or not and if it is not show it in invoice
        public bool IsPaid { get; set; } = false;
        //public bool IsRemaing { get; set; } if 0 don't show
    }

}
