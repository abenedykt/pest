using AutoFixture;
using AutoFixture.Xunit2;

namespace pest.parcel.tests;

public class AutoNSubstituteAttribute() : AutoDataAttribute(() => new Fixture()
    .Customize(new AutoPopulatedNSubstitutePropertiesCustomization()));