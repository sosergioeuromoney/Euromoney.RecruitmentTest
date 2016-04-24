using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using NUnit.Common;
using Moq;
using ContentConsole.Application.Services;
using ContentConsole.Application.Domain;
using ContentConsole.Application.Data;

namespace ContentConsole.Test.Unit.Application.Services
{
    [TestFixture]
    public class NegativeWordTest
    {
        [SetUp]
        public void Init()
        {
            
        }

        [TestCase("a", "b", false)]
        [TestCase("a", "a", true)]
        [TestCase("a", "", false)]
        public void Equals_WhenTwoWordsAreEqual_ShouldReturnTrue(string a, string b, bool expect)
        {
            // Arrange
            var word1 = new NegativeWord(a);
            var word2 = new NegativeWord(b);

            // Act
            var result = word1.Equals(word2);

            //Assert
            Assert.AreEqual(expect, result);
        }
        

    }
}
