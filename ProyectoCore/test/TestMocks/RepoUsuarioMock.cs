namespace Mocks
{
    public class RepoUsuarioMock
    {
        private readonly List<Usuario> _usuarios = new List<Usuario>
        {
            new Usuario 
            { 
                Id = 1, 
                Nombre = "Juan", 
                Apellido = "Rodriguez", 
                Email = "juan.rod@hotmail.com", 
                ContrasenaHash = "1234567890abcdef1234567890abcdef1234567890abcdef1234567890abcdef", 
                EsAdmin = true 
            },
            new Usuario 
            { 
                Id = 2, 
                Nombre = "Seba", 
                Apellido = "Cardina", 
                Email = "seba.card@grandt.com", 
                ContrasenaHash = "abcdef1234567890abcdef1234567890abcdef1234567890abcdef1234567890", 
                EsAdmin = false 
            }
        };

        public List<Usuario> ObtenerTodos()
        {
            return _usuarios;
        }

        public Usuario? ObtenerPorId(int id)
        {
            return _usuarios.FirstOrDefault(u => u.Id == id);
        }

        public Usuario? ObtenerPorEmail(string email)
        {
            return _usuarios.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public bool Agregar(Usuario usuario)
        {
            usuario.Id = _usuarios.Count > 0 ? _usuarios.Max(u => u.Id) + 1 : 1;
            _usuarios.Add(usuario);
            return true;
        }

        public bool Modificar(Usuario usuario)
        {
            var existente = ObtenerPorId(usuario.Id);
            if (existente == null) return false;

            existente.Nombre = usuario.Nombre;
            existente.Apellido = usuario.Apellido;
            existente.Email = usuario.Email;
            existente.ContrasenaHash = usuario.ContrasenaHash;
            existente.EsAdmin = usuario.EsAdmin;
            return true;
        }

        public bool Eliminar(int id)
        {
            var existente = ObtenerPorId(id);
            if (existente == null) return false;

            return _usuarios.Remove(existente);
        }
    }
}