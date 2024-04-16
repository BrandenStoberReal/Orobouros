namespace Orobouros.Bases.Modules;

/// <summary>
/// Dummy class for handling JSON recieved from the public repository manifest.
/// </summary>
public class RemoteModulesList
{
    public IList<RemoteModule> modules = new List<RemoteModule>();
}