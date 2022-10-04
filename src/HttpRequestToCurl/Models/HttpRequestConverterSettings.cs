namespace HttpRequestToCurl.Models;


public class HttpReqestConverterSettings 
{
    internal const bool DefaultIgnoreSensitiveInformation = true;

    internal bool? _ignoreSensitiveInformation;

    public bool IgnoreSensitiveInformation
    {
        get => _ignoreSensitiveInformation ?? DefaultIgnoreSensitiveInformation;
        set => _ignoreSensitiveInformation = value;
    }
}