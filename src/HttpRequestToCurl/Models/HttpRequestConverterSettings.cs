namespace HttpRequestToCurl.Models;

public class HttpRequestConverterSettings
{
	private const bool DefaultIgnoreSensitiveInformation = true;
	private const OperatingSystemSyntax DefaultOperatingSystemSyntax = OperatingSystemSyntax.Unix;
	private const bool DefaultAllowInsecureConnection = false;

	private bool? _ignoreSensitiveInformation;
	private OperatingSystemSyntax? _operatingSystemSyntax;
	private bool? _allowInsecureConnection;

	public bool IgnoreSensitiveInformation
	{
		get => _ignoreSensitiveInformation ?? DefaultIgnoreSensitiveInformation;
		set => _ignoreSensitiveInformation = value;
	}

	public OperatingSystemSyntax OperatingSystemSyntax
	{
		get => _operatingSystemSyntax ?? DefaultOperatingSystemSyntax;
		set => _operatingSystemSyntax = value;
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