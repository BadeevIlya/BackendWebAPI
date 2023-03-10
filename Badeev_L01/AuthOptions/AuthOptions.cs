using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Badeev_L01
{
    public class AuthOptions
    {
        const string Key = "donttouchthat!!!!!!!!!!";
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
