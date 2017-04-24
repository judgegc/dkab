using dkab.Extension;
using dkab.Helpers;
using RGiesecke.DllExport;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace dkab
{
    public class DllEntry
    {
        [DllExport("_RVExtension@12", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi)]
        public static void RVExtension(StringBuilder output, int outputSize, [MarshalAs(UnmanagedType.LPStr)] string function)
        {
            try
            {
                CommandsProcessor proc = new CommandsProcessor();
                ICommand cmd = proc.Parse(function);

                output.Append(cmd.exec());

            }catch(Exception ex)
            {
                Logger.StackTrace(ex);
            }
        }
    }
}