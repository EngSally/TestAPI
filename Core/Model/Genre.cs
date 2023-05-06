
using Microsoft.EntityFrameworkCore;

namespace TestAPI.Core.Model
{
    [Index(nameof(Name), IsUnique = true)]
    public class Genre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } 

    }
}
