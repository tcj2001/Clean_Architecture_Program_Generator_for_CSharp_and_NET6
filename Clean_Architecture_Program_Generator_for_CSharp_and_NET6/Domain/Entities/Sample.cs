/////////////////////////
// generated Sample.cs //
/////////////////////////
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Sample
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
