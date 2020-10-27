using Gallery.Common;
using Gallery.DAL.Interfaces;
using Gallery.DAL.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gallery.DAL
{
    public class ContactsDataAccess : IContactsDataAccess
    {
        private readonly DbContext _dBContext = new DbContext();

        public async Task Delete(long id) =>
            await _dBContext.Delete("[dbo].[sp_Contacts.Delete]", id);

        public async Task<List<ContactModel>> GetAll() =>
            await _dBContext.GetList<ContactModel>("[dbo].[sp_Contacts.GetAll]");

        public async Task<ContactModel> GetById(long id) =>
            await _dBContext.GetSingle<ContactModel>("[dbo].[sp_Contacts.GetById]", id);

        public async Task<long> Insert(ContactModel model) =>
            await _dBContext.Insert("[dbo].[sp_Contacts.Insert]", model);

        public async Task<bool> Update(ContactModel model) =>
            await _dBContext.Update("[dbo].[sp_Contacts.Update]", model);
    }
}
