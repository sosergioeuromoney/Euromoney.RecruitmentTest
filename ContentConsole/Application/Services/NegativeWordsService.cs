using ContentConsole.Application.Data;
using ContentConsole.Application.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContentConsole.Application.Services
{
    public class NegativeWordsService : INegativeWordsService
    {
        INegativeWordsRepository _negativeWordsRepository;
        
        public NegativeWordsService(INegativeWordsRepository negativeWordRepository)
        {
            _negativeWordsRepository = negativeWordRepository;
        }


        public string ObscureText(string input) 
        {
            var obfuscator = '#';
            var result = ScanText(input);
            StringBuilder sb = new StringBuilder(input);
            foreach (var s in result) { 
                for(int i=s.Start+1; i<s.End;i++){
                    sb[i] = obfuscator;
                }
            }
            return sb.ToString();
        }

        public IEnumerable<NegativeWordScan> ScanText(string input) 
        {
            var separator = ' ';
            var fixedInput = Regex.Replace(input, "[^a-zA-Z0-9 ]", separator.ToString());
            var negativeWords = _negativeWordsRepository.GetAll();
            foreach (var w in negativeWords)
            {
                var startIx = fixedInput.IndexOf(w.Value);
                if (startIx > -1)
                {
                    var endIx = startIx + w.Value.Length;
                    if ((endIx == input.Length || fixedInput[endIx] == separator) &&
                       (startIx == 0 || fixedInput[startIx - 1] == separator))
                    {
                        yield return new NegativeWordScan() 
                        {
                            Word = w.Value,
                            Start = startIx,
                            End = endIx-1
                        };
                    }
                }
            }
        }


        public void Add(string input)
        {
            if (string.IsNullOrEmpty(input)) throw new ArgumentException();
            
            var word = new NegativeWord(input.ToLowerInvariant());

            //no duplicates
            var exists = _negativeWordsRepository.Exists(word);
            if (!exists)
                _negativeWordsRepository.Add(word);
        }


        public IEnumerable<string> GetAll()
        {
            return _negativeWordsRepository.GetAll().Select(w => w.Value);
        }

        public void Remove(string input)
        {
           _negativeWordsRepository.Remove(new NegativeWord(input)); 
        }


    }
}
