namespace Extentions
{
    public static class FriendlyURLs
    {
        public static string ToStringFromGuid(this string guidStr)
        {
            Guid guid = new Guid(guidStr);
            return Convert.ToBase64String(guid.ToByteArray())
                .Replace("/", "-")
                .Replace("+", "_")
                .Replace("=", string.Empty);
        }

        public static string ToGuidFromString(this string Str)
        {
            var base64Str = Convert.FromBase64String(Str
                .Replace("-", "/")
                .Replace("_", "+") + "==");
            return new Guid(base64Str).ToString();
        }
    }
}