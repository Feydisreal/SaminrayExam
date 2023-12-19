using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaminrayExam.Saminray.Data.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Count { get; set; }
        public int ProductGroupRef { get; set; }

        #region Foreign Keys
        [ForeignKey("ProductGroupRef")] public virtual ProductGroup ProductGroup { get; set; } 

        #endregion

    }
}
