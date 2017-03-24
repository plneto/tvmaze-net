using System;
using System.IO;
using NUnit.Framework;
using TvMaze.Domain;

namespace TvMaze.Tests {
    [TestFixture]
    public class DomainObjectFactoryTests {

        public const string JSON_DATA_PATH = @"json_data_examples\v1\";
        public string BasePath { get; set; }

        [SetUp]
        public void SetUp() {
            BasePath = AppDomain.CurrentDomain.BaseDirectory;
        }

        [Test]
        public void CreateEpisode_ValidEpisodeJsonData_EpisodeInstance() {
            // Arrange
            var json = File.ReadAllText(Path.Combine(BasePath, JSON_DATA_PATH, "episode.json"));

            // Act
            var episode = DomainObjectFactory.CreateEpisode(json);

            // Assert
            Assert.IsNotNull(episode);
            Assert.AreEqual(1, episode.Id);
            Assert.AreEqual("http://www.tvmaze.com/episodes/1/under-the-dome-1x01-pilot", episode.Url);
            Assert.AreEqual("Pilot", episode.Name);
            Assert.AreEqual(1, episode.Season);
            Assert.AreEqual(1, episode.Number);
            Assert.AreEqual("2013-06-24", episode.AirDate);
            Assert.AreEqual("22:00", episode.AirTime);
            Assert.AreEqual("2013-06-24T22:00:00-04:00", episode.AirStamp);
            Assert.AreEqual(DateTimeOffset.Parse("2013-06-24T22:00:00-04:00"), episode.AirDateTimeOffset);
            Assert.AreEqual(60, episode.RunTime);
            Assert.AreEqual(2, episode.Image.Count);
            Assert.AreEqual("http://tvmazecdn.com/uploads/images/medium_landscape/1/4388.jpg",
                episode.Image[ImageType.Medium]);
            Assert.AreEqual("http://tvmazecdn.com/uploads/images/original_untouched/1/4388.jpg",
                episode.Image[ImageType.Original]);
            Assert.AreEqual(
                            "<p>When the residents of Chester's Mill find themselves trapped under a massive transparent dome with no way out, they struggle to survive as resources rapidly dwindle and panic quickly escalates.</p>",
                episode.Summary);
            Assert.AreEqual(1, episode.Links.Count);
            Assert.AreEqual("http://api.tvmaze.com/episodes/1", episode.Links[LinkType.Self].Href);


        }

        [Test]
        public void CreateEpisodes_ValidEpisodesJsonData_EpisodeInstanceList() {
            // Arrange
            var json = File.ReadAllText(Path.Combine(BasePath, JSON_DATA_PATH, "episodes.json"));

            // Act
            var episodes = DomainObjectFactory.CreateEpisodes(json);

            // Assert
            Assert.IsNotNull(episodes);
            Assert.IsNotEmpty(episodes);

            var episode = episodes[0];
            Assert.IsNotNull(episode);
            Assert.AreEqual(1, episode.Id);
            Assert.AreEqual("http://www.tvmaze.com/episodes/1/under-the-dome-1x01-pilot", episode.Url);
            Assert.AreEqual("Pilot", episode.Name);
            Assert.AreEqual(1, episode.Season);
            Assert.AreEqual(1, episode.Number);
            Assert.AreEqual("2013-06-24", episode.AirDate);
            Assert.AreEqual("22:00", episode.AirTime);
            Assert.AreEqual("2013-06-24T22:00:00-04:00", episode.AirStamp);
            Assert.AreEqual(DateTimeOffset.Parse("2013-06-24T22:00:00-04:00"), episode.AirDateTimeOffset);
            Assert.AreEqual(60, episode.RunTime);
            Assert.AreEqual(2, episode.Image.Count);
            Assert.AreEqual("http://tvmazecdn.com/uploads/images/medium_landscape/1/4388.jpg",
                episode.Image[ImageType.Medium]);
            Assert.AreEqual("http://tvmazecdn.com/uploads/images/original_untouched/1/4388.jpg",
                episode.Image[ImageType.Original]);
            Assert.AreEqual(
                            "<p>When the residents of Chester's Mill find themselves trapped under a massive transparent dome with no way out, they struggle to survive as resources rapidly dwindle and panic quickly escalates.</p>",
                episode.Summary);
            Assert.AreEqual(1, episode.Links.Count);
            Assert.AreEqual("http://api.tvmaze.com/episodes/1", episode.Links[LinkType.Self].Href);

        }

