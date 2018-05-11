using System;
using NUnit.Framework;
using TreeSizer;

namespace TreeSizer.UnitTest
{
    [TestFixture]
    public class TreeListProcessorTests
    {
        [Test]
        public void CTOR_CMTreeListProcessor_NoPathForFileInfo()
        {
            CMDTreeListProcessor objTLP = null;

            objTLP = new CMDTreeListProcessor(new System.IO.FileInfo(null));
        }
    }
}
