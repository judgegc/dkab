namespace dkab.Extension.Commands
{
    [CommandName("reset")]
    public class Reset: ICommand
    {
        public string exec()
        {
            ExtensionEntry.Instance.GameClient.Events.Reset();
            return "true";
        }
    }
}
