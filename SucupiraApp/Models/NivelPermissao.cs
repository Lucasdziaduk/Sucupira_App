using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SucupiraApp.Models
{
    public enum NivelPermissao //criando alguns niveis de permissao  
    {
        Usuario = 0, //0 = apenas visualizar,
        Professor = 1,//1 = criar, publicar, editar, excluir os seua trabalhos,
        Administrador = 2,//2 = editar e excluir trbalhos dos outros e desativar um professor,
        AdministradorSenior = 3,//3 = pode alterar o NivelPermissao de outros abaixo,
        Builder = 4//4 = ainda nao pensei
    }
}
