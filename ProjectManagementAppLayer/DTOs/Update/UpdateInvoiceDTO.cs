using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementAppLayer.DTOs.Update
{
    public class UpdateInvoiceDTO
    {

        public Guid InvoiceId { get; set; }
        [Display(Name = "Invoice Title")]
        public string InvoiceTitle { get; set; }

        [Display(Name = "Invoice Date")]
        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime InvoiceDate { get; set; }

        [Display(Name = "ProjectId")]
        public Guid ProjectId { get; set; }
        public List<Guid> InvoicePaymentsIds { get; set; }
    }
}
