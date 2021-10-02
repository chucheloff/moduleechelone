#nullable enable
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ModuleEcheloneRebooted.Models;
using Npgsql;

namespace ModuleEcheloneRebooted.Controllers
{
    public class StoreController : Controller
    {
        private string _connStr;
        private readonly ILogger<StoreController> _logger;
        private Helpers _helper;
        private const Boolean UpdateGuids = false;

        public StoreController(ILogger<StoreController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _connStr = configuration.GetConnectionString("postgre");
            _helper = new Helpers(configuration);
            if (UpdateGuids)
            {
                _helper.UpdateAllGuid();
            }
        }

        // GET : Store/Catalog
        [Route("Store/Catalog")]
        public IActionResult Catalog()
        {
            List<ProductModel> list = GetDisplayedProducts(false);
            return View(list);
        }

        // GET : Store/Warehouse
        [Route("Store/Warehouse")]
        public IActionResult Warehouse()
        {
            List<ProductModel> displayedProducts = GetDisplayedProducts();
            return View(displayedProducts);
        }

        [Route("Store/ProductPage/{id}")]
        public IActionResult ProductPage(int id)
        {
            ProductModel product = GetProductDetails(id);
            return View("ProductPage", product);
        }

        public IActionResult Add()
        {
            ProductModel product = GetNewProduct();
            return View("ProductDetails", product);
        }
        
        [Route("Store/ProductDetails/{id}")]
        public IActionResult ProductDetails(int id)
        {
            ProductModel product = GetProductDetails(id);
            
            return View("ProductDetails", product);
        }

        private ProductModel GetNewProduct()
        {
            return new ProductModel()
            {
                ProductId = _helper.GetNextId(),
                Name = "",
                Price = 0,
                BuyPrice = 0,
                Stock = 0,
                Manufacturer = "",
                Description_Short = "",
                Description_Full = "",
                Breakpoints = "",
                Img = ""
            };
        }
        private ProductModel GetProductDetails(int id)
        {
            try
            {


                ProductModel product = new();
                NpgsqlConnection conn = new(_connStr);
                conn.Open();
                NpgsqlCommand comm = new();
                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.CommandText = "select * from \"Products\" where \"ProductID\" = (@Id)";
                comm.Parameters.AddWithValue("Id", id);

                NpgsqlDataReader sdr = comm.ExecuteReader();
                sdr.Read();

                product.ProductId = (int)sdr["ProductID"];
                product.Name = sdr["Name"].ToString();
                if (sdr["Price"] != DBNull.Value)
                {
                    product.Price = (decimal)sdr["Price"];
                }
                else
                {
                    product.Price = 0;
                }
                if (sdr["BuyPrice"] != DBNull.Value)
                {
                    product.BuyPrice = (decimal)sdr["BuyPrice"];
                }
                else
                {
                    product.BuyPrice = 0;
                }

                if (sdr["Stock"] != DBNull.Value)
                {
                    product.Stock = (int)sdr["Stock"];
                }
                else
                {
                    product.Stock = 0;
                }
                product.Manufacturer = sdr["Manufacturer"].ToString();
                product.Description_Short = sdr["Description_Short"].ToString();
                product.Description_Full = sdr["Description_Full"].ToString();
                product.Breakpoints = sdr["Breakpoints"].ToString();
                product.Img = sdr["Img"].ToString();
                return product;
            }
            catch (NpgsqlException e)
            {
                TempData["msg"] = $"Error occured with product [@id={id}][@msg={e.Message}]";
                return GetNewProduct();
            }
        }

        private List<ProductModel> GetDisplayedProducts(bool all = true)
        {
            List<ProductModel> dp = new(); // displayed products
            try
            {
                NpgsqlConnection conn = new(_connStr);
                conn.Open();
                NpgsqlCommand comm = new()
                {
                    Connection = conn,
                    CommandType = CommandType.Text,
                    CommandText = "select * from \"Products\" order by \"ProductID\" asc"
                };

                NpgsqlDataReader sdr = comm.ExecuteReader();

                while (sdr.Read())
                {
                    var productlist = new ProductModel();
                    productlist.ProductId = (int)sdr["ProductID"];
                    productlist.Manufacturer = sdr["Manufacturer"].ToString();
                    productlist.Name = sdr["Name"].ToString();
                    if (sdr["Price"] != DBNull.Value)
                    {
                        productlist.Price = (decimal)sdr["Price"];
                    }
                    else
                    {
                        productlist.Price = 0;
                    }

                    if (sdr["BuyPrice"] != DBNull.Value)
                    {
                        productlist.BuyPrice = (decimal)sdr["BuyPrice"];
                    }
                    else
                    {
                        productlist.BuyPrice = 0;
                    }

                    if (sdr["Stock"] != DBNull.Value)
                    {
                        productlist.Stock = (int)sdr["Stock"];
                    }
                    else
                    {
                        productlist.Stock = 0;
                    }
                    productlist.Img = sdr["Img"].ToString();
                    productlist.Description_Short = sdr["Description_Short"].ToString();
                    productlist.Description_Full = sdr["Description_Full"].ToString();
                    productlist.Breakpoints = sdr["Breakpoints"].ToString();
                    if (all || productlist.Stock > 0)
                    {
                        dp.Add(productlist);
                    }
                }
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e);
                TempData["msg"] = $"Error - {e.Message}";
            }

