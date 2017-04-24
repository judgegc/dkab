namespace dkab.Extension.Commands
{
    [CommandName("connect")]
    public class Connect : ICommand
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Connect(string username, string password)
        {
            Username = username;
            Password = password;
        }
        public string exec()
        {
            ExtensionEntry.Instance.Connect(Username, Password);
            return true.ToString().ToLower();
        }
    }
}
