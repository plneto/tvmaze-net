using System;

namespace TvMaze.Domain {
    public class ShowUpdate {
        public string ShowId { get; set; }
        public long Timestamp { get; set; }
        public DateTimeOffset DateTimeOffset => DateTimeOffset.FromUnixTimeSeconds(Timestamp);
    }
}