        [Test]
        public void CreateSeasons_ValidSeasonsJsonData_SeasonInstanceList() {
            // Arrange
            var json = File.ReadAllText(Path.Combine(BasePath, JSON_DATA_PATH, "seasons.json"));

            // Act
            var seasons = DomainObjectFactory.CreateSeasons(json);

            // Assert
            Assert.IsNotNull(seasons);
            Assert.IsNotEmpty(seasons);

            var season = seasons[0];
            Assert.IsNotNull(season);
            Assert.AreEqual(1, season.Id);
            Assert.AreEqual("http://www.tvmaze.com/seasons/1/under-the-dome-season-1", season.Url);
            Assert.AreEqual(1, season.Number);
            Assert.IsEmpty(season.Name);
            Assert.AreEqual(13, season.EpisodeOrder);
            Assert.AreEqual(new DateTime(2013, 06, 24), season.PremiereDate);
            Assert.AreEqual(new DateTime(2013, 09, 16), season.EndDate);
            Assert.IsNotNull(season.Network);
            Assert.AreEqual(2, season.Network.Id);
            Assert.AreEqual("CBS", season.Network.Name);
            Assert.AreEqual("United States", season.Network.Country.Name);
            Assert.AreEqual("US", season.Network.Country.Code);
            Assert.AreEqual("America/New_York", season.Network.Country.Timezone);
            Assert.IsNull(season.WebChannel);
            Assert.AreEqual(2, season.Image.Count);
            Assert.AreEqual("http://tvmazecdn.com/uploads/images/medium_portrait/24/60941.jpg",
                season.Image[ImageType.Medium]);
            Assert.AreEqual("http://tvmazecdn.com/uploads/images/original_untouched/24/60941.jpg",
                season.Image[ImageType.Original]);
            Assert.IsEmpty(season.Summary);
            Assert.AreEqual(1, season.Links.Count);
            Assert.AreEqual("http://api.tvmaze.com/seasons/1", season.Links[LinkType.Self].Href);


        }

        [Test]
        public void CreatePerson_ValidPersonJsonData_PersonInstance() {
            // Arrange
            var json = File.ReadAllText(Path.Combine(BasePath, JSON_DATA_PATH, "person.json"));

            // Act
            var person = DomainObjectFactory.CreatePerson(json);

            // Assert
            Assert.IsNotNull(person);
            Assert.AreEqual(1, person.Id);
            Assert.AreEqual("http://www.tvmaze.com/people/1/mike-vogel", person.Url);
            Assert.AreEqual("Mike Vogel", person.Name);
            Assert.IsNotEmpty(person.Image);
            Assert.IsTrue(person.Image.ContainsKey(ImageType.Medium));
            Assert.AreEqual("http://tvmazecdn.com/uploads/images/medium_portrait/0/1815.jpg", person.Image[ImageType.Medium]);
            Assert.IsNull(person.CastCredits);

        }

        [Test]
        public void CreatePerson_ValidPersonJsonDataWithCastCredits_PersonInstanceWithCastCredits() {
            // Arrange
            var json = File.ReadAllText(Path.Combine(BasePath, JSON_DATA_PATH, "person_embed_castcredits.json"));

            // Act
            var person = DomainObjectFactory.CreatePerson(json);

            // Assert
            Assert.IsNotNull(person);
            Assert.AreEqual(1, person.Id);
            Assert.AreEqual("http://www.tvmaze.com/people/1/mike-vogel", person.Url);
            Assert.AreEqual("Mike Vogel", person.Name);
            Assert.IsNotEmpty(person.Image);
            Assert.IsTrue(person.Image.ContainsKey(ImageType.Medium));
            Assert.AreEqual("http://tvmazecdn.com/uploads/images/medium_portrait/0/1815.jpg", person.Image[ImageType.Medium]);
            Assert.IsNotNull(person.CastCredits);
            Assert.AreEqual("http://api.tvmaze.com/shows/1", person.CastCredits[0].Links[LinkType.Show].Href);

        }

