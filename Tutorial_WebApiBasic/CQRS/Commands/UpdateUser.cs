using MediatR;
using Tutorial_WebApiBasic.Infrastructure;
using Tutorial_WebApiBasic.Interface.Contract;

namespace Tutorial_WebApiBasic.CQRS.Commands;

public record UpdateUser : IRequest<User>
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class UpdateUserHandler : IRequestHandler<UpdateUser, User>
{
    private readonly MyDbContext _dbContext;

    public UpdateUserHandler(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public Task<User> Handle(UpdateUser request, CancellationToken cancellationToken)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Id == request.Id) ?? throw new Exception($"Can not found user, id = {request.Id}");
        user.Name = request.Name;
        user.Email = request.Email;

        _dbContext.Users.Update(user);
        _dbContext.SaveChanges();

        var findUser = _dbContext.Users.FirstOrDefault(u => u.Id == request.Id) ?? throw new Exception($"Can not found user after update user, id = {request.Id}");
        var response = new User
        {
            Id = findUser.Id,
            Email = findUser.Email,
            Name = findUser.Name,
        };

        return Task.FromResult(response);
    }
}