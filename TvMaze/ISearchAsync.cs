using System.Collections.Generic;
using System.Threading.Tasks;
using TvMaze.Domain;

namespace TvMaze {
    /// <summary>
    /// Search interface
    /// </summary>
    public interface ISearchAsync {

        /// <summary>
        /// Search through all the tv shows in TvMaze database by show's name.
        /// </summary>
        /// <param name="query">Search query - show's name</param>
        /// <returns>Show search results</returns>
        Task<IEnumerable<SearchResult<Show>>> ShowSearchAsync(string query);

        /// <summary>
        /// Search the single tv show in TvMaze database by show's name.
        /// </summary>
        /// <param name="query">Search query - show's name</param>
        /// <param name="embed">Embedded additional information</param>
        /// <returns>Returns exactly one result, or no result at all.</returns>
        Task<Show> ShowSingleSearchAsync(string query, EmbedType? embed = null);

        /// <summary>
        /// Find a tv show by providing ID from external tv show providers
        /// </summary>
        /// <param name="showId">External provider's show id</param>
        /// <param name="externalTvShowProvider">External tv show provider</param>
        // TODO
        //void Lookup(string showId, ExternalTvShowProvider externalTvShowProvider);

        /// <summary>
        /// Search through all the people in TvMaze database.
        /// </summary>
        /// <param name="query">Search query</param>
        /// <returns>Person search result</returns>
        Task<IEnumerable<SearchResult<Person>>> PeopleSearchAsync(string query);

    }
}
