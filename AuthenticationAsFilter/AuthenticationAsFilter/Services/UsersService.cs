using AuthenticationAsFilter.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace AuthenticationAsFilter.Services
{
    public class UsersService
    {
        private List<User> _usersCollection;
        private IOptions<AuthenticationAsFilter.Configuration.AuthenticationSettings> Settings;

        public UsersService(IOptions<AuthenticationAsFilter.Configuration.AuthenticationSettings> settings)
        {
            this.Settings= settings;
            this._usersCollection =new List<User>() {
                new User
                {
                    Id = "4229C87220572990A39DC647",
                    Name= "D",
                    Email="d@mail.com",
                    Password="dpass+wordj"
                },
                new User
                {
                    Id = "4229C87220572990A39DC648",
                    Name= "J",
                    Email="j@mail.com",
                    Password="jpass+wordd"
                } 
            };
        }

        public IEnumerable<User> GetUsers()
        {
            var users = this._usersCollection;
            return users;
        }

        public User? GetUserByLogin(string email, string password)
        {
            var user = this._usersCollection.Find(x => x.Email == email && x.Password == password);

            if (user == null)
                return null;

            return user;
        }



        public string? Authenticate(string email, string password)
        {
            var user = this._usersCollection.Find(x => x.Email == email && x.Password == password);

            if (user == null)
                return null;

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("4229C87220572990A39DC647"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: "https://localhost:7262",
                audience: "https://localhost:7262",
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(600),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;

            //var tokenHandler = new JwtSecurityTokenHandler();

            //var tokenKey = Encoding.ASCII.GetBytes(this.Settings.Value.JwtKey);
            //var tokenDescription = new SecurityTokenDescriptor()
            //{
            //    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
            //    {
            //    new Claim(ClaimTypes.Email, email),
            //    }),
            //    Expires = DateTime.UtcNow.AddHours(5),
            //    SigningCredentials = new SigningCredentials(
            //            new SymmetricSecurityKey(tokenKey),
            //            SecurityAlgorithms.HmacSha256Signature
            //            )
            //};
            //var token = tokenHandler.CreateToken(tokenDescription);

            //return tokenHandler.WriteToken(token);

        }
    }
}
