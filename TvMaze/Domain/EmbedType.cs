using System.ComponentModel;

namespace TvMaze.Domain {
    public enum EmbedType {
        [Description("episodes")]
        Episodes,

        [Description("cast")]
        Cast,

        [Description("castcredits")]
        CastCredits,

        [Description("show")]
        Show
    }
}
