using Zed.DataAnnotations;

namespace TvMaze.Domain {
    public enum EmbedType {
        [DisplayName("episodes")]
        Episodes,

        [DisplayName("cast")]
        Cast,

        [DisplayName("castcredits")]
        CastCredits,

        [DisplayName("show")]
        Show
    }
}
