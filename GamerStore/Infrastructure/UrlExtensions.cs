namespace GamerStore.Infrastructure
{
    public static class UrlExtensions
    {
        public static string PathAndQuery(this HttpRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);
            var result = request.QueryString.HasValue ? $"{request.Path}{request.QueryString}" : request.Path.ToString();
            return result;
        }
    }
}
