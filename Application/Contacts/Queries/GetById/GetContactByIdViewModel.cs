using AutoMapper;
using Domain.Entities;
using Gallery.Application.Common.Mappings;

namespace Application.Contacts.Queries.GetById
{
    public class GetContactByIdViewModel : IMapFrom<Contact>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Contact, GetContactByIdViewModel>();
        }
    }
}
