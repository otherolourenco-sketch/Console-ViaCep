# Console-ViaCep

Aplicação de console em C# para consulta de endereços a partir de um CEP, utilizando a API pública [ViaCEP](https://viacep.com.br/).

## Como funciona

1. O usuário informa um CEP pelo console (com ou sem hífen/pontuação).
2. O CEP é validado (deve conter exatamente 8 dígitos numéricos).
3. O programa faz uma requisição HTTP para a API ViaCEP.
4. A resposta JSON é convertida para o objeto `Endereco` e exibida de forma formatada.

## Funcionalidades

* Consulta de CEP pelo console
* Validação do CEP digitado (formato, antes de chamar a API)
* Consumo de API REST (`HttpClient`)
* Desserialização da resposta JSON com `System.Text.Json`
* Tratamento de CEP inexistente (a ViaCEP responde `200 OK` com `{"erro": "true"}` nesse caso, em vez de um erro HTTP)
* Tratamento de falhas de rede/HTTP e de respostas em formato inesperado

## API utilizada

Endpoint utilizado:

```text
https://viacep.com.br/ws/{cep}/json/
```

Exemplo de resposta para um CEP válido:

```json
{
  "cep": "01310-930",
  "logradouro": "Avenida Paulista",
  "bairro": "Bela Vista",
  "localidade": "São Paulo",
  "uf": "SP",
  "ddd": "11"
}
```

Exemplo de resposta para um CEP inexistente:

```json
{
  "erro": "true"
}
```

## Estrutura do projeto

* [Program.cs](Program.cs) — fluxo principal: leitura do CEP, chamada à API e exibição do resultado.
* [Endereco.cs](Endereco.cs) — modelo (`Endereco`) que representa o retorno da API ViaCEP.

## Tecnologias

* C# / .NET (net10.0)
* `HttpClient`
* `System.Text.Json`
* API ViaCEP

## Como executar

```bash
dotnet run
```

Em seguida, digite o CEP solicitado no console.

## Exemplo de execução

```text
=== Consulta de CEP (ViaCEP) ===
Digite o CEP que deseja consultar: 01310-930
Consultando: https://viacep.com.br/ws/01310930/json/

--- Endereço encontrado ---
CEP:         01310-930
Logradouro:  Avenida Paulista
Complemento: 2100
Bairro:      Bela Vista
Cidade:      São Paulo
UF:          SP
DDD:         11
```
