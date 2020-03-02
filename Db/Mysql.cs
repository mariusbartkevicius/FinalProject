using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Final_MoviesDB.Movies;

namespace Final_MoviesDB.Db
{
    class Mysql : SqlConnect, IRepo<Movie>
    {

        private MySqlConnection connection;
        private MySqlCommand cmd;
        private MySqlDataReader dr;

        public override bool Connect()
        {
                return OpenConnection(
                host: "remotemysql.com",
                dbName: "f1SBwFWgE7",
                dbUser: "f1SBwFWgE7",
                dbPassword: "zqbWNQ9JBc",
                port: 3306
            );
        }

        private bool OpenConnection(string host, string dbName, string dbUser, string dbPassword, int port)
        {
           
            string connstring = $"server={host}; database={dbName}; port={port}; user={dbUser}; password={dbPassword};";
            connection = new MySqlConnection(connstring);

            return true;
        }

        /*
        private void InitTable()
        {
            string sql = @"CREATE TABLE `Movies` ( 
                            `Id` INT NOT NULL AUTO_INCREMENT UNIQUE, 
                            `Title` TEXT NOT NULL , 
                            `ReleaseDate` INT NULL , 
                            `Rating` DOUBLE NULL ,
                            `Genre` TEXT NOT NULL ,
                            `Director` TEXT NOT NULL ,
                            `Stars` TEXT NOT NULL ,

                            PRIMARY KEY (`Id`)
                        )";

            connection.Open();
            using (cmd = new MySqlCommand(sql, connection))
            {
                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }

        
        private bool ConnectionEstablished()
        {

            try
            {
                connection.Open();
                //     command.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
               // Console.WriteLine("there was an issue!");
                return false;
            }
        }
        */

