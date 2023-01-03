using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementAppLayer.DTOs.Update
{
    public class UpdatePaymentTermDTO
    {
        public Guid PaymentTermId { get; set; }
        public Guid DeliverableId { get; set; }
        [Display(Name = "PaymentTerm Title")]
        public string PaymentTermTitle { get; set; }
        [Display(Name = "PaymentTerm Amount")]
        public decimal PaymentTermAmount { get; set; }

    }
}
