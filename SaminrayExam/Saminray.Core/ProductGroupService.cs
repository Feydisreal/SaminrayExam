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
            var answer = int.Parse(Console.ReadLine());
            Console.Clear();
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

            }

        }

        public void DeleteProductGroup()
        {
            Console.WriteLine("Please Select Group by number:");

            var products = context.ProductGroups.ToList();
            foreach (var item in products)
            {
                Console.WriteLine(item.ProductGroupId + ":" + item.Name);
            }
            var response = AppService.GetInput();
            var selected = context.ProductGroups
                .FirstOrDefault(x => x.ProductGroupId == int.Parse(response));
            Console.WriteLine("Are You Sure About Deleting {0} ? Y/N",selected.Name);
            var res = AppService.GetInput();
            if (res.ToLower() == "y")
            {
                context.Remove(selected);
                context.SaveChanges();
                Console.WriteLine();
                AppService.ReturnToMainMenu();
            }
            else
            {
                AppService.ReturnToMainMenu();
            }

            AppService.ReturnToMainMenu();
        }
        public void EditProductGroup()
        {
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
            Console.WriteLine(newGroup + " Added to Data base");
            AppService.ReturnToMainMenu();
        }

      
    }
}
