using Newtonsoft.Json;
using Orobouros.Bases;
using Orobouros.Bases.Modules;
using Orobouros.Managers.Web;

namespace Orobouros.Managers.Modules;

/// <summary>
/// Class designed to help ease client-end module management from remote sources. This is helpful for any app that doesn't want users to manage modules themselves.
/// </summary>
public static class RemoteModuleManager
{
    /// <summary>
    /// Github manifest URL
    /// </summary>
    private static readonly string ManifestURL =
        "https://raw.githubusercontent.com/BrandenStoberReal/Orobouros-Public-Modules/master/version.json";
    
    /// <summary>
    /// Fetches the remote manifest and converts it into a manageable class.
    /// </summary>
    /// <returns></returns>
    public static RemoteModulesManifest? FetchRemoteManifest()
    {
        HttpContent? possibleJsonContent = HttpManager
            .GET(ManifestURL)
            .Content;
        if (possibleJsonContent == null)
        {
            return null;
        }
        string possibleJson = possibleJsonContent.ReadAsStringAsync().Result;
        return JsonConvert.DeserializeObject<RemoteModulesManifest>(possibleJson);
    }

    /// <summary>
    /// Determines if a module is outdated by its GUID.
    /// </summary>
    /// <param name="module"></param>
    /// <returns>True if the module needs an update, false if it is the latest version. Returns null if it does not exist in the manifest, or manifest couldn't be found.</returns>
    public static bool? IsModuleOutdated(Module module)
    {
        RemoteModulesManifest? manifest = FetchRemoteManifest();
        if (manifest == null)
        {
            return null;
        }
        RemoteModule? remoteModule = manifest.modules.FirstOrDefault(x => x.guid == module.GUID);
        if (remoteModule == null)
        {
            return null;
        }

        Version remoteVersion = new Version(remoteModule.version);
        Version localVersion = new Version(module.Version);
        if (remoteVersion > localVersion)
        {
            // local module needs an update
            return true;
        }
        // local module is latest version
        return false;
    }
}