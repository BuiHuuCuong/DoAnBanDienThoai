namespace DoAnBanDienThoai.Models.ViewModels
{
    public class OrderViewModel
        // Tao truong du lieu cho cac thong tin san pham
    {
        public List<CartItem> CartItems { get; set; }
        public orderDTO OrderDTO { get; set; }
        public decimal GrandTotal { get; set; }

        public class orderDTO
        {
            //public int UserID { get; set; }
           // public int OrderID { get; set; }
            public string? Address { get; set; }
            public string? Email { get; set; }
            public string? Phone { get; set; }

            public DateTime? OrderDate { get; set; }
            public decimal? Total { get; set; }

        }

    }
}
