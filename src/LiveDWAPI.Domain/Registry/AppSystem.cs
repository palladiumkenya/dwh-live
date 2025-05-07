using System.ComponentModel.DataAnnotations;

namespace LiveDWAPI.Domain.Registry;

public class AppSystem
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Config { get; set; }
}