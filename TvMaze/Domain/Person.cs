using System.Collections.Generic;

namespace TvMaze.Domain {
    public class Person {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public IDictionary<string, string>  Image { get; set; }
    }
}
