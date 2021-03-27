using System;
using System.Collections.Generic;
using Domain.Foundation.Events;

namespace Domain.Foundation.EventStore
{
    public interface IEventMetadataProvider
    {
        string GetTypeName<T>();
        string GetTypeName(Type o);  
        public Type GetType(string typeName);
    }

    public class EventMetadataProvider: IEventMetadataProvider, IEventMetadataMapper
    {
        readonly Dictionary<string, Type> _reverseMap = new();
        readonly Dictionary<Type, string> _map        = new();

        public string GetTypeName<T>() => _map[typeof(T)];
        public string GetTypeName(Type o) => _map[o];

        public Type GetType(string typeName) => _reverseMap[typeName];

        public void AddType<T>(string name) {
            _reverseMap[name] = typeof(T);
            _map[typeof(T)]   = name;
        }
    }
}