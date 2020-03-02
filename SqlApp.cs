using Final_MoviesDB.Db;
using Final_MoviesDB.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_MoviesDB
{
    public class SqlApp
    {

        private IRepo<Movie> moviesRepo;

        public void StartMySql()
        {

            Mysql repo = new Mysql();

            if (repo.Connect())
            {
                Console.WriteLine("MySQL connection established successfully.");
                moviesRepo = repo;

                //    RenderMenu();
            }
            else
            {
                Console.WriteLine("Connection to DB failed. Program can't work... Sorry..");

            }
        }

        private void AddTableData()
        {
            bool mandatoryField = false;
            while (mandatoryField == false)
            {
                Console.Write("Insert Title: *");
                string title = Console.ReadLine();
                if (string.IsNullOrEmpty(title))
                {
                    Console.WriteLine("Title can't be empty! Input title once again.");
                }

                else
                {
                    mandatoryField = true;

                    Console.Write("Insert ReleaseDate: ");
                    int releaseDate;
                    while (!int.TryParse(Console.ReadLine(), out releaseDate))
                    {
                        Console.Write("Please enter number. Release date:");
                    }

                    Console.Write("Insert Rating: ");
                    double rating;
                    while (!double.TryParse(Console.ReadLine(), out rating))
                    {
                        Console.Write("Please enter double. Rating:");
                    }

                    Console.Write("Insert Genre: ");
                    string genre = Console.ReadLine();

                    Console.Write("Insert Director: ");
                    string director = Console.ReadLine();

                    Console.Write("Insert Starts: ");
                    string star = Console.ReadLine();

                    moviesRepo.Add(new Movie()
                    {
                        Title = title,
                        ReleaseDate = releaseDate,
                        Rating = rating,
                        Genre = genre,
                        Director = director,
                        Stars = star
                    });

                }

            }
        }

        public void RenderMenu()
        {
            do
            {
                Console.WriteLine();

                Console.WriteLine("1 - Print table.");
                Console.WriteLine("2 - Add data.");
                Console.WriteLine("3 - Update data.");
                Console.WriteLine("4 - Delete data.");
                Console.WriteLine("F - Find data.");
                Console.WriteLine("O - Order data.");
                Console.WriteLine("G - Group data.");
                Console.WriteLine("Q - Quit program.");

                ConsoleKeyInfo keyPress = Console.ReadKey(true);

                Console.Clear();

                switch (keyPress.Key)
                {
                    case ConsoleKey.D1: PrintTable(); break;
                    case ConsoleKey.D2: AddTableData(); break;
                    case ConsoleKey.D3: UpdateTableData(); break;
                    case ConsoleKey.D4: DeleteTableData(); break;
                    case ConsoleKey.F: FindData(); break;
                    case ConsoleKey.O: OrderByData(); break;
                    case ConsoleKey.G: GroupByData(); break;
                    case ConsoleKey.Q: Environment.Exit(0); break;
                }

            } while (true);
        }

        private void UpdateTableData()
        {

            Console.WriteLine("Enter ID number which you want to update: ");
            Console.Write("Movie id:");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Please enter integer number. Movies id:");
            }

            Movie movie = moviesRepo.Get(id);

            if (movie != null)
            {
                Console.Write("New title:*");
                string title = Console.ReadLine();
                Console.Write("New release date:");
                int releaseDate;
                while (!int.TryParse(Console.ReadLine(), out releaseDate))
                {
                    Console.Write("Please enter number. Release date:");
                }
                Console.Write("New Rating:");
                double rating;
                while (!double.TryParse(Console.ReadLine(), out rating))
                {
                    Console.Write("Please enter double. Rating:");
                }
                Console.Write("New genre:");
                string genre = Console.ReadLine();
                Console.Write("New director:");
                string director = Console.ReadLine();
                Console.Write("New star(s):");
                string star = Console.ReadLine();

                moviesRepo.Update(new Movie()
                {
                    ID = id,
                    Title = title,
                    ReleaseDate = releaseDate,
                    Rating = rating,
                    Genre = genre,
                    Director = director,
                    Stars = star
                });
            }
            else
            {
                Console.WriteLine("Movie with this id does not exist.");
            }

        }

        private void PrintTable()
        {
            Console.WriteLine("Current movies in database:");
            List<Movie> movies = moviesRepo.GetAll();
            ForeachMoviesList(movies);
        }


        private void DeleteTableData()
        {
            Console.WriteLine("Enter ID number which you want to delete: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Please enter integer number. Movie id:");
            }

            moviesRepo.Delete(id);
        }

        private void FindData()
        {
            Console.WriteLine("Enter whatever text by what you want to search is database: ");
            string searchText = Console.ReadLine();
            List<Movie> movies = moviesRepo.Find(searchText);
            if (movies.Count > 0)
            {
                Console.WriteLine("Matched results:");
                ForeachMoviesList(movies);
            }
            else
                Console.WriteLine("Nothing found.");
        }

        private void OrderByData()
        {
            Console.WriteLine("Enter column name by what you want to order by from database ");
            Console.WriteLine("Possible input values:");
            Console.Write("Id; Title; ReleaseDate; Rating; Genre; Director; Stars");
            Console.WriteLine("");
            bool correctInput = false;
            while (correctInput == false)
            {
                string searchByColumn = Console.ReadLine();
                if (!searchByColumn.Equals("Id", StringComparison.OrdinalIgnoreCase) && !searchByColumn.Equals("Title", StringComparison.OrdinalIgnoreCase)
                    && !searchByColumn.Equals("ReleaseDate", StringComparison.OrdinalIgnoreCase) && !searchByColumn.Equals("Rating", StringComparison.OrdinalIgnoreCase)
                    && !searchByColumn.Equals("Genre", StringComparison.OrdinalIgnoreCase) && !searchByColumn.Equals("Director", StringComparison.OrdinalIgnoreCase)
                    && !searchByColumn.Equals("Stars", StringComparison.OrdinalIgnoreCase))
                {
                    correctInput = false;
                    Console.WriteLine("Please enter correct column name:");
                }
                else
                {
                    correctInput = true;

                    List<Movie> movies = moviesRepo.OrderBy(searchByColumn);
                    // Console.WriteLine(movies.Count.ToString());
                    if (movies.Count > 0)
                    {
                        Console.WriteLine("Ordered results by " + searchByColumn + " :");
                        ForeachMoviesList(movies);
                    }
                }
            }
        }

        private void GroupByData()
        {
            Console.WriteLine("Enter column name by what you want to group by from database ");
            Console.WriteLine("Possible input values:");
            Console.Write("Id; Title; ReleaseDate; Rating; Genre; Director; Stars");
            Console.WriteLine("");
            bool correctInput = false;
            while (correctInput == false)
            {
                string searchByColumn = Console.ReadLine();
                if (!searchByColumn.Equals("Id", StringComparison.OrdinalIgnoreCase) && !searchByColumn.Equals("Title", StringComparison.OrdinalIgnoreCase)
                    && !searchByColumn.Equals("ReleaseDate", StringComparison.OrdinalIgnoreCase) && !searchByColumn.Equals("Rating", StringComparison.OrdinalIgnoreCase)
                    && !searchByColumn.Equals("Genre", StringComparison.OrdinalIgnoreCase) && !searchByColumn.Equals("Director", StringComparison.OrdinalIgnoreCase)
                    && !searchByColumn.Equals("Stars", StringComparison.OrdinalIgnoreCase))
                {
                    correctInput = false;
                    Console.WriteLine("Please enter correct column name:");
                }
                else
                {
                    correctInput = true;

                    List<Movie> movies = moviesRepo.OrderBy(searchByColumn);
                    //  Console.WriteLine(movies.Count.ToString());
                    if (movies.Count > 0)
                    {
                        Console.WriteLine("Grouped results by " + searchByColumn + " :");
                        ForeachMoviesList(movies);
                    }
                }
            }
        }

        private void ForeachMoviesList(List<Movie> movies)
        {
            if (movies.Count > 100)
            {
                bool correctPress = false;
                bool press = false;
                while (correctPress == false)
                {
                    Console.WriteLine("There are more then 100 movies, would you like to limit them? Press Y - as Yes. If No - press N.");
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Y)
                    {
                        correctPress = true;
                        while (press == false)
                        {
                            Console.WriteLine("To how much do you want to limit a list?");
                            int limit;
                            while (!int.TryParse(Console.ReadLine(), out limit))
                            {
                                Console.Write("Please enter number. Limit of a list:");
                            }
                            press = true;
                            PrintMoviesListWIthLimit(movies, limit);
                        }

                    }
                    else if (keyInfo.Key == ConsoleKey.N)
                    {
                        correctPress = true;
                        PrintMoviesList(movies);
                    }
                }
            }
            else if (movies.Count == 0)
            {
                Console.WriteLine("Nothing to print.");
            }
            else 
            PrintMoviesList(movies);
        }

        private void PrintMoviesList(List<Movie> movies)
        {
            foreach (Movie item in movies)
            {

                Console.WriteLine(String.Format("|{0,3}|{1,40}|{2,3}|{3,4}|{4,10}|", item.ID, item.Title, item.Rating, item.ReleaseDate, item.Genre));
            }
        }

        private void PrintMoviesListWIthLimit(List<Movie> movies, int limit)
        { 
                    for (int i = 0; i<limit; i++)
            {
                Console.WriteLine(String.Format("|{0,3}|{1,40}|{2,3}|{3,4}|{4,10}|", movies[i].ID, movies[i].Title, movies[i].Rating, movies[i].ReleaseDate, movies[i].Genre));
            }
        }   



    }
}

