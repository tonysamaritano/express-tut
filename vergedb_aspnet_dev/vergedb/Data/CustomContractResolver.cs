using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using vergedb.Models;

namespace vergedb.Data
{
    public class CustomContractResolver : DefaultContractResolver
    {
        private readonly Dictionary<Type, HashSet<string>> _ignores;
        private readonly Dictionary<Type, Dictionary<string, string>> _renames;

        public CustomContractResolver()
        {
            _ignores = new Dictionary<Type, HashSet<string>>();
        }

        public void IgnoreProperty(Type type, params string[] jsonPropertyNames)
        {
            if (!_ignores.ContainsKey(type))
                _ignores[type] = new HashSet<string>();

            foreach (var prop in jsonPropertyNames)
                _ignores[type].Add(prop);
        }

        protected override JsonProperty CreateProperty(MemberInfo member,
                                                   MemberSerialization
                                                       memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);
            var propInfo = member as PropertyInfo;

            if (IsIgnored(property.DeclaringType, property.PropertyName))
            {
                property.ShouldSerialize = i => false;
                property.Ignored = true;
            }

            return property;
        }
        private bool IsIgnored(Type type, string jsonPropertyName)
        {
            if (!_ignores.ContainsKey(type))
                return false;

            return _ignores[type].Contains(jsonPropertyName);
        }
    }
}
