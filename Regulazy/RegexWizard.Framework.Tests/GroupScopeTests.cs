using NUnit.Framework;

namespace RegexWizard.Framework.Tests
{
    [TestFixture]
    public class GroupScopeTests
    {
        
        [Test]
        public void Create()
        {
            GroupedScope gs = new GroupedScope();
            Assert.IsNotNull(gs);
        }

    }
}