        [Test]
        public void CreateShow_ValidShowJsonData_ShowInstance() {
            // Arrange
            var json = File.ReadAllText(Path.Combine(BasePath, JSON_DATA_PATH, "show.json"));

            // Act
            var show = DomainObjectFactory.CreateShow(json);

            // Assert
            Assert.IsNotNull(show);
            Assert.AreEqual(1, show.Id);
            Assert.AreEqual("http://www.tvmaze.com/shows/1/under-the-dome", show.Url);
            Assert.AreEqual("Under the Dome", show.Name);
            Assert.AreEqual("Scripted", show.Type);
            Assert.AreEqual("English", show.Language);
            Assert.Contains("Drama", show.Genres);
            Assert.Contains("Science-Fiction", show.Genres);
            Assert.Contains("Thriller", show.Genres);
            Assert.AreEqual("Ended", show.Status);
            Assert.AreEqual(60, show.Runtime);
            Assert.AreEqual(new DateTime(2013, 06, 24), show.Premiered);
            Assert.AreEqual("22:00", show.Schedule.Time);
            Assert.Contains("Thursday", show.Schedule.Days);
            Assert.AreEqual(6.6f, show.Rating.Average);
            Assert.AreEqual(1, show.Weight);
            Assert.AreEqual(2, show.Network.Id);
            Assert.AreEqual("CBS", show.Network.Name);
            Assert.AreEqual("United States", show.Network.Country.Name);
            Assert.AreEqual("US", show.Network.Country.Code);
            Assert.AreEqual("America/New_York", show.Network.Country.Timezone);
            Assert.IsNull(show.WebChannel);
            Assert.IsNotNull(show.Externals);
            Assert.IsNotEmpty(show.Externals);
            Assert.IsTrue(show.Externals.ContainsKey("tvrage"));
            Assert.AreEqual("25988", show.Externals["tvrage"]);
            Assert.IsTrue(show.Externals.ContainsKey("thetvdb"));
            Assert.AreEqual("264492", show.Externals["thetvdb"]);
            Assert.IsTrue(show.Externals.ContainsKey("imdb"));
            Assert.AreEqual("tt1553656", show.Externals["imdb"]);
            Assert.IsNotEmpty(show.Image);
            Assert.IsTrue(show.Image.ContainsKey(ImageType.Medium));
            Assert.AreEqual("http://tvmazecdn.com/uploads/images/medium_portrait/0/1.jpg", show.Image[ImageType.Medium]);
            Assert.IsTrue(show.Image.ContainsKey(ImageType.Original));
            Assert.AreEqual("http://tvmazecdn.com/uploads/images/original_untouched/0/1.jpg", show.Image[ImageType.Original]);
            Assert.AreEqual("<p><em>Under the Dome</em> is the story of a small town that is suddenly and inexplicably sealed off from the rest of the world by an enormous transparent dome. The town's inhabitants must deal with surviving the post-apocalyptic conditions while searching for answers about the dome, where it came from and if and when it will go away.</p>",
                show.Summary);
            Assert.AreEqual(1467986681, show.Updated);

            Assert.IsNull(show.Casts);

        }

