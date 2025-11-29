using System;
using System.ComponentModel.DataAnnotations;

namespace DoAn_web.Models
{
    // 1. Khai báo Partial Class để mở rộng class FlashSaleItem có sẵn
    [MetadataType(typeof(FlashSaleItemMetaData))]
    public partial class FlashSaleItem
    {
        // Class này để trống, chỉ dùng để gắn metadata
    }

    // 2. Class chứa các quy tắc ràng buộc
    public class FlashSaleItemMetaData
    {
        [Required(ErrorMessage = "Vui lòng chọn sản phẩm")]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá Sale")]
        [Range(1, double.MaxValue, ErrorMessage = "Giá Sale phải lớn hơn 0")]
        public decimal SalePrice { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int SaleQuantityLimit { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày bắt đầu")]
        public System.DateTime StartDate { get; set; }

        
        [Required(ErrorMessage = "Vui lòng chọn ngày kết thúc")]
        public System.DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn trạng thái")]
        public bool IsActive { get; set; }
    }
}