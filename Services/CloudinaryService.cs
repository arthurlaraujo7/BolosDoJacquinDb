using Microsoft.Extensions.Options;
using BolosDoJacquinDb.Utils;

namespace BolosDoJacquinDb.Services;

public class CloudinaryService
{
    private readonly CloudinarySettings _settings;

    public CloudinaryService(IOptions<CloudinarySettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task<string?> UploadImagemAsync(Stream stream, string filename)
    {
        await Task.CompletedTask;
        return null;
    }
}