using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ViewModel
{
    /// <summary>
    /// 給特定的View使用
    /// </summary>
    public class ProductLiteVM
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "請輸入商品名稱")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "請輸入商品價格")]
        public Nullable<decimal> Price { get; set; }
        [Required(ErrorMessage = "請輸入商品庫存")]
        public Nullable<decimal> Stock { get; set; }
    }
}