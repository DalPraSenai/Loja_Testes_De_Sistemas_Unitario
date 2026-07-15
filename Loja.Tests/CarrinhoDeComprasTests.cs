using Loja;
using Xunit;

namespace Loja.Tests;

public class CarrinhoDeComprasTests
{
    // Teste 1
    [Fact(DisplayName = "Adicionar item novo")]
    public void AdicionarItem_CalcularSubTotal_AdicionarItemNovo_CalcularSubtotalCorretamente()
    {
        // Arrange
        CarrinhoDeCompras carrinhoDeCompras = new CarrinhoDeCompras();

        // Act
        carrinhoDeCompras.AdicionarItem("Chaveiro", 12, 1);

        // Assert
        Assert.Equal(12, carrinhoDeCompras.CalcularSubtotal());
    }

    // Teste 2
    [Fact(DisplayName = "Adicionar o mesmo item duas vezes")]
    public void AdicionarItem_CalcularSubTotal_AdicionarMesmoItemDuasVezes_CalcularSubtotalCorretamente()
    {
        // Arrange
        CarrinhoDeCompras carrinhoDeCompras = new CarrinhoDeCompras();

        // Act
        carrinhoDeCompras.AdicionarItem("pantufa", 300, 2);
        carrinhoDeCompras.AdicionarItem("pantufa", 300, 1);

        // Assert
        Assert.Single(carrinhoDeCompras.Itens);
        Assert.Equal(3, carrinhoDeCompras.Itens[0].Quantidade);
        Assert.Equal(900, carrinhoDeCompras.CalcularSubtotal());
    }

    // Teste 3
    [Fact(DisplayName = "Adicionar item com preço inválido")]
    public void AdicionarItem_PrecoInvalido_lancaArgumento()
    {
        // Arrange
        CarrinhoDeCompras carrinhoDeCompras = new CarrinhoDeCompras();

        // Act e Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            carrinhoDeCompras.AdicionarItem("Caneta", -30, 3));
        Assert.Equal("O preço deve ser positivo. (Parameter 'preco')", exception.Message);
    }

    // Teste 4
    [Fact(DisplayName = "Adicionar item com quantidade inválida")]
    public void AdicionarItem_QuantidadeInvalida_lancaArgumento()
    {
        // Arrange
        CarrinhoDeCompras carrinhoDeCompras = new CarrinhoDeCompras();

        // Act e Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            carrinhoDeCompras.AdicionarItem("Mochila", 90, -1));
        Assert.Equal("A quantidade deve ser positiva. (Parameter 'quantidade')", exception.Message);
    }

    // Teste 5
    [Fact(DisplayName = "Remover um item existente")]
    public void AdicionarItem_RemoverItem_CalcularSubTotal_RemoverItemExistente_ItemRemovido()
    {
        // Arrange
        CarrinhoDeCompras carrinhoDeCompras = new CarrinhoDeCompras();
        carrinhoDeCompras.AdicionarItem("Mochila", 50, 2);
        carrinhoDeCompras.AdicionarItem("caderno", 30, 3);

        // Act
        carrinhoDeCompras.RemoverItem("Mochila");

        // Assert
        Assert.Equal(90, carrinhoDeCompras.CalcularSubtotal());
        Assert.Single(carrinhoDeCompras.Itens);
    }

    // Teste 6
    [Fact(DisplayName = "Remover um item cujo nome não está no carrinho")]
    public void AdicionarItem_RemoverItem_LancaArgumentException()
    {
        // Arrange
        CarrinhoDeCompras carrinhoDeCompras = new CarrinhoDeCompras();
        string nome = "Esparadrapo";

        // Act e Assert
        InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
            carrinhoDeCompras.RemoverItem(nome));
        Assert.Equal($"Item '{nome}' não encontrado no carrinho.", exception.Message);
    }

    // Teste 7
    [Fact(DisplayName = "Atualizar a quantidade de um item existente")]
    public void AdicionarItem_AtualizarQuantidade_ItemExistente_QuantidadeAtualizada()
    {
        // Arrange
        CarrinhoDeCompras carrinhoDeCompras = new CarrinhoDeCompras();
        carrinhoDeCompras.AdicionarItem("Headset", 150, 1);

        // Act
        carrinhoDeCompras.AtualizarQuantidade("Headset", 5);

        // Assert
        Assert.Equal(5, carrinhoDeCompras.Itens[0].Quantidade);
        Assert.Equal(5, carrinhoDeCompras.QuantidadeDeItens);
    }

    // Teste 8
    [Fact(DisplayName = "Aplicar um desconto válido")]
    public void AdicionarItem_AplicarDesconto_CalcularTotal_DescontoValido_CalcularTotalComDesconto()
    {
        // Arrange
        CarrinhoDeCompras carrinhoDeCompras = new CarrinhoDeCompras();

        // Act
        carrinhoDeCompras.AdicionarItem("Camiseta", 100, 2);
        carrinhoDeCompras.AplicarDesconto(25);

        // Assert
        Assert.Equal(150, carrinhoDeCompras.CalcularTotal());
    }

    // Teste 9
    [Fact(DisplayName = "Aplicar um desconto inválido")]
    public void AplicarDesconto_DescontoInvalido_LancaArgumento()
    {
        // Arrange
        CarrinhoDeCompras carrinhoDeCompras = new CarrinhoDeCompras();

        // Act e Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            carrinhoDeCompras.AplicarDesconto(101));
        Assert.Equal(
            "O percentual de desconto deve estar entre 0 e 100. (Parameter 'percentual')",
            exception.Message);
    }

    // Teste 10
    [Fact(DisplayName = "Esvaziar o carrinho")]
    public void AdicionarItem_Esvaziar_CarrinhoComItens_DeixarCarrinhoVazio()
    {
        // Arrange
        CarrinhoDeCompras carrinhoDeCompras = new CarrinhoDeCompras();
        carrinhoDeCompras.AdicionarItem("Produto", 100, 2);

        // Act
        carrinhoDeCompras.Esvaziar();

        // Assert
        Assert.True(carrinhoDeCompras.EstaVazio());
        Assert.Equal(0, carrinhoDeCompras.CalcularTotal());
    }
}
