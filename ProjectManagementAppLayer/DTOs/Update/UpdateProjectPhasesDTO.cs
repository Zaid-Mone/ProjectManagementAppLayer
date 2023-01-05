using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementAppLayer.DTOs.Update
{
    public class UpdateProjectPhasesDTO
    {
        public Guid ProjectPhaseId { get; set; } // pk 
        [Display(Name = "Project")]
        public Guid ProjectId { get; set; }
        [Display(Name = "Phase")]
        public Guid PhaseId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Phase Start Date")]
        public DateTime StartDate { get; set; }  // must be > project start date

        [DataType(DataType.Date)]
        [Display(Name = "Phase End Date")]
        public DateTime EndDate { get; set; }// must be < project end date
    }
}
