namespace dkab.Extension.Commands
{
    [CommandName("init")]
    public class Init : ICommand
    {
        public Init()
        {         
        }
        public string exec()
        {
            return (ExtensionEntry.Instance.GameClient != null).ToString().ToLower();
        }
    }
}
