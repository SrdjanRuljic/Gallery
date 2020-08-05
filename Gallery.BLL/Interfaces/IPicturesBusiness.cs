using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.BLL.Interfaces
{
    public interface IPicturesBusiness
    {
        Task<int> UploadImage(string content, string extension);
    }
}
