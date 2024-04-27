using Orobouros.Bases.Modules;
using Orobouros.Managers.Modules;

namespace Orobouros.Tests.Modules;

[TestClass]
public class RemoteTests
{
    [TestMethod(displayName: "Remote Module Tests - Fetch Modules")]
    public void Test_Manifest()
    {
        List<RemoteModule>? modules = RemoteModuleManager.FetchRemoteModules();
        Assert.IsNotNull(modules);
    }

    [TestMethod(displayName: "Remote Module Tests - Ensure Definitions Are Present")]
    public void Test_Manifest_PartyMod()
    {
        List<RemoteModule>? modules = RemoteModuleManager.FetchRemoteModules();
        Assert.IsTrue(modules.Count > 0);
    }
}