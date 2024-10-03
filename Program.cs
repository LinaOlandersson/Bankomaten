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

            string[][,] accounts = new string[5][,]
            {
                new string[,] { { "1. Lönekonto: ", "28000,00" }, { "2. Sparkonto: ", "34560,00" } },
                new string[,] { { "1. Lönekonto: ", "543670,00" }, { "2. Sparkonto ", "133400,00" }, { "3. Extrakonto: ", "345000,00" } },
                new string[,] { { "1. Lönekonto: ", "300,00" } },
                new string[,] { { "1. Lönekonto: ", "67500,00" }, { "2. Sparkonto: ", "320,00" }, { "3. Extrakonto: ", "45000,00"}, {"4. Räkningskonto: ", "50000,00" } },
                new string[,] { { "1. Sparkonto: "}, { "3570,00" } },
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
                            Console.WriteLine("\nTryck på valfri tangent för att fortsätta.");
                            Console.ReadKey();
                            break;
                        case "2":
                            //Kalla på metod
                            break;
                        case "3":
                            Withdrawal(index, customers, accounts);
                            Console.WriteLine("\nTryck på valfri tangent för att fortsätta.");
                            Console.ReadKey();
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
        // A method to make a withdrawal
        static void Withdrawal(int index, string[,] customers, string[][,] accounts)
        {
            int accIndex;
            double moneyOut;
            
            SeeAccount(index, accounts);
            Console.Write("\nVilket konto vill du göra uttag från? Ange siffra: ");
            while (!int.TryParse(Console.ReadLine(), out accIndex)) // Om inte kontot finns?
            {
                Console.Write("Fel inmatning. Ange siffra: ");
            }

            Console.Write("\nAnge summa att ta ut: ");
            while (!double.TryParse(Console.ReadLine(), out moneyOut) || moneyOut > Convert.ToDouble(accounts[index][accIndex-1, 1]))
            {
                Console.Write("Fel inmatning. Ange summa: ");
            }
            
            Console.Write("\nSkriv in din PIN-kod för att bekräfta eller 'A' för att avbryta: ");
            string userPin = Console.ReadLine();
            double newBalance;
            if (userPin.ToUpper() == "A")
            {
                return;
            }
            else if (userPin == customers[index, 1])
            {
                newBalance = Convert.ToDouble(accounts[index][accIndex - 1, 1]) - moneyOut;
                accounts[index][accIndex - 1, 1] = String.Format("{0:0.00}", newBalance);
                Console.WriteLine($"\nNytt saldo: {accounts[index][accIndex - 1, 1]}");
            }

            int counter = 0;
            while (userPin != customers[index, 1] && counter < 2)
            {
                Console.Write("Fel kod. Försök igen eller 'A' för att avbryta: ");
                userPin = Console.ReadLine();
                counter++;
                if (userPin.ToUpper() == "A")
                {
                    return; ;
                }
                
                else if (counter >= 2)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nFör många försök. Ärendet avslutas.");
                    Console.ResetColor();
                    return;
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
