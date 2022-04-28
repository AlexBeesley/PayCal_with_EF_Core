using NUnit.Framework;
using PayCal.Extensions;

namespace Tests.Extensions_Tests
{
    public class FriendlyURLsShould
    {
        [Test]
        public void Check_ToStringFromGuid_returns_correct_string()
        {
            // Arrange
            string guidAsString = "0d69de9f-c499-4adb-a0a1-3447813a800d";

            // Act
            var x = guidAsString.ToStringFromGuid();

            // Assert
            Assert.That(x, Is.EqualTo("n95pDZnE20qgoTRHgTqADQ"));
        }

        [Test]
        public void Check_ToGuidFromString_returns_correct_GUID()
        {
            // Arrange
            string guidAsBase64String = "n95pDZnE20qgoTRHgTqADQ";

            // Act
            var x = guidAsBase64String.ToGuidFromString();

            // Assert
            Assert.That(x, Is.EqualTo("0d69de9f-c499-4adb-a0a1-3447813a800d"));
        }
    }
}
