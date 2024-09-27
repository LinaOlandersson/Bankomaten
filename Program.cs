namespace Bankomaten
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                int index = LogIn();
                
                bool menu = true;
                while (menu)
                {
                    Console.Clear();
                    Console.WriteLine("1. Se dina konton och saldon");
                    Console.WriteLine("2. Överföring mellan konton");
                    Console.WriteLine("3. Ta ut pengar");
                    Console.WriteLine("4. Logga ut");

                    Console.Write("\nVälj något från menyn: ");
                    string menuChoice = Console.ReadLine();
                    switch (menuChoice)
                    {
                        case "1":
                            //Kalla på metod
                            break;
                        case "2":
                            //Kalla på metod
                            break;
                        case "3":
                            //Kalla på metod
                            break;
                        case "4":
                            menu = false;
                            break;
                        default:
                            Console.WriteLine("Det menyvalet finns inte. " +
                                "Tryck valfri tangent för att fortsätta.");
                            Console.ReadKey();
                            break;
                    }
                }
            }
            
            
        }

        // A LogIn method
        static int LogIn()
        {
            string[,] customers = new string[5, 3]
             {
                 { "0", "LinOl", "3533" },
                 { "1", "JonOl", "2834" },
                 { "2", "AnnTu", "9763" },
                 { "3", "HanTu", "1095" },
                 { "4", "HenOl", "0702" },
             };

            Console.WriteLine("\t*** Välkommen till Campusbanken ***");
            Console.Write("\nAnge ditt användar-ID för att påbörja inloggning: ");
            string userID = Console.ReadLine();
            int counter1 = 0;
            
            while (counter1 < 3)
            {
                for (int i = 0; i < customers.GetLength(0); i++)
                {
                    for (int j = 0; j < customers.GetLength(1); j++)
                    {
                        if (userID == customers[i, j] && j==1)
                        {
                            Console.Write("Skriv in din PIN-kod: ");
                            string userPin = Console.ReadLine();
                            int counter2 = 0;
                            while (userPin != customers[i, j + 1] && counter2 < 2)
                            {
                                Console.Write("Fel kod. Försök igen: ");
                                userPin = Console.ReadLine();
                                counter2++;
                            }
                            if (counter2 >= 2)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nFör många försök. Banken stänger ned.");
                                Console.ResetColor();
                                return 0;
                            }
                            else
                            {
                                return j - 1;
                            }
                        }
                    }
                }
                Console.Write("\nOkänt ID. Försök igen: ");
                userID = Console.ReadLine();
                counter1++;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nFör många försök. Banken stänger ned.");
            Console.ResetColor();
            return 0;
        }
    }      
}
