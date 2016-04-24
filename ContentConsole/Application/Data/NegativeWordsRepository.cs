using ContentConsole.Application.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentConsole.Application.Data
{
    public class NegativeWordsRepository: INegativeWordsRepository
    {
        private static IList<NegativeWord> _negativeWords;
        public IList<NegativeWord> NegativeWords
        {
            get
            {
                if (_negativeWords == null)
                {
                    _negativeWords = new List<NegativeWord>();
                }
                return _negativeWords;

            }
            set
            {
                _negativeWords = value;
            }
        }

        public IList<NegativeWord> GetAll()
        {
            return NegativeWords;
        }

        public NegativeWord Add(NegativeWord word)
        {
            NegativeWords.Add(word);
            return word;  
        }

        public void Remove(NegativeWord word)
        {
            if (Exists(word))
            {
                NegativeWords.Remove(word);
            }
        }


        public bool Exists(NegativeWord word)
        {
            var found = NegativeWords.SingleOrDefault(w => w.Equals(word));
            return found != null;
        }
    }
}
