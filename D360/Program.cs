using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace D360
{
    static class Program
    {


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

#if !DEBUG
            try
            {
#endif
                Application.Run(new Form1());

#if !DEBUG
            }
            catch (Exception ex)
            {
                string crashPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\crash.txt";
                using (StreamWriter outfile = new StreamWriter(crashPath, true))
                {
                    outfile.WriteLine();
                    outfile.WriteLine(DateTime.Now.ToString());
                    outfile.WriteLine(ex.Message);
                    outfile.WriteLine(ex.StackTrace);
                    outfile.WriteLine();
                }
            }
#endif
        }


    }
}
