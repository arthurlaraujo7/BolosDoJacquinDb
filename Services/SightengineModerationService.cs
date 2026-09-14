using Microsoft.Extensions.Options;
using BolosDoJacquinDb.Utils;

namespace BolosDoJacquinDb.Services;

public class SightengineModerationService
{
    private readonly SightengineSettings _settings;
    private readonly HttpClient _httpClient;

    internal SightengineModerationService(IOptions<SightengineSettings> settings, HttpClient httpClient)
    {
        _settings = settings.Value;
        _httpClient = httpClient;
    }

    public async Task<bool> ModerarTextoAsync(string texto)
    {
        await Task.CompletedTask;
        return true;
    }

    public async Task<bool> ModerarImagemAsync(string imageUrl)
    {
        await Task.CompletedTask;
        return true;
    }
}