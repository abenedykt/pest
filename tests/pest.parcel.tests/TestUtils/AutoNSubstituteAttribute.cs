using AutoFixture;
using AutoFixture.Xunit2;

namespace Pest.Parcel.Tests.TestUtils;

public class AutoNSubstituteAttribute() : AutoDataAttribute(() => new Fixture()
    .Customize(new AutoPopulatedNSubstitutePropertiesCustomization()));