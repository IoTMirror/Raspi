using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace IoT_Mirror
{
    class CameraManager
    {
        public async Task<string> ConvertToByte64(InMemoryRandomAccessStream stream)
        {
            var reader = new DataReader(stream.GetInputStreamAt(0));
            var bytes = new byte[stream.Size];
            await reader.LoadAsync((uint)stream.Size);
            reader.ReadBytes(bytes);
            var stringData = Convert.ToBase64String(bytes);
            return stringData;
        }

        public async Task<byte[]> ConvertToByteArray(InMemoryRandomAccessStream stream)
        {
            var reader = new DataReader(stream.GetInputStreamAt(0));
            var bytes = new byte[stream.Size];
            await reader.LoadAsync((uint)stream.Size);
            reader.ReadBytes(bytes);
            return bytes;
        }

        public async Task<InMemoryRandomAccessStream> TakePicture()
        {
            var mediaCapture = new MediaCapture();
            await mediaCapture.InitializeAsync();
            ImageEncodingProperties imageProperties = ImageEncodingProperties.CreateJpeg();
            InMemoryRandomAccessStream photoStream = new InMemoryRandomAccessStream();
            await mediaCapture.CapturePhotoToStreamAsync(imageProperties, photoStream);
            photoStream.Seek(0);
            return photoStream;
        }
    }
}
