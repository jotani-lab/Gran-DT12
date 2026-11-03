public **List<string> Validar(Plantilla plantilla)**
{
    var errores = new List<string>();

    if (plantilla is null)
    {
        errores.Add("La plantilla es obligatoria.");
        return errores;
    }

    if (plantilla.UsuarioId <= 0)
        errores.Add("El usuario no es válido.");

    if (string.IsNullOrWhiteSpace(plantilla.Nombre))
        errores.Add("El nombre es obligatorio.");

    if (plantilla.PresupuestoMaximo <= 0 ||
        plantilla.PresupuestoMaximo > 100000000.00m)
        errores.Add("El presupuesto debe ser mayor que cero y no superar $100.000.000.");

    if (plantilla.CantidadMaximaJugadores != 20)
        errores.Add("La cantidad máxima de jugadores debe ser 20.");

    return errores;
}