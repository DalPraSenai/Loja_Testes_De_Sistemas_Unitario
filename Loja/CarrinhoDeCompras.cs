namespace Loja;

public class ItemCarrinho
{
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public int Quantidade { get; set; }

    public decimal Subtotal => Preco * Quantidade;
}

public class CarrinhoDeCompras
{
    private readonly List<ItemCarrinho> _itens = new();

    // Exposto como somente leitura para quem consome a classe
    public IReadOnlyList<ItemCarrinho> Itens => _itens;

    public decimal PercentualDesconto { get; private set; } = 0;

    public int QuantidadeDeItens => _itens.Sum(i => i.Quantidade);

    public void AdicionarItem(string nome, decimal preco, int quantidade)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome do item é obrigatório.", nameof(nome));

        if (preco <= 0)
            throw new ArgumentException("O preço deve ser positivo.", nameof(preco));

        if (quantidade <= 0)
            throw new ArgumentException("A quantidade deve ser positiva.", nameof(quantidade));

        var itemExistente = _itens.FirstOrDefault(i =>
            i.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));

        if (itemExistente is not null)
        {
            itemExistente.Quantidade += quantidade;
            return;
        }

        _itens.Add(new ItemCarrinho
        {
            Nome = nome,
            Preco = preco,
            Quantidade = quantidade
        });
    }

    public void RemoverItem(string nome)
    {
        var item = _itens.FirstOrDefault(i =>
            i.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));

        if (item is null)
            throw new InvalidOperationException($"Item '{nome}' não encontrado no carrinho.");

        _itens.Remove(item);
    }

    public void AtualizarQuantidade(string nome, int novaQuantidade)
    {
        if (novaQuantidade <= 0)
            throw new ArgumentException("A quantidade deve ser positiva.", nameof(novaQuantidade));

        var item = _itens.FirstOrDefault(i =>
            i.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));

        if (item is null)
            throw new InvalidOperationException($"Item '{nome}' não encontrado no carrinho.");

        item.Quantidade = novaQuantidade;
    }

    public void AplicarDesconto(decimal percentual)
    {
        if (percentual < 0 || percentual > 100)
            throw new ArgumentException("O percentual de desconto deve estar entre 0 e 100.", nameof(percentual));

        PercentualDesconto = percentual;
    }

    public decimal CalcularSubtotal()
    {
        return _itens.Sum(i => i.Subtotal);
    }

    public decimal CalcularTotal()
    {
        var subtotal = CalcularSubtotal();
        var desconto = subtotal * (PercentualDesconto / 100);
        return subtotal - desconto;
    }

    public bool EstaVazio() => _itens.Count == 0;

    public void Esvaziar()
    {
        _itens.Clear();
        PercentualDesconto = 0;
    }
}