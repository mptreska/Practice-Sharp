using System;

abstract class Movie
{
    public string Title { get; set; }
    public string Genre { get; set; }
    public int Duration { get; set; }

    public Movie(string title, string genre, int duration)
    {
        Title = title;
        Genre = genre;
        Duration = duration;
    }

    public override string ToString()
    {
        return $"Название: {Title}, Жанр: {Genre}, Длительность: {Duration} мин.";
    }
}

sealed class ActionMovie : Movie
{
    public ActionMovie(string title, int duration) : base(title, "Боевик", duration) { }
}

sealed class ComedyMovie : Movie
{
    public ComedyMovie(string title, int duration) : base(title, "Комедия", duration) { }
}

class Cinema
{
    public Movie[] Movies { get; set; }

    public Cinema(Movie[] movies)
    {
        Movies = movies;
    }

    public Movie GetLongestMovie()
    {
        Movie longest = Movies[0];
        for (int i = 1; i < Movies.Length; i++)
            if (Movies[i].Duration > longest.Duration)
                longest = Movies[i];
        return longest;
    }

    public Movie[] GetMoviesByGenre(string genre)
    {
        int count = 0;
        for (int i = 0; i < Movies.Length; i++)
            if (Movies[i].Genre == genre)
                count++;

        Movie[] result = new Movie[count];
        int index = 0;
        for (int i = 0; i < Movies.Length; i++)
            if (Movies[i].Genre == genre)
                result[index++] = Movies[i];

        return result;
    }
}

class Program
{
    static void Main()
    {
        Cinema cinema = new Cinema(new Movie[]
        {
            new ActionMovie("Терминатор", 107),
            new ComedyMovie("Маска", 101),
            new ActionMovie("Матрица", 136),
            new ComedyMovie("Один дома", 103),
            new ActionMovie("Джон Уик", 101)
        });

        Console.WriteLine("Все фильмы:");
        for (int i = 0; i < cinema.Movies.Length; i++)
            Console.WriteLine(cinema.Movies[i]);

        Console.WriteLine("\nСамый длинный фильм:");
        Console.WriteLine(cinema.GetLongestMovie());

        Console.WriteLine("\nБоевики:");
        Movie[] action = cinema.GetMoviesByGenre("Боевик");
        for (int i = 0; i < action.Length; i++)
            Console.WriteLine(action[i]);

        Console.WriteLine("\nКомедии:");
        Movie[] comedy = cinema.GetMoviesByGenre("Комедия");
        for (int i = 0; i < comedy.Length; i++)
            Console.WriteLine(comedy[i]);
    }
}