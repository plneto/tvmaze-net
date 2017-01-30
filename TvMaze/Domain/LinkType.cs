using System.ComponentModel;

namespace TvMaze.Domain {
    public enum LinkType {
        [Description("self")]
        Self,

        [Description("show")]
        Show,

        [Description("character")]
        Character,

        [Description("previousepisode")]
        PreviousEpisode
        
    }
}
