using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ManageNoticeProperty.Infrastructure
{
    public interface IFileToByte
    {
         byte[] GetSavePhoto(HttpPostedFileBase photoToConvert);
    }
}
