namespace Orobouros.Bases.Modules;

/// <summary>
/// Dummy class for handling JSON recieved from the public repository manifest.
/// </summary>
public class RemoteModule
{
    public string name { get; set; }
    public string version { get; set; }
    public string url { get; set; }
    public string guid { get; set; }
}