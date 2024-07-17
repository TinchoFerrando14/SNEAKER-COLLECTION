using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using SneakerCollectionAPI.DataAccess.UserDataAccess.Commands.CreateUser;
using SneakerCollectionAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace SneakerCollectionAPI.Tests.IntegrationTests
{
    public class UserControllerIntegrationTests : IntegrationTestBase
    {
        public UserControllerIntegrationTests(WebApplicationFactory<Program> factory) : base(factory) {
        }

        [Fact]
        public async Task RegisterUser_WithValidData_ReturnsOkResult()
        {
            // Arrange
            var command = new CreateUserCommand
            {
                Email = "test@example.com",
                Password = "SecurePassword123"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/user/", command);

            // Assert
            response.EnsureSuccessStatusCode();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("User registered successfully", responseContent);
        }

        [Fact]
        public async Task RegisterUser_WithInvalidEmail_ReturnsUnprocessableEntity()
        {
            // Arrange
            var command = new CreateUserCommand
            {
                Email = "invalidemail",
                Password = "SecurePassword123"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/user/", command);

            // Assert
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("The Email field is not a valid e-mail address.", responseContent);
        }

        [Fact]
        public async Task RegisterUser_WithInvalidEmail_Empty_ReturnsUnprocessableEntity()
        {
            // Arrange
            var command = new CreateUserCommand
            {
                Email = "",
                Password = "SecurePassword123"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/user/", command);

            // Assert
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("The Email field is not a valid e-mail address.", responseContent);
        }

        [Fact]
        public async Task RegisterUser_WithInvalidPassword_ShortPassword_ReturnsUnprocessableEntity()
        {
            // Arrange
            var command = new CreateUserCommand
            {
                Email = "test@example.com",
                Password = "Secur"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/user/", command);

            // Assert
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("The password must be a string with at least 10 characters.", responseContent);
        }
    }
}
