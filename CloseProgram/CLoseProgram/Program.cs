using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLoseProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter program name:");
            String name = Console.ReadLine();
            Console.WriteLine("----- start searching ----- ");


            Process[] runningProcesses = Process.GetProcesses();
            bool isExist = false;
            foreach (Process process in runningProcesses)
            {
                try
                {
                    // now check the modules of the process
                    foreach (ProcessModule module in process.Modules)
                    {
                        try
                        {
                            String filename = module.FileName;
                            if (!filename.ToLower().StartsWith("c:\\windows\\") 
                                && !filename.ToLower().StartsWith("c:\\users\\") 
                                && !filename.ToLower().StartsWith("c:\\program files")
                                && !filename.ToLower().StartsWith("c:\\programdata\\")
                            )
                            {
                                Console.WriteLine(module.FileName);

                                if (filename.EndsWith(name))
                                {
                                    isExist = true;
                                    process.Kill();

                                    Console.WriteLine("Process killed.");
                                    break;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("[Exception] - " + ex.Message);
                        }
                    }
                }
                catch (Win32Exception ex)
                {
                    Console.WriteLine("[Exceptoin] - " + ex.Message);
                }

                if (isExist)
                {
                    break;
                }
            }

            if (!isExist)
                Console.WriteLine("\n" +　name + " not found");

            Console.WriteLine("---- Press to close -----");
            Console.ReadLine();
        }
    }
}
