using System;

namespace TvMaze {
    /// <summary>
    /// Show interface
    /// </summary>
    interface IShow {

        /// <summary>
        /// Retreive all primary information for a given sow
        /// with possible embedding of additional information.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="embed">Embedded additional information</param>
        void GetShow(string id, string embed);

        /// <summary>
        /// Gets a complete list of episodes for the given show.
        /// Episodes are returned in their airing order and include
        /// full episode information.
        /// 
        /// By default, specials are not included in the list.
        /// </summary>
        /// <param name="showId">Show id</param>
        /// <param name="includeSpecials">True to include special episodes, otherwise false. Default is false.</param>
        void GetShowEpisodeList(string showId, bool includeSpecials = false);

        /// <summary>
        /// Retreive one specific episode from particular show given
        /// its season and episode number.
        /// </summary>
        /// <param name="showId">Show id</param>
        /// <param name="season">Season number</param>
        /// <param name="episodeNumber">Episode number</param>
        void GetShowEpisode(string showId, string season, string episodeNumber);

        /// <summary>
        /// Retreive all episodes from particular show that have aired on a specific date.
        /// </summary>
        /// <param name="showId">Show id</param>
        /// <param name="airDate">Air date</param>
        void GetShowEpisodes(string showId, DateTime airDate);

        /// <summary>
        /// Gets a complete list of seasons for the given show.
        /// Seasons are returned in ascending order and conain the
        /// full informations that's known about them.
        /// </summary>
        /// <param name="showId">Show id</param>
        void GetShowSeasons(string showId);

        /// <summary>
        /// Gets a list of main cast for a show.
        /// Each cast item is a combination of a person and a character.
        /// Items are ordered by importance, which is determined by the total number of appearances of the given
        /// character in show.
        /// </summary>
        /// <param name="showId">Show id</param>
        void GetShowCast(string showId);


        /// <summary>
        /// Gets a list of aliases for a show.
        /// </summary>
        /// <param name="showId"></param>
        void GetShowAliases(string showId);

        /// <summary>
        /// Gets a list of all shows in TvMaze database with all primary information included.
        /// List is paginated with a maximus of 250 results per call.
        /// The pagination is based on show ID, e.g. page 0 will contain
        /// shows with IDs between 0 and 250. This means a single page
        /// might contain less than 250 results in case of deletions.
        /// End of list is reachd when you receive a HTTP 404 response code.
        /// </summary>
        /// <param name="pageNumber"></param>
        void GetAllShows(int pageNumber = 1);

        /// <summary>
        /// Gets a list of all shows in the TvMaze database andthe timestap when they
        /// were last updated.
        /// </summary>
        void GetShowUpdates();

    }
}
