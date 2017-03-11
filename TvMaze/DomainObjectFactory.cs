using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TvMaze.Domain;
using TvMaze.JsonContractResolvers;

namespace TvMaze {
    /// <summary>
    /// DomainObjectFactory responsible for creation of domain objects
    /// based on Json input data
    /// </summary>
    public static class DomainObjectFactory {

        /// <summary>
        /// Creates <see cref="Episode"/> based on Json input data
        /// </summary>
        /// <param name="json">Json episode data</param>
        /// <returns>An episode instance</returns>
        public static Episode CreateEpisode(string json) {
            if(string.IsNullOrEmpty(json)) throw new ArgumentNullException(nameof(json));

            var episode = JsonConvert.DeserializeObject<Episode>(json);
            return episode;
        }

        /// <summary>
        /// Creates list of <see cref="Episode"/> based on Json input data
        /// </summary>
        /// <param name="json">Json episodes data</param>
        /// <returns>List of episode insances</returns>
        public static IList<Episode> CreateEpisodes(string json) {
            if(string.IsNullOrEmpty(json)) throw new ArgumentNullException(nameof(json));

            var episodes = JsonConvert.DeserializeObject<IList<Episode>>(json);
            return episodes;
        }

        /// <summary>
        /// Creates list of <see cref="Season"/> based on Json input data
        /// </summary>
        /// <param name="json">Json seasons data</param>
        /// <returns>List of season insances</returns>
        public static IList<Season> CreateSeasons(string json) {
            if (string.IsNullOrEmpty(json)) throw new ArgumentNullException(nameof(json));

            var seasons = JsonConvert.DeserializeObject<IList<Season>>(json);
            return seasons;
        }

        /// <summary>
        /// Creates <see cref="Person"/> based on Json input data
        /// </summary>
        /// <param name="json">Json person data</param>
        /// <returns>A person instance</returns>
        public static Person CreatePerson(string json) {
            if(string.IsNullOrEmpty(json)) throw new ArgumentNullException(nameof(json));

            //var person = JsonConvert.DeserializeObject<Person>(json);
            JObject jObject = JObject.Parse(json);
            var person = jObject.ToObject<Person>();
            person.CastCredits = jObject["_embedded"]?["castcredits"]?.ToObject<IList<CastCredit>>();

            return person;
        }

        /// <summary>
        /// Creates <see cref="Show"/> based on Json input data
        /// </summary>
        /// <param name="json">Json show sata</param>
        /// <returns>A show instance</returns>
        public static Show CreateShow(string json) {
            if(string.IsNullOrEmpty(json)) throw new ArgumentNullException(nameof(json));

            //var show = JsonConvert.DeserializeObject<Show>(json);
            JObject jObject = JObject.Parse(json);
            var show = jObject.ToObject<Show>();
            show.Casts = jObject["_embedded"]?["cast"]?.ToObject<IList<Cast>>();
            
            return show;
        }

        /// <summary>
        /// Creates list of <see cref="Show"/> based on Json input data
        /// </summary>
        /// <param name="json">Json shows data</param>
        /// <returns>List of show insances</returns>
        public static IList<Show> CreateShows(string json) {
            if (string.IsNullOrEmpty(json)) throw new ArgumentNullException(nameof(json));

            var shows = JsonConvert.DeserializeObject<IList<Show>>(json);
            return shows;
        }

        /// <summary>
        /// Creates list of <see cref="ShowUpdate"/> based on Json input data
        /// </summary>
        /// <param name="json">Json show updates data</param>
        /// <returns>List of show update insances</returns>
        public static IList<ShowUpdate> CreateShowUpdates(string json) {
            if (string.IsNullOrEmpty(json)) throw new ArgumentNullException(nameof(json));

            var showUpdatesDict = JsonConvert.DeserializeObject<IDictionary<string, string>>(json);
            var showUpdates = showUpdatesDict.Select(x => new ShowUpdate { ShowId = x.Key, Timestamp = long.Parse(x.Value)}).ToList();
            return showUpdates;
        }

