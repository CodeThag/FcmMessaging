namespace FcmMessaging.Commons.Configurations;

public class FirebaseConfiguration
{
    public string Type { get; init; } = null!;
    public string ProjectId { get; init; } = null!; 
    public string PrivateKeyId { get; init; } = null!;
    public string PrivateKey { get; init; } = null!;
    public string ClientEmail { get; init; } = null!;
    public string ClientId { get; init; } = null!;
    public string AuthUri { get; init; } = null!;
    public string TokenUri  { get; init; } = null!;
    public string AuthProviderX509CertUrl { get; init; } = null!;
    public string ClientX509CertUrl { get; init; } = null!;
    public string UniverseDomain { get; init; } = null!;
}