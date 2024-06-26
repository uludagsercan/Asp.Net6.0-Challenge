﻿using Core.CrossCuttingConcerns.Validation.FluentValidation;
using FluentValidation;
using PostSharp.Aspects;
using PostSharp.Aspects.Configuration;
using PostSharp.Aspects.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.PostSharp.ValidationAspects.FluentValidation
{
    [OnMethodBoundaryAspectConfiguration(SerializerType = typeof(MsilAspectSerializer))]
    public class FluentValidationAspect : OnMethodBoundaryAspect
    {
        private Type _validatorType;

        public FluentValidationAspect(Type validatorType)
        {
            _validatorType = validatorType;
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            var entities = args.Arguments.Where(x => x.GetType() == entityType);
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
