using EasyClinic.OfficesService.Api.Controllers;
using EasyClinic.OfficesService.Application.Commands.ChangeOfficeStatus;
using EasyClinic.OfficesService.Application.Commands.EditOffice;
using EasyClinic.OfficesService.Application.DTO;
using EasyClinic.OfficesService.Application.Queries.GetAllOffices;
using EasyClinic.OfficesService.Application.Queries.GetOfficeInfo;
using EasyClinic.OfficesService.Domain.Entities;
using EasyClinic.OfficesService.Domain.Enums;
using EasyClinic.OfficesService.Domain.Exceptions;
using EasyClinic.OfficesService.Domain.RepositoryContracts;
using EasyClinic.OfficesService.Infrastructure;
using EasyClinic.OfficesService.Infrastructure.Repository;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;



namespace EasyClinic.OfficesService.Tests
{
    public class OfficeControllerUnitTests
    {
        private readonly IMediator _mediator;
        private readonly OfficesController _controller;
        private readonly ServiceProvider _serviceProvider;
        private readonly OfficesServiceDbContext _context;

        private List<Office> Offices { get;set; } =
        [
            new Office
            {
                Id = Guid.Parse("b0f5b5d0-a42e-404e-b2b8-ace0531b8354"),
                City = "London",
                Street = "Gray hill",
                HouseNumber = 34,
                OfficeNumber = 153,
                Status = OfficeStatus.Available,
                RegistryPhone = "4786553244"
            },
            new Office
            {
                Id = Guid.Parse("b7090e68-5ee6-42f1-95ca-761b0e1d99dc"),
                City = "Miami",
                Street = "Liberation road",
                HouseNumber = 27,
                OfficeNumber = 111,
                Status = OfficeStatus.Available,
                RegistryPhone = "98236574354"
            },
            new Office
            {
                Id = Guid.Parse("ff76093b-dc11-49a0-ad9c-fe0b0fa63e08"),
                City = "Seoul",
                Street = "Shincha suo",
                HouseNumber = 83,
                Status = OfficeStatus.Available,
                RegistryPhone = "23715605473"
            }
        ];

