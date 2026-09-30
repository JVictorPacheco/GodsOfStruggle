namespace GodsOfStruggle.Domain.ValueObjects;

public sealed record Money(decimal Amount, string Currency = "BRL")
{
    public static Money Zero => new(0);

    public Money Add(Money other) => this with { Amount = Amount + other.Amount };
    public Money Subtract(Money other) => this with { Amount = Amount - other.Amount };
    public Money Multiply(int quantity) => this with { Amount = Amount * quantity };

    public static Money operator +(Money left, Money right) => left.Add(right);
    public static Money operator -(Money left, Money right) => left.Subtract(right);
    public static Money operator *(Money money, int quantity) => money.Multiply(quantity);

    public override string ToString() => $"R$ {Amount:N2}";
}