            return dp;
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                NpgsqlConnection conn = new(_connStr);
                conn.Open();
                var rowsAffected = 0;
                using (var cmd = new NpgsqlCommand(
                    "DELETE FROM \"Products\" WHERE \"ProductID\" = (@p)", conn))
                {
                    cmd.Parameters.AddWithValue("p", id);
                    rowsAffected = cmd.ExecuteNonQuery();
                }

                if (rowsAffected == 1)
                {
                    TempData["msg"] = $"Deleted Successfully [@id={id}]";
                }
                else
                {
                    TempData["msg"] = $"Product not found [@id={id}]";
                }
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e);
                TempData["msg"] = $"Error - {e.Message} [id={id}]";
                //throw;
            }

            return Redirect("~/Store/Warehouse");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult UpdateInsert(int id, string name, decimal? price,
            decimal? buyPrice, int? stock, string manufacturer,
            string Description_Short, string Description_Full, string Img, string Breakpoints)
        {
            try
            {
                NpgsqlParameter pId = new("@ProductId", id);
                NpgsqlParameter pName = new("@Name", DBNull.Value);
                if (!string.IsNullOrEmpty(name)) pName.Value = name;
                NpgsqlParameter pManufacturer = new("@Manufacturer", manufacturer);
                if (!string.IsNullOrEmpty(manufacturer)) pManufacturer.Value = manufacturer;
                NpgsqlParameter pPrice = new("@Price", price);
                NpgsqlParameter pBuyPrice = new("@BuyPrice", buyPrice);
                NpgsqlParameter pStock = new("@Stock", stock);
                NpgsqlParameter pDescriptionShort = new("@Description_Short", DBNull.Value);
                if (!string.IsNullOrEmpty(Description_Short)) pDescriptionShort.Value = Description_Short;
                NpgsqlParameter pDescriptionFull = new("@Description_Full", DBNull.Value);
                if (!string.IsNullOrEmpty(Description_Full)) pDescriptionFull.Value = Description_Full;
                NpgsqlParameter pImg = new("@Img", DBNull.Value);
                if (!string.IsNullOrEmpty(Img)) pImg.Value = Img;
                NpgsqlParameter pBreakpoints = new("@Breakpoints", DBNull.Value);
                if (!string.IsNullOrEmpty(Breakpoints)) pBreakpoints.Value = Breakpoints;

                var pReturnedProductId = _helper.ExecuteSQL_Scalar<int>(_connStr, @$"
                insert INTO ""Products"" as p
                (""ProductID"", ""Name"", ""Price"", ""BuyPrice"",
                ""Stock"", ""Manufacturer"", ""Description_Short"",
                ""Description_Full"", ""Img"", ""Breakpoints"") 
                VALUES (@ProductId, @Name, @Price, @BuyPrice, 
                @Stock, @Manufacturer, @Description_Short, 
                @Description_Full, @Img, @Breakpoints)
                on conflict (""ProductID"") do update 
                set ""Name"" = @Name, ""Price"" = @Price, ""BuyPrice"" = @BuyPrice,
                ""Stock"" = @Stock, ""Manufacturer"" = @Manufacturer, ""Description_Short"" = @Description_Short, 
                ""Description_Full"" = @Description_Full, ""Img"" = @Img, ""Breakpoints"" = @Breakpoints
                where p.""ProductID"" = @ProductID
                returning p.""ProductID""",
                    pId, pName, pPrice,pBuyPrice, 
                    pStock, pManufacturer, pDescriptionShort, 
                    pDescriptionFull, pImg, pBreakpoints);
                if (pReturnedProductId == id)
                {
                    TempData["msg"] = $"Updated item [@id={id}]";
                }
                else
                {
                    TempData["msg"] = $"Added item [@id={pReturnedProductId}]";
                }
            }
            catch (NpgsqlException e)
            {
                TempData["msg"] = $"Error : {e.Message} [@id={id}]";
            }
            return Redirect("~/Store/Warehouse");
        }

     
    }
}