using System;
using NUnit.Framework;
using System.IO;
using System.Diagnostics;
using TreeSizer;

namespace TreeSizer.UnitTest
{
    [TestFixture]
    public class TreeListProcessorTests
    {
        [Test]
        public void CTOR_CMTreeListProcessor_NoPathForFileInfo()
        {
            /**Terrence Knoesen 
             * Check that an ArgumentNullException is thrown for this scenario.
            **/
            var ex = Assert.Throws<ArgumentNullException>(() => new CMDTreeListProcessor(new System.IO.FileInfo(null)));
            //Assert.That(ex.Message, Is.EqualTo("Actual exception message"));
        }

        [Test]
        public void CTOR_CMTreeListProcessor_InvalidPath()
        {
            /**Terrence Knoesen 
             * Check that an ArgumentNullException is thrown for this scenario.
            **/
            var ex = Assert.Throws<IOException>(() => new CMDTreeListProcessor(new System.IO.FileInfo(@"c:\Random\TestFile.txt")));
            Assert.That(ex.Message, Is.EqualTo("The TreeListFile doesn't exist!\n\nFile is 'c:\\Random\\TestFile.txt'."));
            //Debug.WriteLine(ex.Message);
        }

        [Test]
        public void CTOR_CMTreeListProcessor_BlankFile()
        {
            /**Terrence Knoesen 
             * Check that an ArgumentNullException is thrown for this scenario.
            **/
            var ex = Assert.Throws<IOException>(() => new CMDTreeListProcessor(new System.IO.FileInfo(@"..\..\data\test\BlankDoc.txt")));
            //Assert.That(ex.Message, Is.EqualTo("The TreeListFile doesn't exist!\n\nFile is 'c:\\Random\\TestFile.txt'."));
            //Debug.WriteLine(ex.Message);
        }

    }
}
