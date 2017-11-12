using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ManageNoticeProperty.Infrastructure
{
    public interface IPhotoConvert
    {
        byte[] PhotoToByte(HttpPostedFileBase photoToConvert);

    }
}
