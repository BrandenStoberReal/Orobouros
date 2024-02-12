using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniScraperDLL.Bases;
using UniScraperDLL.Managers;

namespace UniScraperDLLTests.DLL
{
    [TestClass]
    public class HttpManagerTests
    {
        [TestMethod(displayName: "HTTP GET Test - No Exceptions")]
        public void GET_Exception_Test()
        {
            HttpAPIAsset asset = HttpManager.GET("https://www.msn.com/");
            Assert.IsFalse(asset.Errored);
        }

        [TestMethod(displayName: "HTTP GET Test - Successful")]
        public void GET_Success_Test()
        {
            HttpAPIAsset asset = HttpManager.GET("https://www.msn.com/");
            if (asset.Content != null && asset.Errored == false)
            {
                // If asset exists and exception does not, test asset
                Assert.IsTrue(asset.Successful);
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod(displayName: "HTTP GET Test - Unsuccessful")]
        public void GET_NonSuccess_Test()
        {
            HttpAPIAsset asset = HttpManager.GET("http://rubenmayayo.com/"); // Should always 403
            if (asset.Errored == false)
            {
                // If asset exists and exception does not, test asset
                Assert.IsFalse(asset.Successful);
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}