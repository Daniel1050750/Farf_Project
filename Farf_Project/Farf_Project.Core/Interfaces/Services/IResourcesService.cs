namespace Farf_Project.Core
{
    public interface IResourcesService
    {
        string GetResource(string resourceStringName, params object[] replacements);
    }
}
