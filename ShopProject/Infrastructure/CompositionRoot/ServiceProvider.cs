using ShopProject.Infrastructure.CompositionRoot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ShopProject.Infrastructure.CompositionRoot
{
    public class ServiceProvider
    {
        private readonly Dictionary<Type, Func<Scope, object>> _registrations = new();
        private readonly Dictionary<Type, object> _singletons = new();
        public Scope CurrentScope { get; private set; } = new Scope(); 
        public void CreateScope()
        {
            CurrentScope = new Scope();
        }
        public void RegisterSingleton<TService, TImpl>()
            where TImpl : TService
        {
            _registrations[typeof(TService)] = (scope) =>
            {
                if (!_singletons.TryGetValue(typeof(TService), out var instance))
                {
                    instance = CreateInstance(typeof(TImpl), scope);
                    _singletons[typeof(TService)] = instance;
                }
                return instance;
            };
        }
         
        public void RegisterScoped<TService, TImpl>()
            where TImpl : TService
        {
            _registrations[typeof(TService)] = (scope) =>
            {
                if (!scope.Instances.TryGetValue(typeof(TService), out var instance))
                {
                    instance = CreateInstance(typeof(TImpl), scope);
                    scope.Instances[typeof(TService)] = instance;
                }
                return instance;
            };
        }
         
        public void RegisterTransient<TService, TImpl>()
            where TImpl : TService
        {
            _registrations[typeof(TService)] = (scope) =>
                CreateInstance(typeof(TImpl), scope);
        } 
        public T Get<T>()
        {
            return (T)_registrations[typeof(T)](CurrentScope);
        }

        private object CreateInstance(Type type, Scope scope)
        {
            var ctor = type.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                   .OrderByDescending(c => c.GetParameters().Length)
                   .FirstOrDefault();

            if (ctor == null)
            {
                throw new InvalidOperationException($"Тип {type.Name} не має конструктора");
            }

            var parameters = ctor.GetParameters()
                                 .Select(p => Get(p.ParameterType, scope))  
                                 .ToArray();

            return ctor.Invoke(parameters);  
        }

        private object Get(Type type, Scope scope)
        {
            if (_registrations.TryGetValue(type, out var creator))
                return creator(scope);

            throw new System.Exception($"Тип {type.Name} не зареєстрований");
        }
    }
}
