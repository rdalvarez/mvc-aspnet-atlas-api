using Front_End.Models;
using Front_End.Services.IServices;
using Front_End.Utility;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Text;

namespace Front_End.Services
{
    public class AtlasService : IAtlasService
    {
        private readonly IHttpClientFactory _clientFactory;
        public AtlasService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        private async Task<ResponseDto> SendAsync(RequestDto requestDto)
        {
            var response = new ResponseDto();

            try
            {
                HttpClient client = _clientFactory.CreateClient("AtlasAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(requestDto.Url);
                if (requestDto.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                }

                switch (requestDto.ApiType)
                {
                    case Utility.SD.ApiType.GET:
                        message.Method = HttpMethod.Get;
                        break;
                    case Utility.SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case Utility.SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case Utility.SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                }

                HttpResponseMessage apiResponse = await client.SendAsync(message);

                switch (apiResponse.StatusCode)
                {
                    case System.Net.HttpStatusCode.Unauthorized: //
                        response.IsSuccess = false;
                        response.Message = "Unauthorized";
                        break;
                    case System.Net.HttpStatusCode.Forbidden: //
                        response.IsSuccess = false;
                        response.Message = "Acces Denied";
                        break;
                    case System.Net.HttpStatusCode.NotFound: //
                        response.IsSuccess = false;
                        response.Message = "Not Found";
                        break;
                    case System.Net.HttpStatusCode.InternalServerError: //
                        response.IsSuccess = false;
                        response.Message = "Insernal Server Error";
                        break;
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        response = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        break;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto?> GetPhotosAsync()
        {
            return await SendAsync( new RequestDto()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = SD.AtlasApiBase + "api/AtlasPhoto/GetPhoto",
            });
        }

        public async Task<ResponseDto?> GetPhotoByIdAsync( int id)
        {
            return await SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = SD.AtlasApiBase + $"api/AtlasPhoto/GetPhotoById/{id}",
            });
        }

        public async Task<ResponseDto?> GetPhotoByTitleAsync(string title)
        {
            return await SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = SD.AtlasApiBase + $"api/AtlasPhoto/GetPhotoByTitle/{title}",
            });
        }
        public async Task<ResponseDto?> PostPhotoAsync(AtlasPhoto photo)
        {
            return await SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Url = SD.AtlasApiBase + "api/AtlasPhoto/PostPhoto",
                Data = photo,
            });
        }
        public async Task<ResponseDto?> PutPhotoAsync(AtlasPhoto photo)
        {
            return await SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.PUT,
                Url = SD.AtlasApiBase + "api/AtlasPhoto/PutPhoto",
                Data = photo,
            });
        }
        public async Task<ResponseDto?> DeletePhotoAsync(int id)
        {
            return await SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.DELETE,
                Url = SD.AtlasApiBase + $"api/AtlasPhoto/DeletePhoto/{id}",
            });
        }
    }
}
