using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Registrar
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string UrlImagenPerfil { get; set; }
        public bool Admin {  get; set; }

    }
}
