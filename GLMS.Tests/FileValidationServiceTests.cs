using GLMS.Web.Services;
using Xunit;

namespace GLMS.Tests
{
    public class FileValidationServiceTests
    {
        [Fact]
        public void IsPdfFile_ReturnsTrue_WhenFileIsPdf()
        {
            var service = new FileValidationService();

            var result = service.IsPdfFile("signed_contract.pdf");

            Assert.True(result);
        }

        [Fact]
        public void IsPdfFile_ReturnsFalse_WhenFileIsDocx()
        {
            var service = new FileValidationService();

            var result = service.IsPdfFile("signed_contract.docx");

            Assert.False(result);
        }

        [Fact]
        public void IsPdfFile_ReturnsFalse_WhenFileIsExe()
        {
            var service = new FileValidationService();

            var result = service.IsPdfFile("malware.exe");

            Assert.False(result);
        }

        [Fact]
        public void IsPdfFile_ReturnsFalse_WhenFileNameIsEmpty()
        {
            var service = new FileValidationService();

            var result = service.IsPdfFile("");

            Assert.False(result);
        }
    }
}