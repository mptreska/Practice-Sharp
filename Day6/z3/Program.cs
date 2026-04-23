using System;

abstract class ArtPiece
{
    public string Title { get; set; }
    public string Author { get; set; }

    public ArtPiece(string title, string author)
    {
        Title = title;
        Author = author;
    }

    public override string ToString()
    {
        return $"Название: {Title}, Автор: {Author}";
    }
}

interface IPainting
{
    string GetPaintingStyle();
}

interface ISculpture
{
    string GetMaterial();
}

class Portrait : ArtPiece, IPainting
{
    private string style;

    public Portrait(string title, string author, string style) : base(title, author)
    {
        this.style = style;
    }

    public string GetPaintingStyle() { return style; }

    public override string ToString()
    {
        return $"Картина | {base.ToString()}, Стиль: {GetPaintingStyle()}";
    }
}

class Statue : ArtPiece, ISculpture
{
    private string material;

    public Statue(string title, string author, string material) : base(title, author)
    {
        this.material = material;
    }

    public string GetMaterial() { return material; }

    public override string ToString()
    {
        return $"Скульптура | {base.ToString()}, Материал: {GetMaterial()}";
    }
}

class Program
{
    static void Main()
    {
        ArtPiece[] artPieces = new ArtPiece[]
        {
            new Portrait("Мона Лиза", "Леонардо да Винчи", "Ренессанс"),
            new Statue("Давид", "Микеланджело", "Мрамор"),
            new Portrait("Звёздная ночь", "Ван Гог", "Постимпрессионизм"),
            new Statue("Мыслитель", "Роден", "Бронза"),
            new Portrait("Девочка с персиками", "Серов", "Реализм")
        };

        Console.WriteLine("Все произведения:");
        for (int i = 0; i < artPieces.Length; i++)
            Console.WriteLine(artPieces[i]);

        Console.WriteLine("\nВсе скульптуры:");
        for (int i = 0; i < artPieces.Length; i++)
            if (artPieces[i] is ISculpture)
                Console.WriteLine(artPieces[i]);

        Console.WriteLine("\nВсе картины:");
        for (int i = 0; i < artPieces.Length; i++)
            if (artPieces[i] is IPainting)
                Console.WriteLine(artPieces[i]);
    }
}