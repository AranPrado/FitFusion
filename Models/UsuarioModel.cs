using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitFusion.Models
{
    public class UsuarioModel
    {
        public int UserID { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public float Peso { get; set; }

        public int Idade { get; set; }

        public float Altura { get; set; }

        //Relacionamentos

        //Usuario para treino

        
    }
}