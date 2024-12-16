namespace Etn.MyLittleBoard.UnitTests.Helpers;

internal static class StringHelpers
{
    public static string GenerateHexColor()
    {
        Random random = new();
        return $"#{random.Next(0x1000000):X6}";
    }

    public static string GenerateOverMaximumLengthString(int length)
    {
        return new string('x', Math.Max(length, 0) + 1);
    }
}
