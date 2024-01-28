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
            Tuple<HttpAPIAsset?, Exception?> asset = HttpManager.GET("https://www.msn.com/");
            Assert.IsNull(asset.Item2);
        }

        [TestMethod(displayName: "HTTP GET Test - Successful")]
        public void GET_Success_Test()
        {
            Tuple<HttpAPIAsset?, Exception?> asset = HttpManager.GET("https://www.msn.com/");
            if (asset.Item1 != null && asset.Item2 == null)
            {
                // If asset exists and exception does not, test asset
                Assert.IsTrue(asset.Item1.Successful);
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod(displayName: "HTTP GET Test - Unsuccessful")]
        public void GET_NonSuccess_Test()
        {
            Tuple<HttpAPIAsset?, Exception?> asset = HttpManager.GET("http://rubenmayayo.com/"); // Should always 403
            if (asset.Item1 != null && asset.Item2 == null)
            {
                // If asset exists and exception does not, test asset
                Assert.IsFalse(asset.Item1.Successful);
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}