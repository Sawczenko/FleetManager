using FleetManager.Modules.Orders.Domain.Contractors;
using Shouldly;

namespace FleetManager.Modules.Orders.UnitTests.Contractors;

public class ContractorFactoryTests
{
    [Fact]
    public void Create_ShouldReturnFailure_WhenHeadquartersIdIsEmpty()
    {
        // Arrange
        string name = "Valid Contractor";
        Guid emptyHeadquartersId = Guid.Empty;

        // Act
        var result = ContractorFactory.Create(name, emptyHeadquartersId);

        // Assert
        result.ShouldNotBeNull();
        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldBe(ContractorErrors.MissingHeadquarters());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_ShouldReturnFailure_ForInvalidName(string invalidName)
    {
        // Arrange
        Guid headquartersId = Guid.NewGuid();

        // Act
        var result = ContractorFactory.Create(invalidName, headquartersId);

        // Assert
        result.ShouldNotBeNull();
        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldBe(ContractorErrors.MissingContractorName());
    }

    [Fact]
    public void Create_ShouldReturnSuccess_WhenInputsAreValid()
    {
        // Arrange
        string validName = "Valid Contractor";
        Guid validHeadquartersId = Guid.NewGuid();

        // Act
        var result = ContractorFactory.Create(validName, validHeadquartersId);

        // Assert
        result.ShouldNotBeNull();
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();
        result.Value.Name.ShouldBe(validName);
        result.Value.HeadquartersId.ShouldBe(validHeadquartersId);
    }
}