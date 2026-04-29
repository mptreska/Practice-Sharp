using System;

interface ISubscription
{
    string GetBenefits();
}

class FreeSubscription : ISubscription
{
    public string GetBenefits() => "Бесплатная: базовый доступ, реклама";
}

class PremiumSubscription : ISubscription
{
    public string GetBenefits() => "Премиум: без рекламы, HD качество";
}

class VIPSubscription : ISubscription
{
    public string GetBenefits() => "VIP: всё включено + приоритетная поддержка";
}

abstract class SubscriptionFactory
{
    public abstract ISubscription Create();
}

class FreeFactory : SubscriptionFactory
{
    public override ISubscription Create() => new FreeSubscription();
}

class PremiumFactory : SubscriptionFactory
{
    public override ISubscription Create() => new PremiumSubscription();
}

class VIPFactory : SubscriptionFactory
{
    public override ISubscription Create() => new VIPSubscription();
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Выберите подписку (1-Бесплатная, 2-Премиум, 3-VIP): ");
        string choice = Console.ReadLine();

        SubscriptionFactory factory = choice switch
        {
            "2" => new PremiumFactory(),
            "3" => new VIPFactory(),
            _ => new FreeFactory()
        };

        ISubscription sub = factory.Create();
        Console.WriteLine(sub.GetBenefits());

        Console.WriteLine("\n--- Все подписки ---");
        Console.WriteLine(new FreeFactory().Create().GetBenefits());
        Console.WriteLine(new PremiumFactory().Create().GetBenefits());
        Console.WriteLine(new VIPFactory().Create().GetBenefits());
    }
}