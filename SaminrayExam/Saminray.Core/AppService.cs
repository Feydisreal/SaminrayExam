using SaminrayExam.Saminray.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaminrayExam.Saminray.Core
{
    public static class AppService
    {
       
        public static void Start()
        {
            
            var product = new ProductService();
            var group = new ProductGroupService();
            var count = new ProductCountService();
            Console.WriteLine("1.Products");
            Console.WriteLine("2.Product Groups");
            Console.WriteLine("3.Add Product Count");
            Console.WriteLine("4.Reduce Product Count");
            Console.WriteLine("5.Product Details");
            Console.WriteLine("6.Exit");
            var answer = int.Parse(Console.ReadLine());
            Console.Clear();
           switch (answer)
            {
                case 1:
                    
                    product.ProductMenu();
                    break;


                case 2:
                   group.ProductGroupMenu();
                    break;

                case 3:
                    count.AddCount();
                    break;

                case 4:
                   count.ReduceCount();
                    break;

                case 5:
                    product.AllProductWithDetails();
                    break;

                case 6:
                   
                    break;

            }
        }
        public static void ReturnToMainMenu()
        {
            Console.WriteLine("Enter Any thing To return");
            var returnto = Console.ReadLine();
            if (returnto != null)
            {
                Console.Clear();
                AppService.Start();
            }

        }
        public static string GetInput()
        {
            var res = Console.ReadLine();
            Console.Clear();
            return res;
        }
    }
}
