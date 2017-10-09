using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using TvMaze.Domain;
using Zed.Extensions;
using Zed.Web.Extensions;

namespace TvMaze {
    public class TvMazeClient : ITvMazeClient, ITvMazeClientAsync {

        private const string DATE_ISO_8601 = "yyyy-MM-dd";
        private const string BASE_API_URL = "http://api.tvmaze.com/";

        private readonly Uri baseApiUrl;
        private readonly HttpClient httpClient;

        #region Constructors and Init

        /// <summary>
        /// Creates TvMazeClient instance for default base API URL
        /// </summary>
        public TvMazeClient() : this(BASE_API_URL) { }

        /// <summary>
        /// Creates TvMazeClient instance for provided base API URL
        /// </summary>
        /// <param name="baseApiUrl">Base API URL on which TvMaze service exists</param>
        /// <param name="httpClient">HttpClient</param>
        public TvMazeClient(string baseApiUrl, HttpClient httpClient = null) {
            this.baseApiUrl = new Uri(baseApiUrl);

            this.httpClient = httpClient ?? new HttpClient();
        }

        #endregion

        #region IPeople

        /// <summary>
        /// Retreive all privmary information for a given person with
        /// possible embedding of additional information
        /// </summary>
        /// <param name="personId">Person id</param>
        /// <param name="embed">Embedded additional information</param>
        public Person GetPersonInfo(string personId, EmbedType? embed = null) {
            return GetPersonInfoAsync(personId, embed).Result;
        }

        /// <summary>
        /// Retreive all cast credits for a person. 
        /// A cast credit is a combination of both a show and a character.
        /// </summary>
        /// <param name="personId">Person id</param>
        /// <param name="embed">Embedded additional information</param>
        /// <returns>All cast credits for a person</returns>
        public IEnumerable<CastCredit> GetCastCredits(string personId, EmbedType? embed = null) {
            return GetCastCreditsAsync(personId, embed).Result;
        }

        /// <summary>
        /// Retreive all crew credits for a person.
        /// A crew credit is combination of both a show and a crew type.
        /// </summary>
        /// <param name="personId">Person id</param>
        /// <param name="embed">Embedded additional information</param>
        /// <returns>All crew credits for a person</returns>
        public IEnumerable<CrewCredit> GetCrewCredits(string personId, EmbedType? embed = null) {
            return GetCrewCreditsAsync(personId, embed).Result;
        }

        #endregion

        #region IPeopleAsync

        /// <summary>
        /// Retreive all primary information for a given person with
        /// possible embedding of additional information
        /// </summary>
        /// <param name="personId">Person id</param>
        /// <param name="embed">Embedded additional information</param>
        /// <returns>All primary information for a given person with possible embedding of additional information</returns>
        public async Task<Person> GetPersonInfoAsync(string personId, EmbedType? embed = null) {
            if (string.IsNullOrEmpty(personId)) throw new ArgumentNullException(nameof(personId));

            const string relativeUrl = "/people";

            var uriBuilder = new UriBuilder(baseApiUrl) {
                Path = relativeUrl
            };

            uriBuilder.AppendPathSegment(personId);

            if (embed != null) {
                uriBuilder.BuildQueryString(new { embed = embed.Value.GetEnumDescription() });
            }

            //var response = await httpClient.GetStringAsync(uriBuilder.Uri);
            var httpResponse = await httpClient.GetAsync(uriBuilder.Uri);
            try {
                httpResponse.EnsureSuccessStatusCode();
            } catch (HttpRequestException ex) {
                throw new HttpRequestExtException(httpResponse.StatusCode, ex.Message, ex);
            }

            var response = await httpResponse.Content.ReadAsStringAsync();

            Person person = DomainObjectFactory.CreatePerson(response);

            return person;
        }


