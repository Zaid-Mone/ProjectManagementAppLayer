using System.Collections.Generic;

namespace ProjectManagementBusinessLayer.Entities
{
    public class ProjectDirector : Person
    {
        public List<Project> Projects { get; set; }
    }
}