        [Test]
        public void CreateShows_ValidShowsJsonData_ShowInstanceList() {
            // Arrange
            var json = File.ReadAllText(Path.Combine(BasePath, JSON_DATA_PATH, "shows.json"));

            // Act
            var shows = DomainObjectFactory.CreateShows(json);

            // Assert
            Assert.IsNotNull(shows);
            Assert.IsNotEmpty(shows);

            var show = shows[0];
            Assert.IsNotNull(show);
            Assert.AreEqual(1, show.Id);
            Assert.AreEqual("http://www.tvmaze.com/shows/1/under-the-dome", show.Url);
            Assert.AreEqual("Under the Dome", show.Name);
            Assert.AreEqual("Scripted", show.Type);
            Assert.AreEqual("English", show.Language);
            Assert.Contains("Drama", show.Genres);
            Assert.Contains("Science-Fiction", show.Genres);
            Assert.Contains("Thriller", show.Genres);
            Assert.AreEqual("Ended", show.Status);
            Assert.AreEqual(60, show.Runtime);
            Assert.AreEqual(new DateTime(2013, 06, 24), show.Premiered);
            Assert.AreEqual("22:00", show.Schedule.Time);
            Assert.Contains("Thursday", show.Schedule.Days);
            Assert.AreEqual(6.6f, show.Rating.Average);
            Assert.AreEqual(2, show.Weight);
            Assert.AreEqual(2, show.Network.Id);
            Assert.AreEqual("CBS", show.Network.Name);
            Assert.AreEqual("United States", show.Network.Country.Name);
            Assert.AreEqual("US", show.Network.Country.Code);
            Assert.AreEqual("America/New_York", show.Network.Country.Timezone);
            Assert.IsNull(show.WebChannel);
            Assert.IsNotNull(show.Externals);
            Assert.IsNotEmpty(show.Externals);
            Assert.IsTrue(show.Externals.ContainsKey("tvrage"));
            Assert.AreEqual("25988", show.Externals["tvrage"]);
            Assert.IsTrue(show.Externals.ContainsKey("thetvdb"));
            Assert.AreEqual("264492", show.Externals["thetvdb"]);
            Assert.IsTrue(show.Externals.ContainsKey("imdb"));
            Assert.AreEqual("tt1553656", show.Externals["imdb"]);
            Assert.IsNotEmpty(show.Image);
            Assert.IsTrue(show.Image.ContainsKey(ImageType.Medium));
            Assert.AreEqual("http://static.tvmaze.com/uploads/images/medium_portrait/0/1.jpg", show.Image[ImageType.Medium]);
            Assert.IsTrue(show.Image.ContainsKey(ImageType.Original));
            Assert.AreEqual("http://static.tvmaze.com/uploads/images/original_untouched/0/1.jpg", show.Image[ImageType.Original]);
            Assert.AreEqual("<p><strong>Under the Dome</strong> is the story of a small town that is suddenly and inexplicably sealed off from the rest of the world by an enormous transparent dome. The town's inhabitants must deal with surviving the post-apocalyptic conditions while searching for answers about the dome, where it came from and if and when it will go away.</p>",
                show.Summary);
            Assert.AreEqual(1488136720, show.Updated);

            Assert.IsNull(show.Casts);

        }

        [Test]
        public void CreateShowUpdates_ValidShowUpdatesJsonData_ShowUpdateInstanceList() {
            // Arrange
            var json = File.ReadAllText(Path.Combine(BasePath, JSON_DATA_PATH, "show_updates.json"));

            // Act
            var showUpdates = DomainObjectFactory.CreateShowUpdates(json);

            // Assert
            Assert.IsNotNull(showUpdates);
            Assert.IsNotEmpty(showUpdates);

            var showUpdate = showUpdates[0];
            Assert.AreEqual("1", showUpdate.ShowId);
            Assert.AreEqual(1488136720, showUpdate.Timestamp);
            Assert.AreEqual(DateTimeOffset.Parse("2017-02-26T19:18:40+00:00"), showUpdate.DateTimeOffset);

        }

