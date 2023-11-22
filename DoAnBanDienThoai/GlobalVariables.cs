using System.Globalization;
using System.Security.Principal;

namespace DoAnBanDienThoai
{
    public static class GlobalVariables
    {
        public static bool MyGlobalVariable { get; set; }
        public static IPrincipal CurrentPrinciple { get; set; }
        public static CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
    }
}
