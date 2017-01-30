namespace TvMaze {
    /// <summary>
    /// People interface
    /// </summary>
    interface IPeople {

        /// <summary>
        /// Retreive all privmary information for a given person with
        /// possible embedding of additional information
        /// </summary>
        /// <param name="personId">Person id</param>
        /// <param name="embed">Embedded additional information</param>
        void GetPersonInfo(string personId, string embed);

        /// <summary>
        /// Retreive all cast credits for a person. 
        /// A cast credit is a combination of both a show and a character.
        /// </summary>
        /// <param name="personId">Person id</param>
        /// <param name="embed">Embedded additional information</param>
        void GetCastCredits(string personId, string embed);

        /// <summary>
        /// Retreive all crew credits for a person.
        /// A crew credit is combination of both a show and a crew type.
        /// </summary>
        /// <param name="personId">Person id</param>
        /// <param name="embed">Embedded additional information</param>
        void GetCrewCredits(string personId, string embed);

    }
}
