﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Aspects.Configuration;
using PostSharp.Aspects;
using PostSharp.Aspects.Configuration;
using Core.CrossCuttingConcerns.Caching;
using System.Reflection;
using PostSharp.Aspects.Serialization

namespace Core.Aspects.PostSharp.CacheAspects
{
    [OnMethodBoundaryAspectConfiguration(SerializerType =typeof(MsilAspectSerializer))]
    public class CacheRemoveAspect:OnMethodBoundaryAspect
    {
        private string _pattern;
        private Type _cacheType;
        private ICacheManager _cacheManager;
        public CacheRemoveAspect(Type cacheType)
        {
            _cacheType = cacheType;
        }

        public CacheRemoveAspect(string pattern, Type cacheType)
        {
            _pattern = pattern;
            _cacheType = cacheType;
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
        public override void OnSuccess(MethodExecutionArgs args)
        {
            _cacheManager.RemoveByPattern(string.IsNullOrEmpty(_pattern) ? string.Format("{0}.{1}.*", args.Method.ReflectedType.Namespace,
                args.Method.ReflectedType.Name) : _pattern);
        }
    }
}
