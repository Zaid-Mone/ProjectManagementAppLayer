using ProjectManagementBusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementBusinessLayer.Repositories.Interfaces
{
    public interface IPaymentTermRepository
    {
        public Task<List<PaymentTerm>> GetAllPaymentTerms();
        public List<PaymentTerm> GetProjectPaymentTerms(Guid id);
        public Task<PaymentTerm> GetPaymentTermById(Guid id);
        public Task<List<PaymentTerm>> GetIsNotPaidPaymentTerm(Guid id);
        public Task<List<PaymentTerm>> GetAllPaymentTermByProjectManagerId(string id);
        public void Insert(PaymentTerm paymentTerm);
        public void Update(PaymentTerm paymentTerm);
        public void Delete(PaymentTerm paymentTerm);
        public void Save();
    }
}
