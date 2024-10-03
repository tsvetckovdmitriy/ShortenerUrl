using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace ShortenerUrl.Entities;

/// <summary>
/// Сущность "Короткая ссылка"
/// </summary>
[Table("links")]
[Index(nameof(Code), IsUnique = true)]
public class ShortLink
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; } = 0;

    /// <summary>
    /// Длинный URL
    /// </summary>
    [Required]
    public string FullUrl { get; set; } = string.Empty;

    /// <summary>
    /// Сокращенный URL
    /// </summary>
    [Required]
    public string ShortUrl { get; set; } = string.Empty;

    /// <summary>
    /// Сокращенный URL
    /// </summary>
    [Required]
    public string? Code { get; set; }


    /// <summary>
    /// Количество переходов
    /// </summary>
    [Required]
    public long CountClicks { get; set; } = 0;

    /// <summary>
    /// Дата создания
    /// </summary>
    [Required]
    public DateTimeOffset DataCreted { get; set; } = DateTimeOffset.Now;
}
