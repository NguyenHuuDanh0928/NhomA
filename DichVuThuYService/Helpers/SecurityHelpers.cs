namespace DichVuThuYService.Helpers
{
    namespace WebDichVu.Helpers
    {
        public static class SecurityHelpers
        {
            public static string GenerateRandomKey()
            {
                // Tạo một chuỗi ngẫu nhiên dùng làm salt
                return Guid.NewGuid().ToString("N").Substring(0, 8); // 8 ký tự ngẫu nhiên
            }
        }
    }

}

