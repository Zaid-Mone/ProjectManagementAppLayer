using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementBusinessLayer.Entities
{
    public class Phase
    {
        [Key]
        public Guid Id { get; set; }
        [Display(Name = "Phase Name")]
        public string PhaseName { get; set; }
        public List<ProjectPhase> ProjectPhases { get; set; }
    }

}
