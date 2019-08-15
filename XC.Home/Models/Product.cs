using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace XC.Home.Models
{
    public class Product
    {
        public Product()
        {

        }
        [DisplayName(displayName: "产品系列")]
        public string ProductSerial { get; set; }

        [DisplayName(displayName: "产品编号")]
        public string ProductNo { get; set; }

        [DisplayName(displayName: "产品名称")]
        public string ProductName { get; set; }

        [DisplayName(displayName: "价格")]
        public decimal Price { get; set; }

        [DisplayName(displayName: "产品描述")]
        public string Description { get; set; }

        public List<ProductFeature> feature { get; set; }

    }

    public class ProductFeature
    {
        [DisplayName(displayName: "产品编号")]
        public string ProductNo { get; set; }

        [DisplayName(displayName: "产品特征")]
        public string Feature { get; set; }

        [DisplayName(displayName: "特征描述")]
        public string Description { get; set; }
    }
}
