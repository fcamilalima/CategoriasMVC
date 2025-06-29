using CategoriasMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace CategoriasMVC.Services;

public class ProdutoService : IProdutoService
{
    private readonly IHttpClientFactory _clientFactory;
    const string apiEndpointProdutos = "api/v1/Produtos/";
    private readonly JsonSerializerOptions _options;
    private ProdutoViewModel _produtoVM;
    private IEnumerable<ProdutoViewModel> _produtosViewModel;

    public ProdutoService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    [HttpGet]
    public async Task<IEnumerable<ProdutoViewModel>> GetProdutos(string token)
    {
        var client = _clientFactory.CreateClient("ProdutosAPI");
        PutTokenInHeaderAuthorization(token, client);
        using (var response = await client.GetAsync(apiEndpointProdutos))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                _produtosViewModel = await JsonSerializer
                    .DeserializeAsync<IEnumerable<ProdutoViewModel>>
                    (apiResponse, _options);
            }
            else
            {
                throw new Exception($"Erro ao criar produto: {response.ReasonPhrase}");
            }
        }
        return _produtosViewModel;
    }

    public async Task<ProdutoViewModel> GetProdutoPorId(int id, string token)
    {
        var client = _clientFactory.CreateClient("ProdutosAPI");
        PutTokenInHeaderAuthorization(token, client);

        using(var response = await client.GetAsync($"{apiEndpointProdutos}{id}"))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                _produtoVM = await JsonSerializer
                    .DeserializeAsync<ProdutoViewModel>(apiResponse, _options);
            }
            else
            {
                throw new Exception($"Erro ao criar produto: {response.ReasonPhrase}");
            }
        }
        return _produtoVM;
    }
    public async Task<ProdutoViewModel> CriarProduto(ProdutoViewModel produtoVM, string token)
    {
        var client = _clientFactory.CreateClient("ProdutosAPI");
        PutTokenInHeaderAuthorization(token, client);

        var produto = JsonSerializer.Serialize(produtoVM);
        StringContent content = new StringContent(produto, Encoding.UTF8, "application/json");

        using(var response = await client.PostAsync(apiEndpointProdutos, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                _produtoVM = await JsonSerializer
                    .DeserializeAsync<ProdutoViewModel>(apiResponse, _options);
            }
            else
            {
                throw new Exception($"Erro ao criar produto: {response.ReasonPhrase}");
            }
        }
        return _produtoVM;
    }

    public async Task<bool> AtualizaProduto(int id, ProdutoViewModel produto, string token)
    {
        var client = _clientFactory.CreateClient("ProdutosAPI");
        PutTokenInHeaderAuthorization(token, client);

        using(var response = await client.PutAsJsonAsync($"{apiEndpointProdutos}{id}", produto))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception($"Erro ao atualizar produto com ID {id}: {response.ReasonPhrase}");
            }
        }
    }


    public async Task<bool> DeletaProduto(int id, string token)
    {
        var client = _clientFactory.CreateClient("ProdutosAPI");
        PutTokenInHeaderAuthorization(token, client);

        using (var response = await client.DeleteAsync($"{apiEndpointProdutos}{id}"))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
        }
        return false;
    }

    private static void PutTokenInHeaderAuthorization(string token, HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
    }
}


