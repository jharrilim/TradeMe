using System.Net.Mail;
using System.Security;

namespace TradeMe.Console
{
    public class User : Shareholder
    {
        private readonly SecureString password;
        public MailAddress Email { get; }
        public User(string name, decimal initialBalance, string email, SecureString password) : base(name, initialBalance)
        {
            Email = new MailAddress(email);
            this.password = password;
        }
    }
}