        /// <summary>
        /// Retreive all cast credits for a person. 
        /// A cast credit is a combination of both a show and a character.
        /// </summary>
        /// <param name="personId">Person id</param>
        /// <param name="embed">Embedded additional information</param>
        /// <returns>All cast credits for a person</returns>
        /// //castcredits
        public async Task<IEnumerable<CastCredit>> GetCastCreditsAsync(string personId, EmbedType? embed = null) {
            if (string.IsNullOrEmpty(personId)) throw new ArgumentNullException(nameof(personId));

            const string relativeUrl = "/people";

            var uriBuilder = new UriBuilder(baseApiUrl) {
                Path = relativeUrl
            };

            uriBuilder.AppendPathSegment(personId);
            uriBuilder.AppendPathSegment("castcredits");

            if (embed != null) {
                uriBuilder.BuildQueryString(new { embed = embed.Value.GetEnumDescription() });
            }

            //var response = await httpClient.GetStringAsync(uriBuilder.Uri);
            var httpResponse = await httpClient.GetAsync(uriBuilder.Uri);
            try {
                httpResponse.EnsureSuccessStatusCode();
            } catch (HttpRequestException ex) {
                throw new HttpRequestExtException(httpResponse.StatusCode, ex.Message, ex);
            }

            var response = await httpResponse.Content.ReadAsStringAsync();

            IEnumerable<CastCredit> castCredits = DomainObjectFactory.CreateCastCredits(response);

            return castCredits;
        }


        /// <summary>
        /// Retreive all crew credits for a person.
        /// A crew credit is combination of both a show and a crew type.
        /// </summary>
        /// <param name="personId">Person id</param>
        /// <param name="embed">Embedded additional information</param>
        /// <returns>All crew credits for a person</returns>
        public async Task<IEnumerable<CrewCredit>> GetCrewCreditsAsync(string personId, EmbedType? embed = null) {
            if (string.IsNullOrEmpty(personId)) throw new ArgumentNullException(nameof(personId));

            const string relativeUrl = "/people";

            var uriBuilder = new UriBuilder(baseApiUrl) {
                Path = relativeUrl
            };

            uriBuilder.AppendPathSegment(personId);
            uriBuilder.AppendPathSegment("crewcredits");

            if (embed != null) {
                uriBuilder.BuildQueryString(new { embed = embed.Value.GetEnumDescription() });
            }

            //var response = await httpClient.GetStringAsync(uriBuilder.Uri);
            var httpResponse = await httpClient.GetAsync(uriBuilder.Uri);
            try {
                httpResponse.EnsureSuccessStatusCode();
            } catch (HttpRequestException ex) {
                throw new HttpRequestExtException(httpResponse.StatusCode, ex.Message, ex);
            }

            var response = await httpResponse.Content.ReadAsStringAsync();

            IEnumerable<CrewCredit> crewCredits = DomainObjectFactory.CreateCrewCredits(response);

            return crewCredits;
        }

        #endregion

        #region ISchedule

        /// <summary>
        /// Gets schedule that air in a given country on a given date.
        /// Episodes are returned in the order in which they are aired, and full information about
        /// the episode and the corresponding show is included
        /// </summary>
        /// <example>
        /// http://api.tvmaze.com/schedule?country=US&date=2014-12-01
        /// http://api.tvmaze.com/schedule
        /// </example>
        /// <param name="countryCode">Country for which schedule is requestd. An ISO 3166-1 code of the country; defaults to US</param>
        /// <param name="date">Date for which schedule is requested. Defaults to the current day</param>
        /// <returns>Schedule</returns>
        public Schedule GetSchedule(string countryCode = null, DateTime? date = null) {
            var schedule = GetScheduleAsync(countryCode, date).Result;
            return schedule;
        }

        /// <summary>
        /// Gets the full schedule - a list of all future episodes know to TvMaze,
        /// regardless of their country.
        /// </summary>
        /// <example>http://api.tvmaze.com/schedule/full</example>
        /// <returns>Full schedule</returns>
        public Schedule GetFullSchedule() {
            var schedule = GetFullScheduleAsync().Result;
            return schedule;
        }

        #endregion

        #region IScheduleAsync

