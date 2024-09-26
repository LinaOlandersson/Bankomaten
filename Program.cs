namespace Bankomaten
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int index = LogIn();
            Console.WriteLine(index);
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

            for (int i = 0; i < customers.GetLength(0); i++)
            {
                for (int j = 0; j < customers.GetLength(1); j++)
                {
                    if (userID == customers[i, j])
                    {
                        Console.Write("Skriv in din PIN-kod: ");
                        string userPin = Console.ReadLine();
                        int counter = 1;
                        while (userPin != customers[i, j + 1] && counter < 3)
                        {
                            Console.Write("Fel kod. Försök igen: ");
                            userPin = Console.ReadLine();
                            counter++;
                        }
                        if (counter >= 3)
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
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nOkänt ID. Banken stänger ner.");
            Console.ResetColor();
            return 0;

 


            

        }
    }
        
        
}
