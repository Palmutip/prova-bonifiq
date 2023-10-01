namespace ProvaPub.Models
{
    public class Paginacao<T>
    {
        public List<T> Entities { get; set; }
        public int TotalCount { get; set; }
        public bool HasNext { get; set; }
    }
}
