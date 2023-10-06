using MediatR;
using Microsoft.EntityFrameworkCore;
using Tutorial_WebApiBasic.Infrastructure;
using Tutorial_WebApiBasic.Interface.Contract;

namespace Tutorial_WebApiBasic.CQRS.Commands;

public record AddUser : IRequest<List<User>>
{
    public string Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}


public class AddUserHandler : IRequestHandler<AddUser, List<User>>
{
    private readonly MyDbContext _dbContext;

    public AddUserHandler(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<User>> Handle(AddUser request, CancellationToken cancellationToken)
    {
        _dbContext.Users.Add(new Infrastructure.Models.User
        {
            Id = request.Id,
            Name = request.Name,
            Email = request.Email,
        });

        await _dbContext.SaveChangesAsync(cancellationToken);


        var users = await _dbContext.Users.Select(u => new User()
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,
        }).ToListAsync(cancellationToken: cancellationToken);

        return users;
    }
}