using NUnit.Framework;

namespace RegexWizard.Framework.Tests.Integration.Serialization
{
    [TestFixture]
    public class RegexWizardProjectDataSerializerTests
    {
        private const string TEST_FILE_NAME = "test.a";

        [Test,Ignore()]
        public void SimpleProjectData()
        {
            RegexWizardProjectData data = new RegexWizardProjectData();
            data.RootScope = new Scope("abc");
            data.Save<RegexWizardProjectData>(TEST_FILE_NAME);

            checkEqual(data);
          
        }


        [Test, Ignore()]
        public void SimpleProjectDataWithSampleText()
        {
            RegexWizardProjectData data = new RegexWizardProjectData();
            
            data.SampleText = "test";
            data.Save<RegexWizardProjectData>(TEST_FILE_NAME);

            checkEqual(data);
        }

        private  void checkEqual(RegexWizardProjectData expectedData)
        {
            RegexWizardProjectData loaded =
                RegexWizardProjectData.Load<RegexWizardProjectData>(TEST_FILE_NAME);
            Assert.AreEqual(expectedData, loaded);
        }
    }
}
