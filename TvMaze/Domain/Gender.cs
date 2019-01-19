using Zed.DataAnnotations;

namespace TvMaze.Domain {
    public enum Gender {
        [DisplayName("Unknown")]
        Unknown = 0,

        [DisplayName("Male")]
        Male = 1,

        [DisplayName("Female")]
        Female = 2
    }
}
