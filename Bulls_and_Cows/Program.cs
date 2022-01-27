using System;

namespace Bulls_and_Cows
{
    class Program
    {

        static void Main(string[] args)
        {

            bool play = true;
            while (play)
            {
                PrintTitle();

                int numToGuess = 0;

                switch (Start())
                {
                    case 1:

                        numToGuess = Randomize();
                        break;

                    case 2:

                        numToGuess = InputNumber();
                        break;
                }


                string ans = "";

                while (ans != "4A0B")
                {
                    ans = Process(numToGuess);
                }

                play = Finish();
            }

        }

        public static void PrintTitle()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;;

            Console.WriteLine("Bulls & Cows");

            Console.ResetColor();
            Console.WriteLine();
        }

        public static int Start()
        {
            int option = 0;

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Two options to start, please choose one of them.");
            Console.WriteLine("================================================");
            Console.WriteLine("Option 1: press '1' and the number will be chosen randomly.");
            Console.WriteLine("Option 2: press '2' and choose the number yourself.");
            Console.WriteLine();

            bool optionIsValid = false;

            while (!optionIsValid)
            {
                optionIsValid = true;
                Console.Write("Option: ");

                try
                {
                    option = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    optionIsValid = false;
                }

                if (!(option == 1 || option == 2))
                {
                    optionIsValid = false;
                }

                if (!optionIsValid)
                {
                    Console.WriteLine("Please choose one of the options.");
                    Console.WriteLine();
                }

            }
            
            Console.Clear();

            PrintTitle();

            return option;
        }

        public static int InputNumber()
        {

            int num = 0;
            bool numberIsValid = false;
            int failed = 0;

            while (!numberIsValid)
            {
                numberIsValid = true;

                Console.ForegroundColor = ConsoleColor.Yellow;

                if (failed == 0)
                {
                    Console.Write("Write the 4 digit positive number to guess: ");
                    failed++;
                }
                else
                {
                    Console.Write("Please, try again: ");
                }

                string input = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.Red;

                try
                {
                    num = Convert.ToInt32(input);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Entered number is invalid.");
                    numberIsValid = false;
                }

                if (num < 0 && numberIsValid)
                {
                    Console.WriteLine("The number should be positive.");
                    numberIsValid = false;
                }

                if (input.Length != 4 && numberIsValid)
                {
                    Console.WriteLine("The number should be 4 digit.");
                    numberIsValid = false;
                }

                if (input[0] == '0')
                {
                    Console.WriteLine("The number can't start with 0.");
                    numberIsValid = false;
                }
            }

            Console.Clear();
            PrintTitle();

            return num;
        }

        public static int Randomize()
        {
            Random r = new Random();
            int rnd = r.Next(1000, 9999);

            return rnd;
        }

        public static string Process(int numToGuess)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            string strNum = Convert.ToString(numToGuess);
            string strGuess = "";

            bool guessIsValid = false;

            while (!guessIsValid)
            {
                guessIsValid = true;

                Console.Write("Guess: ");
                strGuess = Console.ReadLine();

                try
                {
                    int numGuess = Convert.ToInt32(strGuess);
                }
                catch (FormatException)
                {
                    guessIsValid = false;
                }

                if (strGuess.Length != 4)
                {
                    guessIsValid = false;
                }

                if (strGuess[0] == '0' || strGuess[0] == '-' || strGuess[0] == '+')
                {
                    guessIsValid = false;
                }

                if (!guessIsValid)
                {
                    Console.WriteLine("Unable to check with that format.");
                    Console.WriteLine("Please try positive 4 digit number.");
                }
            }


            string newStrGuess = "";
            string newStrNum = "";

            string output;
            int bulls = 0;
            int cows = 0;


            ////Timer stuff
            //int miliStart = DateTime.Now.Millisecond;
            //int secsStart = DateTime.Now.Second;


            for (int i = 0; i < 4; i++)
            {
                if (strGuess[i] == strNum[i])
                {
                    bulls++;
                }
                else
                {
                    newStrGuess += strGuess[i];
                    newStrNum += strNum[i];
                }
            }

            //Console.WriteLine(newStrNum);
            //Console.WriteLine(newStrGuess);

            char[] numArr = newStrNum.ToCharArray();
            char[] guessArr = newStrGuess.ToCharArray();
            Array.Sort(numArr);
            Array.Sort(guessArr);

            int length = numArr.Length;
            int arrLength = length * 10;

            char[] finalNumArr = new char[arrLength];
            char[] finalGuessArr = new char[arrLength];

            for (int i = 0; i < 10; i++)
            {
                int ind1 = 0;
                int ind2 = 0;
                char iChar = Convert.ToChar(i + 48);

                for (int j = 0; j < length; j++)
                {
                    if (numArr[j] == iChar)
                    {
                        finalNumArr[i * length + ind1] = iChar;
                        ind1++;
                    }

                    if (guessArr[j] == iChar)
                    {
                        finalGuessArr[i * length + ind2] = iChar;
                        ind2++;
                    }
                }
            }

            for (int i = 0; i < arrLength; i++)
            {
                if (finalNumArr[i] == finalGuessArr[i] && finalGuessArr[i] != '\0')
                {
                    cows++;
                }
            }

            output = bulls + "A" + cows + "B";

            Console.Write("Result: ");
            Console.WriteLine(output);

            ////Timer stuff
            //int miliStop = DateTime.Now.Millisecond;
            //int secsStop = DateTime.Now.Second;
            //Console.WriteLine($"{secsStop - secsStart}:{miliStop - miliStart}");

            Console.WriteLine();


            return output;
        }

        public static bool Finish()
        {
            bool playAgain = false;

            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine("You Win");
            Console.WriteLine("Choose '0' to play again or any other key to exit.");
            Console.Write("Hit: ");

            char again = Convert.ToChar(Console.ReadLine());

            if (again == '0')
            {
                playAgain = true;
                Console.Clear();
            }

            Console.ResetColor();

            return playAgain;
        }
    }
}