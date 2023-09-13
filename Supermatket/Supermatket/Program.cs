using Supermatket.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermatket
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProductDbService dbService = new ProductDbService();
            dbService.GetAllProducs();
            // dbService.GetProductByName("FANTA");
            // dbService.GetProductById(1);
           // dbService.CreateProduct(1, "Sptire", 9000);
            
           // CategoryDbService categoryDbService = new CategoryDbService();
           // categoryDbService.ReadCategories();


        }
    }
}
