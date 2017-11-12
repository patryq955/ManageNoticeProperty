using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace ManageNoticeProperty.Infrastructure
{
    public class PhotoConvert : IPhotoConvert
    {
        public byte[] PhotoToByte(HttpPostedFileBase photoToConvert)
        {
            var test = photoToConvert;
            var file = photoToConvert.InputStream;
            byte[] data;
            using (Stream inputStream = file)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                data = memoryStream.ToArray();
            }
            return data;
        }
    }
}