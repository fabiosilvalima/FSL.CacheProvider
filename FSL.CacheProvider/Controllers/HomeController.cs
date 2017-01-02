using FSL.CacheProvider.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using FSL.CacheProvider.Models;
using System.Threading;

namespace FSL.CacheProvider.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAsyncCacheProvider _cacheProvider;
        private DateTime _dateTime;
        private string _loggedUser = "";

        public HomeController(IAsyncCacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
            _loggedUser = "fabio silva lima"; //sample
        }

        public async Task<ActionResult> Index(string id = "0")
        {
            _dateTime = DateTime.Now;

            var customer = _cacheProvider.CacheAsync(() => GetCustomer(id), id);
            var genders = _cacheProvider.CacheAsync(() => GetGenders());
            var userName = _cacheProvider.CacheAsync(UseCacheOrNot(), () => GetUserName(_loggedUser), _loggedUser);

            await Task.WhenAll(customer, genders, userName);

            var viewModel = new HomePageViewModel()
            {
                Customer = customer.Result,
                Genders = genders.Result,
                UserName = userName.Result,
                Date = _dateTime
            };

            return View(viewModel);
        }

        private Func<bool> UseCacheOrNot()
        {
            return () =>
            {
                if (_dateTime.Second % 2 == 0)
                {
                    return true;
                }

                return false;
            };
        }

        private async Task<string> GetUserName(string userName)
        {
            Thread.Sleep(new Random().Next(1, 400));

            return await Task.Run(() =>
            {
                return userName + " " + _dateTime.ToString();
            });
        }

        private async Task<List<Gender>> GetGenders()
        {
            Thread.Sleep(new Random().Next(1, 800));

            return await Task.Run(() =>
            {
                return new List<Gender>()
                {
                    new Gender()
                    {
                        Id = "M",
                        Name = "Male",
                        Date = _dateTime
                    },
                    new Gender()
                    {
                        Id = "F",
                        Name = "Female",
                        Date = _dateTime
                    }
                };
            });
        }

        private async Task<Customer> GetCustomer(string id)
        {
            Thread.Sleep(new Random().Next(1, 700));

            return await Task.Run(() =>
            {
                var model = new Customer()
                {
                    Id = id,
                    Name = "Customer " + id,
                    Date = _dateTime
                };

                return model;
            });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}