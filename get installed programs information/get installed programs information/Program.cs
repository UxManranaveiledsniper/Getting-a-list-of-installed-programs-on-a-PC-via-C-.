using System;
using Microsoft.Win32;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string keyName = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";

            RegistryKey programs = Registry.LocalMachine.OpenSubKey(keyName);

            int results = programs.GetSubKeyNames().Length;
            int unknowns = 0;
            string displayName, displayVersion;

            foreach (string key in programs.GetSubKeyNames())
            {
                try
                {
                    displayName =
                        Registry.GetValue(@"HKEY_LOCAL_MACHINE\" + keyName + @"\" + key, "DisplayName", "(unknown software)").ToString();
                    if (displayName == "(unknown software)") unknowns++;
                    displayVersion =
                        Registry.GetValue(@"HKEY_LOCAL_MACHINE\" + keyName + @"\" + key, "DisplayVersion", "(unable to determine software version)").ToString();


                    Console.WriteLine("{0} : {1}", displayName, displayVersion);
                }
                catch
                {
                    Console.WriteLine("Unable to read key: {0}", key);
                }
            }

            Console.WriteLine("Total software found: {0}", results);
            Console.WriteLine("Total unknowns: {0}", unknowns);

            Console.ReadLine();
        }
    }
}
