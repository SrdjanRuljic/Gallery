using Gallery.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.DAL.Interfaces
{
    public interface IPicturesDataAccess : IBaseDataAccess<PictureModel>
    {
        Task<List<PictureModel>> Search();
    }
}
