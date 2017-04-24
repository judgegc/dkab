namespace dkab.Extension.Commands
{
    [CommandName("iscon")]
    public class ConnectionChecking : ICommand
    {
        public ConnectionChecking()
        {         
        }
        public string exec()
        {
            return (ExtensionEntry.Instance.GameClient.IsConnected).ToString().ToLower();
        }
    }
}
