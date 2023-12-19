using Microsoft.EntityFrameworkCore;
using SaminrayExam.Saminray.Data.Context;
using SaminrayExam.Saminray.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SaminrayExam.Saminray.Core
{
    public class ProductGroupService
    {
        SaminrayExamContext context = new SaminrayExamContext();
        public void ProductGroupMenu()
        {
            Console.WriteLine("1.Add New Group");
            Console.WriteLine("2.Product Group List");
            Console.WriteLine("3.Edit a Product Group");
            Console.WriteLine("4.Delete a Product Group");
            Console.WriteLine("6.Return");
            int answer;
            int.TryParse(Console.ReadLine(),out answer);
            Console.Clear();
            CheckUserInputForProductGroup(answer);

        }

        public void DeleteProductGroup()
        {
            if (!context.ProductGroups.Any())
            {
                Console.WriteLine("There is no Product Groups in our Database");
                AppService.ReturnToMainMenu();
            }
            Console.WriteLine("Please Select Group by number:");

            var products = context.ProductGroups.ToList();
            foreach (var item in products)
            {
                Console.WriteLine(item.ProductGroupId + ":" + item.Name);
            }
            int response;
            if (int.TryParse(AppService.GetInput(), out response))
            {
                var selected = context.ProductGroups
               .FirstOrDefault(x => x.ProductGroupId == response);
                int childs = 0;
                if (context.Products.Any(x=> x.ProductGroupRef == selected.ProductGroupId))
                {
                    childs = context.Products.Where(x => x.ProductGroupRef == selected.ProductGroupId).Count();
                    Console.WriteLine("You are Going To also delete {0} Prouduct with this group", childs);
                }
                Console.WriteLine("Are You Sure About Deleting {0} ? Y/N", selected.Name);
                var res = AppService.GetInput();
                CheckInputAndDelete(res, selected, childs);
            }
            else
            {
                Console.WriteLine("please write a Correct number");
                DeleteProductGroup();
            }
           
            AppService.ReturnToMainMenu();
        }
        public void EditProductGroup()
        {
            if (!context.ProductGroups.Any())
            {
                Console.WriteLine("There is no Product Groups in our Database");
                AppService.ReturnToMainMenu();
            }
            Console.WriteLine("Please Select Group by number:");

            var products = context.ProductGroups.ToList();
            foreach (var item in products)
            {
                Console.WriteLine(item.ProductGroupId + ":" + item.Name);
            }
            var response = AppService.GetInput();
            var selected = context.ProductGroups
                .FirstOrDefault(x => x.ProductGroupId == int.Parse(response));
            Console.WriteLine("Please Enter New Group Name (Previous {0}):", selected.Name);
            selected.Name = AppService.GetInput();

            context.Update(selected);
            context.SaveChanges();

            Console.WriteLine(selected.Name + " Updated in Data base");
            AppService.ReturnToMainMenu();
        }
        public void GetProductGroupList()
        {
            var groups = context.ProductGroups.ToList();
            foreach (var group in groups)
            {
                Console.WriteLine(group.ProductGroupId + ":" +group.Name);
            }

            AppService.ReturnToMainMenu();
        }
        public void AddNewGroup()
        {
            
            Console.WriteLine("Eneter a Name for A new Group");
            var res = AppService.GetInput();
            var newGroup = new ProductGroup()
            {
                Name = res,
            };
            context.Add(newGroup);
            context.SaveChanges();
            Console.WriteLine(newGroup.Name + " Added to Data base");
            AppService.ReturnToMainMenu();
        }
        public void CheckInputAndDelete(string yesAndNo, ProductGroup group, int childNumbers)
        {
            if (yesAndNo.ToLower() == "y")
            {
                context.Remove(group);
                context.SaveChanges();
                Console.WriteLine(group.Name + " Deleted");
                AppService.ReturnToMainMenu();
            }
            else if (yesAndNo.ToLower() == "n")
            {
                AppService.ReturnToMainMenu();
            }
            else
            {
                Console.WriteLine("Please answer With Y/N");
                if (childNumbers != 0)
                {
                    Console.WriteLine("You are Going To also delete {0} Prouduct with this group", childNumbers);
                }
                Console.WriteLine("Are You Sure About Deleting {0} ?", group.Name);
                var newResponse = AppService.GetInput();
                CheckInputAndDelete(newResponse, group , childNumbers);
            }

        }
        public void CheckUserInputForProductGroup(int answer)
        {
               switch (answer)
            {
                case 1:
                    AddNewGroup();
                    break;


                case 2:
                    GetProductGroupList();
                    break;

                case 3:
                    EditProductGroup();
                    break;

                case 4:
                    DeleteProductGroup();
                    break;

                case 5:
                    AppService.Start();
                    break;
                default:
                    Console.WriteLine("Please Write a Correct Number");
                    ProductGroupMenu();
                    break;
            }
        }
      
    }
}
