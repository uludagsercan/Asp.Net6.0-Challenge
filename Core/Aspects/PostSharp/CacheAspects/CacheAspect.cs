﻿using Core.CrossCuttingConcerns.Caching;
using PostSharp.Aspects;
using PostSharp.Aspects.Configuration;
using PostSharp.Aspects.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.PostSharp.CacheAspects
{
    [MethodInterceptionAspectConfiguration(SerializerType =typeof(MsilAspectSerializer))]
    public class CacheAspect:MethodInterceptionAspect
    {
        private Type _cacheType;
        private int _cacheMinute;
        private ICacheManager _cacheManager;

        public CacheAspect(Type cacheType, int cacheMinute=60)
        {
            _cacheType = cacheType;
            _cacheMinute = cacheMinute;
        }
        public override void RuntimeInitialize(MethodBase method)
        {
            if (!typeof(ICacheManager).IsAssignableFrom(_cacheType))
            {
                throw new Exception("Wrong Cache Manager");
            }
            _cacheManager = (ICacheManager)Activator.CreateInstance(_cacheType);
            base.RuntimeInitialize(method);
        }
        public override void OnInvoke(MethodInterceptionArgs args)
        {
            var methodName = string.Format("{0},{1},{2}", args.Method.ReflectedType.Namespace,
                args.Method.ReflectedType.Name,
                args.Method.Name);
            var arguments = args.Arguments.ToList();
            var key = string.Format("{0}({1})", methodName,
                string.Join(",", arguments.Select(x => x != null ? x.ToString() : "<Null>")));

            if (_cacheManager.IsAdd(key))
            {
                args.ReturnValue = _cacheManager.Get<object>(key);
            }
            base.OnInvoke(args);
            _cacheManager.Add(key, args.ReturnValue, _cacheMinute);

        }

    }
}
