namespace Bankomaten
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // The five customers and their PIN-codes are initiated in a twodimensional string array.
            string[,] customers = new string[5, 2]
             {
                 { "LinOl", "3533" },
                 { "JonOl", "2834" },
                 { "AnnTu", "9763" },
                 { "HanTu", "1095" },
                 { "HenOl", "0702" },
             };

            /* The accounts and balances connected to the five customers are initiated in a jagged
             * array with five rows, each consisting of a twodimensional string array.*/
            string[][,] accounts = new string[5][,]
            {
                new string[,] { { "1. Lönekonto: ", "28000,00" }, { "2. Sparkonto: ", "34560,00" } },
                new string[,] { { "1. Lönekonto: ", "543670,00" }, { "2. Sparkonto ", "133400,00" }, 
                                { "3. Extrakonto: ", "345000,00" } },
                new string[,] { { "1. Lönekonto: ", "300,00" } },
                new string[,] { { "1. Lönekonto: ", "67500,00" }, { "2. Sparkonto: ", "320,00" }, 
                                { "3. Extrakonto: ", "45000,00"}, {"4. Räkningskonto: ", "50000,00" } },
                new string[,] { { "1. Sparkonto: "}, { "3570,00" } },
            };
            // A bool and an outer while loop to keep the program running.
            bool running = true;
            while (running)
            {
                Console.Clear();
                // The customer logs in.
                int index = LogIn(customers);
                /* If the user fails to type in a valid user-ID or correct PIN-code LogIn() returns -1 
                 * and the outer while loop breaks. Program shuts down.*/
                if (index == -1)
                {
                    running = false;
                    return;
                }

                // Customer is logged in. A bool and an inner while loop to keep the menu running. 
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
                    /* A switch to present the menu. 1-3 calls different methods. 4 breaks the
                     * inner while loop and the customer will have to log in again.*/
                    switch (menuChoice)
                    {
                        case "1":
                            SeeAccount(index, accounts);
                            Console.Write("\nTryck på valfri tangent för att fortsätta.");
                            Console.ReadKey();
                            break;
                        case "2":
                            Transfer(index, accounts);
                            Console.Write("\nTryck på valfri tangent för att fortsätta.");
                            Console.ReadKey();
                            break;
                        case "3":
                            Withdrawal(index, customers, accounts);
                            Console.Write("\nTryck på valfri tangent för att fortsätta.");
                            Console.ReadKey();
                            break;
                        case "4":
                            menu = false;
                            break;
                        default:
                            Console.Write("\nDet menyvalet finns inte. " +
                                "Tryck valfri tangent för att fortsätta.");
                            Console.ReadKey();
                            break;
                    }
                }
            }
        }

        /* ---------------------*** LogIn method ***-------------------------
         * A method for logging in a specific user. The metod returns an index, 
         * as a signature for the customer, to be used in other methods. The user 
         * gets three tries on typing in an existing user-ID and three tries on 
         * typing in the correct PIN-code connected to the user-ID. Failure results 
         * in returning index -1. */
        static int LogIn(string[,] customers)
        {
            Console.WriteLine("\t*** Välkommen till Campusbanken ***");
            Console.Write("\nAnge ditt användar-ID för att påbörja inloggning: ");
            string userID = Console.ReadLine();
            int counter1 = 0;

            while (counter1 < 2)
            {
                for (int i = 0; i < customers.GetLength(0); i++)
                {
                    for (int j = 0; j < customers.GetLength(1); j++)
                    {
                        if (userID == customers[i, j] && j == 0)
                        {
                            Console.Write("\nSkriv in din PIN-kod: ");
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
                                return -1;
                            }
                            else
                            {
                                return i;
                            }
                        }
                    }
                }
                Console.Write("Okänt ID. Försök igen: ");
                counter1++;
                userID = Console.ReadLine();
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nFör många försök. Banken stänger ned.");
            Console.ResetColor();
            Console.ReadKey();
            return -1;
        }

        /* ---------------------*** SeeAccount method ***---------------------
         * This metod presents the accounts connected to the logged in customer.*/
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

        /* ---------------------*** Transfer method ***---------------------
         * A method to make a transfer between accounts. The user types the indexes
         * of the to- and from- accounts and the amount of money to transfer. If the 
         * account not exists or if the balance gets negative the user gets a message
         * that the action is not allowed.*/
        static void Transfer(int index, string[][,] accounts)
        {
            int outAcc;
            int inAcc;
            double moneyOut;

            SeeAccount(index, accounts);
            Console.Write("\nVilket konto vill du föra över från? Ange siffra: ");
            while (!int.TryParse(Console.ReadLine(), out outAcc) || outAcc <= 0 
                || outAcc > accounts[index].GetLength(0))
            {
                Console.Write("Fel inmatning. Ange siffra: ");
            }
            Console.Write("\nVilket konto vill du föra över till? Ange siffra: ");
            while (!int.TryParse(Console.ReadLine(), out inAcc) || inAcc <= 0 
                || inAcc > accounts[index].GetLength(0))
            {
                Console.Write("Fel inmatning. Ange siffra: ");
            }
            if (inAcc == outAcc)
            {
                Console.Write("\nFrån- och tillkonto kan inte vara samma. Ärendet avslutas.\n");
                return;
            }
            Console.Write("\nAnge summa att föra över: ");
            while (!double.TryParse(Console.ReadLine(), out moneyOut) 
                || moneyOut > Convert.ToDouble(accounts[index][outAcc - 1, 1]))
            {
                Console.Write("Fel inmatning. Ange summa: ");
            }
            /* Converting the elements in the string array to double. Performing the 
             * transactions. Converting the elements back to string and adding a format.*/ 
            double balanceOutAcc = Convert.ToDouble(accounts[index][outAcc - 1, 1]) - moneyOut;
            accounts[index][outAcc - 1, 1] = String.Format("{0:0.00}", balanceOutAcc);
            double balanceInAcc = Convert.ToDouble(accounts[index][inAcc - 1, 1]) + moneyOut;
            accounts[index][inAcc - 1, 1] = String.Format("{0:0.00}", balanceInAcc);

            Console.WriteLine($"\nNya saldon:\n\n{accounts[index][outAcc - 1, 0]}" +
                $"{accounts[index][outAcc - 1, 1]}\n{accounts[index][inAcc - 1, 0]}" +
                $"{accounts[index][inAcc - 1, 1]}");
        }

        /* ---------------------*** Withdrawal method ***---------------------
         * A metod to make a withdrawal from a specific account. If the account 
         * not exists or if the balance gets negative the user gets a message 
         * that the action is not allowed. The customer have to confirm the with-
         * drawal with PIN-code. Failure to confirm results in returning to menu.*/
        static void Withdrawal(int index, string[,] customers, string[][,] accounts)
        {
            int accIndex = 1;
            double moneyOut;
            
            SeeAccount(index, accounts);
            // If the user has more than one accounts the account needs to be specified.
            if (accounts[index].GetLength(0) != 1)
            {
                Console.Write("\nVilket konto vill du göra uttag från? Ange siffra: ");
                while (!int.TryParse(Console.ReadLine(), out accIndex) || accIndex <= 0
                    || accIndex > accounts[index].GetLength(0))
                {
                    Console.Write("Fel inmatning. Ange siffra: ");
                }
            }

            Console.Write("\nAnge summa att ta ut: ");
            while (!double.TryParse(Console.ReadLine(), out moneyOut) 
                || moneyOut > Convert.ToDouble(accounts[index][accIndex-1, 1]))
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
                /* Converting the element in the string array to double. Performing the 
                * transaction. Converting the element back to string and adding a format.*/
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
                    return;
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
    }
}
