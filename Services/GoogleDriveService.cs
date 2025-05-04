using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AITUC.Services
{
    public class GoogleDriveService
    {
        private DriveService _driveService;

        public async Task InitializeAsync()
        {
            var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = "YOUR_CLIENT_ID",
                    ClientSecret = "YOUR_CLIENT_SECRET"
                },
                new[] { DriveService.Scope.DriveFile },
                "user",
                CancellationToken.None
            );

            _driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "MembershipManager",
            });
        }

        public async Task UploadFileAsync(string filePath)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = Path.GetFileName(filePath)
            };
            using var stream = new FileStream(filePath, FileMode.Open);
            var request = _driveService.Files.Create(fileMetadata, stream, "application/octet-stream");
            await request.UploadAsync();
        }
    }
}
