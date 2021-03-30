using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pictures.Commands.InsertCommand
{
    public class InsertPictureCommand : IRequest<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CategoryId { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Extension { get; set; }
    }
}
