using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneTranslate.Crud;

namespace PhoneTranslateUnitTesting.Crud
{
    [TestClass]
    public class FileFactoryTests
    {
        [TestMethod]
        public void GetFile_FileNameEntered_ReturnsInstance()
        {
            const string type = "Dictionary";

            var result = FileFactory.GetFile(type);

            Assert.IsInstanceOfType(result, typeof(Translate));
        }


        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        public void GetFile_IncorrectFileNameEntered_ReturnsNull(string type)
        {
            var result = FileFactory.GetFile(type);

            Assert.IsNull(result);
        }
    }
}
