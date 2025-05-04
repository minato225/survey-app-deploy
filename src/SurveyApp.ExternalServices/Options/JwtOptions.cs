namespace SurveyApp.ExternalServices.Options;

public sealed class JwtOptions
{
    public required string Issuer {  get; init; }
    public required string Audience { get; init; }
    public required int Expires { get; init; }
    public required string Key { get; init; }
}