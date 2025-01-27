using FleetManager.Domain.Contractors;
using FluentAssertions;

namespace FleetManager.Tests.Unit.Contractors
{
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
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(ContractorErrors.MissingHeadquarters());
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
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(ContractorErrors.MissingContractorName());
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
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Name.Should().Be(validName);
            result.Value.HeadquartersId.Should().Be(validHeadquartersId);
        }
    }
}
