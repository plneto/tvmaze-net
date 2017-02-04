using System.Collections.Generic;

namespace TvMaze.Domain {
    public class Schedule { 
        public IList<Episode> Episodes { get; set; }

        public Schedule(IList<Episode> episodes) {
            Episodes = episodes;
        }
    }
}
