using System;
using Newtonsoft.Json;
using TvMaze.Domain;

namespace TvMaze {
    /// <summary>
    /// DomainObjectFactory responsible for creation of domain objects
    /// based on Json input data
    /// </summary>
    public class DomainObjectFactory {

        /// <summary>
        /// Creates <see cref="Episode"/> based on Json input data
        /// </summary>
        /// <param name="json">Json episode data</param>
        /// <returns>An episode instance</returns>
        public Episode CreateEpisode(string json) {
            if(string.IsNullOrEmpty(json)) throw new ArgumentNullException(nameof(json));

            var episode = JsonConvert.DeserializeObject<Episode>(json);
            return episode;
        }

    }
}