        /// <summary>
        /// Gets schedule that air in a given country on a given date.
        /// Episodes are returned in the order in which they are aired, and full information about
        /// the episode and the corresponding show is included
        /// </summary>
        /// <example>
        /// http://api.tvmaze.com/schedule?country=US&date=2014-12-01
        /// http://api.tvmaze.com/schedule
        /// </example>
        /// <param name="countryCode">Country for which schedule is requestd. An ISO 3166-1 code of the country; defaults to US</param>
        /// <param name="date">Date for which schedule is requested. Defaults to the current day</param>
        /// <returns>Schedule</returns>
        /// <exception cref="HttpRequestExtException">HttpRequest exception with included HttpStatusCode</exception>
        public async Task<Schedule> GetScheduleAsync(string countryCode = null, DateTime? date = null) {
            const string relativeUrl = "/schedule";
            var uriBuilder = new UriBuilder(baseApiUrl) {
                Path = relativeUrl
            };

            var queryParams = new NameValueCollection();

            // (optional) countrycode: an ISO 3166-1 code of the country; defaults to US
            if (countryCode != null) { queryParams["country"] = countryCode; }

            // (optional) date: an ISO 8601 formatted date; defaults to the current day
            if (date != null) { queryParams["date"] = date.Value.ToString(DATE_ISO_8601, CultureInfo.InvariantCulture); }

            uriBuilder.BuildQueryString(queryParams);

            //var response = await httpClient.GetStringAsync(uriBuilder.Uri);
            var httpResponse = await httpClient.GetAsync(uriBuilder.Uri);
            try {
                httpResponse.EnsureSuccessStatusCode();
            } catch (HttpRequestException ex) {
                throw new HttpRequestExtException(httpResponse.StatusCode, ex.Message, ex);
            }

            var response = await httpResponse.Content.ReadAsStringAsync();

            Schedule schedule = DomainObjectFactory.CreateSchedule(response);

            return schedule;
        }

        /// <summary>
        /// Gets the full schedule - a list of all future episodes know to TvMaze,
        /// regardless of their country.
        /// </summary>
        /// <example>http://api.tvmaze.com/schedule/full</example>
        /// <returns>Full schedule</returns>
        /// <exception cref="HttpRequestExtException">HttpRequest exception with included HttpStatusCode</exception>
        public async Task<Schedule> GetFullScheduleAsync() {
            const string relativeUrl = "/schedule/full";
            //var response = await httpClient.GetStringAsync(new Uri(baseApiUrl, relativeUrl));

            var httpResponse = await httpClient.GetAsync(new Uri(baseApiUrl, relativeUrl));
            try {
                httpResponse.EnsureSuccessStatusCode();
            } catch (HttpRequestException ex) { 
                throw new HttpRequestExtException(httpResponse.StatusCode, ex.Message, ex);
            }
            
            var response = await httpResponse.Content.ReadAsStringAsync();

            Schedule schedule = DomainObjectFactory.CreateSchedule(response);

            return schedule;

        }

        #endregion

        #region ISearch

        /// <summary>
        /// Search through all the tv shows in TvMaze database by show's name.
        /// </summary>
        /// <param name="query">Search query - show's name</param>
        public IEnumerable<SearchResult<Show>> ShowSearch(string query) {
            return ShowSearchAsync(query).Result;
        }

        /// <summary>
        /// Search the single tv show in TvMaze database by show's name.
        /// </summary>
        /// <param name="query">Search query - show's name</param>
        /// <param name="embed">Embedded additional information</param>
        /// <returns>Returns exactly one result, or no result at all.</returns>
        public Show ShowSingleSearch(string query, EmbedType? embed = null) {
            return ShowSingleSearchAsync(query).Result;
        }


        /// <summary>
        /// Find a tv show by providing ID from external tv show providers
        /// </summary>
        /// <param name="showId">External provider's show id</param>
        /// <param name="externalTvShowProvider">External tv show provider</param>
        public Show ShowLookup(string showId, ExternalTvShowProvider externalTvShowProvider) {
            return ShowLookupAsync(showId, externalTvShowProvider).Result;
        }

        /// <summary>
        /// Search through all the people in TvMaze database.
        /// </summary>
        /// <param name="query">Search query</param>
        public IEnumerable<SearchResult<Person>> PeopleSearch(string query) {
            return PeopleSearchAsync(query).Result;
        }


        #endregion

        #region ISearchAsync

