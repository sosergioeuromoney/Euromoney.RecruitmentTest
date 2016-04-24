using ContentConsole.Application.Data;
using ContentConsole.Application.Domain;
using ContentConsole.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContentConsole
{
    public static class Program
    {
        private static INegativeWordsRepository _negativeWordRepository; 
        private static INegativeWordsService _negativeWordService;
        
        public static void Main(string[] args)
        {
            //Dependency Injection
            _negativeWordRepository = new NegativeWordsRepository();
            _negativeWordService = new NegativeWordsService(_negativeWordRepository);

            //Seed data
            _negativeWordRepository.Add(new NegativeWord("nasty"));
            _negativeWordRepository.Add(new NegativeWord("bad"));
            _negativeWordRepository.Add(new NegativeWord("horrible"));
            _negativeWordRepository.Add(new NegativeWord("swine"));

        
            var input = string.Empty;
            do
            {
                Console.Clear();
                Console.WriteLine("Press the corresponding key and return");
                Console.WriteLine();
                Console.WriteLine("1 - Count Negative Words In A Text");
                Console.WriteLine("2 - List Negative Words");
                Console.WriteLine("3 - Add Negative Word");
                Console.WriteLine("4 - Remove Negative Word");
                Console.WriteLine("5 - Filter On");
                Console.WriteLine("6 - Filter Off");
                Console.WriteLine("X - Exit");
                Console.WriteLine();
                input = Console.ReadLine() ?? "";
                switch (input)
                {
                    case "1":
                        {
                            Console.Clear();
                            CountNegativeWords();
                            break;
                        }
                    case "2":
                        {
                            Console.Clear();
                            ListNegativeWords();
                            break;
                        }
                    case "3":
                        {
                            Console.Clear();
                            AddNegativeWords();
                            break;
                        }
                    case "4":
                        {
                            Console.Clear();
                            RemoveNegativeWords();
                            break;
                        }
                    case "5":
                        {
                            Console.Clear();
                            FilterOn();
                            break;
                        }
                    case "6":
                        {
                            Console.Clear();
                            FilterOff();
                            break;
                        }
                    default: {
                        Console.WriteLine("Input not recognized. Please try again.");
                        break;
                    }
                }


            } while (input.ToLower() != "x");

        }

        public static void CountNegativeWords() {
            Console.WriteLine("Enter the text to check for negative words:");
            string content = Console.ReadLine();
            var badWords = _negativeWordService.ScanText(content);
            Console.Clear();
            Console.WriteLine("Scanned the text:");
            Console.WriteLine(content);
            Console.WriteLine("Total Number of negative words: " + badWords.Count());
            Console.WriteLine("Press ANY key to exit.");
            Console.ReadKey();
            
        }

        private static void ListNegativeWords()
        {
            var negativeWords = _negativeWordService.GetAll();
            Console.WriteLine("");
            Console.WriteLine("Current list of negative words:");
            foreach (var w in negativeWords)
            {
                Console.WriteLine(w);
            }
            Console.WriteLine("Press ANY key to exit.");
            Console.ReadKey();
        }


        public static void AddNegativeWords()
        {
            Console.WriteLine("Enter the word to add in the negative ones:");
            string content = Console.ReadLine();
            _negativeWordService.Add(content);
            ListNegativeWords();
        }

        public static void RemoveNegativeWords()
        {
            Console.WriteLine("Enter the word to remove from the negative ones:");
            string content = Console.ReadLine();
            _negativeWordService.Remove(content);
            ListNegativeWords();
        }

        
        public static void FilterOn()
        {
            Console.WriteLine("Enter content to be filtered out for negative words:");
            string content = Console.ReadLine();
            var obscured = _negativeWordService.ObscureText(content);
            Console.WriteLine("Text filtered out is:");
            Console.WriteLine(obscured);
            Console.WriteLine("Press ANY key to exit.");
            Console.ReadKey();
        }

        public static void FilterOff()
        {
            Console.WriteLine("Enter content to be checked for bad words:");
            string content = Console.ReadLine();
            var badWords = _negativeWordService.ScanText(content);
            Console.WriteLine("Scanned the text:");
            Console.WriteLine(content);
            Console.WriteLine("Total Number of negative words: " + badWords.Count());

            Console.WriteLine("Press ANY key to exit.");
            Console.ReadKey();
        }

      
    }

}
