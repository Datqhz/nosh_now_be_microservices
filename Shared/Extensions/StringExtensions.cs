using System.Text.RegularExpressions;

namespace Shared.Extensions;

public static class StringExtensions
{
    public static string ToILikePattern(this string str)
    {
        return $"%{str}%";
    }
}