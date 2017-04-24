using System.Diagnostics;
using System.Reflection;

namespace dkab.Extension.Commands
{
    [CommandName("version")]
    public class Version : ICommand
    {
        public Version()
        {
        }
        public string exec()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fvi.FileVersion;
        }
    }
}
