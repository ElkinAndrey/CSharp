using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RussianLettersAboveText
{
    public static class WorkWithText
    {
        public static string RussianLettersAboveText(string text, string upText)
        {
            Dictionary<char, int> upLettets = new Dictionary<char, int>()
            {
                { 'а', 11766 },
                { 'б', 11744 },
                { 'в', 11745 },
                { 'г', 11746 },
                { 'д', 11747 },
                { 'е', 11767 },
                { 'ё', 11767 },
                { 'ж', 11748 },
                { 'з', 11749 },
                { 'и', 42613 },
                { 'й', 42613 },
                { 'к', 11750 },
                { 'л', 11751 },
                { 'м', 11752 },
                { 'н', 11753 },
                { 'о', 11754 },
                { 'п', 11755 },
                { 'р', 11756 },
                { 'с', 11757 },
                { 'т', 11758 },
                { 'у', 42615 },
                { 'х', 11759 },
                { 'ц', 11760 },
                { 'ч', 11761 },
                { 'ш', 11762 },
                { 'щ', 11763 },
                { 'ъ', 42616 },
                { 'ы', 42617 },
                { 'ь', 42618 },
            };

            StringBuilder sb = new StringBuilder();

            foreach (var item in text.Zip(upText, (textLetter, upTextLetter) => (textLetter, upTextLetter)))
            {
                sb.Append(item.textLetter);
                try
                {
                    sb.Append(((char)upLettets[Char.ToLower(item.upTextLetter)]).ToString());
                }
                catch { }
            }

            if (text.Length > upText.Length)
                sb.Append(text.Substring(upText.Length, text.Length - upText.Length));

            return sb.ToString();
        }
    }
}
