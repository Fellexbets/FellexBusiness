using System;
using Xunit;
using Animais;

namespace ClinicaVeterinariaTest
{
    public class PeixeTest
    {
        [Fact]
        public void TestEspecie()
        {
            String especie = "Peixe-palhaço";
            Peixe actual = new Peixe("Nemo", especie, 100, 2);

            String expected = especie;
            Assert.Equal(expected, actual.Especie);
        }
    }
}
