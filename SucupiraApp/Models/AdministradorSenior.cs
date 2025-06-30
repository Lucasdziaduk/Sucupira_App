using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SucupiraApp.Models
{
    public class AdministradorSenior : Administrador
    {
        public AdministradorSenior()
        {
            NivelPermissao = NivelPermissao.AdministradorSenior;
        }
    }
}