        /// <summary>
        /// Search through all the tv shows in TvMaze database by show's name.
        /// </summary>
        /// <param name="query">Search query - show's name</param>
        /// <returns>Show search results</returns>
        public async Task<IEnumerable<SearchResult<Show>>> ShowSearchAsync(string query) {
            if (string.IsNullOrEmpty(query)) throw new ArgumentNullException(nameof(query));

            const string relativeUrl = "/search/shows";

            var uriBuilder = new UriBuilder(baseApiUrl) {
                Path = relativeUrl
            };


            uriBuilder.BuildQueryString(new { q = query });

            //var response = await httpClient.GetStringAsync(uriBuilder.Uri);
            var httpResponse = await httpClient.GetAsync(uriBuilder.Uri);
            try {
                httpResponse.EnsureSuccessStatusCode();
            } catch (HttpRequestException ex) {
                throw new HttpRequestExtException(httpResponse.StatusCode, ex.Message, ex);
            }

            var response = await httpResponse.Content.ReadAsStringAsync();

            IEnumerable<SearchResult<Show>> showSearchResults = DomainObjectFactory.CreateShowSearchResults(response);

            return showSearchResults;
        }


        /// <summary>
        /// Search the single tv show in TvMaze database by show's name.
        /// </summary>
        /// <param name="query">Search query - show's name</param>
        /// <param name="embed">Embedded additional information</param>
        /// <returns>Returns exactly one result, or no result at all.</returns>
        public async Task<Show> ShowSingleSearchAsync(string query, EmbedType? embed = null) {
            if (string.IsNullOrEmpty(query)) throw new ArgumentNullException(nameof(query));

            const string relativeUrl = "/singlesearch/shows";

            var uriBuilder = new UriBuilder(baseApiUrl) {
                Path = relativeUrl
            };


            NameValueCollection queryParams = new NameValueCollection {{"q", query}};
            
            if (embed != null) {
                queryParams["embed"] = embed.Value.GetEnumDescription();
            }

            uriBuilder.BuildQueryString(queryParams);

            //var response = await httpClient.GetStringAsync(uriBuilder.Uri);
            var httpResponse = await httpClient.GetAsync(uriBuilder.Uri);
            try {
                httpResponse.EnsureSuccessStatusCode();
            } catch (HttpRequestException ex) {
                throw new HttpRequestExtException(httpResponse.StatusCode, ex.Message, ex);
            }

            var response = await httpResponse.Content.ReadAsStringAsync();

            Show show = DomainObjectFactory.CreateShow(response);

            return show;
        }


        /// <summary>
        /// Find a tv show by providing ID from external tv show providers
        /// </summary>
        /// <param name="showId">External provider's show id</param>
        /// <param name="externalTvShowProvider">External tv show provider</param>
        public async Task<Show> ShowLookupAsync(string showId, ExternalTvShowProvider externalTvShowProvider) {
            if (string.IsNullOrEmpty(showId)) throw new ArgumentNullException(nameof(showId));

            // lookup/shows?tvrage=24493
            const string relativeUrl = "/lookup/shows";

            var uriBuilder = new UriBuilder(baseApiUrl) {
                Path = relativeUrl
            };

            NameValueCollection queryParams = new NameValueCollection { { externalTvShowProvider.GetEnumDescription(), showId } };

            uriBuilder.BuildQueryString(queryParams);

            //var response = await httpClient.GetStringAsync(uriBuilder.Uri);
            var httpResponse = await httpClient.GetAsync(uriBuilder.Uri);
            try {
                httpResponse.EnsureSuccessStatusCode();
            } catch (HttpRequestException ex) {
                throw new HttpRequestExtException(httpResponse.StatusCode, ex.Message, ex);
            }

            var response = await httpResponse.Content.ReadAsStringAsync();

            Show show = DomainObjectFactory.CreateShow(response);

            return show;
        }

        /// <summary>
        /// Search through all the people in TvMaze database.
        /// </summary>
        /// <param name="query">Search query</param>
        /// <returns>Person search result</returns>
        public async Task<IEnumerable<SearchResult<Person>>> PeopleSearchAsync(string query) {
            if (string.IsNullOrEmpty(query)) throw new ArgumentNullException(nameof(query));

            const string relativeUrl = "/search/people";

            var uriBuilder = new UriBuilder(baseApiUrl) {
                Path = relativeUrl
            };


            uriBuilder.BuildQueryString(new { q = query });

            //var response = await httpClient.GetStringAsync(uriBuilder.Uri);
            var httpResponse = await httpClient.GetAsync(uriBuilder.Uri);
            try {
                httpResponse.EnsureSuccessStatusCode();
            } catch (HttpRequestException ex) {
                throw new HttpRequestExtException(httpResponse.StatusCode, ex.Message, ex);
            }

            var response = await httpResponse.Content.ReadAsStringAsync();

            IEnumerable<SearchResult<Person>> peopleSearchResults = DomainObjectFactory.CreatePeopleSearchResults(response);

            return peopleSearchResults;
        }

