using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneTranslate;

namespace PhoneTranslateUnitTesting
{
    [TestClass]
    public class WordReplaceTests
    {
        [DataTestMethod]
        [DataRow("", "")]
        [DataRow("abc", "abc")]
        [DataRow("afk", "away from keyboard")]
        [DataRow("afk brb\nimo", "away from keyboard be right back\nin my opinion")]
        [DataRow("damn", "damn")]
        public void RunReplace_SlangToWordNoSwearFilter_ReturnsCorrectTranslation(string input, string actual)
        {
            const bool swearFilter = false;
            const bool wordToSlang = false;
            var wordReplace = new WordReplace();

            var result = wordReplace.RunReplace(input, swearFilter, wordToSlang);

            Assert.AreEqual(actual, result, true);
        }


        [DataTestMethod]
        [DataRow("", "")]
        [DataRow("abc", "abc")]
        [DataRow("afk", "away from keyboard")]
        [DataRow("afk brb\nimo", "away from keyboard be right back\nin my opinion")]
        [DataRow("damn", "d***")]
        public void RunReplace_SlangToWordWithSwearFilter_ReturnsCorrectTranslation(string input, string actual)
        {
            const bool swearFilter = true;
            const bool wordToSlang = false;
            var wordReplace = new WordReplace();

            var result = wordReplace.RunReplace(input, swearFilter, wordToSlang);

            Assert.AreEqual(actual, result, true);
        }


        [DataTestMethod]
        [DataRow("", "")]
        [DataRow("abc", "abc")]
        [DataRow("away from keyboard", "afk")]
        [DataRow("away from keyboard be right back\nin my opinion", "afk brb\nimo")]
        [DataRow("damn", "damn")]
        public void RunReplace_WordToSlandNoSwearFilter_ReturnsCorrectTranslation(string input, string actual)
        {
            const bool swearFilter = false;
            const bool wordToSlang = true;
            var wordReplace = new WordReplace();

            var result = wordReplace.RunReplace(input, swearFilter, wordToSlang);

            Assert.AreEqual(actual, result, true);
        }


        [DataTestMethod]
        [DataRow("", "")]
        [DataRow("abc", "abc")]
        [DataRow("away from keyboard", "afk")]
        [DataRow("away from keyboard be right back in my opinion", "afk brb imo")]
        [DataRow("d***", "d***")]
        public void RunReplace_WordToSlandWithSwearFilter_ReturnsCorrectTranslation(string input, string actual)
        {
            const bool swearFilter = true;
            const bool wordToSlang = true;
            var wordReplace = new WordReplace();

            var result = wordReplace.RunReplace(input, swearFilter, wordToSlang);

            Assert.AreEqual(actual, result, true);
        }
    }
}
