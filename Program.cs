namespace Bankomaten
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[,] customers = new string[5, 2]
             {
                 { "LinOl", "3533" },
                 { "JonOl", "2834" },
                 { "AnnTu", "9763" },
                 { "HanTu", "1095" },
                 { "HenOl", "0702" },
             };

            string[][,] accounts = new string[4][,]
            {
                new string[,] { { "Lönekonto: ", "28000.00" }, { "Sparkonto: ", "34560.00" } },
                new string[,] { { "Lönekonto: ", "543670.00" }, { "Sparkonto ", "133400.00" }, { "Extrakonto: ", "345000.00" } },
                new string[,] { { "Lönekonto: ", "300.00" } },
                new string[,] { { "Lönekonto: ", "67500.00" }, { "Sparkonto: ", "320.00" }, { "Extrakonto: ", "45000.00"}, {"Räkningskonto: ", "50000.00" } },
            };

            bool running = true;
            while (running)
            {
                Console.Clear();
                int index = LogIn(customers);

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
                            SeeAccount(index, accounts);
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


        // A metod to view the customers accounts
        static void SeeAccount(int index, string[][,] accounts)
        {
            Console.WriteLine("\nDu har följande konto(n) och saldo(n):\n");

            for (int i = 0; i < accounts[index].GetLength(0); i++)
            {
                for (int j = 0; j < accounts[index].GetLength(1); j++)
                {
                    Console.Write(accounts[index][i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("\nTryck på valfri tangent för att fortsätta.");
            Console.ReadKey();
        }
        
        // A LogIn method
        static int LogIn(string[,] customers)
        {
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
                        if (userID == customers[i, j] && j == 0)
                        {
                            Console.Write("Skriv in din PIN-kod: ");
                            string userPin = Console.ReadLine();
                            int counter2 = 0;
                            while (userPin != customers[i, 1] && counter2 < 2)
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
                                return i;
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
