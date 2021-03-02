using BusinessLayer;
using Xunit;

namespace BusinessLayerTests
{
    public class UserTest
    {
        [Fact]
        public void ValidateValid()
        {
            // Arrange
            User user1 = new User(1) { FirstName = "User", LastName = "Userenko", Email = "user@email.com" };

            // Act
            bool actual = user1.Validate();

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void ValidateOnlyFirstname()
        {
            // Arrange
            User user1 = new User(1) { FirstName = "User" };

            // Act
            bool actual = user1.Validate();

            // Assert
            Assert.False(actual);
        }
    }
}
