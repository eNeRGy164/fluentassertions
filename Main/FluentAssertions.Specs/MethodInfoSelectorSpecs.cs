using System;
using System.Collections.Generic;
using System.Reflection;

using FluentAssertions.Assertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentAssertions.specs
{
    [TestClass]
    public class MethodInfoSelectorSpecs
    {
        [TestMethod]
        public void When_selecting_methods_that_are_public_or_internal_it_should_return_only_the_applicable_methods()
        {
            //-------------------------------------------------------------------------------------------------------------------
            // Arrange
            //-------------------------------------------------------------------------------------------------------------------
            Type type = typeof (TestClassForMethodSelector);

            //-------------------------------------------------------------------------------------------------------------------
            // Act
            //-------------------------------------------------------------------------------------------------------------------
            IEnumerable<MethodInfo> methods = type.Methods().ThatArePublicOrInternal.ToArray();

            //-------------------------------------------------------------------------------------------------------------------
            // Assert
            //-------------------------------------------------------------------------------------------------------------------
            const int PublicMethodCount = 2;
            const int InternalMethodCount = 1;
            methods.Should().HaveCount(PublicMethodCount + InternalMethodCount);
        }

        [TestMethod]
        public void When_selecting_methods_decorated_with_specific_attribute_it_should_return_only_the_applicable_methods()
        {
            //-------------------------------------------------------------------------------------------------------------------
            // Arrange
            //-------------------------------------------------------------------------------------------------------------------
            Type type = typeof (TestClassForMethodSelector);

            //-------------------------------------------------------------------------------------------------------------------
            // Act
            //-------------------------------------------------------------------------------------------------------------------
            IEnumerable<MethodInfo> methods = type.Methods().ThatAreDecoratedWith<DummyMethodAttribute>().ToArray();

            //-------------------------------------------------------------------------------------------------------------------
            // Assert
            //-------------------------------------------------------------------------------------------------------------------
            methods.Should().HaveCount(2);
        }

        [TestMethod]
        public void When_selecting_methods_that_return_a_specific_type_it_should_return_only_the_applicable_methods()
        {
            //-------------------------------------------------------------------------------------------------------------------
            // Arrange
            //-------------------------------------------------------------------------------------------------------------------
            Type type = typeof (TestClassForMethodSelector);

            //-------------------------------------------------------------------------------------------------------------------
            // Act
            //-------------------------------------------------------------------------------------------------------------------
            IEnumerable<MethodInfo> methods = type.Methods().ThatReturn<string>().ToArray();

            //-------------------------------------------------------------------------------------------------------------------
            // Assert
            //-------------------------------------------------------------------------------------------------------------------
            methods.Should().HaveCount(2);
        }

        [TestMethod]
        public void When_selecting_methods_without_return_value_it_should_return_only_the_applicable_methods()
        {
            //-------------------------------------------------------------------------------------------------------------------
            // Arrange
            //-------------------------------------------------------------------------------------------------------------------
            Type type = typeof (TestClassForMethodSelector);

            //-------------------------------------------------------------------------------------------------------------------
            // Act
            //-------------------------------------------------------------------------------------------------------------------
            IEnumerable<MethodInfo> methods = type.Methods().ThatReturnVoid.ToArray();

            //-------------------------------------------------------------------------------------------------------------------
            // Assert
            //-------------------------------------------------------------------------------------------------------------------
            methods.Should().HaveCount(4);
        }

        [TestMethod]
        public void When_combining_filters_to_filter_methods_it_should_return_only_the_applicable_methods()
        {
            //-------------------------------------------------------------------------------------------------------------------
            // Arrange
            //-------------------------------------------------------------------------------------------------------------------
            Type type = typeof (TestClassForMethodSelector);

            //-------------------------------------------------------------------------------------------------------------------
            // Act
            //-------------------------------------------------------------------------------------------------------------------
            IEnumerable<MethodInfo> methods = type.Methods()
                .ThatArePublicOrInternal
                .ThatReturnVoid
                .ToArray();

            //-------------------------------------------------------------------------------------------------------------------
            // Assert
            //-------------------------------------------------------------------------------------------------------------------
            methods.Should().HaveCount(2);
        }
    }

    #region Internal classes used in unit tests

    internal class TestClassForMethodSelector
    {
        public event EventHandler SomethingChanged = delegate { };

        public virtual void PublicVirtualVoidMethod()
        {
        }

        [DummyMethod]
        public virtual void PublicVirtualVoidMethodWithAttribute()
        {
        }

        internal virtual int InternalVirtualIntMethod()
        {
            return 0;
        }

        [DummyMethod]
        protected virtual void ProtectedVirtualVoidMethodWithAttribute()
        {
        }

        private void PrivateVoidDoNothing()
        {
        }

        protected virtual string ProtectedVirtualStringMethod()
        {
            return "";
        }

        private string PrivateStringMethod()
        {
            return "";
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class DummyMethodAttribute : Attribute
    {
    }

    #endregion
}