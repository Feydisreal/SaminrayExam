using Microsoft.EntityFrameworkCore;
using SaminrayExam.Saminray.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaminrayExam.Saminray.Core
{
    public class ProductCountService
    {

        SaminrayExamContext context = new SaminrayExamContext();


        public void ReduceCount()
        {
            Console.WriteLine("Please Select Product by number:");

            var products = context.Products.ToList();
            foreach (var item in products)
            {
                Console.WriteLine(item.ProductId + ":" + item.Name);
            }
            var response = GetInput();
            var selected = context.Products
                .Include(x => x.ProductGroup)
                .FirstOrDefault(x => x.ProductId == int.Parse(response));

            Console.WriteLine("How Many Do you Want To Reduce , Current = {0}", selected.Count);
            selected.Count -= int.Parse(GetInput());
            context.Update(selected);
            context.SaveChanges();

            Console.WriteLine(selected.Name + " new Count is :" + selected.Count);
            AppService.ReturnToMainMenu();
        }
        public void AddCount()
        {
            Console.WriteLine("Please Select Product by number:");

            var products = context.Products.ToList();
            foreach (var item in products)
            {
                Console.WriteLine(item.ProductId + ":" + item.Name);
            }
            var response = GetInput();
            var selected = context.Products
                .Include(x => x.ProductGroup)
                .FirstOrDefault(x => x.ProductId == int.Parse(response));

            Console.WriteLine("How Many Do you Want To Add , Current = {0}",selected.Count);
            selected.Count += int.Parse(GetInput());
            context.Update(selected);
            context.SaveChanges();

            Console.WriteLine(selected.Name + " new Count is :" + selected.Count);
            AppService.ReturnToMainMenu();
        }

        public string GetInput()
        {
            var res = Console.ReadLine();
            Console.Clear();
            return res;
        }
    }
}
