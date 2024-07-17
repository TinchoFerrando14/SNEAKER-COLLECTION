using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SneakerCollectionAPI.Controllers;
using SneakerCollectionAPI.DataAccess.UserDataAccess.Commands.CreateUser;
using SneakerCollectionAPI.Domain.DTOs;
using SneakerCollectionAPI.Domain.Entities;

namespace SneakerCollectionAPI.Tests.UnitTests
{
    public class UserControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _controller = new UserController(_mockMediator.Object);
        }

        [Fact]
        public async Task RegisterUser_ReturnsOkResult()
        {
            // Arrange
            var command = new NewUserInputDto
            {
                Email = "test@example.com",
                Password = "SecurePassword123"
            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new int());

            // Act
            var result = await _controller.User(command);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}