        [Test]
        public void CreateShow_ValidShowJsonDataWithEmbedCast_ShowInstance() {
            // Arrange
            var json = File.ReadAllText(Path.Combine(BasePath, JSON_DATA_PATH, "show_embed_cast.json"));

            // Act
            var show = DomainObjectFactory.CreateShow(json);

            // Assert
            Assert.IsNotNull(show);
            Assert.AreEqual(1, show.Id);
            Assert.AreEqual("http://www.tvmaze.com/shows/1/under-the-dome", show.Url);
            Assert.AreEqual("Under the Dome", show.Name);
            Assert.AreEqual("Scripted", show.Type);
            Assert.AreEqual("English", show.Language);
            Assert.Contains("Drama", show.Genres);
            Assert.Contains("Science-Fiction", show.Genres);
            Assert.Contains("Thriller", show.Genres);
            Assert.AreEqual("Ended", show.Status);
            Assert.AreEqual(60, show.Runtime);
            Assert.AreEqual(new DateTime(2013, 06, 24), show.Premiered);
            Assert.AreEqual("22:00", show.Schedule.Time);
            Assert.Contains("Thursday", show.Schedule.Days);
            Assert.AreEqual(6.6f, show.Rating.Average);
            Assert.AreEqual(1, show.Weight);
            Assert.AreEqual(2, show.Network.Id);
            Assert.AreEqual("CBS", show.Network.Name);
            Assert.AreEqual("United States", show.Network.Country.Name);
            Assert.AreEqual("US", show.Network.Country.Code);
            Assert.AreEqual("America/New_York", show.Network.Country.Timezone);
            Assert.IsNull(show.WebChannel);
            Assert.IsNotNull(show.Externals);
            Assert.IsNotEmpty(show.Externals);
            Assert.IsTrue(show.Externals.ContainsKey("tvrage"));
            Assert.AreEqual("25988", show.Externals["tvrage"]);
            Assert.IsTrue(show.Externals.ContainsKey("thetvdb"));
            Assert.AreEqual("264492", show.Externals["thetvdb"]);
            Assert.IsTrue(show.Externals.ContainsKey("imdb"));
            Assert.AreEqual("tt1553656", show.Externals["imdb"]);
            Assert.IsNotEmpty(show.Image);
            Assert.IsTrue(show.Image.ContainsKey(ImageType.Medium));
            Assert.AreEqual("http://tvmazecdn.com/uploads/images/medium_portrait/0/1.jpg", show.Image[ImageType.Medium]);
            Assert.IsTrue(show.Image.ContainsKey(ImageType.Original));
            Assert.AreEqual("http://tvmazecdn.com/uploads/images/original_untouched/0/1.jpg", show.Image[ImageType.Original]);
            Assert.AreEqual("<p><em>Under the Dome</em> is the story of a small town that is suddenly and inexplicably sealed off from the rest of the world by an enormous transparent dome. The town's inhabitants must deal with surviving the post-apocalyptic conditions while searching for answers about the dome, where it came from and if and when it will go away.</p>",
                show.Summary);
            Assert.AreEqual(1467986681, show.Updated);

            Assert.IsNotNull(show.Casts);
            Assert.IsNotEmpty(show.Casts);

        }


        [Test]
        public void CreateShow_ValidShowJsonDataWithEmbedEpisodes_ShowInstance() {
            // Arrange
            var json = File.ReadAllText(Path.Combine(BasePath, JSON_DATA_PATH, "show_embed_episodes.json"));

            // Act
            var show = DomainObjectFactory.CreateShow(json);

            // Assert
            Assert.IsNotNull(show);
            Assert.AreEqual(1, show.Id);
            Assert.AreEqual("http://www.tvmaze.com/shows/1/under-the-dome", show.Url);
            Assert.AreEqual("Under the Dome", show.Name);
            Assert.AreEqual("Scripted", show.Type);
            Assert.AreEqual("English", show.Language);
            Assert.Contains("Drama", show.Genres);
            Assert.Contains("Science-Fiction", show.Genres);
            Assert.Contains("Thriller", show.Genres);
            Assert.AreEqual("Ended", show.Status);
            Assert.AreEqual(60, show.Runtime);
            Assert.AreEqual(new DateTime(2013, 06, 24), show.Premiered);
            Assert.AreEqual("22:00", show.Schedule.Time);
            Assert.Contains("Thursday", show.Schedule.Days);
            Assert.AreEqual(6.6f, show.Rating.Average);
            Assert.AreEqual(0, show.Weight);
            Assert.AreEqual(2, show.Network.Id);
            Assert.AreEqual("CBS", show.Network.Name);
            Assert.AreEqual("United States", show.Network.Country.Name);
            Assert.AreEqual("US", show.Network.Country.Code);
            Assert.AreEqual("America/New_York", show.Network.Country.Timezone);
            Assert.IsNull(show.WebChannel);
            Assert.IsNotNull(show.Externals);
            Assert.IsNotEmpty(show.Externals);
            Assert.IsTrue(show.Externals.ContainsKey("tvrage"));
            Assert.AreEqual("25988", show.Externals["tvrage"]);
            Assert.IsTrue(show.Externals.ContainsKey("thetvdb"));
            Assert.AreEqual("264492", show.Externals["thetvdb"]);
            Assert.IsTrue(show.Externals.ContainsKey("imdb"));
            Assert.AreEqual("tt1553656", show.Externals["imdb"]);
            Assert.IsNotEmpty(show.Image);
            Assert.IsTrue(show.Image.ContainsKey(ImageType.Medium));
            Assert.AreEqual("http://static.tvmaze.com/uploads/images/medium_portrait/0/1.jpg", show.Image[ImageType.Medium]);
            Assert.IsTrue(show.Image.ContainsKey(ImageType.Original));
            Assert.AreEqual("http://static.tvmaze.com/uploads/images/original_untouched/0/1.jpg", show.Image[ImageType.Original]);
            Assert.AreEqual("<p><strong>Under the Dome</strong> is the story of a small town that is suddenly and inexplicably sealed off from the rest of the world by an enormous transparent dome. The town's inhabitants must deal with surviving the post-apocalyptic conditions while searching for answers about the dome, where it came from and if and when it will go away.</p>",
                show.Summary);
            Assert.AreEqual(1490211618, show.Updated);

            Assert.IsNotNull(show.Episodes);
            Assert.IsNotEmpty(show.Episodes);

        }

