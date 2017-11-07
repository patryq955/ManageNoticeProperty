using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ManageNoticeProperty.Infrastructure
{
    public class FileToByte : IFileToByte
    {
        public byte[] GetSavePhoto(HttpPostedFileBase photoToConvert)
        {
            var test = photoToConvert.InputStream;
            byte[] data;
            using (Stream inputStream = test)
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