using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneTranslate.Crud;

namespace PhoneTranslateUnitTesting.Crud
{
    [TestClass]
    public class TranslateTests
    {
        [TestMethod]
        public void Read_ReadFromFile_ReturnsListOfTranslations()
        {
            const string file = @"Dictionary";
            var translate = new Translate(file);

            var result = translate.Read();

            Assert.IsNotNull(result);
        }


        [DataTestMethod]
        [DataRow(null)]
        [DataRow(@"")]
        public void Read_FileNotFound_ReturnsNull(string file)
        {
            var translate = new Translate(file);

            var result = translate.Read();

            Assert.IsNull(result);
        }


        [TestMethod]
        public void Add_InsertNewTranslation_ReturnsTrue()
        {
            const string file = @"Dictionary";
            var translate = new Translate(file);

            var result = translate.Add("newWord", "newTranslation");

            Assert.IsTrue(result);

            translate.Remove("newWord", "newTranslation"); // Remove test translation
        }


        [TestMethod]
        public void Add_TranslationAlreadyExists_ReturnsFalse()
        {
            const string file = @"Dictionary";
            const string existingWord = "Existing Word";
            const string existingTranslation = "Existing Translation";
            
            var translate = new Translate(file);
            translate.Add(existingWord, existingTranslation); // Create test translation
            
            var result = translate.Add(existingWord, existingTranslation);

            Assert.IsFalse(result);

            translate.Remove(existingWord, existingTranslation); // Remove test translation
        }


        [DataTestMethod]
        [DataRow(null)]
        [DataRow(@"")]
        public void Add_FileNotFound_ReturnsFalse(string file)
        {
            var translate = new Translate(file);

            var result = translate.Add("", "");

            Assert.IsFalse(result);
        }


        [DataTestMethod]
        [DataRow(null, "example")]
        [DataRow("example", null)]
        [DataRow("", "example")]
        [DataRow("example", "")]
        public void Add_InsertNullOrEmptyString_ReturnsFalse(string word, string translation)
        {
            const string file = @"Dictionary";
            var translate = new Translate(file);

            var result = translate.Add(word, translation);

            Assert.IsFalse(result);
        }


        [TestMethod]
        public void Edit_UpdateTranslation_ReturnsTrue()
        {
            const string file = @"Dictionary";
            const string word = "Unedited Word";
            const string translation = "Unedited Translation";
            const string newWord = "Edited Word";
            const string newTranslation = "Edited Translation";
            
            var translate = new Translate(file);
            translate.Add(word, translation); // Create test translation
            
            var result = translate.Edit(word, translation, newWord, newTranslation);

            Assert.IsTrue(result);

            translate.Remove(newWord, newTranslation); // Remove test translation
        }


        [TestMethod]
        public void Edit_TranslationNotFound_ReturnsFalse()
        {
            const string file = @"Dictionary";
            var translate = new Translate(file);

            var result = translate.Edit("", "", "", "");

            Assert.IsFalse(result);
        }


        [DataTestMethod]
        [DataRow(null, "example")]
        [DataRow("example", null)]
        [DataRow("", "example")]
        [DataRow("example", "")]
        public void Edit_UpdateToNullOrEmptyString_ReturnsFalse(string newWord, string newTranslation)
        {
            const string file = @"Dictionary";
            var translate = new Translate(file);

            var result = translate.Edit("", "", newWord, newTranslation);

            Assert.IsFalse(result);
        }


        [DataTestMethod]
        [DataRow(null)]
        [DataRow(@"")]
        public void Edit_FileNotFound_ReturnsFalse(string file)
        {
            var translate = new Translate(file);

            var result = translate.Edit("", "", "", "");

            Assert.IsFalse(result);
        }


        [TestMethod]
        public void Remove_DeleteFromFile_ReturnsTrue()
        {
            const string file = @"Dictionary";
            const string word = "Yet To Be Removed Word";
            const string translation = "Yet To Be Removed Translation";
            
            var translate = new Translate(file);
            translate.Add(word, translation); // Create test translation
            
            var result = translate.Remove(word, translation);

            Assert.IsTrue(result);
        }


        [TestMethod]
        public void Remove_TranslationNotFound_ReturnsFalse()
        {
            const string file = @"Dictionary";
            var translate = new Translate(file);

            var result = translate.Remove("", "");

            Assert.IsFalse(result);
        }


        [DataTestMethod]
        [DataRow(null)]
        [DataRow(@"")]
        public void Remove_FileNotFound_ReturnsFalse(string file)
        {
            var translate = new Translate(file);

            var result = translate.Remove("", "");

            Assert.IsFalse(result);
        }
    }
}
