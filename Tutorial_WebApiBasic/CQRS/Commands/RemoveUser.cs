using MediatR;
using Tutorial_WebApiBasic.Infrastructure;

namespace Tutorial_WebApiBasic.CQRS.Commands;

public record RemoveUser : IRequest
{
    public string Id { get; set; }
}

public class RemoveUserHandler : IRequestHandler<RemoveUser>
{
    private readonly MyDbContext _dbContext;

    public RemoveUserHandler(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task Handle(RemoveUser request, CancellationToken cancellationToken)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Id == request.Id) ?? throw new Exception($"Can not found user, id = {request.Id}");

        _dbContext.Users.Remove(user);
        _dbContext.SaveChanges();

        return Task.CompletedTask;
    }
}