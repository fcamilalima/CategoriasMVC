using CategoriasMVC.Models;
using System.Text;
using System.Text.Json;

namespace CategoriasMVC.Services;

public class Autenticacao : IAutenticacao
{
    private readonly IHttpClientFactory _clientFactory;
    const string apiEndpointAutenticacao = "api/Auth/login?api-version=1";

    private readonly JsonSerializerOptions _options;
    private TokenViewModel _tokenUsuario;

    public Autenticacao(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<TokenViewModel> AutenticaUsuario(UsuarioViewModel usuarioViewModel)
    {
        var client = _clientFactory.CreateClient("AutenticaAPI");
        var usuario = JsonSerializer.Serialize(usuarioViewModel);
        StringContent content = new StringContent(usuario, Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync(apiEndpointAutenticacao, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                _tokenUsuario = await JsonSerializer.DeserializeAsync<TokenViewModel>
                    (apiResponse, _options);
            }
            else
            {
                _tokenUsuario = new TokenViewModel
                {
                    Authenticated = false,
                    Message = "Usuário ou senha inválidos"
                };
                //return null;
            }
        }
        return _tokenUsuario;
    }
}
