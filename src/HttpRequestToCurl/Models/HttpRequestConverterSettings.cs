namespace HttpRequestToCurl.Models;


public class HttpRequestConverterSettings 
{
    internal const bool DefaultIgnoreSensitiveInformation = true;

    private bool? _ignoreSensitiveInformation;

    public bool IgnoreSensitiveInformation
    {
        get => _ignoreSensitiveInformation ?? DefaultIgnoreSensitiveInformation;
        set => _ignoreSensitiveInformation = value;
    }
}