using Microsoft.AspNetCore.Mvc;
using RedisExchangeApp.Services;
using StackExchange.Redis;

namespace RedisExchangeApp.Controllers
{
    public class ListTypesController : Controller
    {
        private RedisService _redisService;
        private readonly IDatabase db;
        private readonly string key = "names";
        public ListTypesController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(1);
        }

        public IActionResult Index()
        {
            var nameList = new List<string>();
            if (db.KeyExists(key))
            {
                db.ListRange(key).ToList().ForEach(x =>
                {
                    nameList.Add(x.ToString());
                });
            }
            return View(nameList);
        }

        [HttpPost]
        public IActionResult Add(string name)
        {
            db.ListRightPush(key, name);

            return RedirectToAction("Index");
        }

        public IActionResult Remove(string name)
        {
            db.ListRemove(key, name);

            return RedirectToAction("Index");
        }
    }
}
