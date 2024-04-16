using Orobouros.Bases.Modules;
using Orobouros.Managers.Modules;

namespace Orobouros.Tests.Modules;
[TestClass]
public class RemoteTests
{
    [TestMethod(displayName: "Remote Module Tests - Fetch Manifest")]
    public void Test_Manifest()
    {
        RemoteModulesManifest Mods = RemoteModuleManager.FetchRemoteManifest();
        Assert.IsNotNull(Mods);
    }
    
    [TestMethod(displayName: "Remote Module Tests - Fetch PartyModule")]
    public void Test_Manifest_PartyMod()
    {
        RemoteModulesManifest manifest = RemoteModuleManager.FetchRemoteManifest();
        Assert.IsTrue(manifest.modules.Count > 0);
    }
}