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
            var factory = new DomainObjectFactory();

            // Act
            var episode = factory.CreateEpisode(json);

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
            Assert.AreEqual("http://tvmazecdn.com/uploads/images/medium_landscape/1/4388.jpg", episode.Image[ImageType.Medium]);
            Assert.AreEqual("http://tvmazecdn.com/uploads/images/original_untouched/1/4388.jpg", episode.Image[ImageType.Original]);
            Assert.AreEqual("<p>When the residents of Chester's Mill find themselves trapped under a massive transparent dome with no way out, they struggle to survive as resources rapidly dwindle and panic quickly escalates.</p>", episode.Summary);
            Assert.AreEqual(1, episode.Links.Count);
            Assert.AreEqual("http://api.tvmaze.com/episodes/1", episode.Links[LinkType.Self].Href);


        }


    }
}
