using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using Moq;
using ContentConsole.Application.Services;
using ContentConsole.Application.Domain;
using ContentConsole.Application.Data;

namespace ContentConsole.Test.Unit.Application.Data
{
    [TestFixture]
    public class NegativeWordsRepositoryTest
    {
       
        [SetUp]
        public void Init()
        {
            
        }

        [Test]
        public void Add_WithValidInput_ShouldStoreInMemory()
        {
            // Arrange
            var repo = new NegativeWordsRepository();
            var negativeWord = new NegativeWord("shouldStoreInMemory");
            var countPreSave = repo.NegativeWords.Count;
            
            // Act
            var persisted = repo.Add(negativeWord);

            //Assert
            Assert.AreEqual(negativeWord, persisted);
            Assert.AreEqual(repo.NegativeWords.Count, countPreSave + 1);
            
        }

        [Test]
        public void Remove_WithValidInput_ShouldRemoveFromMemory()
        {
            // Arrange
            var repo = new NegativeWordsRepository();
            var negativeWord = new NegativeWord("shouldRemoveFromMemory");
            repo.NegativeWords.Add(negativeWord);
            var countPreSave = repo.NegativeWords.Count;

            // Act
            repo.Remove(negativeWord);

            //Assert
            Assert.AreEqual(repo.NegativeWords.Count, countPreSave - 1);
            Assert.IsNull(repo.NegativeWords.SingleOrDefault(w => w.Equals(negativeWord)));

        }

        [Test]
        public void Get_ShouldReturnFromMemory()
        {
            // Arrange
            var repo = new NegativeWordsRepository();
            repo.NegativeWords = new List<NegativeWord>(){new NegativeWord("shouldReturnFromMemory")};
            
            // Act
            var persisted = repo.GetAll();

            //Assert
            Assert.AreEqual(persisted.Count, repo.NegativeWords.Count);

        }



    }
}
