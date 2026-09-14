using System.Text.Json;
using System.Text.RegularExpressions;
using ConsoleViaCep.Models;
using static System.Console;

WriteLine("Consulta de CEP");
Write("Digite o CEP que deseja consultar: ");

var cepDigitado = ReadLine();
var cep = LimparCep(cepDigitado);

if (!CepValido(cep))
{
    WriteLine("CEP inválido. Informe 8 dígitos numéricos, com ou sem hífen (ex.: 35400-000).");
    return;
}

var enderecoUrl = $"https://viacep.com.br/ws/{cep}/json/";
WriteLine($"Consultando: {enderecoUrl}");

using var client = new HttpClient();

try
{
    var response = await client.GetAsync(enderecoUrl);
    response.EnsureSuccessStatusCode();

    var json = await response.Content.ReadAsStringAsync();
    var endereco = JsonSerializer.Deserialize<Endereco>(json, new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    });

    if (endereco is null || endereco.Erro == "true")
    {
        WriteLine("CEP não encontrado.");
        return;
    }

    ExibirEndereco(endereco);
}
catch (HttpRequestException ex)
{
    WriteLine($"Falha ao acessar a API ViaCEP: {ex.Message}");
}
catch (JsonException ex)
{
    WriteLine($"Resposta inesperada da API: {ex.Message}");
}

static string LimparCep(string? cep) => Regex.Replace(cep ?? string.Empty, @"\D", "");

static bool CepValido(string cep) => Regex.IsMatch(cep, @"^\d{8}$");

static void ExibirEndereco(Endereco endereco)
{
    WriteLine();
    WriteLine("--- Endereço encontrado ---");
    WriteLine($"CEP:         {endereco.Cep}");
    WriteLine($"Logradouro:  {endereco.Logradouro}");
    WriteLine($"Complemento: {endereco.Complemento}");
    WriteLine($"Bairro:      {endereco.Bairro}");
    WriteLine($"Cidade:      {endereco.Localidade}");
    WriteLine($"UF:          {endereco.Uf}");
    WriteLine($"DDD:         {endereco.Ddd}");
}
