using Web.Service.IService;
using Web.Models.DTO;

namespace Web.Service {
    public class CouponService : ICouponService {
        private readonly IBaseService _baseService;

        public CouponService(IBaseService baseService) {
            _baseService = baseService;
        }

        public Task<ResponseDTO?> GetCoupon(string couponCode) {
            throw new NotImplementedException();
        }
        public Task<ResponseDTO?> GetAllCouponAsync(string couponCode) {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO?> GetAllCouponByIdAsync(int id) {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO?> CreateCouponAsync(CouponDTO couponDto) {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO?> UpdateCouponAsync(CouponDTO couponDto) {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO?> DeleteCouponAsync(int id) {
            throw new NotImplementedException();
        }
    }
}