using System.Drawing;
using System.Drawing.Imaging;
using System.Text.Json;
using Firebase.Auth;
using Firebase.Storage;

namespace ASP.groep.L.Application.FileManagement
{
    public class FileManagement
    {

        public static async Task <String> UploadImage(MemoryStream memStream,String filename)
        {
            using (StreamReader r = new StreamReader(Directory.GetCurrentDirectory()+"/creds.json"))
            {
                Auth ?creds = JsonSerializer.Deserialize<Auth>(r.ReadToEnd());
                var config = new FirebaseAuthConfig
                {
                    ApiKey = creds!.APIKey
                };
                var auth = new FirebaseAuthClient(config);

                //authentication

                var a = await auth.SignInAnonymouslyAsync();
                var storage = new FirebaseStorage(
                    "groepltrees.appspot.com",
                     new FirebaseStorageOptions
                     {
                         AuthTokenAsyncFactory = () => Task.FromResult(a.User.Credential.IdToken),
                         ThrowOnCancel = true,
                     });

                var task = storage

                    .Child("images")
                    .Child(filename)
                    .PutAsync(memStream);

                
                // Track progress of the upload
                task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");

                // await the task to wait until upload completes and get the download url
                var downloadUrl = await task;
                return downloadUrl;
            }
        }

        public static async Task<String> UploadPDF(MemoryStream memStream, String filename)
        {
            using (StreamReader r = new StreamReader(Directory.GetCurrentDirectory() + "/creds.json"))
            {
                Auth? creds = JsonSerializer.Deserialize<Auth>(r.ReadToEnd());
                var config = new FirebaseAuthConfig
                {
                    ApiKey = creds!.APIKey
                };
                var auth = new FirebaseAuthClient(config);

                //authentication

                var a = await auth.SignInAnonymouslyAsync();

                var task = new FirebaseStorage(
                    "groepltrees.appspot.com",
                     new FirebaseStorageOptions
                     {
                         AuthTokenAsyncFactory = () => Task.FromResult(a.User.Credential.IdToken),
                         ThrowOnCancel = true,
                     })
                    .Child("pdf")
                    .Child(filename)
                    .PutAsync(memStream);

                // Track progress of the upload
                task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");

                // await the task to wait until upload completes and get the download url
                var downloadUrl = await task;
                return downloadUrl;
            }
        }

        public static async Task DeleteImage(String filename)
        {
            using (StreamReader r = new StreamReader(Directory.GetCurrentDirectory() + "/creds.json"))
            {
                Auth? creds = JsonSerializer.Deserialize<Auth>(r.ReadToEnd());

                var config = new FirebaseAuthConfig
                {
                    ApiKey = creds!.APIKey
                };
                var auth = new FirebaseAuthClient(config);

                //authentication

                var a = await auth.SignInAnonymouslyAsync();

                var storage = new FirebaseStorage(
                    "groepltrees.appspot.com",
                     new FirebaseStorageOptions
                     {
                         AuthTokenAsyncFactory = () => Task.FromResult(a.User.Credential.IdToken),
                         ThrowOnCancel = true,
                     });

                var task = storage

                    .Child("images")
                    .Child(filename)
                    .DeleteAsync();
                await task;
            }
        }

        public static async Task DeletePDF(String filename)
        {
            using (StreamReader r = new StreamReader(Directory.GetCurrentDirectory() + "/creds.json"))
            {

                Auth? creds = JsonSerializer.Deserialize<Auth>(r.ReadToEnd());

                var config = new FirebaseAuthConfig
                {
                    ApiKey = creds!.APIKey
                };
                var auth = new FirebaseAuthClient(config);

                //authentication

                var a = await auth.SignInAnonymouslyAsync();

                var storage = new FirebaseStorage(
                    "groepltrees.appspot.com",
                     new FirebaseStorageOptions
                     {
                         AuthTokenAsyncFactory = () => Task.FromResult(a.User.Credential.IdToken),
                         ThrowOnCancel = true,
                     });

                var task = storage

                    .Child("pdf")
                    .Child(filename)
                    .DeleteAsync();
                await task;
            }
        }

        public static async Task DownloadFile(String url)
        {
        }



    private static MemoryStream compressImage(MemoryStream stream,int width, int height, int quality)   // set quality to 1-100, eg 50
    {
        using (Image image = Image.FromStream(stream))
        using (Image memImage = new Bitmap(image, width, height))
        {
            ImageCodecInfo myImageCodecInfo;
            System.Drawing.Imaging.Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;
            myImageCodecInfo = GetEncoderInfo("image/jpeg");
            myEncoder = System.Drawing.Imaging.Encoder.Quality;
            myEncoderParameters = new EncoderParameters(1);
            myEncoderParameter = new EncoderParameter(myEncoder, quality);
            myEncoderParameters.Param[0] = myEncoderParameter;

            MemoryStream memStream = new MemoryStream();
            memImage.Save(memStream, myImageCodecInfo, myEncoderParameters);
            return memStream;
        }
    }

    private static ImageCodecInfo GetEncoderInfo(String mimeType)
    {
        ImageCodecInfo[] encoders;
        encoders = ImageCodecInfo.GetImageEncoders();
        foreach (ImageCodecInfo ici in encoders)
            if (ici.MimeType == mimeType) return ici;

        return null;
    }


}
}
