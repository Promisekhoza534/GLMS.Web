using GLMS.API.Services;
using Xunit;

namespace GLMS.Tests
{
    public class FileValidationServiceTests
    {
        [Fact]
        public void IsPdfFile_ReturnsTrue_WhenFileIsPdf()
        {
            // Arrange
            var service = new FileValidationService();

            // Act
            var result = service.IsPdfFile("signed_contract.pdf");

            // Assert
            Assert.True(result);
        }


        [Fact]
        public void IsPdfFile_ReturnsFalse_WhenFileIsDocx()
        {
            // Arrange
            var service = new FileValidationService();

            // Act
            var result = service.IsPdfFile("signed_contract.docx");

            // Assert
            Assert.False(result);
        }


        [Fact]
        public void IsPdfFile_ReturnsFalse_WhenFileIsExe()
        {
            // Arrange
            var service = new FileValidationService();

            // Act
            var result = service.IsPdfFile("malware.exe");

            // Assert
            Assert.False(result);
        }


        [Fact]
        public void IsPdfFile_ReturnsFalse_WhenFileNameIsEmpty()
        {
            // Arrange
            var service = new FileValidationService();

            // Act
            var result = service.IsPdfFile("");

            // Assert
            Assert.False(result);
        }
    }
}