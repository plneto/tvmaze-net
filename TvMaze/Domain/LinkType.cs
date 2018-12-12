using Zed.DataAnnotations;
namespace TvMaze.Domain {
    public enum LinkType {
        [DisplayName("self")]
        Self,

        [DisplayName("show")]
        Show,

        [DisplayName("character")]
        Character,

        [DisplayName("previousepisode")]
        PreviousEpisode,

        [DisplayName("nextepisode")]
        NextEpisode

    }
}
