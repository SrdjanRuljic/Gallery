using AutoMapper;
using Domain.Entities;
using Gallery.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Categories.Queries
{
    public class DropdownItemViewModel : IMapFrom<Category>
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Category, DropdownItemViewModel>();
        }
    }
}
