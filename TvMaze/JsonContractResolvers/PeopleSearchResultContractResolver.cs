using Newtonsoft.Json.Serialization;
using TvMaze.Domain;

namespace TvMaze.JsonContractResolvers {
    internal class PeopleSearchResultContractResolver : DefaultContractResolver {

        private const string JSON_FIELD_NAME = "person";

        public static readonly PeopleSearchResultContractResolver Instance = new PeopleSearchResultContractResolver();

        protected override string ResolvePropertyName(string propertyName) {
            return propertyName.Equals(nameof(SearchResult<object>.Element)) 
                ? JSON_FIELD_NAME 
                : base.ResolvePropertyName(propertyName);
        }

    }
}
