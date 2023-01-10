using System.Collections.Generic;

namespace ProjectManagementBusinessLayer.Entities
{
    public class ProjectManager : Person
    {
        public List<Project> Projects { get; set; }
    }
}
