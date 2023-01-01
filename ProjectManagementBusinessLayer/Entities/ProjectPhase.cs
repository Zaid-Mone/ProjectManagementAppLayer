using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementBusinessLayer.Entities
{
    public class ProjectPhase
    {
        [Key]
        public Guid Id { get; set; } // pk 

        // fk - uniqe
        [ForeignKey("ProjectId")]
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        // fk - uniqe
        [ForeignKey("PhaseId")]
        public Guid PhaseId { get; set; }
        public Phase Phase { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Phase Start Date")]
        public DateTime StartDate { get; set; }  // must be > project start date
        [DataType(DataType.Date)]
        [Display(Name = "Phase End Date")]
        public DateTime EndDate { get; set; }// must be < project end date
    }

}
