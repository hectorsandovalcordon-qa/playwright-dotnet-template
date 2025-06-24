using Xunit;
using Allure.Xunit.Attributes;

namespace Tests
{
    [AllureSuite("Demo Suite")]
    public class AllureSampleTests
    {
        [Fact]
        [AllureFeature("Simple Test")]
        [AllureTag("demo")]
        [AllureSeverity(Allure.Net.Commons.SeverityLevel.normal)]
        public void MySimpleTest()
        {
            Assert.True(true);
        }
    }
}