        /// <summary>
        /// Creates <see cref="SearchResult{Person}"/> based on Json input data
        /// </summary>
        /// <param name="json">Json people search results data</param>
        /// <returns>Pepople search results</returns>
        public static IList<SearchResult<Person>> CreatePeopleSearchResults(string json) {
            if(string.IsNullOrEmpty(json)) throw new ArgumentNullException(nameof(json));

            var searchResults = JsonConvert.DeserializeObject<IList<SearchResult<Person>>>(json, new JsonSerializerSettings { ContractResolver = PeopleSearchResultContractResolver.Instance });
            //var searchResults = JsonConvert.DeserializeObject<IList<SearchResult<Person>>>(json);
            return searchResults;
        }

        /// <summary>
        /// Creates <see cref="SearchResult{Show}"/> based on Json input data
        /// </summary>
        /// <param name="json">Json show search results data</param>
        /// <returns>Pepople search results</returns>
        public static IList<SearchResult<Show>> CreateShowSearchResults(string json) {
            if (string.IsNullOrEmpty(json)) throw new ArgumentNullException(nameof(json));

            var searchResults = JsonConvert.DeserializeObject<IList<SearchResult<Show>>>(json, new JsonSerializerSettings { ContractResolver = ShowSearchResultContractResolver.Instance });
            //var searchResults = JsonConvert.DeserializeObject<IList<SearchResult<Person>>>(json);
            return searchResults;
        }

        /// <summary>
        /// Creates <see cref="Schedule"/> based on Json input data
        /// </summary>
        /// <param name="json">Json schedules data</param>
        /// <returns>Schedules</returns>
        public static Schedule CreateSchedule(string json) {
            if(string.IsNullOrEmpty(json)) throw new ArgumentNullException(nameof(json));

            var episodes = JsonConvert.DeserializeObject<IList<Episode>>(json);
            return new Schedule(episodes);
        }

        /// <summary>
        /// Creates <see cref="Cast"/> list
        /// </summary>
        /// <param name="json">Json cast data</param>
        /// <returns>Cast list</returns>
        public static IList<Cast> CreateCasts(string json) {
            if (string.IsNullOrEmpty(json)) throw new ArgumentNullException(nameof(json));

            var casts = JsonConvert.DeserializeObject<IList<Cast>>(json);
            return casts;
        }

        /// <summary>
        /// Creates <see cref="Crew"/> list
        /// </summary>
        /// <param name="json">Json crew data</param>
        /// <returns>Crew list</returns>
        public static IList<Crew> CreateCrews(string json) {
            if (string.IsNullOrEmpty(json)) throw new ArgumentNullException(nameof(json));

            var crews = JsonConvert.DeserializeObject<IList<Crew>>(json);
            return crews;
        }

        /// <summary>
        /// Creates <see cref="Alias"/> list
        /// </summary>
        /// <param name="json">Json alias data</param>
        /// <returns>Alias list</returns>
        public static IList<Alias> CreateAliases(string json) {
            if (string.IsNullOrEmpty(json)) throw new ArgumentNullException(nameof(json));

            var aliases = JsonConvert.DeserializeObject<IList<Alias>>(json);
            return aliases;
        }

        /// <summary>
        /// Creates <see cref="CrewCredit"/> list
        /// </summary>
        /// <param name="json">Json crew credit data</param>
        /// <returns>Crew credit list</returns>
        public static IList<CrewCredit> CreateCrewCredits(string json) {
            if (string.IsNullOrEmpty(json)) throw new ArgumentNullException(nameof(json));

            //var crewCredits = JsonConvert.DeserializeObject<IList<CrewCredit>>(json);
            JArray jArray = JArray.Parse(json);
            var crewCredits = jArray.Select(x => {
                var castCredit = x.ToObject<CrewCredit>();
                castCredit.Show = x["_embedded"]?["show"]?.ToObject<Show>();
                return castCredit;
            }).ToList();

            return crewCredits;
        }

        /// <summary>
        /// Creates <see cref="CastCredit"/> list
        /// </summary>
        /// <param name="json">Json cast credit data</param>
        /// <returns>Cast credit list</returns>
        public static IList<CastCredit> CreateCastCredits(string json) {
            if (string.IsNullOrEmpty(json)) throw new ArgumentNullException(nameof(json));

            //var castCredits = JsonConvert.DeserializeObject<IList<CastCredit>>(json);
            JArray jArray = JArray.Parse(json);
            var castCredits = jArray.Select(x => {
                var castCredit = x.ToObject<CastCredit>();
                castCredit.Show = x["_embedded"]?["show"]?.ToObject<Show>();
                return castCredit;
            }).ToList();
            

            return castCredits;
        }


    }
}
