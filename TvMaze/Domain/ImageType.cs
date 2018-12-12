using Zed.DataAnnotations;

namespace TvMaze.Domain {
    public enum ImageType {
        [DisplayName("original")]
        Original,

        [DisplayName("medium")]
        Medium
    }
}
