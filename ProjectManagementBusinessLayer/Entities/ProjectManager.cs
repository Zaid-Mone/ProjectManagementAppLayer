using System.Collections.Generic;

namespace ProjectManagementBusinessLayer.Entities
{
    public class ProjectManager : Person
    {
        public List<Project> Projects { get; set; }
    }

    public class Admin : Person
    {
        public List<Project> Projects { get; set; }
        public List<PaymentTerm> PaymentTerms { get; set; }
        public List<Deliverable> Deliverables { get; set; }
        public List<Invoice> Invoices { get; set; }
    }
}
