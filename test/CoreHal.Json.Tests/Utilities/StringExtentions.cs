namespace CoreHal.Json.Tests.Utilities
{
    public static class StringExtentions
    {
        public static string MakeEasyToCompare(this string str)
        {
            return str.Replace(" ", string.Empty).Replace(System.Environment.NewLine, string.Empty);
        }
    }
}