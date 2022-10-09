namespace HttpRequestToCurl.Models;

public class HttpRequestConverterSettings
{
	private const bool DefaultIgnoreSensitiveInformation = true;
	private const bool DefaultAllowInsecureConnection = false;

	private bool? _ignoreSensitiveInformation;
	private bool? _allowInsecureConnection;

	/// <summary>
	/// Excludes the Authorization header from the cURL command.
	/// </summary>
	public bool IgnoreSensitiveInformation
	{
		get => _ignoreSensitiveInformation ?? DefaultIgnoreSensitiveInformation;
		set => _ignoreSensitiveInformation = value;
	}

	/// <summary>
	/// Adds the insecure flag to the command to allow for insecure connections.
	/// </summary>
	public bool AllowInsecureConnection
	{
		get => _allowInsecureConnection ?? DefaultAllowInsecureConnection;
		set => _allowInsecureConnection = value;
	}
}