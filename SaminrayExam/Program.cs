using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SaminrayExam.Saminray.Core;
using SaminrayExam.Saminray.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaminrayExam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AppService.Start();
        }
    }
}
