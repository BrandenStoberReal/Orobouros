using Newtonsoft.Json;
using Orobouros.Bases.Modules;
using Orobouros.Managers.Web;

namespace Orobouros.Managers.Modules;

/// <summary>
/// Class designed to help ease client-end module management from remote sources. This is helpful for any app that doesn't want users to manage modules themselves.
/// </summary>
public static class RemoteModuleManager
{
    public static RemoteModulesList FetchRemoteManifest()
    {
        RemoteModulesList mods = JsonConvert.DeserializeObject<RemoteModulesList>(HttpManager
            .GET("https://raw.githubusercontent.com/BrandenStoberReal/Orobouros-Public-Modules/master/version.json")
            .Content.ReadAsStringAsync().Result);
        return mods;
    }
}