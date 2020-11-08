using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using VendingMachineAPI.Models;

namespace VendingMachineAPI.Controllers
{
    [RoutePrefix("api/Vending")]
    public class VendingController : ApiController
    {
        VendingMachineDBEntities db = new VendingMachineDBEntities();
        // GET: Vending
        [HttpGet]
        [Route("InitialiseTables")]
        public string InitialiseTables()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var coins = this.CoinInit();
            var products = this.ProductInit();

            try
            {
                db.Coins.AddRange(coins);
                db.SaveChanges();
                db.Products.AddRange(products);
                db.SaveChanges();

                return "Success!!";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        private List<Coin> CoinInit()
        {
            var toReturn = new List<Coin>
            {
                new Coin { CoinQuantity = 5, CoinValue = (decimal)0.5 },
                new Coin { CoinQuantity = 5, CoinValue = 1 },
                new Coin { CoinQuantity = 5, CoinValue = 2 },
                new Coin { CoinQuantity = 10, CoinValue = 5 },
            };

            return toReturn;
        }

        private List<Product> ProductInit()
        {
            var toReturn = new List<Product>
            {
                new Product { ProductName = "Skittles", ProductPrice = (decimal)8.5, ProductQuantity = 5 },
                new Product { ProductName = "Diddle Daddles", ProductPrice = 12, ProductQuantity = 3 },
                new Product { ProductName = "Bar One", ProductPrice = (decimal)11.5, ProductQuantity = 1 },
                new Product { ProductName = "Biltong", ProductPrice = 30, ProductQuantity = 2},
                new Product { ProductName = "Oreos", ProductPrice = 22, ProductQuantity = 6},
                new Product { ProductName = "Coca - Cola", ProductPrice = 10, ProductQuantity = 3},
                new Product { ProductName = "Chips", ProductPrice = 13, ProductQuantity = 3},
                new Product { ProductName = "Jelly Tots", ProductPrice = 7, ProductQuantity = 3},
                new Product { ProductName = "Speckled Eggs", ProductPrice = 8, ProductQuantity = 1},
            };

            return toReturn;
        }

        [Route("GetCoins")]
        [HttpGet]

        public dynamic GetCoins()
        {
            db.Configuration.ProxyCreationEnabled = false;

            var coins = db.Coins.Select(zz => new
            {
                CoinID = zz.CoinID,
                CoinValue = zz.CoinValue,
                CoinQuantity = zz.CoinQuantity
            }).ToList();

            return coins;
        }

        [Route("GetProducts")]
        [HttpGet]

        public dynamic GetProducts()
        {
            db.Configuration.ProxyCreationEnabled = false;

            var products = db.Products.Select(zz => new
            {
                ProductID = zz.ProductID,
                ProductName = zz.ProductName,
                ProductQuantity = zz.ProductQuantity,
                ProductPrice = zz.ProductPrice
            }).ToList();

            return products;
        }
    }
}