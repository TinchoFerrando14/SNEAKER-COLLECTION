using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using SneakerCollectionAPI.DataAccess.SneakerDataAccess.Commands.CreateSneaker;
using SneakerCollectionAPI.Domain.Enums;
using Microsoft.IdentityModel.Tokens;
using SneakerCollectionAPI.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Net.Http.Headers;
using MediatR;
using Moq;
using SneakerCollectionAPI.Domain.DTOs;

namespace SneakerCollectionAPI.Tests.IntegrationTests
{
    public class SneakerControllerIntegrationTests : IntegrationTestBase
    {
        private readonly Mock<IMediator> _mockMediator;

        public SneakerControllerIntegrationTests(WebApplicationFactory<Program> factory) : base(factory) {
            _mockMediator = new Mock<IMediator>();
        }

        [Fact]
        public async Task CreateSneaker_WithValidData_ReturnsCreatedResult()
        {
            var user = new User { Email = "a@a.com", Password = "a" , Role =RoleEnum.Collector , Id = 1};
            var token = GenerateJwtToken(user);

            // Arrange
            var command = new CreateSneakerCommand
            {               
                Name = "Total90",
                Brand = "Nike",
                Price = 50,
                Size = 44,
                Year = 2005,
                Rate = RateEnum.TwoStars
            };

            // Act
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsJsonAsync("/api/sneaker/", command);

            // Assert
            response.EnsureSuccessStatusCode();
            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task CreateSneaker_WithInvalidName_ReturnsUnprocessableEntity()
        {
            var user = new User { Email = "a@a.com", Password = "a", Role = RoleEnum.Collector, Id = 1 };
            var token = GenerateJwtToken(user);

            // Arrange
            var command = new CreateSneakerCommand
            {
                Name = "",
                Brand = "Nike",
                Price = 50,
                Size = 44,
                Year = 2005,
                Rate = RateEnum.TwoStars
            };

            // Act
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsJsonAsync("/api/sneaker/", command);

            // Assert
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("The Name field is required", responseContent);
        }

        [Fact]
        public async Task CreateSneaker_WithInvalidBrand_ReturnsUnprocessableEntity()
        {
            var user = new User { Email = "a@a.com", Password = "a", Role = RoleEnum.Collector, Id = 1 };
            var token = GenerateJwtToken(user);

            // Arrange
            var command = new CreateSneakerCommand
            {
                Name = "Total90",
                Brand = "",
                Price = 50,
                Size = 44,
                Year = 2005,
                Rate = RateEnum.TwoStars
            };

            // Act
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsJsonAsync("/api/sneaker/", command);

            // Assert
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("The Brand field is required", responseContent);
        }

        [Fact]
        public async Task CreateSneaker_WithInvalidPrice_ReturnsUnprocessableEntity()
        {
            var user = new User { Email = "a@a.com", Password = "a", Role = RoleEnum.Collector, Id = 1 };
            var token = GenerateJwtToken(user);

            // Arrange
            var command = new CreateSneakerCommand
            {
                Name = "Total90",
                Brand = "Nike",
                Price = 0,
                Size = 44,
                Year = 2005,
                Rate = RateEnum.TwoStars
            };

            // Act
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsJsonAsync("/api/sneaker/", command);

            // Assert
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("Price must be greater than zero.", responseContent);
        }

        [Fact]
        public async Task CreateSneaker_WithInvalidSize_LessThanMinimum_ReturnsUnprocessableEntity()
        {
            var user = new User { Email = "a@a.com", Password = "a", Role = RoleEnum.Collector, Id = 1 };
            var token = GenerateJwtToken(user);

            // Arrange
            var command = new CreateSneakerCommand
            {
                Name = "Total90",
                Brand = "Nike",
                Price = 10,
                Size = 1,
                Year = 2005,
                Rate = RateEnum.TwoStars
            };

            // Act
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsJsonAsync("/api/sneaker/", command);

            // Assert
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("Size must be between 5 and 50.", responseContent);
        }


        [Fact]
        public async Task CreateSneaker_WithInvalidSize_GreaterThanMaximum_ReturnsUnprocessableEntity()
        {
            var user = new User { Email = "a@a.com", Password = "a", Role = RoleEnum.Collector, Id = 1 };
            var token = GenerateJwtToken(user);

            // Arrange
            var command = new CreateSneakerCommand
            {
                Name = "Total90",
                Brand = "Nike",
                Price = 10,
                Size = 55,
                Year = 2005,
                Rate = RateEnum.TwoStars
            };

            // Act
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsJsonAsync("/api/sneaker/", command);

            // Assert
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("Size must be between 5 and 50.", responseContent);
        }

        [Fact]
        public async Task CreateSneaker_WithInvalidYear_LessThanMinimum_ReturnsUnprocessableEntity()
        {
            var user = new User { Email = "a@a.com", Password = "a", Role = RoleEnum.Collector, Id = 1 };
            var token = GenerateJwtToken(user);

            // Arrange
            var command = new CreateSneakerCommand
            {
                Name = "Total90",
                Brand = "Nike",
                Price = 10,
                Size = 10,
                Year = 1900,
                Rate = RateEnum.TwoStars
            };

            // Act
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsJsonAsync("/api/sneaker/", command);

            // Assert
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("Year must be between 1950 and 2100.", responseContent);
        }

        [Fact]
        public async Task CreateSneaker_WithInvalidYear_GreaterThanMaximum_ReturnsUnprocessableEntity()
        {
            var user = new User { Email = "a@a.com", Password = "a", Role = RoleEnum.Collector, Id = 1 };
            var token = GenerateJwtToken(user);

            // Arrange
            var command = new CreateSneakerCommand
            {
                Name = "Total90",
                Brand = "Nike",
                Price = 10,
                Size = 10,
                Year = 2500,
                Rate = RateEnum.TwoStars
            };

            // Act
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsJsonAsync("/api/sneaker/", command);

            // Assert
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("Year must be between 1950 and 2100.", responseContent);
        }

        [Fact]
        public async Task CreateSneaker_WithInvalidRate_ReturnsUnprocessableEntity()
        {
            var user = new User { Email = "a@a.com", Password = "a", Role = RoleEnum.Collector, Id = 1 };
            var token = GenerateJwtToken(user);

            // Arrange
            var command = new CreateSneakerCommand
            {
                Name = "Total90",
                Brand = "Nike",
                Price = 10,
                Size = 10,
                Year = 2010
            };

            // Act
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsJsonAsync("/api/sneaker/", command);

            // Assert
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("Rate must be between 1 and 5.", responseContent);
        }

        [Fact]
        public async Task DeleteSneaker_ReturnsNoContentResult()
        {
            var user = new User { Email = "a@a.com", Password = "a", Role = RoleEnum.Collector, Id = 1 };
            var token = GenerateJwtToken(user);                       

            // Arrange
            var sneakerId = 8;

            // Act
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.DeleteAsync($"/api/sneaker/{sneakerId}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);          
        }

        [Fact]
        public async Task DeleteSneaker_ReturnsNotFoundResult()
        {
            var user = new User { Email = "a@a.com", Password = "a", Role = RoleEnum.Collector, Id = 1 };
            var token = GenerateJwtToken(user);

            // Arrange
            var sneakerId = 2;

            // Act
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.DeleteAsync($"/api/sneaker/{sneakerId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeleteSneaker_ReturnsForbidResult()
        {
            var user = new User { Email = "a@a.com", Password = "a", Role = RoleEnum.Collector, Id = 1 };
            var token = GenerateJwtToken(user);

            // Arrange
            var sneakerId = 6; // its a sneaker from user 2

            // Act
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.DeleteAsync($"/api/sneaker/{sneakerId}");

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task UpdateSneaker_WithInvalidName_ReturnsUnprocessableEntity()
        {
            var user = new User { Email = "a@a.com", Password = "a", Role = RoleEnum.Collector, Id = 1 };
            var token = GenerateJwtToken(user);

            var sneakerId = 6;

            // Arrange
            var command = new UpdateSneakerInputDto
            {
                Id = sneakerId,
                Name = "",
                Brand = "Nike",
                Price = 10,
                Size = 10,
                Year = 2010
            };

            // Act
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PutAsJsonAsync($"/api/sneaker/{sneakerId}", command);

            // Assert
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("The Name field is required.", responseContent);
        }

        [Fact]
        public async Task UpdateSneaker_WithInvalidBrand_ReturnsUnprocessableEntity()
        {
            var user = new User { Email = "a@a.com", Password = "a", Role = RoleEnum.Collector, Id = 1 };
            var token = GenerateJwtToken(user);

            var sneakerId = 6;

            // Arrange
            var command = new UpdateSneakerInputDto
            {
                Id = sneakerId,
                Name = "Total90",
                Brand = "",
                Price = 10,
                Size = 10,
                Year = 2010
            };

            // Act
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PutAsJsonAsync($"/api/sneaker/{sneakerId}", command);

            // Assert
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("The Brand field is required.", responseContent);
        }


        [Fact]
        public async Task UpdateSneaker_WithInvalidPrice_ReturnsUnprocessableEntity()
        {
            var user = new User { Email = "a@a.com", Password = "a", Role = RoleEnum.Collector, Id = 1 };
            var token = GenerateJwtToken(user);

            var sneakerId = 6;

            // Arrange
            var command = new UpdateSneakerInputDto
            {
                Id = sneakerId,
                Name = "Total90",
                Brand = "Nike",
                Price = 0,
                Size = 10,
                Year = 2010
            };

            // Act
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PutAsJsonAsync($"/api/sneaker/{sneakerId}", command);

            // Assert
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("Price must be greater than zero.", responseContent);
        }

        [Fact]
        public async Task UpdateSneaker_WithInvalidSize_LessThanMinimum_ReturnsUnprocessableEntity()
        {
            var user = new User { Email = "a@a.com", Password = "a", Role = RoleEnum.Collector, Id = 1 };
            var token = GenerateJwtToken(user);
            var sneakerId = 6;

            // Arrange
            var command = new UpdateSneakerInputDto
            {
                Id = sneakerId,
                Name = "Total90",
                Brand = "Nike",
                Price = 10,
                Size = 1,
                Year = 2005,
                Rate = RateEnum.TwoStars
            };

            // Act
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PutAsJsonAsync($"/api/sneaker/{sneakerId}", command);

            // Assert
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("Size must be between 5 and 50.", responseContent);
        }


        [Fact]
        public async Task UpdateSneaker_WithInvalidSize_GreaterThanMaximum_ReturnsUnprocessableEntity()
        {
            var user = new User { Email = "a@a.com", Password = "a", Role = RoleEnum.Collector, Id = 1 };
            var token = GenerateJwtToken(user);
            var sneakerId = 6;

            // Arrange
            var command = new UpdateSneakerInputDto
            {
                Id = sneakerId,
                Name = "Total90",
                Brand = "Nike",
                Price = 10,
                Size = 100,
                Year = 2005,
                Rate = RateEnum.TwoStars
            };

            // Act
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PutAsJsonAsync($"/api/sneaker/{sneakerId}", command);

            // Assert
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("Size must be between 5 and 50.", responseContent);
        }

        [Fact]
        public async Task UpdateSneaker_WithInvalidYear_LessThanMinimum_ReturnsUnprocessableEntity()
        {
            var user = new User { Email = "a@a.com", Password = "a", Role = RoleEnum.Collector, Id = 1 };
            var token = GenerateJwtToken(user);
            var sneakerId = 6;

            // Arrange
            var command = new UpdateSneakerInputDto
            {
                Id = sneakerId,
                Name = "Total90",
                Brand = "Nike",
                Price = 10,
                Size = 15,
                Year = 1900,
                Rate = RateEnum.TwoStars
            };

            // Act
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PutAsJsonAsync($"/api/sneaker/{sneakerId}", command);

            // Assert
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("Year must be between 1950 and 2100.", responseContent);
        }


        [Fact]
        public async Task UpdateSneaker_WithInvalidYear_GreaterThanMaximum_ReturnsUnprocessableEntity()
        {
            var user = new User { Email = "a@a.com", Password = "a", Role = RoleEnum.Collector, Id = 1 };
            var token = GenerateJwtToken(user);
            var sneakerId = 6;

            // Arrange
            var command = new UpdateSneakerInputDto
            {
                Id = sneakerId,
                Name = "Total90",
                Brand = "Nike",
                Price = 10,
                Size = 10,
                Year = 2500,
                Rate = RateEnum.TwoStars
            };

            // Act
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PutAsJsonAsync($"/api/sneaker/{sneakerId}", command);

            // Assert
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("Year must be between 1950 and 2100.", responseContent);
        }

       

       
        [Fact]
        public async Task UpdateSneaker_WithInvalidRate_ReturnsUnprocessableEntity()
        {
            var user = new User { Email = "a@a.com", Password = "a", Role = RoleEnum.Collector, Id = 1 };
            var token = GenerateJwtToken(user);

            var sneakerId = 6;

            // Arrange
            var command = new UpdateSneakerInputDto
            {
                Id = sneakerId,
                Name = "Total90",
                Brand = "Nike",
                Price = 10,
                Size = 10,
                Year = 2010
            };

            // Act
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PutAsJsonAsync($"/api/sneaker/{sneakerId}", command);

            // Assert
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("Rate must be between 1 and 5.", responseContent);
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("2iWBoHbts8keiKrqsYw8h5YlzbMXXMG0BVRx7mOgRyiqo0EiHMOHWtwOc20M3q5"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "https://localhost:7103",
                audience: "https://localhost:7103/swagger/index.html",
                claims: new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                },
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
