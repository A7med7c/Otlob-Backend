namespace DomainLayer.Models
{
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; }//pk - we made it generic because my modles has id int or string
    }
}
