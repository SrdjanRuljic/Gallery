using Gallery.BLL.Interfaces;
using Gallery.Common;
using Gallery.Common.Helpers;
using Gallery.Common.Validations;
using Gallery.DAL;
using Gallery.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.BLL
{
    public class ContactsBusiness : IContactsBusiness
    {
        private readonly IContactsDataAccess _contactsDataAccess;

        public ContactsBusiness()
        {
            _contactsDataAccess = new ContactsDataAccess();
        }

        public async Task Delete(long id)
        {
            if (id <= 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.IdCanNotBeLowerThanOne);
            }
            
            await _contactsDataAccess.Delete(id);           
        }

        public async Task<List<ContactModel>> GetAll() =>
            await _contactsDataAccess.GetAll();

        public async Task<ContactModel> GetById(long id)
        {
            if (id <= 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.IdCanNotBeLowerThanOne);
            }

            ContactModel model = await _contactsDataAccess.GetById(id);

            if (model == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, ErrorMessages.ContactNotFound);
            }

            return model;
        }

        public async Task<long> Insert(ContactModel model)
        {
            long id = 0;
            string errorMessage = null;

            if (!model.IsValid(out errorMessage))
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessage);
            }
            
            id = await _contactsDataAccess.Insert(model);

            return id;
        }

        public async Task<bool> Update(ContactModel model)
        {
            bool isUpdated = false;
            string errorMessage = null;

            if (!model.IsValid(out errorMessage))
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessage);
            }
            
            isUpdated = await _contactsDataAccess.Update(model);

            return isUpdated;
        }
    }
}
