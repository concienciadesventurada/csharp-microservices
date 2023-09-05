using Web.Models.DTO;

namespace Web.Service.IService {
    public interface IBaseService {
        Task<ResponseDTO?> SendAsync(RequestDTO reqDTO);
    }
}