using MediatR;
using Microsoft.EntityFrameworkCore;
using Tutorial_WebApiBasic.Infrastructure;
using Tutorial_WebApiBasic.Interface.Contract;

namespace Tutorial_WebApiBasic.CQRS.Commands;

public record GetUsers : IRequest<List<User>>
{
}

public class GetUsersHandler : IRequestHandler<GetUsers, List<User>>
{
    private readonly MyDbContext _dbContext;

    public GetUsersHandler(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<User>> Handle(GetUsers request, CancellationToken cancellationToken)
    {
        var users = await _dbContext.Users.Select(u => new User
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email
        }).ToListAsync(cancellationToken: cancellationToken);

        return users;
    }


}