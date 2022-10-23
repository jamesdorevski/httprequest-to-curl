# HttpRequestToCurl

A simple `.NET Standard 2.1` library to convert `HttpRequestMessage` objects into cURL for easy sharing of payloads. 

## Usage

### Basic conversion

Use the `ConvertToCurl()` method on `HttpRequestConverter`:

```csharp
var request = new HttpRequestMessage
{
    RequestUri = new Uri("https://www.example.com"),
    Method = HttpMethod.Get
};

string curl = HttpRequestConverter.ConvertToCurl(request);
```

Output:

```shell
curl --request GET 'https://www.example.com'
```

### Settings usage

Instantiate a new `HttpRequestConverterSettings` object and pass it in addition to the `HttpRequestMessage` to `ConvertToCurl()`: 

```csharp
var request = new HttpRequestMessage
{
    RequestUri = new Uri("https://www.example.com"),
    Method = HttpMethod.Get
};

var settings = new HttpRequestConverterSettings
{
    AllowInsecureConnections = true,
    IgnoreSensitiveInformation = false
};

var actual = HttpRequestConverter.ConvertToCurl(request, settings);
```

Available settings and their defaults:

| Setting                      | Type | Default | Description                                                                             | 
|------------------------------| ---- |---------|-----------------------------------------------------------------------------------------|
| `AllowInsecureConnections`   | bool | `false` | Adds the insecure flag (`--insecure`) to the command to allow for insecure connections. |
| `IgnoreSensitiveInformation` | bool | `true`  | Excludes the `Authorization` header from the cURL command.                              |

## Limitations

- Only supports `StringContent` bodies as of now
