using Web.Models.DTO;

namespace Web.Service.IService {
    public interface ICouponService {
        Task<ResponseDTO?> GetCoupon(string couponCode);
        Task<ResponseDTO?> GetAllCouponAsync(string couponCode);
        Task<ResponseDTO?> GetAllCouponByIdAsync(int id);
        Task<ResponseDTO?> CreateCouponAsync(CouponDTO couponDto);
        Task<ResponseDTO?> UpdateCouponAsync(CouponDTO couponDto);
        Task<ResponseDTO?> DeleteCouponAsync(int id);
    }
}