using SaminrayExam.Saminray.Data.Context;
using SaminrayExam.Saminray.Data.Entities;
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

           
            Console.WriteLine("1.Products");
            Console.WriteLine("2.Product Groups");
            Console.WriteLine("3.Add Product Count");
            Console.WriteLine("4.Reduce Product Count");
            Console.WriteLine("5.Product Details");
            Console.WriteLine("6.Exit");
            int answer;
            int.TryParse(Console.ReadLine(), out answer);
            Console.Clear();
            CheckInput(answer);


        }
        public static void ReturnToMainMenu()
        {
            Console.WriteLine("Enter Any thing To return to Main Menu");
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
     
        public static void CheckInput(int input)
        {
            var product = new ProductService();
            var group = new ProductGroupService();
            var count = new ProductCountService();

            switch (input)
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
                default:
                    Console.WriteLine("Please Write a correct number");
                    Start();
                    break;

            }
        }
    }
}
