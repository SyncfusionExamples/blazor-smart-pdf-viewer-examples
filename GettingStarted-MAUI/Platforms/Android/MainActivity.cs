using Android.App;
using Android.Content.PM;
using Android.OS;
using Java.Lang.Annotation;
using Microsoft.ML.OnnxRuntime;

namespace MauiApp2
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {

        //        protected override void OnCreate(Bundle? savedInstanceState)
        //        {
        //            base.OnCreate(savedInstanceState);

        //            try
        //            {
        //                // Get Android's internal files directory
        //                var filesDir = Android.App.Application.Context.FilesDir?.AbsolutePath!;
        //                var targetPath = Path.Combine(filesDir, "model.onnx");

        //#if DEBUG
        //                Console.WriteLine($"Target path: {targetPath}");
        //#endif

        //                if (!File.Exists(targetPath))
        //                {
        //                    // Ensure directory structure exists
        //                    Directory.CreateDirectory(filesDir);

        //                    using var assetStream = Assets!.Open("model.onnx");
        //                    using var fileStream = File.Create(targetPath);
        //                    assetStream.CopyTo(fileStream);

        //#if DEBUG
        //                    Console.WriteLine("Model file copied successfully");
        //#endif
        //                }

        //                // Initialize ONNX runtime
        //                if (File.Exists(targetPath))
        //                {
        //                    var sessionOptions = new SessionOptions();
        //                    using var session = new InferenceSession(targetPath, sessionOptions);
        //                    // Store session in your service or static property
        //                    EmbeddingService.ModelSession = session;
        //                }
        //                else
        //                {
        //                    throw new FileNotFoundException("Model file not found after copy attempt");
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Android.Util.Log.Error("APP_INIT", $"Model initialization failed: {ex}");
        //                // Consider showing user-facing error message
        //                throw new InvalidOperationException("Critical model initialization failed", ex);
        //            }
        //        }

        public static async Task EnsureModelExistsAsync()
        {
            var assetFileName = "model.onnx";
            var targetPath = Path.Combine(FileSystem.AppDataDirectory, "LocalEmbeddingsModel/default/model.onnx");

            if (!File.Exists(targetPath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(targetPath));

                using var assetStream = await FileSystem.OpenAppPackageFileAsync(assetFileName);
                using var fileStream = File.Create(targetPath);
                await assetStream.CopyToAsync(fileStream);
            }
        }


    }
    //public static class EmbeddingService
    //{
    //    public static InferenceSession? ModelSession { get; set; }
    //}
}
