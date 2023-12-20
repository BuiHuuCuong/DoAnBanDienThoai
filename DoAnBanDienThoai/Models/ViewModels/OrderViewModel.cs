namespace DoAnBanDienThoai.Models.ViewModels
{
    public class OrderViewModel
        // Tao truong du lieu cho cac thong tin san pham
    {
        public List<CartItem> CartItems { get; set; }
        public decimal GrandTotal { get; set; }
        // Tao truong du lieu cac thong tin cua khach hang
        public List<Order>? orders { get; set; }
        
    }
}
