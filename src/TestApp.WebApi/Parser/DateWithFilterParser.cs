using System.Globalization;
using TestApp.Common.Enum;

namespace TestApp.WebApi.Parser;

internal static class DateWithFilterParser
{
    /// <summary>
    /// Парсит входную строку фильтра даты вида "eq2022-01-01" или "gt2020-05-20" и возвращает префикс и дату.
    /// Если входная строка пуста или парсинг не удался, возвращается префикс eq и null.
    /// </summary>
    public static (FilterPrefixEnum Prefix, DateTime? Date) Parse(string? date)
    {
        if (string.IsNullOrWhiteSpace(date))
        {
            return (FilterPrefixEnum.eq, null);
        }

        try
        {
            var prefixPart = date.Substring(0, 2);
            var datePart = date.Substring(2);
            var prefix = prefixPart switch
            {
                "eq" => FilterPrefixEnum.eq,
                "ne" => FilterPrefixEnum.ne,
                "gt" => FilterPrefixEnum.gt,
                "lt" => FilterPrefixEnum.lt,
                "ge" => FilterPrefixEnum.ge,
                "le" => FilterPrefixEnum.le,
                _ => FilterPrefixEnum.eq
            };

            var parsedDate = DateTime.Parse(datePart).ToUniversalTime();
            return (prefix, parsedDate);
        }
        catch
        {
            // При ошибке парсинга возвращаем фильтр по умолчанию (без даты)
            return (FilterPrefixEnum.eq, null);
        }
    }

    
    
}