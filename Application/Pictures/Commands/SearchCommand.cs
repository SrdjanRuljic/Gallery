﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pictures.Commands
{
    public class SearchCommand : IRequest<List<SearchCommandViewModel>>
    {
        public string Name { get; set; }
        public long? CategoryId { get; set; }
    }
}
