﻿using AutoMapper;
using Gallery.Common;
using Gallery.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gallery.WebAPI.AutoMapper
{
    public class PrictureProfile : Profile
    {
        public PrictureProfile()
        {
            CreateMap<PictureViewModel, PictureModel>();
        }
    }
}
