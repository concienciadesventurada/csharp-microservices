using Web.Models.DTO;
using Web.Service.IService;
using Web.Utility;

namespace Web.Service {
    public class CouponService : ICouponService {
        private readonly IBaseService _baseService;

        public CouponService(IBaseService baseService) {
            _baseService = baseService;
        }

        public async Task<ResponseDTO?> GetCouponAsync(string couponCode) {
            return await _baseService.SendAsync(
                    new RequestDTO() {
                        ApiType = Web.Utility.SD.ApiType.GET,
                        Url = SD.CouponAPIBase + "/api/coupon/GetByCode/" + couponCode
                    }
                );
        }

        public async Task<ResponseDTO?> GetAllCouponsAsync() {
            return await _baseService.SendAsync(
                    new RequestDTO() {
                        ApiType = Web.Utility.SD.ApiType.GET,
                        Url = SD.CouponAPIBase + "/api/coupon"
                    }
            );
        }

        public async Task<ResponseDTO?> GetCouponByIdAsync(int id) {
            return await _baseService.SendAsync(
                    new RequestDTO() {
                        ApiType = Web.Utility.SD.ApiType.GET,
                        Url = SD.CouponAPIBase + "/api/coupon/" + id
                    }
                );
        }

        public async Task<ResponseDTO?> CreateCouponAsync(CouponDTO couponDto) {
            return await _baseService.SendAsync(
                    new RequestDTO() {
                        ApiType = Web.Utility.SD.ApiType.POST,
                        Url = SD.CouponAPIBase + "/api/coupon/",
                        Data = couponDto
                    }
            );
        }

        public async Task<ResponseDTO?> UpdateCouponAsync(CouponDTO couponDto) {
            return await _baseService.SendAsync(
                    new RequestDTO() {
                        ApiType = Web.Utility.SD.ApiType.PUT,
                        Url = SD.CouponAPIBase + "/api/coupon/",
                        Data = couponDto
                    }
            );
        }

        public async Task<ResponseDTO?> DeleteCouponAsync(int id) {
            return await _baseService.SendAsync(
                    new RequestDTO() {
                        ApiType = SD.ApiType.DELETE,
                        Url = SD.CouponAPIBase + "/api/coupon/" + id
                    }
                );
        }
    }
}