        public OfficeControllerUnitTests()
        {
            var loggerMock = new Mock<ILogger<OfficesController>>();

            _serviceProvider = new ServiceCollection()
                .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetAllOfficesQuery).Assembly))
                .AddScoped<IRepository<Office>, Repository<Office>>()
                .AddDbContext<OfficesServiceDbContext>(op => op.UseInMemoryDatabase("OfficesServiceTestDb"))
                .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
                .BuildServiceProvider();

            _mediator = _serviceProvider.GetRequiredService<IMediator>();
            _context = _serviceProvider.GetRequiredService<OfficesServiceDbContext>();
        

            _controller = new OfficesController(_mediator, loggerMock.Object);
        }


        [Fact]
        public async Task GetAllOffices_ValidRequest_ReturnsOk()
        {
            // Arrange
            _context.RemoveRange(_context.Offices);
            _context.AddRange(Offices);
            await _context.SaveChangesAsync();

            var testOffices = Offices;

            // Act
            var apiResponse = await _controller.GetAllOffices();
            var offices = ((ObjectResult)apiResponse).Value as List<Office>;

            // Assert
            apiResponse.Should().BeOfType<OkObjectResult>();
            offices.Should().BeEquivalentTo(testOffices);
        }

        [Fact]
        public async Task GetOfficeInfo_ValidIdProvided_ReturnsOk()
        {
            // Arrange
            _context.RemoveRange(_context.Offices);
            _context.AddRange(Offices);
            await _context.SaveChangesAsync();
            
            var testOffice = Offices[0];
            var request = new GetOfficeInfoQuery{ Id = testOffice.Id };

            // Act
            var apiResponse = await _controller.GetOfficeInfo(request);
            var office = ((ObjectResult)apiResponse).Value as Office;

            // Assert
            apiResponse.Should().BeOfType<OkObjectResult>();
            office.Should().BeEquivalentTo(testOffice);
        }

        [Fact]
        public async Task GetOfficeInfo_NonexistentIdProvided_ThrowsNotfound()
        {
            // Arrange
            _context.RemoveRange(_context.Offices);
            _context.AddRange(Offices);
            await _context.SaveChangesAsync();

            var testOfficeId = Guid.Parse("39735a44-6698-4ea2-8b31-6d6fef9d6fcd");
            var request = new GetOfficeInfoQuery{ Id = testOfficeId };

            // Act
            var func = async () => await _controller.GetOfficeInfo(request);

            // Assert
            await func.Should().ThrowExactlyAsync<NotFoundException>();
        }
		
		
		[Fact]
        public async Task UpdateOfficeStatus_ValidIdProvided_ReturnsOk()
        {
            // Arrange
            _context.RemoveRange(_context.Offices);
            _context.AddRange(Offices);
            await _context.SaveChangesAsync();

            var testOfficeId = Offices[0].Id;
            var newStatus = (byte)OfficeStatus.Occupied;
            var request = new ChangeOfficeStatusCommand
            { 
                OfficeId = testOfficeId, 
                NewStatus = newStatus
            };
            var testQuery = new GetOfficeInfoQuery{ Id = testOfficeId };

            // Act
            await _controller.UpdateOfficeStatus(request);

            var apiResponse = await _controller.GetOfficeInfo(testQuery);
            var office = ((ObjectResult)apiResponse).Value as Office;

            // Assert
            apiResponse.Should().BeOfType<OkObjectResult>();
            office?.Status.Should().HaveValue(newStatus);
        }

        [Fact]
        public async Task UpdateOfficeStatus_NonexistentIdProvided_ThrowsNotfound()
        {
            // Arrange
            _context.RemoveRange(_context.Offices);
            _context.AddRange(Offices);
            await _context.SaveChangesAsync();

            var testOfficeId = Guid.Parse("39735a44-6698-4ea2-8b31-6d6fef9d6fcd");
            var newStatus = (byte)OfficeStatus.Occupied;
            var request = new ChangeOfficeStatusCommand
            { 
                OfficeId = testOfficeId, 
                NewStatus = newStatus
            };

            // Act
            var func = async () => await _controller.UpdateOfficeStatus(request);

            // Assert
            await func.Should().ThrowExactlyAsync<NotFoundException>();
        }

        [Fact]
        public async Task EditOffice_ValidIdAndModelProvided_ReturnsOk()
        {
            // Arrange
            _context.RemoveRange(_context.Offices);
            _context.AddRange(Offices);
            await _context.SaveChangesAsync();

            var testOfficeId = Offices[0].Id;
            var testOffice = new OfficeDto
            { 
                City = "New London",
                Street = "Red hill",
                HouseNumber = 341,
                OfficeNumber = 531,
                Status = OfficeStatus.Occupied,
                RegistryPhone = "47865532441"
            };
            var testQuery = new GetOfficeInfoQuery{ Id = testOfficeId };

            // Act
            await _controller.EditOffice(testOfficeId, testOffice);

            var apiResponse = await _controller.GetOfficeInfo(testQuery);
            var office = ((ObjectResult)apiResponse).Value as Office;

            // Assert
            apiResponse.Should().BeOfType<OkObjectResult>();
            office?.City.Should().Be(testOffice.City);
            office?.Street.Should().Be(testOffice.Street);
            office?.HouseNumber.Should().Be(testOffice.HouseNumber);
            office?.OfficeNumber.Should().Be(testOffice.OfficeNumber);
            office?.Status.Should().Be(testOffice.Status);
            office?.RegistryPhone.Should().Be(testOffice.RegistryPhone);
        }

        [Fact]
        public async Task EditOffice_NonexistentIdProvided_ThrowsNotfound()
        {
            // Arrange
            _context.RemoveRange(_context.Offices);
            _context.AddRange(Offices);
            await _context.SaveChangesAsync();

            var testOfficeId = Guid.Parse("39735a44-6698-4ea2-8b31-6d6fef9d6fcd");
            var testOffice = new OfficeDto
            { 
                City = "New London",
                Street = "Red hill",
                HouseNumber = 341,
                OfficeNumber = 531,
                Status = OfficeStatus.Occupied,
                RegistryPhone = "47865532441"
            };

            // Act
            var func = async () => await _controller.EditOffice(testOfficeId, testOffice);

            // Assert
            await func.Should().ThrowExactlyAsync<NotFoundException>();
        }

        [Fact]
        public async Task CreateOffice_ValidModelProvided_ReturnsOk()
        {
            // Arrange
            _context.RemoveRange(_context.Offices);
            _context.AddRange(Offices);
            await _context.SaveChangesAsync();

            var testOffice = new OfficeDto
            { 
                City = "New London",
                Street = "Red hill",
                HouseNumber = 341,
                OfficeNumber = 531,
                Status = OfficeStatus.Occupied,
                RegistryPhone = "47865532441"
            };

            // Act
            var apiResponse = await _controller.CreateOffice(testOffice);
            var office = ((ObjectResult)apiResponse).Value as Office;

            // Assert
            apiResponse.Should().BeOfType<CreatedAtActionResult>();
            office?.City.Should().Be(testOffice.City);
            office?.Street.Should().Be(testOffice.Street);
            office?.HouseNumber.Should().Be(testOffice.HouseNumber);
            office?.OfficeNumber.Should().Be(testOffice.OfficeNumber);
            office?.Status.Should().Be(testOffice.Status);
            office?.RegistryPhone.Should().Be(testOffice.RegistryPhone);
        }
    }
}
