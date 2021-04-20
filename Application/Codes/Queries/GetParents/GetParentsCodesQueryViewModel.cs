using AutoMapper;
using Domain.Entities;
using Gallery.Application.Common.Mappings;

namespace Application.Codes.Queries.GetParents
{
    public class GetParentsCodesQueryViewModel : IMapFrom<CodeLanguage>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? ParentId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CodeLanguage, GetParentsCodesQueryViewModel>()
                   .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Code.Id))
                   .ForMember(d => d.ParentId, opt => opt.MapFrom(s => s.Code.ParentId))
                   .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Translation));
        }
    }
}
