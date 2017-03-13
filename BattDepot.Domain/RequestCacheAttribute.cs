using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using PostSharp.Aspects;

namespace Davisoft_BDSProject.Domain
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Method)]
    public class RequestCacheAttribute : OnMethodBoundaryAspect
    {
        public string Name { get; set; }

        public override void OnEntry(MethodExecutionArgs args)
        {
            if (string.IsNullOrEmpty(Name))
                Name = ComputeName(args);

            var cache = DependencyContainer.Current.Get<ICacheStorageLocation>("InRequest");
            if (cache == null)
                return;

            if (cache.HasKey(Name))
            {
                args.ReturnValue = cache.Get<object>(Name);
                args.FlowBehavior = FlowBehavior.Return;
            }
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            if (string.IsNullOrEmpty(Name))
                Name = ComputeName(args);

            var cache = DependencyContainer.Current.Get<ICacheStorageLocation>("InRequest");
            if (cache == null)
                return;

            if (!cache.HasKey(Name))
                cache.Set(Name, args.ReturnValue);
        }

        private string ComputeName(MethodExecutionArgs args)
        {
            StringBuilder sb = new StringBuilder();

            string assemblyName = args.Method.DeclaringType.FullName;
            string methodName = args.Method.Name;
            sb.AppendFormat("{0}.{1}", assemblyName, methodName);

            List<string> parameterNames = args.Method.GetParameters().Select(p => p.ParameterType.FullName).ToList();

            for (int i = 0; i < args.Arguments.Count; i++)
                sb.AppendFormat("__{0}={1}", parameterNames[i], args.Arguments.GetArgument(i));

            return sb.ToString();
        }
    }
}
