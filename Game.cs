using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oparski
{
    internal class Game
    {
        int howManyRolls = 0, value, current = 0, points = 0, numberInResults = 0, pointsInResults = 0;
        int[] rolls = new int[3], frame = new int[10], frameOfRolls = new int[23], results = new int[20];
        bool BonusRoll = false;

        static void Main(string[] args)
        {
            Game game = new Game();
            game.BowlingGame();
        }

        public void BowlingGame() 
        {
            while (howManyRolls < 10)
            {
                do
                {
                    Console.WriteLine("Podaj ilosc zbitych kregli w pierwszym rzucie: ");
                } while (!int.TryParse(Console.ReadLine(), out value) || value < 0 || value > 10);
                rolls[0] = value;
                if (value == 10)
                {
                    if (howManyRolls != 9)
                    {
                        Roll(rolls[0], 0);
                        GetScore();
                    }
                    howManyRolls += 1;
                }

                if (value != 10 || howManyRolls == 10)
                {
                    if (howManyRolls == 10 && rolls[0] == 10)
                    {
                        do
                        {
                            Console.WriteLine("Podaj ilosc zbitych kregli w drugim rzucie: ");
                        } while (!int.TryParse(Console.ReadLine(), out value) || value < 0 || value > 10);
                    }
                    else
                    {
                        do
                        {
                            Console.WriteLine("Podaj ilosc zbitych kregli w drugim rzucie: ");
                        } while (!int.TryParse(Console.ReadLine(), out value) || value < 0 || value > 10 || value + rolls[0] > 10);
                    }
                    rolls[1] = value;
                    if (howManyRolls == 10) howManyRolls--;
                    Roll(rolls[0], rolls[1]);
                    GetScore();
                    howManyRolls += 1;
                }
            }
            if(BonusRoll == true)
            {
                if (rolls[1] < 10)
                {
                    do
                    {
                        Console.WriteLine("Podaj ilosc zbitych kregli w bonusowym rzucie: ");
                    } while (!int.TryParse(Console.ReadLine(), out value) || value < 0 || value > 10 - rolls[1]);
                }
                else
                {
                    do
                    {
                        Console.WriteLine("Podaj ilosc zbitych kregli w bonusowym rzucie: ");
                    } while (!int.TryParse(Console.ReadLine(), out value) || value < 0 || value > 10);
                }
               
                Console.Clear();
                frame[9] = points + value + frameOfRolls[19] + frameOfRolls[18];
                Console.WriteLine("Aktualny wynik: " + frame[9]);
            }
            PrintFrame();
        }

        public void Roll(int first, int second)
        {
            frameOfRolls[current] = first;
            frameOfRolls[current +1] = second;
            current+=2;
            if(howManyRolls == 9 && first + second >= 10)
            {
                BonusRoll = true;
            }
        } 
        public void GetScore()
        {
            points = 0;
            for(int i = 0; i < 21; i+=2) 
            {
                if (frameOfRolls[i] + frameOfRolls[i+1] == 10 && frameOfRolls[i] != 10 && frameOfRolls[i+2] != 0) //spare
                {
                    points += frameOfRolls[i] + frameOfRolls[i + 1] + frameOfRolls[i + 2];
                    results[i / 2] = frameOfRolls[i] + frameOfRolls[i + 1] + frameOfRolls[i + 2] + pointsInResults;
                }
                else if (frameOfRolls[i] == 10) //strike
                {
                    if (frameOfRolls[i + 2] == 10 && frameOfRolls[i + 4]!=0)
                    {
                        points += frameOfRolls[i] + frameOfRolls[i + 2] + frameOfRolls[i + 4];
                        results[i/2] = frameOfRolls[i] + frameOfRolls[i + 2] + frameOfRolls[i + 4] + pointsInResults;
                    }
                    else if(frameOfRolls[i + 2]!=0 && frameOfRolls[i + 3] != 0)
                    {
                        points += frameOfRolls[i] + frameOfRolls[i + 2] + frameOfRolls[i + 3];
                        results[i / 2] = frameOfRolls[i] + frameOfRolls[i + 2] + frameOfRolls[i + 3] + pointsInResults;
                    }
                }
                else if (frameOfRolls[i] != 10 && frameOfRolls[i] + frameOfRolls[i + 1] != 10 && frameOfRolls[i] != 0) //klasyczny przyapdek
                {
                    points += frameOfRolls[i] + frameOfRolls[i + 1];
                    results[i / 2] = frameOfRolls[i] + frameOfRolls[i + 1] + pointsInResults;
                }
            }
            Console.Clear();
            Console.WriteLine("Aktualny wynik: " + points);
            frame[howManyRolls] = points;
        }

        public void PrintFrame()
        {
            for(int i = 0; i <9; i++)
            {
                if (i != 0)
                {
                    Console.WriteLine("Tura " + (i + 1) + ": ");
                    results[i] = results[i] + results[i - 1];
                    Console.WriteLine(results[i]);
                }
                else
                {
                    Console.WriteLine("Tura " + (i + 1) + ": ");
                    Console.WriteLine(results[i]);
                }
            }
            Console.WriteLine("Ostatnia tura: ");
            Console.WriteLine(frame[9]);
        }
    }
}