        #endregion

        #region IShow

        /// <summary>
        /// Retreive all primary information for a given show
        /// with possible embedding of additional information.
        /// </summary>
        /// <param name="showId"></param>
        /// <param name="embed">Embedded additional information</param>
        /// <returns>Show info</returns>
        public Show GetShow(string showId, EmbedType? embed = null) {
            return GetShowAsync(showId, embed).Result;
        }

        /// <summary>
        /// Gets a complete list of episodes for the given show.
        /// Episodes are returned in their airing order and include
        /// full episode information.
        /// 
        /// By default, specials are not included in the list.
        /// </summary>
        /// <param name="showId">Show id</param>
        /// <param name="includeSpecials">True to include special episodes, otherwise false. Default is true.</param>
        /// <returns>Epiodes list</returns>
        public IEnumerable<Episode> GetShowEpisodeList(string showId, bool includeSpecials = true) {
            return GetShowEpisodeListAsync(showId, includeSpecials).Result;
        }


        /// <summary>
        /// Retreive one specific episode from particular show given
        /// its season and episode number.
        /// </summary>
        /// <param name="showId">Show id</param>
        /// <param name="season">Season number</param>
        /// <param name="episodeNumber">Episode number</param>
        /// <returns>Specific episode</returns>
        public Episode GetShowEpisode(string showId, string season, string episodeNumber) {
            return GetShowEpisodeAsync(showId, season, episodeNumber).Result;
        }


        /// <summary>
        /// Retreive all episodes from particular show that have aired on a specific date.
        /// </summary>
        /// <param name="showId">Show id</param>
        /// <param name="airDate">Air date</param>
        /// <returns>List of epiodes of particular show</returns>
        public IEnumerable<Episode> GetShowEpisodes(string showId, DateTime airDate) {
            return GetShowEpisodesAsync(showId, airDate).Result;
        }

        /// <summary>
        /// Gets a complete list of seasons for the given show.
        /// Seasons are returned in ascending order and conain the
        /// full informations that's known about them.
        /// </summary>
        /// <param name="showId">Show id</param>
        /// <returns>Coplete list of seasons for the given show</returns>
        public IEnumerable<Season> GetShowSeasons(string showId) {
            return GetShowSeasonsAsync(showId).Result;
        }

        /// <summary>
        /// Gets a list of main cast for a show.
        /// Each cast item is a combination of a person and a character.
        /// Items are ordered by importance, which is determined by the total number of appearances of the given
        /// character in show.
        /// </summary>
        /// <param name="showId">Show id</param>
        /// <returns>List of main cast for a show</returns>
        public IEnumerable<Cast> GetShowCast(string showId) {
            return GetShowCastAsync(showId).Result;
        }

        /// <summary>
        /// A list of main crew for a show. Each crew item is a combination of a person and their crew type.
        /// </summary>
        /// <param name="showId">Show id</param>
        /// <returns>A list of main crew for a show.</returns>
        public IEnumerable<Crew> GetShowCrew(string showId) {
            return GetShowCrewAsync(showId).Result;
        }


        /// <summary>
        /// Gets a list of aliases for a show.
        /// </summary>
        /// <param name="showId"></param>
        /// <returns>A list of aliases for a show</returns>
        public IEnumerable<Alias> GetShowAliases(string showId) {
            return GetShowAliasesAsync(showId).Result;
        }

        /// <summary>
        /// Gets a list of all shows in TvMaze database with all primary information included.
        /// List is paginated with a maximum of 250 results per call.
        /// The pagination is based on show ID, e.g. page 0 will contain
        /// shows with IDs between 0 and 250. This means a single page
        /// might contain less than 250 results in case of deletions.
        /// End of list is reachd when you receive a HTTP 404 response code.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns>A list of all shows</returns>
        public IEnumerable<Show> GetAllShows(int pageNumber = 0) {
            return GetAllShowsAsync(pageNumber).Result;
        }

