using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PhotosApi.Models;

[PrimaryKey(nameof(Id))]
public class Photo
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime LastModifiedDateTime { get; set; }
    public string Url { get; set; } = string.Empty;
}