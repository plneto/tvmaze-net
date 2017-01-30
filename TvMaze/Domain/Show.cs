using System;
using System.Collections.Generic;

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
        public int Runtime { get; set; }
        public DateTime Premiered { get; set; }
        // TODO
        /*
          "schedule": {
            "time": "22:00",
            "days": [
              "Thursday"
            ]
          },
         */
        public string Schedule { get; set; }
        // TODO
        /*
         "rating": {
            "average": 6.6
          },
         */
        public decimal Rating { get; set; }
        public int Weight { get; set; }
        public Network Network { get; set; }
        public string WebChannel { get; set; }
        public IDictionary<string, string> ExternalIds { get; set; }
        public IDictionary<string, string> Image { get; set; }
        public string Summary { get; set; }
        public int Updated { get; set; }
    }
}
