using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public static class Seguridad
    {
        public static bool sessionActiva(object user)
        {
            Registrar registrar = user != null ? (Registrar)user : null;
            if (registrar != null && registrar.Id != 0)
                return true;
            else
                return false;
        }
        public static bool esAdmin(object user)
        {
            Registrar registrar = user != null ? (Registrar)user : null;
            return registrar != null ? registrar.Admin : false;
        }
    }
}
