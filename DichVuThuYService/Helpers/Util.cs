using System.Text;

namespace DichVuThuYService.Helpers
{
    public class Util
    {
        public static string GenerateRamdomKey(int length = 5)

        {
            var pattern = @"qawsxzedcrfvtgbyhnujmiklopQAWZEDCRFVTGBYHNUJMIKLOP!#$%&*";  
            var sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                sb.Append(pattern[new Random().Next(0, pattern.Length)]);
            }
            return sb.ToString();
        }
    }
}
