
using DALL_E.Models;
using Newtonsoft.Json;
using System.Text;

namespace DALL_E.Services
{
    public class Consulta : IConsulta
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private string _apiKey;
        public Consulta(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _HttpContextAccessor = httpContextAccessor;
            _apiKey = _configuration.GetSection("Llaves:apiKey").Value;
        }



        public async Task<ResponseModel> GenerateImage(Input input)
        {
            input.n = 1;
            input.size = "256x256";
            ResponseModel? responseModel = null;
            using (var httpClient = _httpClient)
            {
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);
                var inpJSON = JsonConvert.SerializeObject(input);
                var Message = await httpClient.PostAsync("https://api.openai.com/v1/images/generations",
                    new StringContent(inpJSON,Encoding.UTF8, "application/json"));
                if (Message.IsSuccessStatusCode)
                {
                    var content = await Message.Content.ReadAsStringAsync();
                    responseModel = JsonConvert.DeserializeObject<ResponseModel>(content);
                }

            }
            return responseModel;
        }
    }
}
