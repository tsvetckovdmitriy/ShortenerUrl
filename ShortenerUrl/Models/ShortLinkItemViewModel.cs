using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShortenerUrl.Models;

public class ShortLinkItemViewModel
{
    public long Id { get; set; }

    [Required]
    [DisplayName("Длинный URL")]
    [DataType(DataType.Url)]
    public string FullUrl { get; set; } = string.Empty;

    [Required]
    [DisplayName("Сокращенный URL")]
    [DataType(DataType.Url)]
    public string ShortUrl { get; set; } = string.Empty;

    [Required]
    [DisplayName("Количество переходов")]
    public long CountClicks { get; set; }

    [Required]
    [DisplayName("Дата создания")]
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = true)]
    public DateTimeOffset DataCreted { get; set; }
}
