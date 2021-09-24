using System;
using System.Collections.Generic;
using System.Data;
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
        private string connStr;
        private readonly ILogger<StoreController> _logger;
        private Helpers helper;
        private const Boolean updateGuids = false;
        public StoreController(ILogger<StoreController> logger, IConfiguration _configuration)
        {
            _logger = logger;
            connStr = _configuration.GetConnectionString("postgre");
            helper = new Helpers(_configuration);
            if (updateGuids) {helper.UpdateAllGuid();}
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
        public IActionResult ProductPage(int Id)
        {
            ProductModel product = GetProductDetails(Id);
            return View("ProductPage",product);
        }
        
        [Route("Store/ProductDetails/{id}")] 
        public IActionResult ProductDetails(int Id)
        {
            ProductModel product = GetProductDetails(Id);
            return View("ProductDetails",product);
        }
        
        private ProductModel GetProductDetails(int Id)
        {
            ProductModel product = new ProductModel();
            NpgsqlConnection conn = new NpgsqlConnection(connStr);
            conn.Open();
            NpgsqlCommand comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select * from \"Products\" where \"ProductID\" = (@Id)";
            comm.Parameters.AddWithValue("Id", Id);

            NpgsqlDataReader sdr = comm.ExecuteReader();
            sdr.Read();
            return new ProductModel()
            {
                ProductID = (int) sdr["ProductID"],
                Name = sdr["Name"].ToString(),
                Price = (decimal)sdr["Price"],
                BuyPrice = (decimal)sdr["BuyPrice"],
                Stock = (int)sdr["Stock"],
                Manufacturer = sdr["Manufacturer"].ToString(),
                Description_Short = sdr["Description_Short"].ToString(),
                Description_Full = sdr["Description_Full"].ToString(),
                Breakpoints = sdr["Breakpoints"].ToString()?.Split('#'),
                Img = sdr["Img"].ToString()
            };
        }
        private List<ProductModel> GetDisplayedProducts(bool all = true)
        {
            List<ProductModel> dp = new List<ProductModel>(); // displayed products
            try
            {
                NpgsqlConnection conn = new NpgsqlConnection(connStr);
                conn.Open();
                NpgsqlCommand comm = new NpgsqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.Text,
                    CommandText = "select * from \"Products\" order by \"ProductID\" asc"
                };

                NpgsqlDataReader sdr = comm.ExecuteReader();

                while (sdr.Read())
                {
                    var productlist = new ProductModel();
                    productlist.ProductID = (int) sdr["ProductID"];
                    productlist.Manufacturer = sdr["Manufacturer"].ToString();
                    productlist.Name = sdr["Name"].ToString();
                    productlist.Price = (decimal) sdr["Price"];
                    productlist.BuyPrice = (decimal) sdr["BuyPrice"];
                    productlist.Stock = (int) sdr["Stock"];
                    productlist.Img = sdr["Img"].ToString();
                    productlist.Description_Short = sdr["Description_Short"].ToString();
                    productlist.Description_Full = sdr["Description_Full"].ToString();
                    productlist.Breakpoints = sdr["Breakpoints"].ToString()?.Split('#');
                    if (all || productlist.Stock > 0)
                    {
                        dp.Add(productlist);
                    }
                }
            }
            catch (NpgsqlException e){
                Console.WriteLine(e);
                TempData["msg"] = $"Error - {e.Message}"; 
            }
            return dp;
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            int rowsAffected = 0;
            try
            {
                NpgsqlConnection conn = new NpgsqlConnection(connStr);
                conn.Open();
                using (var cmd = new NpgsqlCommand(
                    "DELETE FROM \"Products\" WHERE \"ProductID\" = (@p)", conn))
                {
                    cmd.Parameters.AddWithValue("p", Id);
                    rowsAffected = cmd.ExecuteNonQuery();
                }

                if (rowsAffected == 1)
                {
                    TempData["msg"] = "Deleted Successfully";
                }
                else
                {
                    TempData["msg"] = $"Product with id = {Id} not found";
                }
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e);
                TempData["msg"] = $"Error - {e.Message} [id={Id}]";
                //throw;
            }

            return Redirect("~/Store/Warehouse");

        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}