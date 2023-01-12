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
    public class ClientControllerTest
    {
        private ClientController clientController;
        private readonly IClientRepository _clientRepository;
        public ClientControllerTest()
        {
            _clientRepository = A.Fake<IClientRepository>();
            clientController = new ClientController(_clientRepository);
        }

        [Fact]
        public void ClientController_Test_IndexAction()
        {
            //Arrange
            var clients = A.Fake<List<Client>>();
            A.CallTo(() => _clientRepository.GetAllClients()).Returns(clients);
            // act
            var result = clientController.Index();
            // assert 
            result.Should().BeOfType<Task<IActionResult>>();
        }


        [Fact]
        public void ClientController_Test_DetailsAction()
        {
            var id = new Guid();

            //Arrange
            var client = A.Fake<Client>();
            A.CallTo(() => _clientRepository.GetClientById(id)).Returns(client);
            // act
            var result = clientController.Details(id);
            // assert object chcek actions
            result.Should().BeOfType<Task<IActionResult>>();
        }




    }
}
