using System.Text;
using Utilities.Enums;

namespace Utilities.Helpers;

/// <summary>
/// Utility class for string manipulation.
/// </summary>
public static class MaskString
{
    /// <summary>
    /// Default masking character used in a mask.
    /// </summary>
    public static readonly char DefaultMaskCharacter = '*';


    /// <summary>
    /// Returns true if the string is non-null and at least the specified number of characters.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="length">The minimum length.</param>
    /// <returns>True if string is non-null and at least the length specified.</returns>
    /// <exception>throws ArgumentOutOfRangeException if length is not a non-negative number.</exception>
    public static bool IsLengthAtLeast(this string value, int length)
    {
        if (length < 0)
        {
            throw new ArgumentOutOfRangeException("length", length,
                "The length must be a non-negative number.");
        }

        return value != null
            ? value.Length >= length
            : false;
    }

    /// <summary>
    /// Mask the source string with the mask char except for the last exposed digits.
    /// </summary>
    /// <param name="sourceValue">Original string to mask.</param>
    /// <param name="maskChar">The character to use to mask the source.</param>
    /// <param name="numExposed">Number of characters exposed in masked value.</param>
    /// <param name="style">The masking style to use (all characters or just alpha-nums).</param>
    /// <returns>The masked account number.</returns>
    public static string Mask(this string sourceValue, char maskChar, int numExposed, MaskStyle style)
    {
        var maskedString = sourceValue;

        if (sourceValue.IsLengthAtLeast(numExposed))
        {
            var builder = new StringBuilder(sourceValue.Length);
            int index = maskedString.Length - numExposed;

            if (style == MaskStyle.AlphaNumericOnly)
            {
                CreateAlphaNumMask(builder, sourceValue, maskChar, index);
            }
            else
            {
                builder.Append(maskChar, index);
            }

            builder.Append(sourceValue.Substring(index));
            maskedString = builder.ToString();
        }

        return maskedString;
    }

    /// <summary>
    /// Mask the source string with the mask char except for the last exposed digits.
    /// </summary>
    /// <param name="sourceValue">Original string to mask.</param>
    /// <param name="maskChar">The character to use to mask the source.</param>
    /// <param name="numExposed">Number of characters exposed in masked value.</param>
    /// <returns>The masked account number.</returns>
    public static string Mask(this string sourceValue, char maskChar, int numExposed)
    {
        return Mask(sourceValue, maskChar, numExposed, MaskStyle.All);
    }

    /// <summary>
    /// Mask the source string with the mask char.
    /// </summary>
    /// <param name="sourceValue">Original string to mask.</param>
    /// <param name="maskChar">The character to use to mask the source.</param>
    /// <returns>The masked account number.</returns>
    public static string Mask(this string sourceValue, char maskChar)
    {
        return Mask(sourceValue, maskChar, 0, MaskStyle.All);
    }

    /// <summary>
    /// Mask the source string with the default mask char except for the last exposed digits.
    /// </summary>
    /// <param name="sourceValue">Original string to mask.</param>
    /// <param name="numExposed">Number of characters exposed in masked value.</param>
    /// <returns>The masked account number.</returns>
    public static string Mask(this string sourceValue, int numExposed)
    {
        return Mask(sourceValue, DefaultMaskCharacter, numExposed, MaskStyle.All);
    }

    /// <summary>
    /// Mask the source string with the default mask char.
    /// </summary>
    /// <param name="sourceValue">Original string to mask.</param>
    /// <returns>The masked account number.</returns>
    public static string Mask(this string sourceValue)
    {
        return Mask(sourceValue, DefaultMaskCharacter, 0, MaskStyle.All);
    }

    /// <summary>
    /// Mask the source string with the mask char.
    /// </summary>
    /// <param name="sourceValue">Original string to mask.</param>
    /// <param name="maskChar">The character to use to mask the source.</param>
    /// <param name="style">The masking style to use (all characters or just alpha-nums).</param>
    /// <returns>The masked account number.</returns>
    public static string Mask(this string sourceValue, char maskChar, MaskStyle style)
    {
        return Mask(sourceValue, maskChar, 0, style);
    }

    /// <summary>
    /// Mask the source string with the default mask char except for the last exposed digits.
    /// </summary>
    /// <param name="sourceValue">Original string to mask.</param>
    /// <param name="numExposed">Number of characters exposed in masked value.</param>
    /// <param name="style">The masking style to use (all characters or just alpha-nums).</param>
    /// <returns>The masked account number.</returns>
    public static string Mask(this string sourceValue, int numExposed, MaskStyle style)
    {
        return Mask(sourceValue, DefaultMaskCharacter, numExposed, style);
    }

    /// <summary>
    /// Mask the source string with the default mask char.
    /// </summary>
    /// <param name="sourceValue">Original string to mask.</param>
    /// <param name="style">The masking style to use (all characters or just alpha-nums).</param>
    /// <returns>The masked account number.</returns>
    public static string Mask(this string sourceValue, MaskStyle style)
    {
        return Mask(sourceValue, DefaultMaskCharacter, 0, style);
    }

    /// <summary>
    /// Masks all characters for the specified length.
    /// </summary>
    /// <param name="buffer">String builder to store result in.</param>
    /// <param name="source">The source string to pull non-alpha numeric characters.</param>
    /// <param name="mask">Masking character to use.</param>
    /// <param name="length">Length of the mask.</param>
    private static void CreateAlphaNumMask(StringBuilder buffer, string source, char mask, int length)
    {
        for (int i = 0; i < length; i++)
        {
            buffer.Append(char.IsLetterOrDigit(source[i])
                ? mask
                : source[i]);
        }
    }

    public static string MaskAccountNumber(this string number, char character, int maskLength)
    {
        if (string.IsNullOrEmpty(number))
        {
            return number;
        }

        return number.Length <= maskLength + 3
            ? number
            : number.Substring(0, 3) + new string(character, maskLength) + number.Substring(number.Length - 3);
    }
}