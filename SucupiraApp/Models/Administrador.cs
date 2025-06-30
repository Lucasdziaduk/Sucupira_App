using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SucupiraApp.Models
{
    public class Administrador : Professor
    {
        public Administrador ()
        {
            NivelPermissao = NivelPermissao.Administrador;
        }
    }
}
