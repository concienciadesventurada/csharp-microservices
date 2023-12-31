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

            if (res != null && res.IsSuccess) {
                coupons = JsonConvert.DeserializeObject<List<CouponDTO>>(Convert.ToString(res.Result)!);
            }
            else {
                TempData["error"] = res?.Message;
            }

            return View(coupons);
        }

        public async Task<IActionResult> CouponCreate() => View();

        // NOTE: The same way the brackets tell the method in the API controller
        // it'll work the same, with the exception of it taking the model
        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDTO model) {
            if (ModelState.IsValid) {
                ResponseDTO? res = await _couponService.CreateCouponAsync(model);

                if (res != null && res.IsSuccess) {
                    TempData["success"] = "Coupon created successfully";
                    return RedirectToAction(nameof(CouponIndex));
                }
                else {
                    TempData["error"] = res?.Message;
                }
            }

            return View(model);
        }

        public async Task<IActionResult> CouponDelete(int id) {
            ResponseDTO? res = await _couponService.GetCouponByIdAsync(id);

            if (res != null && res.IsSuccess) {
                CouponDTO? model = JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(res.Result)!);
                TempData["success"] = "Coupon deleted successfully";
                return View(model);
            }
            else TempData["error"] = res?.Message;

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDTO couponDto) {
            ResponseDTO? res = await _couponService.DeleteCouponAsync(couponDto.CouponId);

            if (res != null && res.IsSuccess) return RedirectToAction(nameof(CouponIndex));
            else TempData["error"] = res?.Message;

            return View(couponDto);
        }

        /* TODO:
        public async Task<IActionResult> CouponUpdate(CouponDTO couponDto) {
            ResponseDTO? res = await _couponService.UpdateCouponAsync(couponDto);

            if (res != null && res.IsSuccess) {
                CouponDTO? coupon = JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(res.Result)!);
                return View(coupon);
            }

            return NotFound();
        }
        */
    }
}