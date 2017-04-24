namespace dkab.Extension.Commands
{
    [CommandName("disconnect")]
    class Disconnect: ICommand
    {
        public Disconnect()
        {
        }
        public string exec()
        {
            ExtensionEntry.Instance.GameClient.Disconnect();
            return true.ToString().ToLower();
        }
    }
}
