using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PhotosApi.Models;

[PrimaryKey(nameof(Id))]
public class Photo
{
    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
    public DateTime LastModifiedDateTime { get; }
    public string Url { get; }

    public Photo(
        Guid id, string name, string description, DateTime lastModifiedDateTime, string url)
    {
        Id = id;
        Name = name;
        Description = description;
        LastModifiedDateTime = lastModifiedDateTime;
        Url = url;
    }
    public Photo() { }
}