namespace dkab.Extension.Commands
{
    [CommandName("events")]
    public class PullEvents: ICommand
    {
        public string exec()
        {
            return string.Join("|", ExtensionEntry.Instance.GameClient.Events.Pop());
        }
    }
}
