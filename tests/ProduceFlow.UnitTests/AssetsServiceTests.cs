using Xunit;

namespace ProduceFlow.UnitTests;

public class AssetServiceTests
{
    [Fact]
    public void Test_Contoh_Sederhana()
    {

        int angka1 = 5;
        int angka2 = 10;


        int hasil = angka1 + angka2;


        Assert.Equal(15, hasil);
    }
}