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


            // Undo insert
            translate.Remove("newWord", "newTranslation");
        }


        [TestMethod]
        public void Add_TranslationExists_ReturnsFalse()
        {
            const string file = @"Dictionary";
            var translate = new Translate(file);

            const string existingWord = "Existing Word";
            const string existingTranslation = "Existing Translation";
            translate.Add(existingWord, existingTranslation); // Create example translation

            var result = translate.Add(existingWord, existingTranslation);

            Assert.IsFalse(result);

            
            // Undo insert
            translate.Remove(existingWord, existingTranslation); 
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
            var translate = new Translate(file);

            const string word = "Unedited Word";
            const string translation = "Unedited Translation";
            translate.Add(word, translation); // Create example translation

            const string newWord = "Edited Word";
            const string newTranslation = "Edited Translation";

            var result = translate.Edit(word, translation, newWord, newTranslation);

            Assert.IsTrue(result);


            // Undo insert
            translate.Remove(newWord, newTranslation);
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
            var translate = new Translate(file);

            const string word = "Yet To Be Removed Word";
            const string translation = "Yet To Be Removed Translation";
            translate.Add(word, translation);

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
