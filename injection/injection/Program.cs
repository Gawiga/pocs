// See https://aka.ms/new-console-template for more information

using System;

Main();
//based on https://www.youtube.com/watch?v=NkTF_6IQPiY 
//https://github.com/T0shik/raw-coding-101-tutorials/tree/main/Dependency%20Injection

void Main()
{
    var container = new DependencyContainer();
    //container.AddDependency(typeof(HelloService));
    //container.AddDependency<ServiceConsumer>();

    container.AddTransient<ServiceConsumer>();
    container.AddSingleton<HelloService>();

    var resolver = new DependencyResolver(container);

    var service = resolver.GetService<ServiceConsumer>();
    service.Print();
    var service2 = resolver.GetService<ServiceConsumer>();
    service2.Print();
    var service3 = resolver.GetService<ServiceConsumer>();
    service3.Print();   
    
    //var service = new HelloService();
    //var consumer = new ServiceConsumer(service);
    //var service = Activator.CreateInstance(typeof(HelloService));
    //var consumer = (ServiceConsumer) Activator.CreateInstance(typeof(ServiceConsumer), service);
    //consumer.Print();
}

//classe responsavel por resolver as dependencias dos containers
public class DependencyResolver
{
    DependencyContainer _container;

    public DependencyResolver(DependencyContainer container)
    {
        _container = container;
    }

    public T GetService<T>()
    {
        return (T) GetService(typeof(T));
    }

    public object GetService(Type type)
    {
        var dependency = _container.GetDependency(type);
        var constructor = dependency.Type.GetConstructors().Single();
        var parameters = constructor.GetParameters().ToArray();
        var parameterImplementations = new object[parameters.Length];

        if (parameters.Length > 0)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                parameterImplementations[i] = GetService(parameters[i].ParameterType);
            }

            return CreateImplementation(dependency, t => Activator.CreateInstance(t, parameterImplementations));
        }

        return CreateImplementation(dependency, t => Activator.CreateInstance(t));

    }

    public object CreateImplementation(Dependency dependency, Func<Type, object> factory)
    {
        if (dependency.Implemented)
        {
            return dependency.Implementation;
        }

        var implementation = factory(dependency.Type);

        if (dependency.LifeTime == DependencyLifetime.Singleton)
        {
            dependency.AddImplementation(implementation);
        }

        return implementation;
    }
}

//container responsavel por adicionar e obter as dependencias (classes)
public class DependencyContainer
{
    List<Type> _dependenciesType;
    List<Dependency> _dependencies;

    public DependencyContainer()
    {
        _dependencies = new List<Dependency>();
    }

    public void AddSingleton<T>()
    {
        _dependencies.Add(new Dependency(typeof(T), DependencyLifetime.Singleton));
    }

    public void AddTransient<T>()
    {
        _dependencies.Add(new Dependency(typeof(T), DependencyLifetime.Transient));
    }

    public void AddDependency(Type type)
    {
        _dependenciesType = new List<Type>();
        _dependenciesType.Add(type);
    }

    public void AddDependency<T>()
    {
        _dependenciesType.Add(typeof(T));
    }

    public Dependency GetDependency(Type type)
    {
        return _dependencies.First(x => x.Type.Name == type.Name);
    }

    public Type GetDependencyType(Type type)
    {
        return _dependenciesType.First(x => x.Name == type.Name);
    }
}

//classe que consumo outra classe e injeta ela em seu construtor
public class ServiceConsumer
{
    HelloService _hello;
    string _guid;

    public ServiceConsumer(HelloService hello)
    {
        _hello = hello;
        _guid = Guid.NewGuid().ToString();
    }

    public void Print()
    {
        Console.WriteLine($"ServiceConsumer: {_guid}");
        _hello.Print();
    }
}

//classe simples com um método
public class HelloService
{
    string _guid;

    public HelloService()
    {
        _guid = Guid.NewGuid().ToString();
    }

    public void Print()
    {
        Console.WriteLine($"HelloService: {_guid}");
    }
}

//lifetime

public class Dependency
{
    public Dependency(Type type, DependencyLifetime lifeTime)
    {
        Type = type;
        LifeTime = lifeTime;
    }

    public Type Type { get; set; }
    public DependencyLifetime LifeTime { get; set; }
    public object Implementation { get; set; }
    public bool Implemented { get; set; }

    public void AddImplementation(object i)
    {
        Implementation = i;
        Implemented = true;
    }
}
public enum DependencyLifetime
{
    Singleton = 0,
    Transient = 1,
}


