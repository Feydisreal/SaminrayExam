using Microsoft.EntityFrameworkCore;
using SaminrayExam.Saminray.Data.Context;
using SaminrayExam.Saminray.Data.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace SaminrayExam.Saminray.Core
{
    
    public class ProductService
    {
        SaminrayExamContext context = new SaminrayExamContext();
        ProductGroupService groupService = new ProductGroupService();
        public ProductService()
        {
        }
        
        public void ProductMenu()
        {
            Console.WriteLine("1.Add New Product");
            Console.WriteLine("2.Product List");
            Console.WriteLine("3.Search for a Product");
            Console.WriteLine("4.Edit a Product");
            Console.WriteLine("5.Delete a Product");
            Console.WriteLine("6.Return"); 
            int answer;
            int.TryParse(Console.ReadLine(), out answer);
            Console.Clear();
            CheckUserInputForProduct(answer);

        }

        public void AllProductWithDetails()
        {
            if (context.Products.Any())
            {
                var products = context.Products
                    .Include(x=> x.ProductGroup)
                    .ToList();
                foreach (var item in products)
                {
                    Console.WriteLine("id: {0},Name: {1}, Price: {2}, Count: {3}, Group: {4}",item.ProductId,item.Name,item.Price,item.Count,item.ProductGroup.Name);
                }
                AppService.ReturnToMainMenu();
            }
            else
            {
                Console.WriteLine("There is no Product in our Database , Add a new one");
                AppService.ReturnToMainMenu();
            }

        }
        public void AddProduct()
        {
           
            Console.WriteLine("Please Enter Product Name :");
            var productName = Console.ReadLine();
            Console.Clear() ;

            Console.WriteLine("Please Enter Product Price :");
            var price = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Please Enter Product Count :");
            var count = Console.ReadLine();
            Console.Clear();

            
            var groups = context.ProductGroups.ToList();
            if (groups.Count != 0)
            {
                Console.WriteLine("Please Select Product Group by Number:");
                foreach (var item in groups)
                {
                    Console.WriteLine(item.ProductGroupId + ":" + item.Name + " ");
                }
            }
            else
            {
                Console.WriteLine("There is no Group , Please Create New Group");
                groupService.AddNewGroup();
            }
            var group = Console.ReadLine();
            Console.Clear();

            context.Products.Add(new Product()
            {
                Name = productName,
                ProductGroupRef = int.Parse(group),
                Price = float.Parse(price),
                Count = int.Parse(count)

            });
            context.SaveChanges();
            Console.WriteLine(productName + " Added to Data base");
            AppService.ReturnToMainMenu();
        }

        public void RemoveProduct() {
            if (!context.Products.Any())
            {
                Console.WriteLine("There is no Product in our Database");
                AppService.ReturnToMainMenu();
            }
            Console.WriteLine("Please Select Product by number:");
            var products = context.Products.ToList();
            foreach (var item in products)
            {
                Console.WriteLine(item.ProductId + ":" + item.Name);
            }
            int response;

            if (int.TryParse(AppService.GetInput(),out response))
            {
                var selected = context.Products.SingleOrDefault(x => x.ProductId == response);
                Console.WriteLine("Are You Sure About Deleting {0} ? Y/N", selected.Name);
                var res = AppService.GetInput();
                CheckInputAndDelete(res, selected) ;
            }
            else
            {
                Console.WriteLine("Please Write a Correct number!");
                RemoveProduct();
            }
            
           
        }
        public void CheckInputAndDelete(string yesAndNo, Product product)
        {
            if (yesAndNo.ToLower() == "y")
            {
                context.Remove(product);
                context.SaveChanges();
                Console.WriteLine(product.Name + " Deleted");
                AppService.ReturnToMainMenu();
            }
            else if (yesAndNo.ToLower() == "n")
            {
                AppService.ReturnToMainMenu();
            }
            else
            {
                Console.WriteLine("Please answer With Y/N");

                Console.WriteLine("Are You Sure About Deleting {0} ?", product.Name);
                var newResponse = AppService.GetInput();
                CheckInputAndDelete(newResponse,product);
            }
            
        }


        public void UpdateProduct() {
            if (!context.Products.Any())
            {
                Console.WriteLine("There is no Product in our Database");
                AppService.ReturnToMainMenu();
            }
            Console.WriteLine("Please Select Product by number:");

           var products = context.Products.ToList();
            foreach (var item in products)
            {
                Console.WriteLine(item.ProductId + ":" + item.Name);
            }
            int response;
            
            
            if (int.TryParse(AppService.GetInput(), out response))
            {
                ApplyUpdateById(response);
            }
            else
            {
                Console.WriteLine("Please Use Correct Number");
                UpdateProduct();
            }
           

            
        }
        public void ApplyUpdateById(int id)
        {
            var selected = context.Products
              .Include(x => x.ProductGroup)
              .FirstOrDefault(x => x.ProductId == id);
            Console.WriteLine("Please Enter New Product Name (Previous {0}):", selected.Name);
            selected.Name = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Please Enter New Product Price (Previous {0}):", selected.Price);
            selected.Price = float.Parse(Console.ReadLine());
            Console.Clear();

            Console.WriteLine("Please Enter New Product Count (Previous {0}):", selected.Count);
            selected.Count = int.Parse(Console.ReadLine());
            Console.Clear();


            var groups = context.ProductGroups.ToList();
            if (groups.Count != 0)
            {
                Console.WriteLine("Please Select New Product Group by Number (Previous {0}):", selected.ProductGroup.Name);
                foreach (var item in groups)
                {
                    Console.WriteLine(item.ProductGroupId + ":" + item.Name + " ");
                }
            }
            else
            {
                Console.WriteLine("There is no Group , Please Create New Group");
                groupService.AddNewGroup();
            }
            selected.ProductGroupRef = int.Parse(Console.ReadLine());
            Console.Clear();
            context.Update(selected);
            context.SaveChanges();
            Console.WriteLine(selected.Name + " Edited");
            AppService.ReturnToMainMenu();
        }

        public void ProductList()
        {
            var contwerext = new SaminrayExamContext();
            if (contwerext.Products.Any())
            {
                var products = contwerext.Products.ToList();
                foreach (var item in products)
                {
                    Console.WriteLine(item.ProductId + "." + item.Name);
                }
                AppService.ReturnToMainMenu();
            }
            else
            {
                Console.WriteLine("There is no Product in our DataBase");
               AppService.ReturnToMainMenu();
            }
        }
    
        public void SearchForAProduct()
        {
            Console.WriteLine("Enter Product Name:");
            var res = AppService.GetInput();
           
                if (context.Products.Any(p => p.Name == res))
                {
                    var found = context.Products.Include(x => x.ProductGroup)
                        .FirstOrDefault(p => p.Name == res);
                    WriteProductInfo(found);

                }
                else
                {
                    Console.WriteLine("There is no Product With that name in our DataBase");
                AppService.ReturnToMainMenu();
                }
        }
        public void WriteProductInfo(Product product)
        {
            Console.WriteLine("Product :" + product.Name);
            Console.WriteLine("Price :" + product.Price);
            Console.WriteLine("Count :" + product.Count);
            Console.WriteLine("Group :" + product.ProductGroup.Name);
            Console.WriteLine();
            AppService.ReturnToMainMenu();
        }
     public void CheckUserInputForProduct(int answer)
        {
            switch (answer)
            {
                case 1:
                    AddProduct();
                    break;
                case 2:
                    ProductList();
                    break;

                case 3:
                    SearchForAProduct();
                    break;

                case 4:
                    UpdateProduct();
                    break;

                case 5:
                    RemoveProduct();
                    break;

                case 6:
                    AppService.Start();
                    break;
                default:
                    Console.WriteLine("Please Write a correct number");
                    ProductMenu();
                    break;

            }
        }
      
    }
}
