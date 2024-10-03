using Microsoft.EntityFrameworkCore;

using ShortenerUrl.Interfaces;

namespace ShortenerUrl.Services;

public class ShortUrlService
{
    /// <summary>
    /// Максимальная длина кода
    /// </summary>
    private const int Length = 7;

    /// <summary>
    /// Набор символов участвующих в генерации кода
    /// </summary>
    private const string Alphabet = "abcdefghijklmnopqrstuvwxyz0123456789";

    private readonly Random _random = new();
    private readonly IApplicationDbContext _dbContext;

    public ShortUrlService(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Генерирует уникальный код для сокращённой ссылки
    /// </summary>
    public async Task<string> GenerateUniqueCode()
    {
        var codeChars = new char[Length];

        while (true)
        {
            for (var i = 0; i < Length; i++)
            {
                var randomIndex = _random.Next(Alphabet.Length);
                codeChars[i] = Alphabet[randomIndex];
            }

            var code = new string(codeChars);

            if (!await _dbContext.ShortLinks.AnyAsync(s => s.Code == code))
                return code;
        }
    }
}
