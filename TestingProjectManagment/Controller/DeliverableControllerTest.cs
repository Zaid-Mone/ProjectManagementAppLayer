using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementAppLayer.Areas.ProjectManagment.Controllers;
using ProjectManagementBusinessLayer.Entities;
using ProjectManagementBusinessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

using Xunit;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Http;

namespace TestingProjectManagment.Controller
{
    public class DeliverableControllerTest
    {
        private DeliverableController deliverableController;
        private readonly IDeliverableRepository _deliverableRepository;
        private readonly IProjectPhaseRepository _projectPhaseRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly UserManager<Person> _userManager;
        private readonly HttpContextAccessor _httpContextAccessor;
        public DeliverableControllerTest()
        {
            _deliverableRepository = A.Fake<IDeliverableRepository>();
            _projectPhaseRepository = A.Fake<IProjectPhaseRepository>();
            _projectRepository = A.Fake<IProjectRepository>();
            _userManager = A.Fake<UserManager<Person>>();
            _httpContextAccessor = A.Fake<HttpContextAccessor>();
            deliverableController = new DeliverableController(
                _deliverableRepository,
                _projectPhaseRepository,
                _projectRepository,
                _userManager
                );
        }
        /// <summary>
        ///  Error because the deliverable rely on ProjectPhase 
        ///  so i need Create a ProjectPhaseId to get the Success 
        /// </summary>

        //[Fact]
        //public async void DeliverableController_Test_IndexAction()
        //{
        //    ProjectPhase projectPhase = new ProjectPhase();
        //    projectPhase.Id = new Guid();
        //    var obj = _httpContextAccessor.HttpContext;
        //    Arrange
        //    var deliverables = A.Fake<List<Deliverable>>();
        //    var res1 = await _deliverableRepository.GetAllDeliverables();
        //    var user = _userManager.FindByNameAsync(obj.User.Identity.Name);
        //    var res2 = await _deliverableRepository
        //        .GetAllDeliverableForProjectManager(Convert.ToString(user.Id));
        //    if (obj.User.IsInRole("Admin"))
        //    {
        //        A.CallTo(() => res1).Returns(deliverables);
        //    }
        //    else
        //    {
        //        A.CallTo(() => res2).Returns(deliverables);
        //    }
        //    act
        //   var result = deliverableController.Index();
        //    assert
        //    result.Should().BeOfType<Task<IActionResult>>();
        //}


        //[Fact]
        //public async void DeliverableController_Test_DetailsAction()
        //{
        //    var id = new Guid();
        //    //Arrange
        //    var deliverable = A.Fake<Deliverable>();
        //    var res = await _deliverableRepository.GetDeliverableById(id);
        //    A.CallTo(() => res).Returns(deliverable);

        //    // act
        //    var result = deliverableController.Details(id);
        //    // assert object chcek actions
        //    result.Should().BeOfType<Task<IActionResult>>();
        //}



    }
}
