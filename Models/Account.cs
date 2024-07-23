namespace WebMVC2.Models
{
    public interface IAccount
    {
        public string GetName();

        public void SetName(string name);
        
    }
    public class Account
    {
        public string Username { get; set; } = "13333";
    }

    public class AccountInfo : IAccount
    {
        private Account _account;

        public AccountInfo(Account account)
        {
            _account = account;
        }

        public string GetName()
        {
            return _account.Username;
        }

        public void SetName(string name)
        {
            _account.Username = name;
        }
    }
}
