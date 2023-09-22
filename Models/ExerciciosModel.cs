using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitFusion.Models
{
    public class ExerciciosModel
    {
        public int ExercicioID { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public double Peso { get; set; }

        public int Repeticoes { get; set; }

        public int Descanso { get; set; }

        public int Series { get; set; }

        public bool Biset { get; set; }

        public bool Drop { get; set; }

        //Relacionamento entre tabelas

        //Exercicio para treino

    }
}