using System;
using System.Reflection;
using log4net;
using System.Windows.Forms;
using com.treesizer.ui;

namespace com.treesizer
{
    static class Program
    {
        private static readonly ILog mobjLog =
        LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            mobjLog.Info("First Log");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }


        public static string GetApplicationName(int intReturnType)
        {
            mobjLog.Debug("Enter");
            string strVer;

            Assembly asm = Assembly.GetExecutingAssembly();
            AssemblyName asmName = asm.GetName();
            Version objVer = asmName.Version;

            string strName = asmName.Name;
            switch (intReturnType)
            {
                case 0:
                    mobjLog.Debug("Exit");
                    return strName;
                case 1:
                    strVer = String.Format("{0} - {1}.{2}.{3}.{4}", strName, objVer.Major, objVer.Minor, objVer.Build, objVer.Revision);
                    mobjLog.Debug("Exit");
                    return strVer;
                default:
                    mobjLog.Debug("Exit");
                    return strName;
            }
        }

        
    }
}
