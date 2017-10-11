using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Hangman
{
    class Game
    {
        private int guesses;
        private List<string> bank;
        private List<char> letters;
        private int length;

        public Game(int length)
        {
            guesses = 0;
            bank = new List<string>();
            letters = new List<char>();
            this.length = length;
        }

        public bool Play()
        {
            CreateBank();
            while (bank.Count != 1)
            {
                char key = GuessLetter();
                int[] indicies = AskUserLetter(key);
                RemoveWords(key, indicies);
                if (guesses == 6 || bank.Count == 0)
                {
                    return false;
                }
            }
            //ask user if we won
            bool win = AskUserWord();
            if (win)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //can be removed or replaced by Unity input
        bool AskUserWord()
        {
            string word = bank.ToArray()[0];

            Console.WriteLine("Is the word {0}?", word);
            string answer = Console.ReadLine();
            return answer.ToLower().Contains("y");

        }

        //can be removed or replaced by Unity input
        int[] AskUserLetter(char key)
        {
            Console.WriteLine("Is there a {0}?: ", key);
            string answer = Console.ReadLine();
            List<int> indicies = new List<int>();
            if (answer.ToLower().Contains("y"))
            {
                int index = 0;
                while (index >= 0)
                {
                    Console.WriteLine("Enter indexes where the number exists. -1 to stop: ");
                    index = Convert.ToInt32(Console.ReadLine());
                    if (index != -1)
                    {
                        indicies.Add(index);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return indicies.ToArray();
        }

        char GuessLetter()
        {
            int[] frequency = new int[26];
            for (int i = 0; i < frequency.Length; i++)
            {
                char x = (char)(96 + i);
                foreach (string word in bank)
                {
                    
                    if (word.Contains(x.ToString())) 
                    {
                        frequency[i]++;
                    }
                }
            }
            char guess = FindMax(frequency);
            letters.Add(guess);
            return guess;
        }

        char FindMax(int[] a)
        {
            int maxIndex = 0;
            int max = 0;
            for (int i = 0; i < a.Length; i++) {
                if (a[i] > max && !letters.Contains((char)(i+96)))
                {
                    max = a[i];
                    maxIndex = i;
                }
            }

            return (char)(maxIndex + 96);
        }

        void RemoveWords(char key, int[] indicies)
        {
            string[] bankCopy = bank.ToArray();
            if (indicies.Length == 0)
            {
                for (int i = 0; i < bankCopy.Length; i++)
                {
                    if (bankCopy[i].Contains(key.ToString()))
                    {
                        bank.Remove(bankCopy[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < bankCopy.Length; i++)
                {
                    foreach (int index in indicies)
                    {
                        if (bankCopy[i].ToCharArray()[index] != key)
                        {
                            bank.Remove(bankCopy[i]);
                        }
                    }
                }
            }
        }

        void CreateBank()
        {
            string[] words = GetWordsFromFile();

            foreach (string word in words)
            {
                if (word.Length == length)
                {
                    bank.Add(word);
                }
            }
        }

        string[] GetWordsFromFile()
        {
            string filePath = @"C:\Users\mpaba\Desktop\wordBank.txt";

            return File.ReadAllLines(filePath, Encoding.UTF8);
        }
    }

    class TestDriver
    {
        static void Main()
        {
            int length;
            Console.WriteLine("Enter a number between 3 and 20 [except 19]: ");
            length = Convert.ToInt32(Console.ReadLine());
            Game game = new Game(length);
            bool result = game.Play();
            if (result)
            {
                Console.WriteLine("I win!");
            }
            else
            {
                Console.WriteLine("I lose :(");
            }
        }
    }
}