        /// <summary>
        /// Gets a list of all shows in the TvMaze database andthe timestap when they
        /// were last updated.
        /// </summary>
        /// <returns>List of all updated shows</returns>
        public IEnumerable<ShowUpdate> GetShowUpdates() {
            return GetShowUpdatesAsync().Result;
        }

        #endregion

        #region IShowAsync

        /// <summary>
        /// Retreive all primary information for a given hsow
        /// with possible embedding of additional information.
        /// </summary>
        /// <param name="showId">Show id</param>
        /// <param name="embed">Embedded additional information</param>
        /// <returns>Show info</returns>
        public async Task<Show> GetShowAsync(string showId, EmbedType? embed = null) {
            if(string.IsNullOrEmpty(showId)) throw new ArgumentNullException(nameof(showId));

            const string relativeUrl = "/shows";

            var uriBuilder = new UriBuilder(baseApiUrl) {
                Path = relativeUrl
            };

            uriBuilder.AppendPathSegment(showId);

            if (embed != null) {
                uriBuilder.BuildQueryString(new { embed = embed.Value.GetEnumDescription() });
            }

            //var response = await httpClient.GetStringAsync(uriBuilder.Uri);
            var httpResponse = await httpClient.GetAsync(uriBuilder.Uri);
            try {
                httpResponse.EnsureSuccessStatusCode();
            } catch (HttpRequestException ex) {
                throw new HttpRequestExtException(httpResponse.StatusCode, ex.Message, ex);
            }

            var response = await httpResponse.Content.ReadAsStringAsync();

            Show show = DomainObjectFactory.CreateShow(response);

            return show;

        }

        /// <summary>
        /// Gets a complete list of episodes for the given show.
        /// Episodes are returned in their airing order and include
        /// full episode information.
        /// 
        /// By default, specials are not included in the list.
        /// </summary>
        /// <param name="showId">Show id</param>
        /// <param name="includeSpecials">True to include special episodes, otherwise false. Default is true.</param>
        /// <returns>Epiodes list</returns>
        public async Task<IEnumerable<Episode>> GetShowEpisodeListAsync(string showId, bool includeSpecials = true) {
            if (string.IsNullOrEmpty(showId)) throw new ArgumentNullException(nameof(showId));

            const string relativeUrl = "/shows";

            var uriBuilder = new UriBuilder(baseApiUrl) {
                Path = relativeUrl
            };

            uriBuilder.AppendPathSegments(showId, "episodes");

            if (includeSpecials) {
                uriBuilder.BuildQueryString(new { specials = "1" });
            }

            //var response = await httpClient.GetStringAsync(uriBuilder.Uri);
            var httpResponse = await httpClient.GetAsync(uriBuilder.Uri);
            try {
                httpResponse.EnsureSuccessStatusCode();
            } catch (HttpRequestException ex) {
                throw new HttpRequestExtException(httpResponse.StatusCode, ex.Message, ex);
            }

            var response = await httpResponse.Content.ReadAsStringAsync();

            var episodes = DomainObjectFactory.CreateEpisodes(response);

            return episodes;
        }


        /// <summary>
        /// Retreive one specific episode from particular show given
        /// its season and episode number.
        /// </summary>
        /// <param name="showId">Show id</param>
        /// <param name="season">Season number</param>
        /// <param name="episodeNumber">Episode number</param>
        /// <returns>Specific episode</returns>
        public async Task<Episode> GetShowEpisodeAsync(string showId, string season, string episodeNumber) {
            if (string.IsNullOrEmpty(showId)) throw new ArgumentNullException(nameof(showId));
            if (string.IsNullOrEmpty(season)) throw new ArgumentNullException(nameof(season));
            if (string.IsNullOrEmpty(episodeNumber)) throw new ArgumentNullException(nameof(episodeNumber));

            const string relativeUrl = "/shows";

            var uriBuilder = new UriBuilder(baseApiUrl) {
                Path = relativeUrl
            };

            uriBuilder.AppendPathSegments(showId, "episodebynumber");
            uriBuilder.BuildQueryString(new {season, number = episodeNumber });

            //var response = await httpClient.GetStringAsync(uriBuilder.Uri);
            var httpResponse = await httpClient.GetAsync(uriBuilder.Uri);
            try {
                httpResponse.EnsureSuccessStatusCode();
            } catch (HttpRequestException ex) {
                throw new HttpRequestExtException(httpResponse.StatusCode, ex.Message, ex);
            }

            var response = await httpResponse.Content.ReadAsStringAsync();

            var episode = DomainObjectFactory.CreateEpisode(response);

            return episode;
        }

