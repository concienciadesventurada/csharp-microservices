using Web.Models.DTO;

namespace Web.Service.IService {
    public interface ICouponService {
        Task<ResponseDTO?> GetCouponAsync(string couponCode);
        Task<ResponseDTO?> GetAllCouponsAsync();
        Task<ResponseDTO?> GetCouponByIdAsync(int id);
        Task<ResponseDTO?> CreateCouponAsync(CouponDTO couponDto);
        Task<ResponseDTO?> UpdateCouponAsync(CouponDTO couponDto);
        Task<ResponseDTO?> DeleteCouponAsync(int id);
    }
}