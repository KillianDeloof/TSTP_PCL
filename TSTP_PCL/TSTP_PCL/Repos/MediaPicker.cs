using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using System.IO;
using System.Linq;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

//this could also work
//https://blog.xamarin.com/getting-started-with-the-media-plugin-for-xamarin/


namespace TSTP_PCL.Repositories
{

    public class MediaPicker// : ICameraService
    {
        public static async Task<MediaFile> TakePhoto()
        {


            await CrossMedia.Current.Initialize();

            if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
            {
                // Supply media options for saving our photo after it's taken.
                var mediaOptions = new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Receipts",
                    Name = $"{DateTime.UtcNow}.jpg",
                    DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Rear,
                    PhotoSize = PhotoSize.Small
                };

                // Take a photo of the business receipt.
                MediaFile file = await CrossMedia.Current.TakePhotoAsync(mediaOptions);
                return file;
            }
            else
            {
                return null;
            }


        }


        public static async Task<MediaFile> PickPhoto()
        {
            // Select a photo. 
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                var mediaOptions = new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = PhotoSize.Small
                };


                MediaFile photo = await CrossMedia.Current.PickPhotoAsync(mediaOptions);
                return photo;
            }
            else
            {
                return null;
            }
                

            // Select a video. 
            //if (CrossMedia.Current.IsPickVideoSupported)
            //    MediaFile video = await CrossMedia.Current.PickVideoAsync();
        }


        public static byte[] MediaFileToByteArr(MediaFile mediafile)
        {
            byte[] byteArr;
            using (var memoryStream = new MemoryStream())
            {
                mediafile.GetStream().CopyTo(memoryStream);
                mediafile.Dispose();
                byteArr = memoryStream.ToArray();
            }
            return byteArr;
        }


       


    }
}
