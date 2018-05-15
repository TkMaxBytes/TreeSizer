using System;
using NUnit.Framework;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using TreeSizer;

namespace TreeSizer.UnitTest
{
    [TestFixture]
    public class TreeListProcessorTests
    {
        #region Constants

        #endregion //================================================

        #region Enums

        #endregion //================================================

        #region Variables
        private string mstrDataDir = null;
        #endregion //================================================

        #region Constructors

        #endregion //================================================

        #region Properties

        #endregion //================================================

        #region Events

        #endregion //================================================

        #region Methods
        [OneTimeSetUp]
        public void SetupDirs()
        {
            string strAppName = Utils.GetApplicationName(0);
            string strDirPath = Utils.AssemblyDirectory();
            string strDataPath = strDirPath.Substring(0, strDirPath.IndexOf(strAppName));
            strDataPath = Path.Combine(strDataPath, strAppName);
            strDataPath = Path.Combine(strDataPath, "data");
            strDataPath = Path.Combine(strDataPath, "test");
            mstrDataDir = strDataPath;
        }

        [Test]
        public void CMTreeListProcessor_CTOR_NoPathForFileInfo()
        {
            /**Terrence Knoesen 
             * Check that an ArgumentNullException is thrown for this scenario.
            **/
            var ex = Assert.Throws<ArgumentNullException>(() => new CMDTreeListProcessor(new System.IO.FileInfo(null)));
            //Assert.That(ex.Message, Is.EqualTo("Actual exception message"));
        }

        [Test]
        public void CMTreeListProcessor_CTOR_InvalidPath()
        {
            /**Terrence Knoesen 
             * Check that an ArgumentNullException is thrown for this scenario.
            **/
            var ex = Assert.Throws<IOException>(() => new CMDTreeListProcessor(new System.IO.FileInfo(@"c:\Random\TestFile.txt")));
            Assert.That(ex.Message, Is.EqualTo("The TreeListFile doesn't exist!\n\nFile is 'c:\\Random\\TestFile.txt'."));
            //Debug.WriteLine(ex.Message);
        }

        [Test]
        public void CMTreeListProcessor_CTOR_BlankFile()
        {

            /**Terrence Knoesen 
             * Get the path to the BlankDoc.txt as this is used for this test
            **/
            string strFileName = Path.Combine(mstrDataDir, "BlankDoc.txt");

            /**Terrence Knoesen 
             * Check that an ArgumentNullException is thrown for this scenario.
            **/
            var ex = Assert.Throws<ApplicationException>(() => new CMDTreeListProcessor(new System.IO.FileInfo(strFileName)));
            string strExpectedMessage = "The TreeListFile '" + strFileName + "' \n\nIs zero bytes in length!";
            Assert.That(ex.Message, Is.EqualTo(strExpectedMessage));
            //Debug.WriteLine(ex.Message);
        }

        [Test]
        public void CMTreeListProcessor_Start_CheckLagerDirs()
        {
            string strMess;
            string strFileName = Path.Combine(mstrDataDir, "smalldir.txt");
            CMDTreeListProcessor objProc = null;
            try
            {
                objProc = new CMDTreeListProcessor(new FileInfo(strFileName));
            }
            catch (Exception)
            {
                strMess = "Creating the CMDTreeListProcessor should work!";
                Assert.Fail(strMess);
            }
            try
            {
                objProc.Start();
            }
            catch (Exception ex)
            {
                strMess = "Straight fail!\n" + ex.Message;
                Assert.Fail(strMess);
            }


        }

        [Test]
        public void CMTreeListProcessor_Start_CheckSmallDirs()
        {
            string strMess;
            string strFileName = Path.Combine(mstrDataDir, "Largerdirs.txt");
            CMDTreeListProcessor objProc = null;
            try
            {
                objProc = new CMDTreeListProcessor(new FileInfo(strFileName));
            }
            catch (Exception)
            {
                strMess = "Creating the CMDTreeListProcessor should work!";
                Assert.Fail(strMess);
            }
            try
            {
                objProc.Start();
            }
            catch (Exception ex)
            {
                strMess = "Straight fail!\n" + ex.Message;
                Assert.Fail(strMess);
            }


        }

        [Test]
        public void CMTreeListProcessor_Start_CheckBigDirs()
        {
                       
            string strMess;
            string strFileName = Path.Combine(mstrDataDir, "BigDirs.txt");
            
            CMDTreeListProcessor objProc = null;
            try
            {
                objProc = new CMDTreeListProcessor(new FileInfo(strFileName));
            }
            catch (Exception)
            {
                strMess = "Creating the CMDTreeListProcessor should work!";
                Assert.Fail(strMess);
            }
            try
            {
                objProc.Start();
            }
            catch (Exception ex)
            {
                strMess = "Straight fail!\n" + ex.Message;
                Assert.Fail(strMess);
            }
        }


        [Test]
        public void CMTreeListProcessor_Start_NonDosListingCheck()
        {

            string strMess;
            string strFileName = Path.Combine(mstrDataDir, "NotDosList.txt");
            CMDTreeListProcessor objProc = null;
            try
            {
                objProc = new CMDTreeListProcessor(new FileInfo(strFileName));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            try
            {
                objProc.Start();
            }
            catch (Exception ex)
            {
                Assert.True(ex.GetType() == typeof(ApplicationException));
                Assert.True(ex.Message.Contains(" is not a DOS directory listing!"));
            }
        }

        [Test]
        public void CMTreeListProcessor_Start_NonDosListingSmallFile()
        {

            string strMess;
            string strFileName = Path.Combine(mstrDataDir, "NotDosListSmallFile.txt");
            CMDTreeListProcessor objProc = null;
            try
            {
                objProc = new CMDTreeListProcessor(new FileInfo(strFileName));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            try
            {
                objProc.Start();
            }
            catch (Exception ex)
            {
                Assert.True(ex.GetType() == typeof(ApplicationException));
                Assert.True(ex.Message.Contains(" is too small to be a DOS directory list!"));
            }
        }



        #endregion //================================================
    }

    static class Utils
    {

        public static string AssemblyDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
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
