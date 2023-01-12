using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementAppLayer.Areas.Administrator.Controllers;
using ProjectManagementBusinessLayer.Entities;
using ProjectManagementBusinessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestingProjectManagment.Controller
{
    public class PhaseControllerTest
    {
        private PhaseController phaseController;
        private readonly IPhaseRepository _phaseRepository;

        public PhaseControllerTest()
        {
            _phaseRepository = A.Fake<IPhaseRepository>();
            phaseController = new PhaseController(_phaseRepository);
        }

        [Fact]
        public void PhaseController_Test_IndexAction()
        {
            //Arrange
            var phase = A.Fake<List<Phase>>();
            A.CallTo(() => _phaseRepository.GetAllPhases()).Returns(phase);
            // act
            var result = phaseController.Index();
            // assert 
            result.Should().BeOfType<Task<IActionResult>>();
        }


        [Fact]
        public void PhaseController_Test_DetailsAction()
        {
            var id = new Guid();

            //Arrange
            var phase = A.Fake<Phase>();
            A.CallTo(() => _phaseRepository.GetPhaseById(id)).Returns(phase);
            // act
            var result = phaseController.Details(id);
            // assert object chcek actions
            result.Should().BeOfType<Task<IActionResult>>();
        }

        }
}
