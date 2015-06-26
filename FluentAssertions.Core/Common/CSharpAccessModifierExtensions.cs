﻿using System;
using System.Reflection;

namespace FluentAssertions.Common
{
    public static class CSharpAccessModifierExtensions
    {
        internal static CSharpAccessModifier GetCSharpAccessModifier(this MethodBase methodBase)
        {
            if (methodBase.IsPrivate)
            {
                return CSharpAccessModifier.Private;
            }

            if (methodBase.IsFamily)
            {
                return CSharpAccessModifier.Protected;
            }

            if (methodBase.IsAssembly)
            {
                return CSharpAccessModifier.Internal;
            }

            if (methodBase.IsPublic)
            {
                return CSharpAccessModifier.Public;
            }

            if (methodBase.IsFamilyOrAssembly)
            {
                return CSharpAccessModifier.ProtectedInternal;
            }

            return CSharpAccessModifier.InvalidForCSharp;
        }

        internal static CSharpAccessModifier GetCSharpAccessModifier(this Type type)
        {
            if (type.IsNestedPrivate)
            {
                return CSharpAccessModifier.Private;
            }

            if (type.IsNestedFamily)
            {
                return CSharpAccessModifier.Protected;
            }

            if (type.IsNestedAssembly || (type.IsClass && type.IsNotPublic))
            {
                return CSharpAccessModifier.Internal;
            }
            
            if (type.IsPublic || type.IsNestedPublic)
            {
                return CSharpAccessModifier.Public;
            }
            
            if (type.IsNestedFamORAssem)
            {
                return CSharpAccessModifier.ProtectedInternal;
            }

            return CSharpAccessModifier.InvalidForCSharp;
        }
    }
}