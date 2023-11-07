
public static class StringUtilities
{
    public static string StartCase(this string s)
    {
        string temp = s[0] == '_' ? string.Empty : s[0].ToString().ToUpper();

        for (int i = 1; i < s.Length; i++)
        {
            if (char.IsUpper(s[i])) temp += " " + s[i];
            else if (s[i] == '_') temp += " ";
            else temp += s[i];
        }

        return temp;
    }

    public static string SentenceCase(this string s)
    {
        string temp = s[0] == '_' ? string.Empty : s[0].ToString().ToUpper();

        for (int i = 1; i < s.Length; i++)
        {
            if (char.IsUpper(s[i])) temp += ' ' + s[i].ToString().ToLower();
            else if (s[i] == '_') temp += ' ';
            else temp += s[i];
        }

        return temp;
    }
}
