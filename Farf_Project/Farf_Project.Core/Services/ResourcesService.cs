using System.Resources;

namespace Farf_Project.Core
{
    public class ResourcesService : IResourcesService
    {
        #region Private Readonly variables

        private readonly ResourceManager resourceManager;

        #endregion

        #region Constructor

        public ResourcesService(ResourceManager resourceManager)
        {
            this.resourceManager = resourceManager;
        }

        #endregion

        #region Public Methods

        /// Builds up a string looking up a resource and doing the replacements.
        /// <param name="resourceStringName">Name of resource to use</param>
        /// <param name="replacements">Strings to use for replacing in the resource string</param>
        public string GetResource(string resourceStringName, params object[] replacements)
        {         
            // Localization: Here we are using the more clasic way of getting resources using the ResourceManager
            //               instead of the IStringLocalizer to look up resource strings from the .resx files. We
            //               will get the appropriate resource based on the request culture.
            return string.Format(this.resourceManager.GetString(resourceStringName), replacements);
        }

        #endregion
    }
}
