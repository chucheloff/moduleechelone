using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModuleEcheloneRebooted.Models
{
    public class ProductModel
    {
        [Key]
        public int ProductID { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal BuyPrice { get; set; }
    public int Stock { get; set; }
    public string Manufacturer { get; set; }
    public string Description_Short { get; set; }
    public string Description_Full { get; set; }
    public string Img { get; set; }
    
    public String[] Breakpoints { get; set; }

    public ProductModel()
    {
        
    }
    
}

}