        public void Add(Movie item)
        {
           using (cmd = new MySqlCommand(@"INSERT INTO Movies (`Title`, `ReleaseDate`, `Rating`, `Genre`, `Director`, `Stars`) 
                                                           VALUES(@title, @releasedate, @rating, @genre, @director, @stars);", connection))
            {
                connection.Open();

                cmd.Parameters.Add(new MySqlParameter("title", item.Title));
                cmd.Parameters.Add(new MySqlParameter("releasedate", item.ReleaseDate));
                cmd.Parameters.Add(new MySqlParameter("rating", item.Rating));
                cmd.Parameters.Add(new MySqlParameter("genre", item.Genre));
                cmd.Parameters.Add(new MySqlParameter("director", item.Director));
                cmd.Parameters.Add(new MySqlParameter("stars", item.Stars));
                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void Update(Movie item)
        {
            using (cmd = new MySqlCommand(@"UPDATE Movies 
                                               SET Title = @title, ReleaseDate = @releasedate, Rating = @rating, Genre = @genre, Director = @director, Stars = @stars  
                                             WHERE Id=@id", connection))
            {
                connection.Open();

                cmd.Parameters.Add(new MySqlParameter("id", item.ID));
                cmd.Parameters.Add(new MySqlParameter("title", item.Title));
                cmd.Parameters.Add(new MySqlParameter("releasedate", item.ReleaseDate));
                cmd.Parameters.Add(new MySqlParameter("rating", item.Rating));
                cmd.Parameters.Add(new MySqlParameter("genre", item.Genre));
                cmd.Parameters.Add(new MySqlParameter("director", item.Director));
                cmd.Parameters.Add(new MySqlParameter("stars", item.Stars));
                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void Delete(int id)
        {
            using (cmd = new MySqlCommand(@"DELETE FROM Movies WHERE ID = @id;", connection))
            {
                connection.Open();

                cmd.Parameters.Add(new MySqlParameter("id", id));
                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        public List<Movie> GetAll()
        {
            List<Movie> retVal = new List<Movie>();

            using (cmd = new MySqlCommand("SELECT * FROM Movies", connection))
            {
                connection.Open();
                using (dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        retVal.Add(new Movie()
                        {
                            ID = dr.GetInt32(0),
                            Title = dr.GetString(1),
                            ReleaseDate = dr.GetInt32(2),
                            Rating = dr.GetDouble(3),
                            Genre = dr.GetString(4),
                            Director = dr.GetString(5),
                            Stars = dr.GetString(6),
                        });
                    }
                }
                connection.Close();
            }

            return retVal;
        }

        public Movie Get(int id)
        {
            Movie retVal = null;
            using (cmd = new MySqlCommand(@"SELECT * FROM Movies WHERE Id = @id", connection))
            {
                connection.Open();
                cmd.Parameters.Add(new MySqlParameter("id", id));
                using (dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        dr.Read();
                        retVal = new Movie()
                        {
                            ID = dr.GetInt32(0),
                            Title = dr.GetString(1),
                            ReleaseDate = dr.GetInt32(2),
                            Rating = dr.GetDouble(3),
                            Genre = dr.GetString(4),
                            Director = dr.GetString(5),
                            Stars = dr.GetString(6),
                        };
                    }
                }
                connection.Close();
            }
            return retVal;
        }

        public List<Movie> Find(string findText)
        {
            List <Movie> searchRes = new List<Movie>();
            using (cmd = new MySqlCommand(@"SELECT * FROM Movies WHERE Title LIKE @findText OR Stars LIKE @findText OR Genre LIKE @findText OR Director LIKE @findText", connection))
            {
                connection.Open();
                cmd.Parameters.Add(new MySqlParameter ("findText", "%" + findText + "%"));

                using (dr = cmd.ExecuteReader())
                {
                    while (dr.Read()) 
                    {
                        searchRes.Add(new Movie()
                        {
                            ID = dr.GetInt32(0),
                            Title = dr.GetString(1),
                            ReleaseDate = dr.GetInt32(2),
                            Rating = dr.GetDouble(3),
                            Genre = dr.GetString(4),
                            Director = dr.GetString(5),
                            Stars = dr.GetString(6),
                        });
                    } 
                }
                connection.Close();
            }

            return searchRes;
        }


        public List<Movie> OrderBy(string orderBy)
        {
            List<Movie> searchRes = new List<Movie>();
            using (cmd = new MySqlCommand("SELECT * FROM Movies order by " + orderBy + " asc", connection)) 
            {
                connection.Open();
               // cmd.Parameters.Add(new MySqlParameter("@orderby", orderBy));  //nes neorderina pagal add parameter, todel i sql tiesiai paduodam teksta

                using (dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        searchRes.Add(new Movie()
                        {
                            ID = dr.GetInt32(0),
                            Title = dr.GetString(1),
                            ReleaseDate = dr.GetInt32(2),
                            Rating = dr.GetDouble(3),
                            Genre = dr.GetString(4),
                            Director = dr.GetString(5),
                            Stars = dr.GetString(6),
                        });
                    }
                  
                }
                connection.Close();
            }

            return searchRes;
        }

        public List<Movie> GroupBy(string groupBy)
        {
            List<Movie> searchRes = new List<Movie>();
            using (cmd = new MySqlCommand(@"SELECT @groupby, Count(*) FROM Movies group by @groupby ORDER BY Count(*) desc", connection)) //TODO kazkaip kreivai veikia...
            {
                connection.Open();
                cmd.Parameters.Add(new MySqlParameter("groupby", groupBy));

                using (dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        searchRes.Add(new Movie()
                        {
                            ID = dr.GetInt32(0),
                            Title = dr.GetString(1),
                            ReleaseDate = dr.GetInt32(2),
                            Rating = dr.GetDouble(3),
                            Genre = dr.GetString(4),
                            Director = dr.GetString(5),
                            Stars = dr.GetString(6),
                        });
                    }
                    
                }
            }
                connection.Close();
            

            return searchRes;
        }



    }
}
