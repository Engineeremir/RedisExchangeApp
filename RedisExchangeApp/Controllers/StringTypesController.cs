using Microsoft.AspNetCore.Mvc;
using RedisExchangeApp.Services;
using StackExchange.Redis;

namespace RedisExchangeApp.Controllers
{
    public class StringTypesController : Controller
    {
        private RedisService _redisService;
        private readonly IDatabase db;

        public StringTypesController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(0);
        }

        public IActionResult Index()
        {
            db.StringSet("name", "Emirhan KALEM");
            db.StringSet("visitor", 100);
            return View();
        }

        public IActionResult Show()
        {
            var value = db.StringGet("name");

            db.StringIncrement("visitor", 1);

            if (value.HasValue)
            {
                ViewBag.value = value.ToString();
            }
            
            return View();
        }
    }
}