        /// <summary>
        /// Retreive all episodes from particular show that have aired on a specific date.
        /// </summary>
        /// <param name="showId">Show id</param>
        /// <param name="airDate">Air date</param>
        /// <returns>List of epiodes of particular show</returns>
        public async Task<IEnumerable<Episode>> GetShowEpisodesAsync(string showId, DateTime airDate) {
            if (string.IsNullOrEmpty(showId)) throw new ArgumentNullException(nameof(showId));

            const string relativeUrl = "/shows";

            var uriBuilder = new UriBuilder(baseApiUrl) {
                Path = relativeUrl
            };

            uriBuilder.AppendPathSegments(showId, "episodesbydate");
            uriBuilder.BuildQueryString(new { date = airDate.ToString(DATE_ISO_8601, CultureInfo.InvariantCulture) });

            //var response = await httpClient.GetStringAsync(uriBuilder.Uri);
            var httpResponse = await httpClient.GetAsync(uriBuilder.Uri);
            try {
                httpResponse.EnsureSuccessStatusCode();
            } catch (HttpRequestException ex) {
                throw new HttpRequestExtException(httpResponse.StatusCode, ex.Message, ex);
            }

            var response = await httpResponse.Content.ReadAsStringAsync();

            var episodes = DomainObjectFactory.CreateEpisodes(response);

            return episodes;
        }

        /// <summary>
        /// Gets a complete list of seasons for the given show.
        /// Seasons are returned in ascending order and conain the
        /// full informations that's known about them.
        /// </summary>
        /// <param name="showId">Show id</param>
        /// <returns>Coplete list of seasons for the given show</returns>
        public async Task<IEnumerable<Season>> GetShowSeasonsAsync(string showId) {
            if (string.IsNullOrEmpty(showId)) throw new ArgumentNullException(nameof(showId));

            const string relativeUrl = "/shows";

            var uriBuilder = new UriBuilder(baseApiUrl) {
                Path = relativeUrl
            };

            uriBuilder.AppendPathSegments(showId, "seasons");

            //var response = await httpClient.GetStringAsync(uriBuilder.Uri);
            var httpResponse = await httpClient.GetAsync(uriBuilder.Uri);
            try {
                httpResponse.EnsureSuccessStatusCode();
            } catch (HttpRequestException ex) {
                throw new HttpRequestExtException(httpResponse.StatusCode, ex.Message, ex);
            }

            var response = await httpResponse.Content.ReadAsStringAsync();

            var seasons = DomainObjectFactory.CreateSeasons(response);

            return seasons;
        }

        /// <summary>
        /// Gets a list of main cast for a show.
        /// Each cast item is a combination of a person and a character.
        /// Items are ordered by importance, which is determined by the total number of appearances of the given
        /// character in show.
        /// </summary>
        /// <param name="showId">Show id</param>
        /// <returns>List of main cast for a show</returns>
        public async Task<IEnumerable<Cast>> GetShowCastAsync(string showId) {
            if (string.IsNullOrEmpty(showId)) throw new ArgumentNullException(nameof(showId));

            const string relativeUrl = "/shows";

            var uriBuilder = new UriBuilder(baseApiUrl) {
                Path = relativeUrl
            };

            uriBuilder.AppendPathSegments(showId, "cast");

            //var response = await httpClient.GetStringAsync(uriBuilder.Uri);
            var httpResponse = await httpClient.GetAsync(uriBuilder.Uri);
            try {
                httpResponse.EnsureSuccessStatusCode();
            } catch (HttpRequestException ex) {
                throw new HttpRequestExtException(httpResponse.StatusCode, ex.Message, ex);
            }

            var response = await httpResponse.Content.ReadAsStringAsync();

            var cast = DomainObjectFactory.CreateCasts(response);

            return cast;
        }


