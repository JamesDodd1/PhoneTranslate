using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneTranslate;
using PhoneTranslate.Crud;

namespace PhoneTranslateUnitTesting.Crud
{
    [TestClass]
    public class SaveLoadTests
    {
        [TestMethod]
        public void Load_RetrievesTextFromFile_ReturnsDisplayText()
        {
            const string file = @"Save 1";
            var saveLoad = new SaveLoad();

            var result = saveLoad.Load(file);

            Assert.IsNotNull(result);
        }


        [DataTestMethod]
        [DataRow(null)]
        [DataRow(@"")]
        public void Load_FileNotFound_ReturnsNull(string file)
        {
            var saveLoad = new SaveLoad();

            var result = saveLoad.Load(file);

            Assert.IsNull(result);
        }


        [TestMethod]
        public void Save_SaveToFile_ReturnsTrue()
        {
            const string file = @"Save 1";
            DisplayText text = new DisplayText { Input = "", Output = "" };
            
            var saveLoad = new SaveLoad();
            var previousSave = saveLoad.Load(file); // Store original saved data

            var result = saveLoad.Save(file, text);

            Assert.IsTrue(result);
            
            saveLoad.Save(file, previousSave); // Restore previous save
        }

        
        [TestMethod]
        public void Save_DisplayTextValueIsNull_ReturnFalse()
        {
            const string file = @"Save 1";
            var saveLoad = new SaveLoad();

            var result = saveLoad.Save(file, null);

            Assert.IsFalse(result);
        }


        [DataTestMethod]
        [DataRow(null)]
        [DataRow(@"")]
        public void Save_FileNotFound_ReturnsFalse(string file)
        {
            var saveLoad = new SaveLoad();

            var result = saveLoad.Save(file, new DisplayText());

            Assert.IsFalse(result);
        }
    }
}
