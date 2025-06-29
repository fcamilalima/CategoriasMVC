using CategoriasMVC.Models;
using System.Text;
using System.Text.Json;

namespace CategoriasMVC.Services
{
    public class CategoriaService : ICategoriaService
    {
        private const string apiEndpointCategorias = "/api/v1/Categorias/";
        private readonly JsonSerializerOptions _options;
        private readonly IHttpClientFactory _clientFactory;

        private CategoriaViewModel categoriaViewModel;
        private IEnumerable<CategoriaViewModel> categoriasViewModel;

        public CategoriaService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<bool> AtualizarCategoriaAsync(int id, CategoriaViewModel categoriaVM)
        {
            var client = _clientFactory.CreateClient("CategoriasAPI");

            using (var response = await client
                .PutAsJsonAsync($"{apiEndpointCategorias}{id}", categoriaVM))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    //throw new Exception($"Erro ao atualizar categoria.");
                    return false;
                }
            }
        }

        public async Task<CategoriaViewModel> CriarCategoriaAsync(CategoriaViewModel categoriaVM)
        {
            var client = _clientFactory.CreateClient("CategoriasAPI");
            var categoria = JsonSerializer.Serialize(categoriaVM);
            StringContent content = new StringContent(categoria, Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync(apiEndpointCategorias, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    categoriaViewModel = await JsonSerializer
                        .DeserializeAsync<CategoriaViewModel>(apiResponse, _options);
                }
                else
                {
                    throw new Exception($"Erro ao criar categoria.");
                }
                return categoriaViewModel;
            }
        }

        public async Task<bool> DeletaCategoriaAsync(int id)
        {
            var client = _clientFactory.CreateClient("CategoriasAPI");

            using (var response = await client.DeleteAsync($"{apiEndpointCategorias}{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<CategoriaViewModel> GetCategoriaPorIdAsync(int id)
        {
            var client = _clientFactory.CreateClient("CategoriasAPI");

            using (var response = await client.GetAsync($"{apiEndpointCategorias}{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    categoriaViewModel = await JsonSerializer
                        .DeserializeAsync<CategoriaViewModel>(apiResponse, _options);
                }
                else
                {
                    throw new Exception($"Erro ao obter a categorias {id}.");
                }
                return categoriaViewModel;
            }
        }

        public async Task<IEnumerable<CategoriaViewModel>> GetCategoriasAsync()
        {
            var client = _clientFactory.CreateClient("CategoriasAPI");

            using (var response = await client.GetAsync(apiEndpointCategorias))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    categoriasViewModel = await JsonSerializer
                        .DeserializeAsync<IEnumerable<CategoriaViewModel>>(apiResponse, _options);
                }
                else
                {
                    throw new Exception($"Erro ao obter as categorias.");
                }
                return categoriasViewModel;
            }
        }
    }
}
