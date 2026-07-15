# Testes reutilizáveis do Carrinho de Compras

Este documento reúne a suíte de testes do `CarrinhoDeCompras` em um formato fácil
de copiar para outro computador ou projeto.

## Requisitos

- .NET SDK 9
- xUnit
- Microsoft.NET.Test.Sdk
- xunit.runner.visualstudio
- Referência ao projeto que contém `CarrinhoDeCompras`

## Código completo

Salve o código abaixo como `CarrinhoDeComprasTests.cs` em um projeto xUnit:

```csharp
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
        CarrinhoDeCompras carrinho = new CarrinhoDeCompras();

        // Act
        carrinho.AdicionarItem("Chaveiro", 12, 1);

        // Assert
        Assert.Equal(12, carrinho.CalcularSubtotal());
    }

    // Teste 2
    [Fact(DisplayName = "Adicionar o mesmo item duas vezes")]
    public void AdicionarItem_CalcularSubtotal_AdicionarItemDuasVezes_SomarQuantidadeSemDuplicar()
    {
        // Arrange
        CarrinhoDeCompras carrinhoDeCompras = new CarrinhoDeCompras();

        // Act
        carrinhoDeCompras.AdicionarItem("Pantufa", 300, 2);
        carrinhoDeCompras.AdicionarItem("Pantufa", 300, 1);

        // Assert
        Assert.Single(carrinhoDeCompras.Itens);
        Assert.Equal(3, carrinhoDeCompras.Itens[0].Quantidade);
        Assert.Equal(900, carrinhoDeCompras.CalcularSubtotal());
    }

    // Teste 3
    [Fact(DisplayName = "Adicionar item com preço inválido")]
    public void AdicionarItem_PrecoInvalido_LancaArgumentException()
    {
        // Arrange
        CarrinhoDeCompras carrinhoDeCompras = new CarrinhoDeCompras();

        // Act e Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            carrinhoDeCompras.AdicionarItem("Caneta", -30, 3));
        Assert.Equal("O preço deve ser positivo. (Parameter 'preco')", exception.Message);
    }

    // Teste 4
    [Fact(DisplayName = "Não adicionar item com quantidade inválida")]
    public void AdicionarItem_QuantidadeInvalida_LancarArgumentException()
    {
        // Arrange
        CarrinhoDeCompras carrinho = new CarrinhoDeCompras();

        // Act / Assert
        Assert.Throws<ArgumentException>(() =>
            carrinho.AdicionarItem("Cadeira", 300, 0));
    }

    // Teste 5
    [Fact(DisplayName = "Remover item existente")]
    public void RemoverItem_CalcularSubtotal_RemoverItemExistente_RecalcularSubtotal()
    {
        // Arrange
        CarrinhoDeCompras carrinho = new CarrinhoDeCompras();
        carrinho.AdicionarItem("Notebook", 3000, 1);
        carrinho.AdicionarItem("Mouse", 50, 2);

        // Act
        carrinho.RemoverItem("Notebook");

        // Assert
        Assert.Equal(100, carrinho.CalcularSubtotal());
    }

    // Teste 6
    [Fact(DisplayName = "Não remover item inexistente")]
    public void RemoverItem_ItemInexistente_LancarInvalidOperationException()
    {
        // Arrange
        CarrinhoDeCompras carrinho = new CarrinhoDeCompras();

        // Act / Assert
        Assert.Throws<InvalidOperationException>(() =>
            carrinho.RemoverItem("Microfone"));
    }

    // Teste 7
    [Fact(DisplayName = "Atualizar quantidade de um item")]
    public void AtualizarQuantidade_ItemExistente_AtualizarQuantidadeCorretamente()
    {
        // Arrange
        CarrinhoDeCompras carrinho = new CarrinhoDeCompras();
        carrinho.AdicionarItem("Headset", 150, 1);

        // Act
        carrinho.AtualizarQuantidade("Headset", 5);

        // Assert
        Assert.Equal(5, carrinho.QuantidadeDeItens);
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
    [Fact(DisplayName = "Não aplicar desconto inválido")]
    public void AplicarDesconto_DescontoInvalido_LancarArgumentException()
    {
        // Arrange
        CarrinhoDeCompras carrinho = new CarrinhoDeCompras();

        // Act / Assert
        Assert.Throws<ArgumentException>(() =>
            carrinho.AplicarDesconto(101));
    }

    // Teste 10
    [Fact(DisplayName = "Esvaziar carrinho")]
    public void Esvaziar_CarrinhoComItem_DeixarCarrinhoVazio()
    {
        // Arrange
        CarrinhoDeCompras carrinho = new CarrinhoDeCompras();
        carrinho.AdicionarItem("Produto", 100, 2);

        // Act
        carrinho.Esvaziar();

        // Assert
        Assert.True(carrinho.EstaVazio());
        Assert.Equal(0, carrinho.CalcularTotal());
    }
}
```

## O que cada teste verifica

1. Um item novo entra no carrinho e gera o subtotal esperado.
2. O mesmo produto é agrupado, somando a quantidade sem duplicar a entrada.
3. Um preço negativo gera `ArgumentException` com a mensagem esperada.
4. Uma quantidade inválida não pode ser adicionada.
5. Remover um item existente recalcula o subtotal.
6. Remover um item inexistente gera `InvalidOperationException`.
7. A quantidade de um produto existente pode ser atualizada.
8. Um desconto válido é aplicado ao total da compra.
9. Um desconto acima de 100% é recusado.
10. Esvaziar remove os itens e deixa o total igual a zero.

## Como executar em outro computador

Na pasta da solução, execute:

```powershell
dotnet test
```

Se o projeto de testes for criado do zero:

```powershell
dotnet new xunit -n Loja.Tests
dotnet add Loja.Tests/Loja.Tests.csproj reference Loja/Loja.csproj
dotnet test
```

Se os namespaces ou nomes dos projetos forem diferentes, ajuste `using Loja;`,
`namespace Loja.Tests;` e o caminho da referência.
