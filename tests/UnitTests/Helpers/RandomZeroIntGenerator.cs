using AutoFixture.Kernel;

namespace Etn.MyLittleBoard.UnitTests.Helpers;

public sealed class RandomZeroIntGenerator : ISpecimenBuilder
{
    private readonly Random random = new();

    public object Create(object request, ISpecimenContext context)
    {
        if (request is Type type && type == typeof(int))
        {
            return this.random.Next(0, 100);
        }
        else
        {
            return new NoSpecimen();
        }
    }
}
