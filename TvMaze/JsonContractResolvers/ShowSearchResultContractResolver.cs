using Newtonsoft.Json.Serialization;
using TvMaze.Domain;

namespace TvMaze.JsonContractResolvers {
    internal class ShowSearchResultContractResolver : DefaultContractResolver {

        private const string JSON_FIELD_NAME = "show";

        public static readonly ShowSearchResultContractResolver Instance = new ShowSearchResultContractResolver();

        protected override string ResolvePropertyName(string propertyName) {
            return propertyName.Equals(nameof(SearchResult<object>.Element)) 
                ? JSON_FIELD_NAME 
                : base.ResolvePropertyName(propertyName);
        }

    }
}
