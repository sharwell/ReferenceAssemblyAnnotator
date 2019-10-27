﻿// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace TunnelVisionLabs.ReferenceAssemblyAnnotator
{
    using System.Linq;
    using Mono.Cecil;

    internal partial class WellKnownTypes
    {
        private sealed class NullableAttributeProvidedType : ProvidedAttributeType
        {
            internal NullableAttributeProvidedType()
                : base("System.Runtime.CompilerServices", "NullableAttribute")
            {
            }

            protected override void ImplementAttribute(ModuleDefinition module, TypeDefinition attribute, WellKnownTypes wellKnownTypes, CustomAttributeFactory attributeFactory)
            {
                MethodDefinition compilerGeneratedConstructor = wellKnownTypes.SystemRuntimeCompilerServicesCompilerGeneratedAttribute.Resolve().Methods.Single(method => method.IsConstructor && !method.IsStatic && method.Parameters.Count == 0);
                attribute.CustomAttributes.Add(new CustomAttribute(wellKnownTypes.Module.ImportReference(compilerGeneratedConstructor)));
                attribute.CustomAttributes.Add(new CustomAttribute(wellKnownTypes.MicrosoftCodeAnalysisEmbeddedAttribute.Value.Resolve().Methods.Single(method => method.IsConstructor && !method.IsStatic && method.Parameters.Count == 0)));

                var constructorByte = MethodFactory.Constructor(wellKnownTypes.TypeSystem);
                constructorByte.Parameters.Add(new ParameterDefinition(wellKnownTypes.TypeSystem.Byte));

                var constructorByteArray = MethodFactory.Constructor(wellKnownTypes.TypeSystem);
                constructorByteArray.Parameters.Add(new ParameterDefinition(new ArrayType(wellKnownTypes.TypeSystem.Byte)));

                attribute.Methods.Add(constructorByte);
                attribute.Methods.Add(constructorByteArray);
            }
        }
    }
}
