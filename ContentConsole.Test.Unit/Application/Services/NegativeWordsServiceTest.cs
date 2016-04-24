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
    public class NegativeWordsServiceTest
    {
        Mock<INegativeWordsRepository> _negativeWordsRepo;
        List<NegativeWord> _negativeWords;
        
        NegativeWordsService _negativeWordService;

        [SetUp]
        public void Init()
        {
            //Arrange
            _negativeWordsRepo = new Mock<INegativeWordsRepository>();
            _negativeWordService = new NegativeWordsService(_negativeWordsRepo.Object);
            //Stubbing negative words
            _negativeWords = new List<NegativeWord>() { 
                new NegativeWord("badword1"),
                new NegativeWord("badword2"),
                new NegativeWord("badword3"),
                new NegativeWord("badword4"),
                new NegativeWord("badword5"),
                new NegativeWord("bad"),
                new NegativeWord("horrible"),
                new NegativeWord("let me down")
            };
        }

        #region ScanText
        [TestCase("sample text with no badword", 0)]
        [TestCase("sample text with badword1", 1)]
        [TestCase("sample text with badword1 badword2", 2)]
        [TestCase("sample text with badword1 badword2 badword3", 3)]
        [TestCase("sample text with badword1 badword2 badword3 badword4", 4)]
        [TestCase("sample text with badword1 badword2 badword3 badword4 badword5", 5)]
        [TestCase("The weather in Manchester in winter is bad. It rains all the time - it must be horrible for people visiting.", 2)]
        [TestCase("Why would you let me down", 1)]
        [TestCase("Follow me down this path", 0)]
        public void ScanText_WithXNegativeWord_ShouldReturnX(string sample, int expectedResult)
        {
            // Arrange
            _negativeWordsRepo.Setup(e => e.GetAll()).Returns(_negativeWords);

            // Act
            var result = _negativeWordService.ScanText(sample);
            var count = result.Count();
            //Assert
            _negativeWordsRepo.Verify(e => e.GetAll(), Times.Once());
            Assert.AreEqual(count, expectedResult);
        }
        #endregion

        #region AddNegativeWord
        [Test]
        public void AddNegativeWord_WithValidInput_ShouldIncreaseTheNumberOfWords()
        {
            // Arrange
            var negativeWord = new NegativeWord("badword");
            _negativeWordsRepo.Setup(e => e.Add(negativeWord));
            
            // Act
            _negativeWordService.Add(negativeWord.Value);

            //Assert
            _negativeWordsRepo.Verify(e => e.Add(negativeWord), Times.Once());
            
        }

        [Test]
        public void AddNegativeWord_WithInvalidInput_ShouldThrow()
        {
            
            // Arrange
            string word = null;
            
            //Act/Assert
            Assert.That(() => _negativeWordService.Add(word), Throws.TypeOf<ArgumentException>());
        }
        #endregion

        #region RemoveNegativeWord
        [Test]
        public void RemoveNegativeWord_WithValidInput_ShouldCallTheRepo()
        {
            // Arrange
            var negativeWord = new NegativeWord("badword");
            _negativeWordsRepo.Setup(e => e.Remove(negativeWord));

            // Act
            _negativeWordService.Remove(negativeWord.Value);

            //Assert
            _negativeWordsRepo.Verify(e => e.Remove(negativeWord), Times.Once());

        }
        #endregion

        #region GetNegativeWords
        [Test]
        public void GetAll_ShouldCallTheRepository()
        {
            // Arrange
            _negativeWordsRepo.Setup(e => e.GetAll()).Returns(_negativeWords);

            // Act
            var result = _negativeWordService.GetAll();

            //Assert
            _negativeWordsRepo.Verify(e => e.GetAll(), Times.Once());
            Assert.AreEqual(result, _negativeWords.Select(w => w.Value));

        }
        #endregion

        #region ObscureText
        [TestCase("sample text with no badword", "sample text with no badword")]
        [TestCase("sample text with badword1", "sample text with b######1")]
        [TestCase("The weather in Manchester in winter is bad. It rains all the time - it must be horrible for people visiting.", "The weather in Manchester in winter is b#d. It rains all the time - it must be h######e for people visiting.")]
        public void ObscureText_WithXNegativeWord_ShouldReturnObscured(string sample, string expectedResult)
        {
            // Arrange
            _negativeWordsRepo.Setup(e => e.GetAll()).Returns(_negativeWords);

            // Act
            var result = _negativeWordService.ObscureText(sample);

            //Assert
            _negativeWordsRepo.Verify(e => e.GetAll(), Times.Once());
            Assert.AreEqual(result, expectedResult);
        }
        #endregion
    }
}
