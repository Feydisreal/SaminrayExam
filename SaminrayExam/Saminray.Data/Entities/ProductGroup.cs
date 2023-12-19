using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaminrayExam.Saminray.Data.Entities
{
    public class ProductGroup
    {
        [Key]
        public int ProductGroupId { get; set; }
        public string Name { get; set; }

        #region InverseProperties
        [InverseProperty("ProductGroup")] public virtual List<Product> Products { get; set; }
        #endregion
    }
}
