namespace HttpRequestToCurl.Models;

public class HttpRequestConverterSettings
{
	private const bool DefaultIgnoreSensitiveInformation = true;
	private const OperatingSystemSyntax DefaultOperatingSystemSyntax = OperatingSystemSyntax.Unix;

	private bool? _ignoreSensitiveInformation;
	private OperatingSystemSyntax? _operatingSystemSyntax;

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
}