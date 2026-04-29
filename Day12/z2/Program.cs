using System;

interface IPost
{
    string GetContent();
}

class BasicPost : IPost
{
    private string content;
    public BasicPost(string content) { this.content = content; }
    public string GetContent() => content;
}

abstract class PostDecorator : IPost
{
    protected IPost post;
    public PostDecorator(IPost post) { this.post = post; }
    public abstract string GetContent();
}

class LikeDecorator : PostDecorator
{
    private int likes;
    public LikeDecorator(IPost post, int likes) : base(post) { this.likes = likes; }
    public override string GetContent() => $"{post.GetContent()} | 👍 {likes} лайков";
}

class DislikeDecorator : PostDecorator
{
    private int dislikes;
    public DislikeDecorator(IPost post, int dislikes) : base(post) { this.dislikes = dislikes; }
    public override string GetContent() => $"{post.GetContent()} | 👎 {dislikes} дизлайков";
}

class ShareDecorator : PostDecorator
{
    private int shares;
    public ShareDecorator(IPost post, int shares) : base(post) { this.shares = shares; }
    public override string GetContent() => $"{post.GetContent()} | 🔁 {shares} репостов";
}

class Program
{
    static void Main()
    {
        Console.Write("Введите текст поста: ");
        string text = Console.ReadLine();

        Console.Write("Количество лайков: ");
        int likes = int.Parse(Console.ReadLine());

        Console.Write("Количество дизлайков: ");
        int dislikes = int.Parse(Console.ReadLine());

        Console.Write("Количество репостов: ");
        int shares = int.Parse(Console.ReadLine());

        IPost post = new BasicPost(text);
        Console.WriteLine($"\nБазовый пост: {post.GetContent()}");

        post = new LikeDecorator(post, likes);
        Console.WriteLine($"После лайков: {post.GetContent()}");

        post = new DislikeDecorator(post, dislikes);
        Console.WriteLine($"После дизлайков: {post.GetContent()}");

        post = new ShareDecorator(post, shares);
        Console.WriteLine($"После репостов: {post.GetContent()}");
    }
}