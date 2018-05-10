using System;
using System.Reflection;
using log4net;
using System.Windows.Forms;

namespace TreeSizer
{
    static class Program
    {
        private static readonly ILog Log =
        LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Log.Info("First Log");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }


        public static string GetApplicationName(int intReturnType)
        {
            Log.Debug("Enter");
            string strVer;

            Assembly asm = Assembly.GetExecutingAssembly();
            AssemblyName asmName = asm.GetName();
            Version objVer = asmName.Version;

            string strName = asmName.Name;
            switch (intReturnType)
            {
                case 0:
                    Log.Debug("Exit");
                    return strName;
                case 1:
                    strVer = String.Format("{0} - {1}.{2}.{3}.{4}", strName, objVer.Major, objVer.Minor, objVer.Build, objVer.Revision);
                    Log.Debug("Exit");
                    return strVer;
                default:
                    Log.Debug("Exit");
                    return strName;
            }
        }

    }
}
