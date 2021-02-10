using AutoMapper;
using Gallery.Common;
using Gallery.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gallery.WebAPI.AutoMapper
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<ContactModel, ContactViewModel>();
            CreateMap<ContactViewModel, ContactModel>();
        }
    }
}
