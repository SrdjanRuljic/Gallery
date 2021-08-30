﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetUserByUsername
{
    public class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, UserLoginDetailsViewModel>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public GetUserByUsernameQueryHandler(UserManager<AppUser> userManager,
                                       IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserLoginDetailsViewModel> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            UserLoginDetailsViewModel viewModel = await _userManager.Users
                                                                    .Where(x => x.UserName.Equals(request.Username))
                                                                    .ProjectTo<UserLoginDetailsViewModel>(_mapper.ConfigurationProvider)
                                                                    .FirstOrDefaultAsync();

            return viewModel;
        }
    }
}
