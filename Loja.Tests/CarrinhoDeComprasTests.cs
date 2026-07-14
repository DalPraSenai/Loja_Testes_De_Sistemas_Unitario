using Loja;
using Xunit;

namespace Loja.Tests;

public class CarrinhoDeComprasTests
{
    [Fact]
    public void AdicionarItem_ItemNovo_CalculaSubtotalCorretamente()
    {
        // Arrange
        CarrinhoDeCompras carrinhoDeCompras = new CarrinhoDeCompras();

        // Act
        carrinhoDeCompras.AdicionarItem("Teclado", 100m, 2);

        // Assert
        Assert.Equal(200m, carrinhoDeCompras.CalcularSubtotal());
    }

    [Fact]
    public void AdicionarItem_MesmoItemDuasVezes_SomaQuantidadeENaoDuplicaItem()
    {
        // Arrange
        CarrinhoDeCompras carrinhoDeCompras = new CarrinhoDeCompras();

        // Act
        carrinhoDeCompras.AdicionarItem("Mouse", 50m, 1);
        carrinhoDeCompras.AdicionarItem("Mouse", 50m, 3);

        // Assert
        Assert.Equal(4, carrinhoDeCompras.Itens[0].Quantidade);
        Assert.Single(carrinhoDeCompras.Itens);
    }

    [Fact]
    public void AdicionarItem_PrecoInvalido_LancaArgumentException()
    {
        // Arrange
        CarrinhoDeCompras carrinhoDeCompras = new CarrinhoDeCompras();

        // Act / Assert
        Assert.Throws<ArgumentException>(() =>
            carrinhoDeCompras.AdicionarItem("Monitor", 0m, 1));

        Assert.Throws<ArgumentException>(() =>
            carrinhoDeCompras.AdicionarItem("Monitor", -10m, 1));
    }

    [Fact]
    public void AdicionarItem_QuantidadeInvalida_LancaArgumentException()
    {
        // Arrange
        CarrinhoDeCompras carrinhoDeCompras = new CarrinhoDeCompras();

        // Act / Assert
        Assert.Throws<ArgumentException>(() =>
            carrinhoDeCompras.AdicionarItem("Cadeira", 300m, 0));

        Assert.Throws<ArgumentException>(() =>
            carrinhoDeCompras.AdicionarItem("Cadeira", 300m, -2));
    }

    [Fact]
    public void RemoverItem_ItemExistente_RecalculaSubtotalCorretamente()
    {
        // Arrange
        CarrinhoDeCompras carrinhoDeCompras = new CarrinhoDeCompras();
        carrinhoDeCompras.AdicionarItem("Notebook", 3000m, 1);
        carrinhoDeCompras.AdicionarItem("Mouse", 50m, 2);

        // Act
        carrinhoDeCompras.RemoverItem("Notebook");

        // Assert
        Assert.Equal(100m, carrinhoDeCompras.CalcularSubtotal());
    }

    [Fact]
    public void RemoverItem_ItemInexistente_LancaInvalidOperationException()
    {
        // Arrange
        CarrinhoDeCompras carrinhoDeCompras = new CarrinhoDeCompras();
        carrinhoDeCompras.AdicionarItem("Webcam", 200m, 1);

        // Act / Assert
        Assert.Throws<InvalidOperationException>(() =>
            carrinhoDeCompras.RemoverItem("Microfone"));
    }

    [Fact]
    public void AtualizarQuantidade_ItemExistente_AtualizaQuantidadeDeItens()
    {
        // Arrange
        CarrinhoDeCompras carrinhoDeCompras = new CarrinhoDeCompras();
        carrinhoDeCompras.AdicionarItem("Headset", 150m, 1);

        // Act
        carrinhoDeCompras.AtualizarQuantidade("Headset", 5);

        // Assert
        Assert.Equal(5, carrinhoDeCompras.QuantidadeDeItens);
    }

    [Fact]
    public void AplicarDesconto_PercentualValido_CalculaTotalComDesconto()
    {
        // Arrange
        CarrinhoDeCompras carrinhoDeCompras = new CarrinhoDeCompras();
        carrinhoDeCompras.AdicionarItem("Produto", 100m, 1);

        // Act / Assert
        carrinhoDeCompras.AplicarDesconto(10m);
        Assert.Equal(90m, carrinhoDeCompras.CalcularTotal());

        carrinhoDeCompras.AplicarDesconto(25m);
        Assert.Equal(75m, carrinhoDeCompras.CalcularTotal());

        carrinhoDeCompras.AplicarDesconto(50m);
        Assert.Equal(50m, carrinhoDeCompras.CalcularTotal());
    }

    [Fact]
    public void AplicarDesconto_PercentualInvalido_LancaArgumentException()
    {
        // Arrange
        CarrinhoDeCompras carrinhoDeCompras = new CarrinhoDeCompras();

        // Act / Assert
        Assert.Throws<ArgumentException>(() =>
            carrinhoDeCompras.AplicarDesconto(-1m));

        Assert.Throws<ArgumentException>(() =>
            carrinhoDeCompras.AplicarDesconto(101m));
    }

    [Fact]
    public void Esvaziar_CarrinhoComItensEDesconto_DeixaCarrinhoVazioETotalZero()
    {
        // Arrange
        CarrinhoDeCompras carrinhoDeCompras = new CarrinhoDeCompras();
        carrinhoDeCompras.AdicionarItem("Produto", 100m, 2);
        carrinhoDeCompras.AplicarDesconto(10m);

        // Act
        carrinhoDeCompras.Esvaziar();

        // Assert
        Assert.True(carrinhoDeCompras.EstaVazio());
        Assert.Equal(0m, carrinhoDeCompras.CalcularTotal());
    }
}
