﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Aspects;
using PostSharp.Serialization;
using PostSharp.Aspects.Configuration;
using PostSharp.Aspects.Serialization;
using PostSharp.Extensibility;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using System.Reflection;
using Core.CrossCuttingConcerns.Logging;

namespace Core.Aspects.PostSharp.LogAspects
{
    [OnMethodBoundaryAspectConfiguration(SerializerType =typeof(MsilAspectSerializer))]
    [MulticastAttributeUsage(MulticastTargets.Method, 
        TargetMemberAttributes =MulticastAttributes.Instance)]
    public class LogAspects:OnMethodBoundaryAspect
    {
        private Type _loggerType;
        private LoggerService _loggerService;
        public LogAspects(Type loggerType)
        {
            _loggerType = loggerType;
        }
        public override void RuntimeInitialize(MethodBase method)
        {
            if (_loggerType.BaseType!=typeof(LoggerService))
            {
                throw new Exception("Wrong Logger type");
            }
            _loggerService = (LoggerService)Activator.CreateInstance(_loggerType);
            base.RuntimeInitialize(method);
        }
        public override void OnEntry(MethodExecutionArgs args)
        {

            if (!_loggerService.IsInfoEnabled)
            {
                return;
            }
            try
            {
                var logParameters = args.Method.GetParameters().Select((t, i) => new LogParameter
                {
                    Name = t.Name,
                    Type = t.ParameterType.Name,
                    Value = args.Arguments.GetArgument(i)
                }).ToList();
                var logDetail = new LogDetail
                {
                    FullName = args.Method.DeclaringType == null ? null : args.Method.DeclaringType.Name,
                    MethodName = args.Method.Name,
                    Parameters = logParameters
                };
                _loggerService.Info(logDetail);
            }
            catch (Exception)
            {

             
            }
          
        }

    }
}
