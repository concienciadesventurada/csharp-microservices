using System.Text;

using Newtonsoft.Json;

using Web.Models.DTO;
using Web.Service.IService;

namespace Web.Service {
    public class BaseService : IBaseService {
        // NOTE: This preferable in a proper dependency injection, but for our
        // use case this is good enough
        private readonly IHttpClientFactory _httpClientFactory;

        public BaseService(IHttpClientFactory httpClientFactory) {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseDTO?> SendAsync(RequestDTO reqDTO) {

            try {
                HttpClient client = _httpClientFactory.CreateClient("MangoAPI");
                HttpRequestMessage msg = new();
                msg.Headers.Add("Accept", "application/json");
                // TODO: Token

                // NOTE: This would work directly with GET, but we need to check and
                // serialize if its another operation
                msg.RequestUri = new Uri(reqDTO.Url);

                if (reqDTO.Data != null) {
                    msg.Content = new StringContent(JsonConvert.SerializeObject(reqDTO.Data), Encoding.UTF8, "application/json");
                }

                HttpResponseMessage? apiRes = null;

                switch (reqDTO.ApiType) {
                    case Utility.SD.ApiType.POST:
                        msg.Method = HttpMethod.Post;
                        break;

                    case Utility.SD.ApiType.PUT:
                        msg.Method = HttpMethod.Put;
                        break;

                    case Utility.SD.ApiType.DELETE:
                        msg.Method = HttpMethod.Delete;
                        break;

                    default:
                        msg.Method = HttpMethod.Get;
                        break;
                }

                apiRes = await client.SendAsync(msg);

                switch (apiRes.StatusCode) {
                    case System.Net.HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "Not found" };

                    case System.Net.HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "Access Denied" };

                    case System.Net.HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Unauthorized" };

                    case System.Net.HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "Internal Server Error" };

                    default:
                        var apiContent = await apiRes.Content.ReadAsStringAsync();
                        var apiResDto = JsonConvert.DeserializeObject<ResponseDTO>(apiContent);

                        return apiResDto;
                }
            }
            catch (Exception ex) {
                var dto = new ResponseDTO {
                    Message = ex.Message.ToString(),
                    IsSuccess = false
                };

                return dto;
            }
        }
    }
}