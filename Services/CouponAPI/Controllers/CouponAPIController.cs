using Microsoft.AspNetCore.Mvc;
using Services.CouponAPI.Data;
using Services.CouponAPI.Models;
using Services.CouponAPI.Models.DTO;
using AutoMapper;

namespace Services.CouponAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CouponAPIController : ControllerBase
  {
    private readonly AppDbContext _db;
    private ResponseDTO _res;
    private IMapper _mapper;

    public CouponAPIController(AppDbContext db, IMapper mapper)
    {
      _db = db;
      _mapper = mapper;
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

        // NOTE: We previously returned the object itself, but that's not good.
        // Instead, we now use AutoMapper to pass it. It'll work as long as the
        // attributes of the class and DTO coincide.
        _res.Result = _mapper.Map<CouponDTO>(coupon);

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

        _res.Result = _mapper.Map<IEnumerable<CouponDTO>>(obj);
        _res.IsSuccess = true;
      }
      catch (Exception ex)
      {
        _res.IsSuccess = false;
        _res.Message = ex.Message;
      }

      return _res;
    }

    [HttpGet]
    // NOTE: Funny how it crashes if I declare it as string...
    [Route("GetByCode/{code}")]
    public ResponseDTO GetByCode(string code)
    {
      try
      {
        Coupon coupon = _db.Coupons.First(u => u.CouponCode.ToLower() == code.ToLower());

        // TODO: It'll crash the server if I add the CouponId myself, learn why...
        _res.Result = _mapper.Map<CouponDTO>(coupon);
        _res.IsSuccess = true;
      }
      catch (Exception ex)
      {
        _res.IsSuccess = false;
        _res.Message = ex.Message;
      }

      return _res;
    }

    [HttpPost]
    public ResponseDTO Post([FromBody] CouponDTO couponDto)
    {
      try
      {
        Coupon coupon = _mapper.Map<Coupon>(couponDto);
        _db.Coupons.Add(coupon); // Enqueues for insertion in db
        _db.SaveChanges(); // Write the changes to the db

        _res.Result = _mapper.Map<CouponDTO>(coupon);
      }
      catch (Exception ex)
      {
        _res.IsSuccess = false;
        _res.Message = ex.Message;
      }

      return _res;
    }

    [HttpPut]
    public ResponseDTO Put([FromBody] CouponDTO couponDto)
    {
      try
      {
        Coupon coupon = _mapper.Map<Coupon>(couponDto);
        _db.Coupons.Update(coupon);
        _db.SaveChanges();

        _res.Result = _mapper.Map<CouponDTO>(coupon);
      }
      catch (Exception ex)
      {
        _res.IsSuccess = false;
        _res.Message = ex.Message;
      }

      return _res;
    }

    [HttpDelete]
    [Route("{id:int}")]
    public ResponseDTO Delete(int id)
    {
      try
      {
        Coupon coupon = _db.Coupons.First(u => u.CouponId == id);
        _db.Coupons.Remove(coupon);
        _db.SaveChanges();
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
