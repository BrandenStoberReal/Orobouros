using Orobouros.Bases.Modules;
using Orobouros.Managers.Modules;

namespace Orobouros.Tests.Modules;
[TestClass]
public class RemoteTests
{
    [TestMethod(displayName: "Remote Module Tests - Fetch Manifest")]
    public void Test_Manifest()
    {
        RemoteModuleManager.FetchRemoteManifest();
    }
    
    [TestMethod(displayName: "Remote Module Tests - Fetch PartyModule")]
    public void Test_Manifest_PartyMod()
    {
        RemoteModulesList List = RemoteModuleManager.FetchRemoteManifest();
        Assert.IsTrue(List.modules.Count > 0);
    }
}