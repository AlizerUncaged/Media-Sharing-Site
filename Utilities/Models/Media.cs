using System.ComponentModel.DataAnnotations;

namespace Utilities.Models;

/// <summary>
/// A media model class for videos/audios.
/// </summary>
public class Media
{
    [Key]
    [DataType(DataType.Text)]
    [Display(Name = "Id")]
    public long Id { get; set; }

    [Required]
    [DataType(DataType.Text)]
    [Display(Name = "Media Name")]
    public string Name { get; set; }

    [Required]
    [DataType(DataType.Text)]
    [Display(Name = "Author")]
    public string Author { get; set; }

    [Required]
    [EnumDataType(typeof(MediaType))]
    [Display(Name = "Media Type")]
    public MediaType MediaType { get; set; }

    [Required] [DataType(DataType.Text)] public int Stocks { get; set; } = 100;


    public string Thumbnail { get; set; }

    public string FileName { get; set; }

    [DataType(DataType.Currency)]
    [Display(Name = "Media Price")]
    public double Price { get; set; }
}