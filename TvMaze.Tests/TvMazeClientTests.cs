using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using RichardSzalay.MockHttp;
using TvMaze.Domain;

namespace TvMaze.Tests {
    [TestFixture]
    public class TvMazeClientTests {

        private const string BASE_API_URL = "http://localhost";

        public string BasePath { get; set; }

        [SetUp]
        public void SetUp() {
            BasePath = AppDomain.CurrentDomain.BaseDirectory;
        }

        #region IPeople
        #endregion

        #region IPeopleAsync

        [Test]
        public async Task GetPersonInfoAsync_MockWebApi_PersonId_Person() {
            // Arrange
            const int personId = 1;
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "person.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/people/{personId}")
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var person = await tvMazeClient.GetPersonInfoAsync(personId.ToString());

            // Assert
            Assert.IsNotNull(person);
            Assert.AreEqual(personId, person.Id);
            mockHttp.VerifyNoOutstandingExpectation();
        }

        [Test]
        public async Task GetGetPersonInfoAsync_MockWebApi_PersonId_WithQueryParam_Person() {
            // Arrange
            const int personId = 1;
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "person_embed_castcredits.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/people/{personId}")
                .WithQueryString("embed", "castcredits")
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var person = await tvMazeClient.GetPersonInfoAsync(personId.ToString(), EmbedType.CastCredits);

