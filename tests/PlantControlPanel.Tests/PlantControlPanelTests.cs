using NSubstitute;
using Xunit;
using PlantControlPanel.Application.Abstractions.Persistence;
using PlantControlPanel.Application.Abstractions.Persistence.Queries;
using PlantControlPanel.Application.Abstractions.Persistence.Repositories;
using PlantControlPanel.Application.Contracts.RollService.Operations;
using PlantControlPanel.Application.Services;
using PlantControlPanel.Domain;

namespace PlantControlPanel.Tests;

public class RollServiceTests
{
    private readonly IRollRepository _rollRepositoryMock;
    private readonly IPersistenceContext _contextMock;
    private readonly RollService _rollService;

    public RollServiceTests()
    {
        _rollRepositoryMock = Substitute.For<IRollRepository>();
        _contextMock = Substitute.For<IPersistenceContext>();
        _contextMock.RollRepository.Returns(_rollRepositoryMock);
        _rollService = new RollService(_contextMock);
    }
    
    [Fact]
    public async Task AddRoll_ShouldReturnSuccess_AndCallRepository()
    {
        // Arrange
        var request = new AddRoll.Request(Length: 100.5, Weight: 500.0);
        var savedRoll = new Roll(1, request.Length, request.Weight, DateTime.UtcNow); 
        
        _rollRepositoryMock.Add(Arg.Any<Roll>(), Arg.Any<CancellationToken>())
            .Returns(savedRoll);

        // Act
        var response = await _rollService.AddRoll(request, CancellationToken.None);

        // Assert
        var successResponse = Assert.IsType<AddRoll.Response.Success>(response);
        Assert.Equal(1, successResponse.Roll.Id);
        await _rollRepositoryMock.Received(1).Add(Arg.Any<Roll>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task DeleteRoll_ShouldSetRemoveTime_WhenRollExists()
    {
        // Arrange
        var rollId = 10;
        var request = new DeleteRoll.Request(rollId);
        var existingRoll = new Roll(rollId, 100, 200, DateTime.UtcNow);
        var mockQuery = new List<Roll> { existingRoll }.AsQueryable();
        
        _rollRepositoryMock.Query(Arg.Any<RollQuery>()).Returns(mockQuery);
        
        _rollRepositoryMock.Delete(Arg.Any<Roll>(), Arg.Any<CancellationToken>())
            .Returns(existingRoll);

        // Act
        var response = await _rollService.DeleteRoll(request, CancellationToken.None);

        // Assert
        var successResponse = Assert.IsType<DeleteRoll.Response.Success>(response);
        Assert.NotNull(successResponse.Roll.RemoveTime); 
        
        await _rollRepositoryMock.Received(1).Delete(Arg.Is<Roll>(r => r.Id == rollId), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task DeleteRoll_ShouldReturnBadRequest_WhenRollNotFound()
    {
        // Arrange
        var request = new DeleteRoll.Request(99);
        
        // База пустая
        var emptyQuery = new List<Roll>().AsQueryable();
        _rollRepositoryMock.Query(Arg.Any<RollQuery>()).Returns(emptyQuery);

        // Act
        var response = await _rollService.DeleteRoll(request, CancellationToken.None);

        // Assert
        Assert.IsType<DeleteRoll.Response.BadRequest>(response);
        await _rollRepositoryMock.DidNotReceive().Delete(Arg.Any<Roll>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task DeleteRoll_ShouldReturnBadRequest_WhenAlreadyDeleted()
    {
        // Arrange
        var request = new DeleteRoll.Request(1);
        var alreadyDeletedRoll = new Roll(1, 100, 200, DateTime.UtcNow);
        alreadyDeletedRoll.RemoveTime = DateTime.UtcNow;

        var mockQuery = new List<Roll> { alreadyDeletedRoll }.AsQueryable();
        _rollRepositoryMock.Query(Arg.Any<RollQuery>()).Returns(mockQuery);

        // Act
        var response = await _rollService.DeleteRoll(request, CancellationToken.None);

        // Assert
        Assert.IsType<DeleteRoll.Response.BadRequest>(response);
    }
}