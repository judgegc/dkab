namespace dkab.Extension.Commands
{
    [CommandName("mypos")]
    public class MyDestination : ICommand
    {
        public float X { get; private set; }
        public float Y { get; private set; }

        public MyDestination(float x, float y)
        {
            X = x;
            Y = y;
        }
        public string exec()
        {
            ExtensionEntry.Instance.GameClient.MoveTo(X, Y);
            return true.ToString().ToLower();
        }
    }
}