            // Assert
            Assert.IsNotNull(person);
            Assert.AreEqual(personId, person.Id);
            Assert.IsNotEmpty(person.CastCredits);
            mockHttp.VerifyNoOutstandingExpectation();

        }

        [Test]
        public async Task GetCastCreditsAsync_MockWebApi_PersonId_CastCredits() {
            // Arrange
            const int personId = 1;
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "person_castcredits.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/people/{personId}/castcredits")
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var castCredits = await tvMazeClient.GetCastCreditsAsync(personId.ToString());

            // Assert
            Assert.IsNotNull(castCredits);
            Assert.IsNotEmpty(castCredits);
            mockHttp.VerifyNoOutstandingExpectation();
        }


        [Test]
        public async Task GetCastCreditsAsync_MockWebApi_PersonId_WithQueryParam_CastCredits() {
            // Arrange
            const int personId = 1;
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "person_castcredits_embed_show.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/people/{personId}/castcredits")
                .WithQueryString("embed", "show")
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var castCredits = await tvMazeClient.GetCastCreditsAsync(personId.ToString(), EmbedType.Show);

            // Assert
            Assert.IsNotNull(castCredits);
            Assert.IsNotEmpty(castCredits);
            Assert.IsNotNull(castCredits.ToArray()[0].Show);
            mockHttp.VerifyNoOutstandingExpectation();

        }

        // public async Task<IEnumerable<CrewCredit>> GetCrewCreditsAsync(string personId, EmbedType? embed = null) {
        [Test]
        public async Task GetCrewCreditsAsync_MockWebApi_PersonId_CrewCredits() {
            // Arrange
            const int personId = 1;
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "person_crewcredits.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/people/{personId}/crewcredits")
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var crewCredits = await tvMazeClient.GetCrewCreditsAsync(personId.ToString());

            // Assert
            Assert.IsNotNull(crewCredits);
            Assert.IsNotEmpty(crewCredits);
            mockHttp.VerifyNoOutstandingExpectation();
        }

        [Test]
        public async Task GetCrewCreditsAsync_MockWebApi_PersonId_WithQueryParam_CrewCredits() {
            // Arrange
            const int personId = 1;
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "person_crewcredits_embed_show.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/people/{personId}/crewcredits")
                .WithQueryString("embed", "show")
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var crewCredits = await tvMazeClient.GetCrewCreditsAsync(personId.ToString(), EmbedType.Show);

            // Assert
            Assert.IsNotNull(crewCredits);
            Assert.IsNotEmpty(crewCredits);
            Assert.IsNotNull(crewCredits.ToArray()[0].Show);
            mockHttp.VerifyNoOutstandingExpectation();

        }

        #endregion

        #region ISchedule

        [Test]
        public void GetFullSchedule_MockWebApi_FullSchedule() {
            // Arrange
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "schedule.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/schedule/full")
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var schedule = tvMazeClient.GetFullSchedule();

            // Assert
            Assert.IsNotNull(schedule);
            Assert.IsNotEmpty(schedule.Episodes);
            mockHttp.VerifyNoOutstandingExpectation();

        }

        [Test]
        public void GetSchedule_MockWebApi_DefaultQueryParams_Schedule() {
            // Arrange
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "schedule.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/schedule")
                .With(x => string.IsNullOrEmpty(x.RequestUri.Query))
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var schedule = tvMazeClient.GetSchedule();

            // Assert
            Assert.IsNotNull(schedule);
            Assert.IsNotEmpty(schedule.Episodes);
            mockHttp.VerifyNoOutstandingExpectation();

        }

        [Test]
        public void GetSchedule_MockWebApi_WithQueryParams_Schedule() {
            // Arrange
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "schedule.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/schedule?country=US&date=2014-12-01")
                .WithQueryString("country", "US")
                .WithQueryString("date", "2014-12-01")
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var schedule = tvMazeClient.GetSchedule("US", new DateTime(2014, 12, 01));

            // Assert
            Assert.IsNotNull(schedule);
            Assert.IsNotEmpty(schedule.Episodes);
            mockHttp.VerifyNoOutstandingExpectation();

        }

        #endregion

        #region IScheduleAsync

        [Test]
        public async Task GetFullScheduleAsync_MockWebApi_FullSchedule() {
            // Arrange
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "schedule.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/schedule/full")
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var schedule = await tvMazeClient.GetFullScheduleAsync();

            // Assert
            Assert.IsNotNull(schedule);
            Assert.IsNotEmpty(schedule.Episodes);
            mockHttp.VerifyNoOutstandingExpectation();

        }

        [Test]
        public void GetFullScheduleAsync_MockWebApiInternalServerError_HttpRequestExtException() {
            // Arrange
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/schedule/full")
                .Respond(HttpStatusCode.InternalServerError);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act and Assert
            AsyncTestDelegate act = async () => await tvMazeClient.GetFullScheduleAsync();
            //Assert.That(act, Throws.TypeOf<HttpRequestException>());
            
            var ex = Assert.ThrowsAsync<HttpRequestExtException>(act);
            Assert.That(ex.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError));


        }

        [Test]
        public void GetFullScheduleAsync_MockWebApiNotFound_HttpRequestExtException() {
            // Arrange
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When($"{BASE_API_URL}/schedule/full")
                .Respond(HttpStatusCode.NotFound);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act and Assert
            AsyncTestDelegate act = async () => await tvMazeClient.GetFullScheduleAsync();
            //Assert.That(act, Throws.TypeOf<HttpRequestException>());

            var ex = Assert.ThrowsAsync<HttpRequestExtException>(act);
            Assert.That(ex.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            //Assert.AreEqual(HttpStatusCode.NotFound, ex.StatusCode);

        }

        [Test]
        public async Task GetScheduleAsync_MockWebApi_DefaultQueryParams_Schedule() {
            // Arrange
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "schedule.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/schedule")
                .With(x => string.IsNullOrEmpty(x.RequestUri.Query))
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var schedule = await tvMazeClient.GetScheduleAsync();

            // Assert
            Assert.IsNotNull(schedule);
            Assert.IsNotEmpty(schedule.Episodes);
            mockHttp.VerifyNoOutstandingExpectation();

        }

        [Test]
        public async Task GetScheduleAsync_MockWebApi_WithQueryParams_Schedule() {
            // Arrange
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "schedule.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/schedule?country=US&date=2014-12-01")
                .WithQueryString("country", "US")
                .WithQueryString("date", "2014-12-01")
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var schedule = await tvMazeClient.GetScheduleAsync("US", new DateTime(2014, 12, 01));

            // Assert
            Assert.IsNotNull(schedule);
            Assert.IsNotEmpty(schedule.Episodes);
            mockHttp.VerifyNoOutstandingExpectation();

        }

        #endregion

        #region ISearch
        #endregion

        #region ISearchAsync

        [Test]
        public async Task ShowSearchAsync_MockWebApi_SearchQuery_ShowSearchResults() {
            // Arrange
            const string query = "girls";
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "search_show.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/search/shows")
                .WithQueryString("q", query)
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var results = await tvMazeClient.ShowSearchAsync(query);

            // Assert
            Assert.IsNotNull(results);
            Assert.IsNotEmpty(results);
            Assert.IsInstanceOf<Show>(results.ToArray()[0].Element);
            Assert.AreEqual(139, results.ToArray()[0].Element.Id);
            mockHttp.VerifyNoOutstandingExpectation();
        }

        //public async Task<Show> ShowSingleSearchAsync(string query, EmbedType? embed = null) {
        [Test]
        public async Task ShowSingleSearchAsync_MockWebApi_SearchQuery_Show() {
            // Arrange
            const string query = "under the dome";
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "show.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/singlesearch/shows")
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var show = await tvMazeClient.ShowSingleSearchAsync(query);

            // Assert
            Assert.IsNotNull(show);
            Assert.AreEqual(1, show.Id);
            mockHttp.VerifyNoOutstandingExpectation();

        }

        [Test]
        public async Task ShowSingleSearchAsync_MockWebApi_ShowId_WithQueryParam_Show() {
            // Arrange
            const string query = "under the dome";
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "show_embed_episodes.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/singlesearch/shows")
                .WithQueryString("embed", "episodes")
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var show = await tvMazeClient.ShowSingleSearchAsync(query, EmbedType.Episodes);

            // Assert
            Assert.IsNotNull(show);
            Assert.AreEqual(1, show.Id);
            Assert.IsNotEmpty(show.Episodes);
            mockHttp.VerifyNoOutstandingExpectation();

        }


        [Test]
        public async Task PeopleSearchAsync_MockWebApi_SearchQuery_PeopleSearchResults() {
            // Arrange
            const string query = "lauren";
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "search_people.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/search/people")
                .WithQueryString("q", query)
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var results = await tvMazeClient.PeopleSearchAsync(query);

            // Assert
            Assert.IsNotNull(results);
            Assert.IsNotEmpty(results);
            Assert.IsInstanceOf<Person>(results.ToArray()[0].Element);
            Assert.AreEqual(123897, results.ToArray()[0].Element.Id);
            mockHttp.VerifyNoOutstandingExpectation();
        }

        #endregion

        #region IShow

        [Test]
        public void GetGetShow_MockWebApi_ShowId_Show() {
            // Arrange
            const int showId = 1;
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "show.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/shows/{showId}")
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var show = tvMazeClient.GetShow(showId.ToString());

            // Assert
            Assert.IsNotNull(show);
            Assert.AreEqual(showId, show.Id);
            mockHttp.VerifyNoOutstandingExpectation();

        }

        [Test]
        public void GetGetShow_MockWebApi_ShowId_WithQueryParam_Show() {
            // Arrange
            const int showId = 1;
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "show_embed_cast.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/shows/{showId}")
                .WithQueryString("embed", "cast")
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var show = tvMazeClient.GetShow(showId.ToString(), EmbedType.Cast);

            // Assert
            Assert.IsNotNull(show);
            Assert.AreEqual(showId, show.Id);
            Assert.IsNotEmpty(show.Casts);
            mockHttp.VerifyNoOutstandingExpectation();

        }

        [Test]
        public void GetShowEpisodeList_MockWebApi_ShowId_With_Specials_EpisodeList() {
            // Arrange
            const int showId = 1;
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "episodes.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/shows/{showId}/episodes")
                .WithQueryString("specials", "1")
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var episodeList = tvMazeClient.GetShowEpisodeList(showId.ToString());

            // Assert
            Assert.IsNotNull(episodeList);
            Assert.IsNotEmpty(episodeList);
            mockHttp.VerifyNoOutstandingExpectation();
        }

        [Test]
        public void GetShowEpisode_MockWebApi_ShowId_Season_EpisodeNumber_Episode() {
            // Arrange
            const int showId = 1;
            const int season = 1;
            const int episodeNumber = 1;
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "episode.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/shows/{showId}/episodebynumber")
                .WithQueryString("season", "1")
                .WithQueryString("number", "1")
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var episode = tvMazeClient.GetShowEpisode(showId.ToString(), season.ToString(), episodeNumber.ToString());

            // Assert
            Assert.IsNotNull(episode);
            Assert.AreEqual(1, episode.Id);
            mockHttp.VerifyNoOutstandingExpectation();
        }

        [Test]
        public void GetShowEpisodes_MockWebApi_ShowId_AirDate_EpisodeListByAirDate() {
            // Arrange
            const int showId = 1;
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "episodes.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/shows/{showId}/episodesbydate")
                .WithQueryString("date", "2013-07-01")
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var episodeList = tvMazeClient.GetShowEpisodes(showId.ToString(), new DateTime(2013, 7, 1));

            // Assert
            Assert.IsNotNull(episodeList);
            Assert.IsNotEmpty(episodeList);
            mockHttp.VerifyNoOutstandingExpectation();

        }

        [Test]
        public void GetShowSeasons_MockWebApi_ShowId_Seasons() {
            // Arrange
            const int showId = 1;
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "seasons.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/shows/{showId}/seasons")
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var seasons = tvMazeClient.GetShowSeasons(showId.ToString());

            // Assert
            Assert.IsNotNull(seasons);
            Assert.IsNotEmpty(seasons);
            mockHttp.VerifyNoOutstandingExpectation();

        }

        [Test]
        public void GetShowCast_MockWebApi_ShowId_ShowCast() {
            // Arrange
            const int showId = 1;
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "cast.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/shows/{showId}/cast")
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var cast = tvMazeClient.GetShowCast(showId.ToString());

            // Assert
            Assert.IsNotNull(cast);
            Assert.IsNotEmpty(cast);
            mockHttp.VerifyNoOutstandingExpectation();
        }

        [Test]
        public void GetShowCrew_MockWebApi_ShowId_ShowCrew() {
            // Arrange
            const int showId = 1;
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "crew.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/shows/{showId}/crew")
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var crew = tvMazeClient.GetShowCrew(showId.ToString());

            // Assert
            Assert.IsNotNull(crew);
            Assert.IsNotEmpty(crew);
            mockHttp.VerifyNoOutstandingExpectation();
        }

        [Test]
        public void GetShowAliases_MockWebApi_ShowId_ShowAliases() {
            // Arrange
            const int showId = 1;
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "cast.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/shows/{showId}/akas")
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var aliases = tvMazeClient.GetShowAliases(showId.ToString());

            // Assert
            Assert.IsNotNull(aliases);
            Assert.IsNotEmpty(aliases);
            mockHttp.VerifyNoOutstandingExpectation();
        }

        [Test]
        public void GetAllShows_MockWebApi_Page0_Shows() {
            // Arrange
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "cast.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/shows")
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var shows = tvMazeClient.GetAllShows();

            // Assert
            Assert.IsNotNull(shows);
            Assert.IsNotEmpty(shows);
            mockHttp.VerifyNoOutstandingExpectation();

        }

        [Test]
        public void GetAllShows_MockWebApi_Page1_Shows() {
            // Arrange
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "cast.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/shows")
                .WithQueryString("page", "1")
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var shows = tvMazeClient.GetAllShows(1);

            // Assert
            Assert.IsNotNull(shows);
            Assert.IsNotEmpty(shows);
            mockHttp.VerifyNoOutstandingExpectation();
        }

        [Test]
        public void GetShowUpdates_MockWebApi_ShowsUpdates() {
            // Arrange
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "show_updates.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/updates/shows")
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var showUpdates = tvMazeClient.GetShowUpdates();

            // Assert
            Assert.IsNotNull(showUpdates);
            Assert.IsNotEmpty(showUpdates);
            mockHttp.VerifyNoOutstandingExpectation();

        }

        #endregion

        #region IShowAsync

        [Test]
        public async Task GetShowAsync_MockWebApi_ShowId_Show() {
            // Arrange
            const int showId = 1;
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "show.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/shows/{showId}")
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var show = await tvMazeClient.GetShowAsync(showId.ToString());

            // Assert
            Assert.IsNotNull(show);
            Assert.AreEqual(showId, show.Id);
            mockHttp.VerifyNoOutstandingExpectation();

        }

        [Test]
        public async Task GetShowAsync_MockWebApi_ShowId_WithQueryParam_Show() {
            // Arrange
            const int showId = 1;
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "show_embed_cast.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/shows/{showId}")
                .WithQueryString("embed", "cast")
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var show = await tvMazeClient.GetShowAsync(showId.ToString(), EmbedType.Cast);

            // Assert
            Assert.IsNotNull(show);
            Assert.AreEqual(showId, show.Id);
            Assert.IsNotEmpty(show.Casts);
            mockHttp.VerifyNoOutstandingExpectation();

        }


        [Test]
        public async Task GetShowEpisodeListAsync_MockWebApi_ShowId_With_Specials_EpisodeList() {
            // Arrange
            const int showId = 1;
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "episodes.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/shows/{showId}/episodes")
                .WithQueryString("specials", "1")
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var episodeList = await tvMazeClient.GetShowEpisodeListAsync(showId.ToString());

            // Assert
            Assert.IsNotNull(episodeList);
            Assert.IsNotEmpty(episodeList);
            mockHttp.VerifyNoOutstandingExpectation();
        }

        [Test]
        public async Task GetShowEpisodeAsync_MockWebApi_ShowId_Season_EpisodeNumber_Episode() {
            // Arrange
            const int showId = 1;
            const int season = 1;
            const int episodeNumber = 1;
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "episode.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/shows/{showId}/episodebynumber")
                .WithQueryString("season", "1")
                .WithQueryString("number", "1")
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var episode = await tvMazeClient.GetShowEpisodeAsync(showId.ToString(), season.ToString(), episodeNumber.ToString());

            // Assert
            Assert.IsNotNull(episode);
            Assert.AreEqual(1, episode.Id);
            mockHttp.VerifyNoOutstandingExpectation();
        }

        [Test]
        public async Task GetShowEpisodesAsync_MockWebApi_ShowId_AirDate_EpisodeListByAirDate() {
            // Arrange
            const int showId = 1;
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "episodes.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/shows/{showId}/episodesbydate")
                .WithQueryString("date", "2013-07-01")
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var episodeList = await tvMazeClient.GetShowEpisodesAsync(showId.ToString(), new DateTime(2013, 7, 1));

            // Assert
            Assert.IsNotNull(episodeList);
            Assert.IsNotEmpty(episodeList);
            mockHttp.VerifyNoOutstandingExpectation();

        }

        [Test]
        public async Task GetShowSeasonsAsync_MockWebApi_ShowId_Seasons() {
            // Arrange
            const int showId = 1;
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "seasons.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/shows/{showId}/seasons")
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var seasons = await tvMazeClient.GetShowSeasonsAsync(showId.ToString());

            // Assert
            Assert.IsNotNull(seasons);
            Assert.IsNotEmpty(seasons);
            mockHttp.VerifyNoOutstandingExpectation();

        }

        [Test]
        public async Task GetShowCastAsync_MockWebApi_ShowId_ShowCast() {
            // Arrange
            const int showId = 1;
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "cast.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/shows/{showId}/cast")
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var cast = await tvMazeClient.GetShowCastAsync(showId.ToString());

            // Assert
            Assert.IsNotNull(cast);
            Assert.IsNotEmpty(cast);
            mockHttp.VerifyNoOutstandingExpectation();
        }


        [Test]
        public async Task GetShowCrewAsync_MockWebApi_ShowId_ShowCrew() {
            // Arrange
            const int showId = 1;
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "crew.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/shows/{showId}/crew")
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var crew = await tvMazeClient.GetShowCrewAsync(showId.ToString());

            // Assert
            Assert.IsNotNull(crew);
            Assert.IsNotEmpty(crew);
            mockHttp.VerifyNoOutstandingExpectation();
        }

        [Test]
        public async Task GetShowAliasesAsync_MockWebApi_ShowId_ShowAliases() {
            // Arrange
            const int showId = 1;
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "cast.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/shows/{showId}/akas")
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var aliases = await tvMazeClient.GetShowAliasesAsync(showId.ToString());

            // Assert
            Assert.IsNotNull(aliases);
            Assert.IsNotEmpty(aliases);
            mockHttp.VerifyNoOutstandingExpectation();
        }

        [Test]
        public async Task GetAllShowsAsync_MockWebApi_Page0_Shows() {
            // Arrange
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "cast.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/shows")
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var shows = await tvMazeClient.GetAllShowsAsync();

            // Assert
            Assert.IsNotNull(shows);
            Assert.IsNotEmpty(shows);
            mockHttp.VerifyNoOutstandingExpectation();

        }

        [Test]
        public async Task GetAllShowsAsync_MockWebApi_Page1_Shows() {
            // Arrange
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "cast.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/shows")
                .WithQueryString("page", "1")
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var shows = await tvMazeClient.GetAllShowsAsync(1);

            // Assert
            Assert.IsNotNull(shows);
            Assert.IsNotEmpty(shows);
            mockHttp.VerifyNoOutstandingExpectation();
        }

        [Test]
        public async Task GetShowUpdatesAsync_MockWebApi_ShowsUpdates() {
            // Arrange
            var json = File.ReadAllText(Path.Combine(BasePath, DomainObjectFactoryTests.JSON_DATA_PATH, "show_updates.json"));
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect($"{BASE_API_URL}/updates/shows")
                .Respond("application/json", json);

            var tvMazeClient = new TvMazeClient(BASE_API_URL, mockHttp.ToHttpClient());

            // Act
            var showUpdates = await tvMazeClient.GetShowUpdatesAsync();

            // Assert
            Assert.IsNotNull(showUpdates);
            Assert.IsNotEmpty(showUpdates);
            mockHttp.VerifyNoOutstandingExpectation();

        }

        #endregion

    }
}
