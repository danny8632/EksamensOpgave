using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Eksamen
{
    /// <summary>
    /// Main program
    /// </summary>
    class Program
    {
        public static Varibles v = new Varibles();
        static void Main(string[] args)
        {
            //Skriver lige lidt om spillet
            Print("Velkommen til den simpel Rulette");
            Print("Reglerne er simple, hvis du rød og sort er X2 og grøn er X14");
            Print("Du starter med 3000kr så brug dem klogt ;) ");
            v.money = 3000;
            Print("");
            Print("Click på en tilfældig knap, for at komme videre");
            Console.ReadKey();
            Console.Clear();

            //starter det første loop, som så er gameloopet
            while(v.gameLoop)
            {
                //Starter det næste loop, for at få input fra brugeren.
                while (v.placebet)
                {
                    PrintLine("Du har: ");
                    Console.Write(v.money);
                    Print("$");
                    Print("");
                    Print("Hvilken farve vil du bette på??(Rød,Sort,Grøn)");
                    //Køre et whileloop for at tjekke om der er gyldigt input fra brugeren
                    while (v.checkUserColor)
                    {
                        //Køre et if statement for at tjekke om brugerens input er Rød/Sort/Grøn.
                        //Hvis ikke vil den sige ugyldigt input og bede om nyt svar.
                        v.userColor = Console.ReadLine().ToLower().Trim();
                        if(v.userColor == "rød")
                        {
                            v.checkUserColor = false;
                        } else if (v.userColor == "sort")
                        {
                            v.checkUserColor = false;
                        } else if (v.userColor == "grøn")
                        {
                            v.checkUserColor = false;
                        } else
                        {
                            Console.Clear();
                            Print("Ugyldigt input");
                        }
                    }

                    //Køre endnu et whileloop for at sikre sig at brugeren ikke intaster minus værdier.
                    while(v.insertMoney)
                    {
                        Print("Hvor meget vil du gerne bette?");
                        v.currentbet = ReadFloat();
                        if(v.currentbet < 1 || v.currentbet > v.money)
                        {
                            Print("Du skal smide et + beløb");
                            Print("Ellers har du ikke penge nok");
                        } else
                        {
                            v.insertMoney = false;
                        }
                    }

                    
                    //Køre funktionen PlaceBet
                    PlaceBet(v.userColor, v.bet, v.currentbet);

                    //Køre if statement for at spørge om du vil bette på flere farver.
                    //Eller ændre en nuværende værdi på en farve
                    Print("Vil du gerne fortage flere bets eller ændre et bet? (ja/nej)");
                    if (Console.ReadLine().ToLower() == "ja")
                    {
                        Console.Clear();
                        v.checkUserColor = true;
                    }
                    else
                    {
                        v.placebet = false;
                        Console.Clear();
                    }
                }
                //Køre funktionen for at få en tilfældig farve(Sort,Rød,Grøn)
                RandomColor(v.color);

                //Køre en funktion der tjekker om du har vundet eller ej.
                CheckWin(v.bet, v.color);

                Console.WriteLine(v.money);

                //Køre et if statement for at se om brugeren vil blive ved med at spille.
                Print("Vil du gerne gamble videre? (ja/nej)");
                if (Console.ReadLine().ToLower() == "ja")
                {
                    v.placebet = true;
                    v.checkUserColor = true;
                    v.insertMoney = true;
                    Nulstil(v.color, v.bet, v.currentbet, v.insertMoney, v.checkUserColor);
                    Console.Clear();
                }
                else
                {
                    v.placebet = false;
                    v.gameLoop = false;
                }
            }
            

        }

        /// <summary>
        /// Printer på den nuværende linje
        /// </summary>
        /// <param name="text">Texten den skal printe</param>
        public static void PrintLine(string text)
        {
            Console.Write(text);
        }

        /// <summary>
        /// Printer og skifter linje
        /// </summary>
        /// <param name="text">Teskten den skal printe</param>
        public static void Print(string text)
        {
            Console.WriteLine(text);
        }

        /// <summary>
        /// Reader int, og chekcer om det er en korrekt int, brugeren har indtastet
        /// </summary>
        /// <returns>Int værdi</returns>
        public static int ReadInt()
        {
            int nummer;
            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out nummer))
                {
                    Print("Det er ikke et tal");
                }
                else
                {
                    break;
                }

            }
            return nummer;

        }

        /// <summary>
        /// Reader float, og checker om det er gyldigt input
        /// </summary>
        /// <returns>Float værdi</returns>
        public static float ReadFloat()
        {
            float nummer;
            while (true)
            {
                if (!float.TryParse(Console.ReadLine(), out nummer))
                {
                    Print("Det er ikke et tal");
                }
                else
                {
                    break;
                }

            }
            return nummer;

        }

        /// <summary>
        /// Genere den randomme farve.
        /// </summary>
        /// <param name="color">Skal bruge et bool array til at gemme farven</param>
        public static void RandomColor(bool[] color)
        {
            Random rnd = new Random();
            int rndNumber = rnd.Next(1, 18);
            if(rndNumber <= 8)
            {
                color[0] = true;
                Print("Farven er: Rød");
            } else if (rndNumber >= 10)
            {
                color[1] = true;
                Print("Farven er: Sort");
            } else
            {
                color[2] = true;
                Print("Farven er: Grøn");
            }
        }

        /// <summary>
        /// Checker om spilleren har vundet, ved at bruge den randomme color og Arrayet med bets i.
        /// </summary>
        /// <param name="bet">Float array, som indeholder bets</param>
        /// <param name="color">Bool array, som inderholde den randomme farve</param>
        public static void CheckWin(float[] bet, bool[] color)
        {
            for(int i = 0; i < 2; i++)
            {
                if(bet[i] > 0 && color[i] == true)
                {
                    v.money = v.money + bet[i];
                    Print("Du vandt");
                } else if (bet[i] > 0 && color[i] == false)
                {
                    v.money = v.money - bet[i];
                    Print("Du tabte");
                }
            }
            if(bet[2] > 0 && color[2] == true)
            {
                bet[2] = bet[2] * 14;
                v.money = v.money + bet[2];
            }
        }
        
        /// <summary>
        /// Sætter dit bet på den valgte farve
        /// </summary>
        /// <param name="color">Farve, som skal bettes på</param>
        /// <param name="bet">Skal bruge et array med 3 pladser til at gemme bettet i</param>
        /// <param name="amount">Hvor meget brugeren ønsker at bette på den såkaldte farve</param>
        public static void PlaceBet(string color, float[] bet, float amount)
        {
            //Print("Hvor maget vil du gerne bette på den farve");
            if(color == "rød")
            {
                bet[0] = amount;
                //v.money = v.money - amount;
            } else if (color == "sort")
            {
                bet[1] = amount;
                //v.money = v.money - amount;
            } else if (color == "grøn")
            {
                bet[2] = amount;
                //v.money = v.money - amount;
            } else
            {
                Print("Ugyldig farve");
            }
        }

        /// <summary>
        /// Nulstiller brugerens verdier, sådan at han kan spille igen
        /// </summary>
        /// <param name="color">Bool array, hvor alle værdier bliver sat til false</param>
        /// <param name="bet">Float array, hvor alle værdier bliver sat til 0</param>
        /// <param name="currentbet">Sætter current bet til 0</param>
        /// <param name="userinput">Sætter user input til true</param>
        /// <param name="insertmoney">Sætter inserMoney til true</param>
        public static void Nulstil(bool[] color, float[] bet, float currentbet, bool userinput, bool insertmoney)
        {
            for(int i = 0; i < 2; i++)
            {
                color[i] = false;
            }
            for(int i = 0; i < 2; i++)
            {
                bet[i] = 0;
            }
            currentbet = 0;
            userinput = true;
            insertmoney = true;
        }

    }

    /// <summary>
    /// Class hvor alle mine variabler bliver gemt i.
    /// </summary>
    class Varibles
    {
        public float money;
        public bool[] color = new bool[3];
        public string userColor;
        public float[] bet = new float[3];
        public float currentbet;
        public bool placebet = true;
        public bool gameLoop = true;
        public bool checkUserColor = true;
        public bool insertMoney = true;
    }
}
