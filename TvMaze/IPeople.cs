using System.Collections.Generic;
using TvMaze.Domain;

namespace TvMaze {
    /// <summary>
    /// People interface
    /// </summary>
    public interface IPeople {

        /// <summary>
        /// Retreive all primary information for a given person with
        /// possible embedding of additional information
        /// </summary>
        /// <param name="personId">Person id</param>
        /// <param name="embed">Embedded additional information</param>
        /// <returns>All primary information for a given person with possible embedding of additional information</returns>
        Person GetPersonInfo(string personId, EmbedType? embed = null);

        /// <summary>
        /// Retreive all cast credits for a person. 
        /// A cast credit is a combination of both a show and a character.
        /// </summary>
        /// <param name="personId">Person id</param>
        /// <param name="embed">Embedded additional information</param>
        /// <returns>All cast credits for a person</returns>
        IEnumerable<CastCredit> GetCastCredits(string personId, EmbedType? embed = null);

        /// <summary>
        /// Retreive all crew credits for a person.
        /// A crew credit is combination of both a show and a crew type.
        /// </summary>
        /// <param name="personId">Person id</param>
        /// <param name="embed">Embedded additional information</param>
        /// <returns>All crew credits for a person</returns>
        IEnumerable<CrewCredit> GetCrewCredits(string personId, EmbedType? embed = null);

    }
}
