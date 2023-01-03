using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementAppLayer.DTOs.Insert
{
    public class InsertProjectDTO
    {
        [Required]
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Project Start Date")]
        public DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Project End Date")]
        public DateTime EndDate { get; set; }
        [Required]
        [Display(Name = "Project Type")]
        public Guid ProjectTypeId { get; set; }
        [Required]
        [Display(Name = "Project Status")]
        public Guid ProjectStatusId { get; set; }
        [Required]
        [Display(Name = "Client")]
        public Guid ClientId { get; set; }
        //public List<Guid> PhasesIds { get; set; }
        [Required]
        [Display(Name = "Contract Amount")]
        public decimal ContractAmount { get; set; }
        public IFormFile ContractFile { get; set; }
    }
}
