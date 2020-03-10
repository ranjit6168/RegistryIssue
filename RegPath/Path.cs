using System;
using System.Management.Automation;
using Microsoft.Win32;

namespace HelloCisco
{
    [Cmdlet(VerbsCommon.Get, "Path")]
    [OutputType(typeof(String))]
    public class GetPath : Cmdlet
    {

        [Parameter(Position = 1, ValueFromPipelineByPropertyName = true)]
        public string Name { get; set; }

       protected override void BeginProcessing()
        {
            base.BeginProcessing();
        }

        protected override void ProcessRecord()
        {
            string javapath = String.Empty;
            string path = String.Empty;
            using (Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\JavaSoft\\Java Runtime Environment\\"))
            {
                if (rk != null)
                {
                    string currentVersion = rk.GetValue("CurrentVersion").ToString();
                    if (currentVersion != null)
                    {
                        using (Microsoft.Win32.RegistryKey key = rk.OpenSubKey(currentVersion))
                        {
                            if (key != null)
                                path = key.GetValue("JavaHome").ToString();
                        }
                    }
                }
            }
            if (Name != null)
            {
                javapath += path;
            }

            WriteObject(javapath);
        }

    }
}
