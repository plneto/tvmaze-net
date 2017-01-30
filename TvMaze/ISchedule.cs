using System;

namespace TvMaze {
    /// <summary>
    /// Schedule interface
    /// </summary>
    interface ISchedule {
        /// <summary>
        /// Gets schedule that air in a given country on a given date.
        /// Episodes are returned in the order in which they are aired, and full information about
        /// the episode and the corresponding show is included
        /// </summary>
        /// <param name="country"></param>
        /// <param name="date"></param>
        void GetSchedule(string country, DateTime date);

        /// <summary>
        /// Gets the full schedule - a list of all future episodes know to TvMaze,
        /// regardless of their country.
        /// </summary>
        void GetFullSchedule();
    }
}
