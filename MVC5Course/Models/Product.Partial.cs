namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using ValidationAttributes;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product : IValidatableObject
    {
        public int 訂單數量
        {
            get
            {
                return this.OrderLine.Count();
                //return this.OrderLine.Count(p => p.Qty > 400);
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Stock < 10)
            {
                yield return new ValidationResult("庫存數量過少", new string[] { "Stock" });
            }

            if (this.Price < 50)
            {
                yield return new ValidationResult("價格太便宜", new string[] { "Price" });
            }
            yield break;
        }
    }
    
    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "請輸入商品名稱")]
        [商品名稱必須包含Nas字串(ErrorMessage = "商品名稱必須包含Nas字串")]
        //[RegularExpression("(.+)-(.+)", ErrorMessage = "商品名稱格式錯誤")]
        //[StringLength(80, ErrorMessage="欄位長度不得大於 80 個字元")]
        [DisplayName("商品名稱")]
        public string ProductName { get; set; }
        [Required]
        [Range(0, 9999999, ErrorMessage = "請設定正確的商品價格範圍")]
        [DisplayFormat(DataFormatString = "{0:0}")]
        [DisplayName("商品價格")]
        public Nullable<decimal> Price { get; set; }
        [Required]
        [DisplayName("商品上架")]
        public Nullable<bool> Active { get; set; }
        [Required]
        [DisplayName("商品庫存")]
        //[Range(0, 100, ErrorMessage = "請設定正確的商品價格範圍")]
        public Nullable<decimal> Stock { get; set; }
    
        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
