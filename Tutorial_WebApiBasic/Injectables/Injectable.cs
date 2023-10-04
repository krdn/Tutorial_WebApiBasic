namespace Tutorial_WebApiBasic.Injectables;

public interface IInjectableService { }
public interface ITransientService : IInjectableService { }
public interface IScopedService : IInjectableService { }
public interface ISingletonService : IInjectableService { }