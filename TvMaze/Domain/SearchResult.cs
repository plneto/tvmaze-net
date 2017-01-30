namespace TvMaze.Domain {
    public class SearchResult<T> where T:class {
        public float Score { get; set; }
        public T Element { get; set; }
    }
}
