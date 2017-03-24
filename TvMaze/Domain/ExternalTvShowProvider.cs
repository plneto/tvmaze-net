using System.ComponentModel;

namespace TvMaze.Domain {
    public enum ExternalTvShowProvider {
        [Description("tvrage")]
        TvRage,
        [Description("thetvdb")]
        TheTvDb,
        [Description("imdb")]
        Imdb
    }
}