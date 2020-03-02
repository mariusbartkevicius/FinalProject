using Final_MoviesDB.Movies;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace Final_MoviesDB
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Marius movies database. Have fun to find something usefull!" + Environment.NewLine);

            Controler.MainMenu();
        }
    }
}
