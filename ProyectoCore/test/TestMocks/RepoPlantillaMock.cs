namespace Mocks
{
    public class RepoPlantillaMock
    {
        private readonly List<Plantilla> _plantillas = new List<Plantilla>
        {
            new Plantilla
            {
                Id = 1,
                Nombre = "Equipo primal",
                UsuarioId = 2,
                FechaCreacion = DateTime.Now.AddDays(-5),
                Detalles = new List<DetallePlantilla>
                {
                    new DetallePlantilla { Id = 1, PlantillaId = 1, FutbolistaId = 1, EsTitular = true },
                    new DetallePlantilla { Id = 2, PlantillaId = 1, FutbolistaId = 2, EsTitular = true },
                    new DetallePlantilla { Id = 3, PlantillaId = 1, FutbolistaId = 3, EsTitular = false }
                }
            }
        };

        public List<Plantilla> ObtenerTodas()
        {
            return _plantillas;
        }

        public List<Plantilla> ObtenerPorUsuarioId(int usuarioId)
        {
            return _plantillas.Where(p => p.UsuarioId == usuarioId).ToList();
        }

        public Plantilla? ObtenerPorId(int id)
        {
            return _plantillas.FirstOrDefault(p => p.Id == id);
        }

        public bool Agregar(Plantilla plantilla)
        {
            plantilla.Id = _plantillas.Count > 0 ? _plantillas.Max(p => p.Id) + 1 : 1;
            _plantillas.Add(plantilla);
            return true;
        }

        public bool Modificar(Plantilla plantilla)
        {
            var existente = ObtenerPorId(plantilla.Id);
            if (existente == null) return false;

            existente.Nombre = plantilla.Nombre;
            existente.UsuarioId = plantilla.UsuarioId;
            existente.Detalles = plantilla.Detalles;
            return true;
        }

        public bool Eliminar(int id)
        {
            var existente = ObtenerPorId(id);
            if (existente == null) return false;

            return _plantillas.Remove(existente);
        }
    }
}