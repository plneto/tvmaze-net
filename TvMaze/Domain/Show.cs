using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TvMaze.Domain {
    /// <summary>
    /// Class that represents a show
    /// </summary>
    public class Show {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Language { get; set; }
        public string[] Genres { get; set; }
        public string Status { get; set; }
        public int? Runtime { get; set; }
        public DateTime? Premiered { get; set; }
        public ShowSchedule Schedule { get; set; }
        public Rating Rating { get; set; }
        public int Weight { get; set; }
        public Network Network { get; set; }
        public WebChannel WebChannel { get; set; }
        public IDictionary<string, string> Externals { get; set; }
        public IDictionary<ImageType, string> Image { get; set; }
        public string Summary { get; set; }
        public int Updated { get; set; }
        [JsonProperty(PropertyName = "_links")]
        public IDictionary<LinkType, Link> Links { get; set; }
        public IList<Cast> Casts { get; set; }
        public IList<Episode>  Episodes { get; set; }
    }
}
