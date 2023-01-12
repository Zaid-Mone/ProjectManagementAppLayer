using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementAppLayer.Areas.ProjectManagment.Controllers;
using ProjectManagementBusinessLayer.Data;
using ProjectManagementBusinessLayer.Entities;
using ProjectManagementBusinessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestingProjectManagment.Controller
{
    public class PaymentTermControllerTest
    {
        private PaymentTermController paymentTermController;
        private readonly ApplicationDbContext _context;
        private readonly IPaymentTermRepository _paymentTermRepository;
        private readonly IDeliverableRepository _deliverableRepository;
        private readonly UserManager<Person> _userManager;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoicePaymentTermsRepository _invoicePaymentTermsRepository;

        public PaymentTermControllerTest()
        {
            _context = A.Fake<ApplicationDbContext>();
            _deliverableRepository = A.Fake<IDeliverableRepository>();
            _invoicePaymentTermsRepository = A.Fake<IInvoicePaymentTermsRepository>();
            _invoiceRepository = A.Fake<IInvoiceRepository>();
            _paymentTermRepository = A.Fake<IPaymentTermRepository>();
            _userManager = A.Fake<UserManager<Person>>();
             paymentTermController = new PaymentTermController(
                 _context,
                 _paymentTermRepository,
                 _deliverableRepository,
                 _userManager,
                 _invoiceRepository,
                 _invoicePaymentTermsRepository
                 );
        }
        //[Fact]
        //public async Task PaymentTermController_Test_IndexAction()
        //{
        //    //Arrange
        //    var delv = new Deliverable();
        //    delv.Id = new Guid();
        //    var paymentTerms = A.Fake<List<PaymentTerm>>();
        //    var res = await _paymentTermRepository.GetAllPaymentTerms();
        //    A.CallTo(() => res).Returns(paymentTerms);
          
        //    // act
        //    var result = paymentTermController.Index();
        //    // assert 
        //    result.Should().BeOfType<Task<IActionResult>>();
        //}

    }
}
