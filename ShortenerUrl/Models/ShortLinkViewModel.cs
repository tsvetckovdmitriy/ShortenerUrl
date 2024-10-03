using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShortenerUrl.Models;

public class ShortLinkViewModel
{
    public long Id { get; set; }

    [DisplayName("URL")]
    [DataType(DataType.Url)]
    [Required(ErrorMessage = "���� URL ����������� ��� ����������.")]
    [Url(ErrorMessage = "������������ URL.")]
    public string FullUrl { get; set; } = string.Empty;
}
