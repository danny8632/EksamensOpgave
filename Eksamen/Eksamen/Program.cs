using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Eksamen
{
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

        //Bruger funktioner til at printe
        public static void PrintLine(string text)
        {
            Console.Write(text);
        }

        public static void Print(string text)
        {
            Console.WriteLine(text);
        }

        //Bruger funktioner til at tjekke om det er int eller float
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
        //------------------------------------------------------------------//

        //Finder farven ved at generere et tilfældigt tal, og så derefter igennem if statements
        //siger at 8 og derunder er rød, 10 og derover er sort og 9 er grøn
        //Bare for at gøre grøn lidt mere "sjælen"
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

        //Tjeker for at se om den farve brugeren har indsat en værdi er vinderen
        //Igennen en håndfuld ifstatements og et lille forloop
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
        //Indsætter værdien brugeren har sat på den såkaldte farve, ved at gemme det i et array
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

        //nulstiller alle værdier bortset for brugerens currentcy.
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

    //class for alle mine variabler
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
