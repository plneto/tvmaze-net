using System;
using System.Collections.Generic;

namespace TvMaze.Domain {
    public class Season {
        public int Id { get; set; }
        public string Url { get; set; }
        public byte Number { get; set; }
        public string Name { get; set; }
        public ushort EpisodeOrder { get; set; }
        public DateTime PremiereDate { get; set; }
        public DateTime EndDate { get; set; }
        public Network Network { get; set; }
        public string WebChannel { get; set; }
        public IDictionary<string, string> Image { get; set; }
        public string Summary { get; set; }
    }
}
