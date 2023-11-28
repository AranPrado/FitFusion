using System.ComponentModel.DataAnnotations;

namespace FitFusion.DTOs.TreinosDTO
{
    public class CriarTreino
    {

        public CriarTreino()
        {
            DataCriacao = DateTime.Now;
        }

        public int TreinoID { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
        public DateTime DataCriacao { get; set; }

    }
}