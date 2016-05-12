using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "";
            do
            {
                Console.WriteLine("Query Movies Database:\nEnter 1 for Movies and their Details.\nEnter 2 for Actors and their Movies.\nEnter 3 for Movie Schedules\nEnter E to quit.");
                input = Console.ReadLine();
                if (input == "1")
                    MoviesAndRatings();
                else if (input == "2")
                    ActorsAndMovies();
                else if (input == "3")
                    MovieSchedules();
                else if (input.ToLower() == "e")
                    return;
                else
                    Console.WriteLine("That is not an option. Please type in one of the selections");

                Console.WriteLine("To Continue Press Any Key...");
                Console.ReadKey();
                Console.Clear();
            }
            while(input.ToLower() != "e");
        }

        private static void MoviesAndRatings()
        {
            Console.WriteLine();
            using (MoviesEntities ctx = new MoviesEntities())
            {
                var movies = from m in ctx.Movies join g in ctx.Genres on m.GenreID equals g.GenreID select new {Rating = m.Rating, Title = m.Title, mGenre = g.TypeOfGenre, Length = m.LengthInMinutes, Desc = m.Description, Release = m.ReleaseDate, Dir = m.Director};

                foreach (var m in movies)
                {
                    Console.WriteLine("{0}", m.Title);
                    Console.WriteLine(new String('-', 30));
                    Console.WriteLine("Relased:{0} Director:{1} Genre:{2} Rating:{3} Length:{4} min", m.Release, m.Dir, m.mGenre, m.Rating, m.Length);
                    Console.WriteLine();
                    Console.WriteLine("Enter 1 to read a description or any key to continue.");
                    string input = Console.ReadLine();
                    if (input == "1")
                        Console.WriteLine("{0}", m.Desc);
                    Console.WriteLine();
                    Console.WriteLine("Enter 0 to return to Menu or press enter to see next movie.");
                    input = Console.ReadLine();
                    if (input == "0")
                        return;
                    //Console.ReadKey();
                }
            }
        }

        private static void ActorsAndMovies()
        {
            Console.WriteLine();
            using (MoviesEntities ctx = new MoviesEntities())
            {
                var actors = from a in ctx.Actors select a;
                foreach (var a in actors)
                {
                    Console.WriteLine("{0} {1}", a.FirstName, a.LastName);                 
                    Console.WriteLine(new String('-', 30));
                    foreach(var m in a.Filmographies)
                    {
                        Console.WriteLine("{0}", m.Movie.Title);
                    }
                    Console.WriteLine();
                }
            }
        }

        private static void MovieSchedules()
        {
            Console.WriteLine();
            using (MoviesEntities ctx = new MoviesEntities())
            {
                var sched = from s in ctx.MovieSchedules join m in ctx.Movies on s.MovieID equals m.MovieID join t in ctx.Theaters on s.TheaterID equals t.TheaterID orderby t.NameOfTheater, s.StartDateAndTime descending select new { TheaterName = t.NameOfTheater, Time = s.StartDateAndTime, Title = m.Title };

                foreach (var s in sched)
                {
                    Console.WriteLine("{0} {1}\n{2}", s.TheaterName, s.Time, s.Title);
                    Console.WriteLine();
                }
            }
        }
    }
}
