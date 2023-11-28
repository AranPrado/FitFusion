namespace FitFusion.StartupConfigureToken
{
    public class RemoveBearerPrefixOptions
    {
        public const string DefaultHeaderName = "Authorization";
        public const string DefaultPrefix = "Bearer";

        public string HeaderName { get; set; } = DefaultHeaderName;
        public string Prefix { get; set; } = DefaultPrefix;
    }
}