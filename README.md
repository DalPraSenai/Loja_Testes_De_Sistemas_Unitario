# Testes unitarios do Carrinho de Compras

Este repositorio contem um projeto em C# com uma classe `CarrinhoDeCompras` e um projeto de testes usando xUnit.

O objetivo da atividade e validar se o carrinho respeita as regras de negocio antes da homologacao.

## O que foi testado

Os testes estao no arquivo:

```text
Loja.Tests/CarrinhoDeComprasTests.cs
```

Foram criados 10 testes unitarios:

1. `AdicionarItem_ItemNovo_CalculaSubtotalCorretamente`
2. `AdicionarItem_MesmoItemDuasVezes_SomaQuantidadeENaoDuplicaItem`
3. `AdicionarItem_PrecoInvalido_LancaArgumentException`
4. `AdicionarItem_QuantidadeInvalida_LancaArgumentException`
5. `RemoverItem_ItemExistente_RecalculaSubtotalCorretamente`
6. `RemoverItem_ItemInexistente_LancaInvalidOperationException`
7. `AtualizarQuantidade_ItemExistente_AtualizaQuantidadeDeItens`
8. `AplicarDesconto_PercentualValido_CalculaTotalComDesconto`
9. `AplicarDesconto_PercentualInvalido_LancaArgumentException`
10. `Esvaziar_CarrinhoComItensEDesconto_DeixaCarrinhoVazioETotalZero`

Todos seguem o padrao AAA:

```text
Arrange - prepara os dados
Act     - executa a acao
Assert  - verifica o resultado
```

## Como rodar os testes

Abra o PowerShell dentro da pasta do projeto ou entre nela com:

```powershell
cd C:\Users\PCNEW\Documents\GitHub\Loja_Testes_De_Sistemas_Unitario
```

Depois rode:

```powershell
dotnet test
```

## Resultado esperado

Ao executar o comando, o resultado esperado e parecido com este:

```text
Aprovado! - Com falha: 0, Aprovado: 10, Ignorado: 0, Total: 10
```

Esse resultado pode ser usado como evidencia da execucao da suite de testes.

## O que entregar

Para a atividade, entregue:

1. O projeto com o arquivo `CarrinhoDeComprasTests.cs`.
2. Um print do terminal depois de rodar `dotnet test`.
3. A saida mostrando que os 10 testes foram aprovados.

## Observacao

Se o comando `dotnet test` nao funcionar, instale o .NET SDK 9 e abra o PowerShell de novo.
