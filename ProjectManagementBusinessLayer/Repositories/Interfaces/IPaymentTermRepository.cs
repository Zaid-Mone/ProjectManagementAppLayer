using ProjectManagementBusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementBusinessLayer.Repositories.Interfaces
{
    public interface IPaymentTermRepository
    {

        // to Get All Payments
        public Task<List<PaymentTerm>> GetAllPaymentTerms();
        // to get All Payments based on Project id
        public List<PaymentTerm> GetProjectPaymentTerms(Guid id);
        //to get single payment by payment id
        public Task<PaymentTerm> GetPaymentTermById(Guid id);
        // to get All payments that is not paid
        public Task<List<PaymentTerm>> GetIsNotPaidPaymentTerm(Guid id);
        // to get all payment belong to project manager id
        public Task<List<PaymentTerm>> GetAllPaymentTermByProjectManagerId(string id);
        public void Insert(PaymentTerm paymentTerm);
        public void Update(PaymentTerm paymentTerm);
        public void Delete(PaymentTerm paymentTerm);
        public void Save();
    }
}
