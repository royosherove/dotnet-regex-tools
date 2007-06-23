using NUnit.Framework;

namespace RegexWizard.Framework.Tests
{
    /// <summary>
    /// Summary description for RegexOptimizerTests
    /// </summary>
    [TestFixture]
    public class RegexOptimizerTests
    {

        [Test]
        public void Create()
        {
            RegexOptimizer r = new RegexOptimizer();
            Assert.IsNotNull(r);
        }


    }
}