        [Test]
        public void CreatePeopleSearchResults_ValidSearchPeopleJsonData_PersonSearchResults() {
            // Arrange
            var json = File.ReadAllText(Path.Combine(BasePath, JSON_DATA_PATH, "search_people.json"));

            // Act
            var searchResults = DomainObjectFactory.CreatePeopleSearchResults(json);

            // Assert
            Assert.IsNotNull(searchResults);
            Assert.IsNotEmpty(searchResults);

            var searchResult = searchResults[1];
            Assert.AreEqual(3.6249506f, searchResult.Score);
            Assert.IsNotNull(searchResult.Element);
            Assert.IsInstanceOf<Person>(searchResult.Element);
            Assert.AreEqual(36952, searchResult.Element.Id);
            Assert.AreEqual("Lauren Sweetser", searchResult.Element.Name);
            
        }

        [Test]
        public void CreateShowSearchResults_ValidSearchShowJsonData_ShowSearchResults() {
            // Arrange
            var json = File.ReadAllText(Path.Combine(BasePath, JSON_DATA_PATH, "search_show.json"));

            // Act
            var searchResults = DomainObjectFactory.CreateShowSearchResults(json);

            // Assert
            Assert.IsNotNull(searchResults);
            Assert.IsNotEmpty(searchResults);

            var searchResult = searchResults[0];
            Assert.AreEqual(2.030192f, searchResult.Score);
            Assert.IsNotNull(searchResult.Element);
            Assert.IsInstanceOf<Show>(searchResult.Element);
            Assert.AreEqual(139, searchResult.Element.Id);
            Assert.AreEqual("http://www.tvmaze.com/shows/139/girls", searchResult.Element.Url);
            Assert.AreEqual("Girls", searchResult.Element.Name);
            Assert.AreEqual("Scripted", searchResult.Element.Type);

        }

        [Test]
        public void CreateSchedule_ValidScheduleJsonData_ScheduleList() {
            // Arrange
            var json = File.ReadAllText(Path.Combine(BasePath, JSON_DATA_PATH, "schedule.json"));

            // Act
            var schedule = DomainObjectFactory.CreateSchedule(json);

            // Assert
            Assert.IsNotNull(schedule);
            Assert.IsNotEmpty(schedule.Episodes);

            var episode = schedule.Episodes[0];
            Assert.IsNotNull(episode);
            Assert.IsNotNull(episode.Show);
            Assert.AreEqual(905632, episode.Id);

        }

        [Test]
        public void CreateCast_ValidCastJsonData_CastList() {
            // Arrange
            var json = File.ReadAllText(Path.Combine(BasePath, JSON_DATA_PATH, "cast.json"));

            // Act
            var casts = DomainObjectFactory.CreateCasts(json);

            // Assert
            Assert.IsNotNull(casts);
            Assert.IsNotEmpty(casts);

            var cast = casts[0];
            Assert.AreEqual(1, cast.Person.Id);
            Assert.AreEqual("Mike Vogel", cast.Person.Name);
            Assert.AreEqual(1, cast.Character.Id);
            Assert.AreEqual("Dale \"Barbie\" Barbara", cast.Character.Name);
        }

