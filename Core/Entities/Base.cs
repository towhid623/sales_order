using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Base
    {
        [Key]
        public long Id { get; set; }
    }
}
