using System.ComponentModel.DataAnnotations;

namespace AppAuthLesson4.Models
{
    public class Role
    {
        [Key] public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