        [Test]
        public void CreateCrew_ValidCrewJsonData_CrewList() {
            // Arrange
            var json = File.ReadAllText(Path.Combine(BasePath, JSON_DATA_PATH, "crew.json"));

            // Act
            var crews = DomainObjectFactory.CreateCrews(json);

            // Assert
            Assert.IsNotNull(crews);
            Assert.IsNotEmpty(crews);

            var crew = crews[0];
            Assert.AreEqual("Creator", crew.Type);
            Assert.AreEqual(15, crew.Person.Id);
            Assert.AreEqual("Stephen King", crew.Person.Name);
        }

        [Test]
        public void CreateAliases_ValidAliasJsonData_AliasList() {
            // Arrange
            var json = File.ReadAllText(Path.Combine(BasePath, JSON_DATA_PATH, "alias.json"));

            // Act
            var aliases = DomainObjectFactory.CreateAliases(json);

            // Assert
            Assert.IsNotNull(aliases);
            Assert.IsNotEmpty(aliases);

            var alias = aliases[1];
            Assert.AreEqual("A búra alatt", alias.Name);
            Assert.AreEqual("Hungary", alias.Country.Name);

        }


        [Test]
        public void CreateCastCredits_ValidCastCreditJsonData_CastCreditList() {
            // Arrange
            var json = File.ReadAllText(Path.Combine(BasePath, JSON_DATA_PATH, "person_castcredits.json"));

            // Act
            var castCredits = DomainObjectFactory.CreateCastCredits(json);

            // Assert
            Assert.IsNotNull(castCredits);
            Assert.IsNotEmpty(castCredits);

            var castCredit = castCredits[1];
            Assert.AreEqual("http://api.tvmaze.com/shows/942", castCredit.Links[LinkType.Show].Href);
            Assert.AreEqual("http://api.tvmaze.com/characters/89565", castCredit.Links[LinkType.Character].Href);
            Assert.IsNull(castCredit.Show);

        }

        [Test]
        public void CreateCastCredits_ValidCastCreditJsonDataWithEmbedShow_CastCreditListWithShow() {
            // Arrange
            var json = File.ReadAllText(Path.Combine(BasePath, JSON_DATA_PATH, "person_castcredits_embed_show.json"));

            // Act
            var castCredits = DomainObjectFactory.CreateCastCredits(json);

            // Assert
            Assert.IsNotNull(castCredits);
            Assert.IsNotEmpty(castCredits);

            var castCredit = castCredits[1];
            Assert.AreEqual("http://api.tvmaze.com/shows/942", castCredit.Links[LinkType.Show].Href);
            Assert.AreEqual("http://api.tvmaze.com/characters/89565", castCredit.Links[LinkType.Character].Href);
            Assert.IsNotNull(castCredit.Show);
            Assert.AreEqual(942, castCredit.Show.Id);

        }

        [Test]
        public void CreateCrewCredits_ValidCrewCreditJsonData_CrewCreditList() {
            // Arrange
            var json = File.ReadAllText(Path.Combine(BasePath, JSON_DATA_PATH, "person_crewcredits.json"));

            // Act
            var crewCredits = DomainObjectFactory.CreateCrewCredits(json);

            // Assert
            Assert.IsNotNull(crewCredits);
            Assert.IsNotEmpty(crewCredits);

            var crewCredit = crewCredits[1];
            Assert.AreEqual("Consulting Producer", crewCredit.Type);
            Assert.AreEqual("http://api.tvmaze.com/shows/231", crewCredit.Links[LinkType.Show].Href);
            Assert.IsNull(crewCredit.Show);


        }

        [Test]
        public void CreateCrewCredits_ValidCrewCreditJsonDataWithShow_CrewCreditListWithShow() {
            // Arrange
            var json = File.ReadAllText(Path.Combine(BasePath, JSON_DATA_PATH, "person_crewcredits_embed_show.json"));

            // Act
            var crewCredits = DomainObjectFactory.CreateCrewCredits(json);

            // Assert
            Assert.IsNotNull(crewCredits);
            Assert.IsNotEmpty(crewCredits);

            var crewCredit = crewCredits[1];
            Assert.AreEqual("Consulting Producer", crewCredit.Type);
            Assert.AreEqual("http://api.tvmaze.com/shows/231", crewCredit.Links[LinkType.Show].Href);
            Assert.IsNotNull(crewCredit.Show);
            Assert.AreEqual(231, crewCredit.Show.Id);


        }

    }
}
