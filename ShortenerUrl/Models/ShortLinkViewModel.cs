using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShortenerUrl.Models;

public class ShortLinkViewModel
{
    public long Id { get; set; }

    [DisplayName("URL")]
    [DataType(DataType.Url)]
    [Required(ErrorMessage = "Поле URL обязательно для заполнения.")]
    [Url(ErrorMessage = "Некорректный URL.")]
    public string FullUrl { get; set; } = string.Empty;
}
