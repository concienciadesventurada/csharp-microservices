using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using Web.Models.DTO;
using Web.Service.IService;

namespace Web.Controllers {
    public class CouponController : Controller {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService) {
            _couponService = couponService;
        }

        public async Task<IActionResult> CouponIndex() {
            List<CouponDTO>? coupons = new();

            ResponseDTO? res = await _couponService.GetAllCouponsAsync();

            if (res != null && res.IsSuccess)
                coupons = JsonConvert.DeserializeObject<List<CouponDTO>>(Convert.ToString(res.Result)!);

            return View(coupons);
        }

        public async Task<IActionResult> CouponCreate() {
            return View();
        }
    }
}