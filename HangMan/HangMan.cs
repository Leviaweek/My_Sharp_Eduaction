using System;
using System.Collections;
using System.Collections.Generic;
namespace Hangman

{
    public static class Program
    {
        private static readonly string[] words = new string[] {"python", "sharp", "javascript", "typescript"};
        private static string gameChoosedWord = "";
        private static string[] closedWord = Array.Empty<string>();

        private static bool ClosedWordChecker()
        {
            for(var i = 0; i < closedWord.Length; i++)
            {
                var letter = gameChoosedWord[i].ToString();
                if(closedWord[i] != letter)
                    return false;
            }
            return true;
        }

        private static void PrintingClosedWord()
        {
            Console.WriteLine(string.Join("", closedWord));
        }
        
        private static void GuessTheLetter(string userLetter)
        {
            for(var i = 0; i < closedWord.Length; i++)
            {
                var letter = gameChoosedWord[i].ToString();
                if(userLetter == letter)
                    closedWord[i] = userLetter;
            }
        }
        
        private static string[] CreatingClosedWord()
        {
            var cldWrd = new string[gameChoosedWord.Length];
            for(var i = 0; i < gameChoosedWord.Length; i++)
                    cldWrd[i] = "-";
            return cldWrd;
        }
        
        private static string RandomWord()
        {
            var rnd = new Random();
            var value = rnd.Next(0, words.Length);
            var choosedWord = words[value];
            return choosedWord;
        }
        
        private static string UserChoosingLetter()
        {
            while(true)
            {
                Console.Write(">>> ");
                var userLetter = Console.ReadLine()!;
                if(userLetter.Length == 1 && userLetter[0] >= 'a' && userLetter[0] <= 'z')
                    return userLetter;
                else
                    Console.WriteLine("Incorrect input");
            }
        }
        
        private static void GameLoop()
        {
            var usedLetters = new List<string>();
            var mistakes = 0;
            gameChoosedWord = RandomWord();
            closedWord = CreatingClosedWord();
            

            while(true)
            {
                Console.WriteLine($"You have {mistakes} mistakes");
                Console.Write("Your word: ");
                PrintingClosedWord();

                var userLetter = UserChoosingLetter();

                if(usedLetters.Contains(userLetter))
                    {
                        Console.WriteLine("You use this letter");
                        continue;
                    }
                
                usedLetters.Add(userLetter);
                
                if(!gameChoosedWord.Contains(userLetter))
                {
                    Console.WriteLine("You don't guess");
                    mistakes += 1;

                    if(mistakes == 8)
                    {
                        Console.WriteLine($"You have {mistakes} mistakes and lose");
                        Console.WriteLine($"You don't guess word \"{gameChoosedWord}\"");
                        break;
                    }

                    continue;
                }
                
                GuessTheLetter(userLetter);
                
                var winChecker = ClosedWordChecker();

                if(winChecker)
                {
                    Console.WriteLine($"You win! Word is \"{gameChoosedWord}\"");
                    break;
                }
            }
        }

        public static void Main()
        {
            Console.WriteLine("\t\t HANGMAN \t\t\n");

            Console.WriteLine("Hello, it's hangman game");

            while(true)
            {
                Console.Write("Do you want to start?(yes or no)\n>>> ");
                var userInput = Console.ReadLine()!;
                
                if(userInput.ToLower() == "yes")
                    GameLoop();

                else if(userInput.ToLower() == "no")
                    break;

                else
                    Console.WriteLine("Unknown command :(");
            }

            Console.WriteLine("Goodbye");
        }
    }
}