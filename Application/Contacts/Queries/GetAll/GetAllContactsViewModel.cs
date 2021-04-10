using AutoMapper;
using Domain.Entities;
using Gallery.Application.Common.Mappings;

namespace Application.Contacts.Queries.GetAll
{
    public class GetAllContactsViewModel : IMapFrom<Contact>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Contact, GetAllContactsViewModel>();
        }
    }
}
