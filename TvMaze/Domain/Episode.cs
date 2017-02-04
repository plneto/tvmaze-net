using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TvMaze.Domain {
    /// <summary>
    /// Class that represents an episode
    /// </summary>
    public class  Episode {
        /// <summary>
        /// Episode id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Episode url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Episode name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Episode season number
        /// </summary>
        public int Season { get; set; }

        /// <summary>
        /// Episode ordinal number in season
        /// </summary>
        public int? Number { get; set; }

        /// <summary>
        /// Episode air date
        /// </summary>
        public string AirDate { get; set; }

        /// <summary>
        /// Episode air time
        /// </summary>
        public string AirTime { get; set; }

        /// <summary>
        /// Episode air stamp
        /// </summary>
        public string AirStamp { get; set; }

        /// <summary>
        /// Episode air date time with offset based on AirStamp
        /// </summary>
        public DateTimeOffset AirDateTimeOffset => DateTimeOffset.Parse(AirStamp);

        /// <summary>
        /// Episode duration in minutes - runtime
        /// </summary>
        public int? RunTime { get; set; }
        public IDictionary<ImageType, string> Image { get; set; }

        /// <summary>
        /// Episode summary
        /// </summary>
        public string Summary { get; set; }

        [JsonProperty(PropertyName = "_links")]
        public IDictionary<LinkType, Link>  Links { get; set; }

        public Show Show { get; set; }
    }
}
