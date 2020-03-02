using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace Final_MoviesDB.Movies
{
 class Controler 
    {

        public static void MainMenu()
        {
            bool correctPress = false;
            while (correctPress == false)
            {
                Console.WriteLine("Press Enter to enter. Press Q to Quit.");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    correctPress = true;
                    Start();

                }
                else if (keyInfo.Key == ConsoleKey.Q)
                {
                    correctPress = true;
                    Quit();
                }
            }
        }

        /*
        public static void SelectionMenu(List<Movie> list)
        {
            SqlApp app = new SqlApp();
            app.StartMySql();
            bool correctPress = false;

            bool press = false;
            while (correctPress == false)
            {
                Console.WriteLine("Do you want read about one of this more? Then press Y. If Quit - press Q. Modify current list - press M");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Y)
                {

                    correctPress = true;
                    EnterIDNUmber(list, "Details");
                }
                

                else if (keyInfo.Key == ConsoleKey.M)
                {
                    correctPress = true;
                    while (press == false)
                    {
                        Console.WriteLine("Do you want to add new (press A), delete (press D) or edit one of present (press E). Or Quit - press Q.");
                        ConsoleKeyInfo keyInfo2 = Console.ReadKey(true);
                        if (keyInfo2.Key == ConsoleKey.A)
                        {
                            press = true;
                         //   app.AddTableData();

                        }
                        else if (keyInfo2.Key == ConsoleKey.E)
                        {
                            press = true;
                        //    app.UpdateTableData();
                          //  EnterIDNUmber(list, "Edit");
                        }
                        else if (keyInfo2.Key == ConsoleKey.D)
                        {
                            press = true;
                        //    app.DeleteTableData();
                      //      EnterIDNUmber(list, "Delete");
                            
                        }
                        else if (keyInfo2.Key == ConsoleKey.Q)
                        {
                            press = true;
                            Quit();
                        }
                    }                
                }
                else if (keyInfo.Key == ConsoleKey.Q)
                {
                    correctPress = true;
                    Quit();
                }
            }
        }
        */

        public static void Start()
        {
            SqlApp app = new SqlApp();
            app.StartMySql();
            app.RenderMenu();
        }


        public static int Quit()
        {
            return 0;
        }

        /*
        public static void EnterIDNUmber(List<Movie> list, string task)
        {
            bool isDigit = false;
            while (isDigit == false)
            {
                Console.WriteLine("Enter ID number.");
                String skaicius = Console.ReadLine();
                int value;
                if (int.TryParse(skaicius, out value))
                {
                    isDigit = true;
                    foreach (Movie item in list)
                    {

                    }
                    if (task == "Delete")
                    { 
                    MoviesActions.DeleteSpecificMovie(list, value);
                    } 
                    else if (task == "Details")
                    { 
                    MoviesActions.ShowMovieDetails(list, value);
                    }
                    else if (task == "Edit")
                    {
                        MoviesActions.EditMovie(list, value);
                    }
                }
                else
                {
                    Console.WriteLine("isn't number!");
                }
            }
        }
        */
    }
}
