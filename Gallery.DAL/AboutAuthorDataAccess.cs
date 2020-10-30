using Gallery.Common;
using Gallery.DAL.Interfaces;
using Gallery.DAL.Persistence;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gallery.DAL
{
    public class AboutAuthorDataAccess : IAboutAuthorDataAccess
    {
        private readonly DbContext _dBContext = new DbContext();

        public Task Delete(long id) =>
            throw new NotImplementedException();

        public Task<List<AboutAuthorModel>> GetAll() =>
            throw new NotImplementedException();

        public async Task<AboutAuthorModel> GetById(long id) =>
            await _dBContext.GetSingle<AboutAuthorModel>("[dbo].[sp_AboutAuthor.GetById]", id);

        public async Task<long> Insert(AboutAuthorModel model) =>
            await _dBContext.Insert("[dbo].[sp_AboutAuthor.Insert]", model);

        public async Task<bool> Update(AboutAuthorModel model) =>
            await _dBContext.Update("[dbo].[sp_AboutAuthor.Update]", model);
    }
}