        /// <summary>
        /// A list of main crew for a show. Each crew item is a combination of a person and their crew type.
        /// </summary>
        /// <param name="showId">Show id</param>
        /// <returns>A list of main crew for a show.</returns>
        public async Task<IEnumerable<Crew>> GetShowCrewAsync(string showId) {
            if (string.IsNullOrEmpty(showId)) throw new ArgumentNullException(nameof(showId));

            const string relativeUrl = "/shows";

            var uriBuilder = new UriBuilder(baseApiUrl) {
                Path = relativeUrl
            };

            uriBuilder.AppendPathSegments(showId, "crew");

            //var response = await httpClient.GetStringAsync(uriBuilder.Uri);
            var httpResponse = await httpClient.GetAsync(uriBuilder.Uri);
            try {
                httpResponse.EnsureSuccessStatusCode();
            } catch (HttpRequestException ex) {
                throw new HttpRequestExtException(httpResponse.StatusCode, ex.Message, ex);
            }

            var response = await httpResponse.Content.ReadAsStringAsync();

            var crew = DomainObjectFactory.CreateCrews(response);

            return crew;
        }

        /// <summary>
        /// Gets a list of aliases for a show.
        /// </summary>
        /// <param name="showId"></param>
        /// <returns>A list of aliases for a show</returns>
        public async Task<IEnumerable<Alias>> GetShowAliasesAsync(string showId) {
            if (string.IsNullOrEmpty(showId)) throw new ArgumentNullException(nameof(showId));

            const string relativeUrl = "/shows";

            var uriBuilder = new UriBuilder(baseApiUrl) {
                Path = relativeUrl
            };

            uriBuilder.AppendPathSegments(showId, "akas");

            //var response = await httpClient.GetStringAsync(uriBuilder.Uri);
            var httpResponse = await httpClient.GetAsync(uriBuilder.Uri);
            try {
                httpResponse.EnsureSuccessStatusCode();
            } catch (HttpRequestException ex) {
                throw new HttpRequestExtException(httpResponse.StatusCode, ex.Message, ex);
            }

            var response = await httpResponse.Content.ReadAsStringAsync();

            var aliases = DomainObjectFactory.CreateAliases(response);

            return aliases;
        }

        /// <summary>
        /// Gets a list of all shows in TvMaze database with all primary information included.
        /// List is paginated with a maximus of 250 results per call.
        /// The pagination is based on show ID, e.g. page 0 will contain
        /// shows with IDs between 0 and 250. This means a single page
        /// might contain less than 250 results in case of deletions.
        /// End of list is reachd when you receive a HTTP 404 response code.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns>A list of all shows</returns>
        public async Task<IEnumerable<Show>> GetAllShowsAsync(int pageNumber = 0) {
            if (pageNumber < 0) throw new ArgumentException(nameof(pageNumber));

            const string relativeUrl = "/shows";

            var uriBuilder = new UriBuilder(baseApiUrl) {
                Path = relativeUrl
            };

            if (pageNumber > 0) {
                uriBuilder.BuildQueryString(new { page = pageNumber.ToString() });
            }

            //var response = await httpClient.GetStringAsync(uriBuilder.Uri);
            var httpResponse = await httpClient.GetAsync(uriBuilder.Uri);
            try {
                httpResponse.EnsureSuccessStatusCode();
            } catch (HttpRequestException ex) {
                throw new HttpRequestExtException(httpResponse.StatusCode, ex.Message, ex);
            }

            var response = await httpResponse.Content.ReadAsStringAsync();

            var shows = DomainObjectFactory.CreateShows(response);

            return shows;
        }

        /// <summary>
        /// Gets a list of all shows in the TvMaze database and the timestap when they
        /// were last updated.
        /// </summary>
        /// <returns>List of all updated shows</returns>
        public async Task<IEnumerable<ShowUpdate>> GetShowUpdatesAsync() {
            const string relativeUrl = "/updates/shows";

            var uriBuilder = new UriBuilder(baseApiUrl) {
                Path = relativeUrl
            };

            //var response = await httpClient.GetStringAsync(uriBuilder.Uri);
            var httpResponse = await httpClient.GetAsync(uriBuilder.Uri);
            try {
                httpResponse.EnsureSuccessStatusCode();
            } catch (HttpRequestException ex) {
                throw new HttpRequestExtException(httpResponse.StatusCode, ex.Message, ex);
            }

            var response = await httpResponse.Content.ReadAsStringAsync();

            var showUpdates = DomainObjectFactory.CreateShowUpdates(response);

            return showUpdates;
        }

        #endregion

    }
}
