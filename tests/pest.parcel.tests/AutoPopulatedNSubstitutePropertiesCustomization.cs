using System.Reflection;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Kernel;
using MethodInvoker = AutoFixture.Kernel.MethodInvoker;

namespace pest.parcel.tests;

internal class AutoPopulatedNSubstitutePropertiesCustomization
    : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.ResidueCollectors.Add(
            new Postprocessor(
                new NSubstituteBuilder(
                    new MethodInvoker(
                        new NSubstituteMethodQuery())),
                new AutoPropertiesCommand(
                    new PropertiesOnlySpecification())));
    }

    private class PropertiesOnlySpecification : IRequestSpecification
    {
        public bool IsSatisfiedBy(object request)
        {
            return request is PropertyInfo;
        }
    }
}