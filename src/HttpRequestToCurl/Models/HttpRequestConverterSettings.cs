namespace HttpRequestToCurl.Models;

public class HttpRequestConverterSettings
{
	private const bool DefaultIgnoreSensitiveInformation = true;
	private const bool DefaultAllowInsecureConnections = false;
	
	private bool? _allowInsecureConnections;
	private bool? _ignoreSensitiveInformation;

	/// <summary>
	///     Excludes the Authorization header from the cURL command.
	/// </summary>
	public bool IgnoreSensitiveInformation
	{
		get => _ignoreSensitiveInformation ?? DefaultIgnoreSensitiveInformation;
		set => _ignoreSensitiveInformation = value;
	}

	/// <summary>
	///     Adds the insecure flag to the command to allow for insecure connections.
	/// </summary>
	public bool AllowInsecureConnections
	{
		get => _allowInsecureConnections ?? DefaultAllowInsecureConnections;
		set => _allowInsecureConnections = value;
	}
}