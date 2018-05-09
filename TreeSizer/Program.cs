using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace TreeSizer
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
            Application.Run(new Form1());
        }


        public static string GetApplicationName(int intReturnType)
        {
            string strVer;

            Assembly asm = Assembly.GetExecutingAssembly();
            AssemblyName asmName = asm.GetName();
            Version objVer = asmName.Version;

            string strName = asmName.Name;
            switch (intReturnType)
            {
                case 0:
                    return strName;
                case 1:
                    strVer = String.Format("{0} - {1}.{2}.{3}.{4}", strName, objVer.Major, objVer.Minor, objVer.Build, objVer.Revision);
                    return strVer;
                default:
                    return strName;
            }
        }


    }
}
