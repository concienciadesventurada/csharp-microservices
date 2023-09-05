using Microsoft.AspNetCore.Mvc;
using Services.CouponAPI.Data;
using Services.CouponAPI.Models;
using Services.CouponAPI.Models.DTO;

namespace Services.CouponAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CouponAPIController : ControllerBase
  {
    private readonly AppDbContext _db;
    private ResponseDTO _res;

    public CouponAPIController(AppDbContext db)
    {
      _db = db;
      _res = new ResponseDTO();
    }

    // NOTE: All type declaration of Route params must be with "{param:type}"
    // or it will crash with a 500. It won't be able to infer it.
    [HttpGet]
    [Route("{id:int}")]
    public ResponseDTO Get(int id)
    {
      try
      {
        Coupon coupon = _db.Coupons.First(u => u.CouponId == id);
        _res.Result = coupon;
        _res.IsSuccess = true;
      }
      catch (Exception ex)
      {
        _res.IsSuccess = false;
        _res.Message = ex.Message;
      }

      return _res;
    }

    // NOTE: These are the same objects. The only way it distinguishes them is
    // by the Route params provided previously
    [HttpGet]
    public ResponseDTO Get()
    {
      try
      {
        IEnumerable<Coupon> obj = _db.Coupons.ToList();
        _res.Result = obj;
        _res.IsSuccess = true;
      }
      catch (Exception ex)
      {
        _res.IsSuccess = false;
        _res.Message = ex.Message;
      }

      return _res;
    }
  }
}
