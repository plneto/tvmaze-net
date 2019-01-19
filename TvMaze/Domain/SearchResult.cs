namespace TvMaze.Domain {
    public class SearchResult<T> where T:class {
        public decimal Score { get; set; }
        public T Element { get; set; }
    }
}
