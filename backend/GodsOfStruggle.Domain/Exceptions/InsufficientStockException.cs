namespace GodsOfStruggle.Domain.Exceptions;

public class InsufficientStockException(string sku, int requested, int available)
    : DomainException($"Estoque insuficiente para SKU '{sku}': solicitado {requested}, disponível {available}.");
