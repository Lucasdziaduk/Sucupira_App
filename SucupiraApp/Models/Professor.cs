using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SucupiraApp.Models
{
    public  class Professor
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty;
        public string LattesUrl {get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
        public NivelPermissao NivelPermissao { get; set; } = NivelPermissao.Professor;
    }
}

