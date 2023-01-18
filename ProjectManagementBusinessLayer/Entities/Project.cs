using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementBusinessLayer.Entities
{
    public class Project
    {
        [Key]
        public Guid Id { get; set; }
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        [ForeignKey("ProjectTypeId")]
        public Guid ProjectTypeId { get; set; }
        public ProjectType ProjectType { get; set; }
        [ForeignKey("ProjectStatusId")]
        public Guid ProjectStatusId { get; set; }
        public ProjectStatus ProjectStatus { get; set; }
        public List<ProjectPhase> ProjectPhases { get; set; }
        [Display(Name = "Contract Amount")]
        public decimal ContractAmount { get; set; }
        public string  ContractFileName { get; set; }
        public string ContractFileType { get; set; }
        public Byte[] ContractFile { get; set; }
        [ForeignKey("ProjectManagerId")]
        public string ProjectManagerId { get; set; }
        public ProjectManager ProjectManager { get; set; }
        [ForeignKey("ClientId")]
        public Guid ClientId { get; set; }
        public Client Client { get; set; }
        //public string ProjectState { get; set; } = "Pending";
        public bool IsApproved { get; set; } = false;

    }

}
