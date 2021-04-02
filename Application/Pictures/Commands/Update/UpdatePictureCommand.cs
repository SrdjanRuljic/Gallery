﻿using MediatR;

namespace Application.Pictures.Commands.Update
{
    public class UpdatePictureCommand : IRequest<bool>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CategoryId { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Extension { get; set; }
    }
}
