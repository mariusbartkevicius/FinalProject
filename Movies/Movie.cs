using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Final_MoviesDB.Movies
{
   public class Movie
    {

        public int ID { get; set; }
        public string Title { get; set; }
        public int ReleaseDate { get; set; }
        public double Rating { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Stars { get; set; }

        /*
        public Movie(int ID, string Title, int ReleaseDate, double Rating, string Genre, string Director, string Stars)
        {
            this.ID = ID;
            this.Title = Title;
            this.ReleaseDate = ReleaseDate;
            this.Rating = Rating;
            this.Genre = Genre;
            this.Director = Director;
            this.Stars = Stars;
        }
        */
    }
}
