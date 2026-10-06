using ProyectoCore.Models;
using Xunit;

namespace ProyectoCore.Test.TestModels;

public class TestPlantilla
{
    [Fact]
    public void Plantilla_CantidadMaximaJugadores_UsaValorPredeterminadoYPermiteConfiguracion()
    {
        var plantilla = new Plantilla();

        Assert.Equal(20, plantilla.CantidadMaximaJugadores);

        plantilla.CantidadMaximaJugadores = 12;
        Assert.Equal(12, plantilla.CantidadMaximaJugadores);
    }

    [Fact]
    public void Plantilla_CantidadMaximaJugadoresInvalida_NoVerifica()
    {
        var plantilla = new Plantilla
        {
            UsuarioId = 1,
            CantidadMaximaJugadores = 0
        };

        Assert.False(plantilla.Verificar());
    }

    [Fact]
    public void Plantilla_FormacionTitular1442Valida_RetornaTrue()
    {
        var p = new Plantilla();
        // 1 Arquero (PosicionId=1)
        p.Titulares.Add(new PlantillaTitular { Jugador = new Jugador { PosicionId = 1, Cotizacion = 1000 } });
        // 4 Defensores (PosicionId=2)
        for (int i = 0; i < 4; i++) p.Titulares.Add(new PlantillaTitular { Jugador = new Jugador { PosicionId = 2, Cotizacion = 1000 } });
        // 4 Mediocampistas (PosicionId=3)
        for (int i = 0; i < 4; i++) p.Titulares.Add(new PlantillaTitular { Jugador = new Jugador { PosicionId = 3, Cotizacion = 1000 } });
        // 2 Delanteros (PosicionId=4)
        for (int i = 0; i < 2; i++) p.Titulares.Add(new PlantillaTitular { Jugador = new Jugador { PosicionId = 4, Cotizacion = 1000 } });

        Assert.True(p.EsValidaFormacionTitular());
    }

    [Fact]
    public void Plantilla_PuntajeFecha_SumaSoloTitularesDeEsaFecha()
    {
        var p = new Plantilla();
        var j1 = new Jugador();
        j1.Puntuaciones.Add(new Puntuacion { NroFecha = 1, Nota = 8.5m });
        var j2 = new Jugador();
        j2.Puntuaciones.Add(new Puntuacion { NroFecha = 1, Nota = 7.5m });

        p.Titulares.Add(new PlantillaTitular { Jugador = j1 });
        p.Titulares.Add(new PlantillaTitular { Jugador = j2 });

        Assert.Equal(16.0m, p.PuntajeFecha(1));
    }
}
