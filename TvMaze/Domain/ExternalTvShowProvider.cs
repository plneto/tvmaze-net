using Zed.DataAnnotations;

namespace TvMaze.Domain {
    public enum ExternalTvShowProvider {
        [DisplayName("tvrage")]
        TvRage,
        [DisplayName("thetvdb")]
        TheTvDb,
        [DisplayName("imdb")]
        Imdb
    }
}