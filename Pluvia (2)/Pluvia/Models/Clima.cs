using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Pluvia.Models
{
    [Table("T_PL_CLIMA")]
    public class Clima
    {
        [Key]
        [Column("ID_CLIMA")]
        public int Id { get; set; }

        [ForeignKey("Endereco")]
        [Column("ID_ENDERECO")]
        public int EnderecoId { get; set; }

        [Column("DT_HORARIO")]
        public DateTime DataHorario { get; set; }

        [Column("DS_CONDICAO")]
        public string Condicao { get; set; }

        [Column("DS_DESCRICAO")]
        public string Descricao { get; set; }

        [Column("VL_TEMPERATURA")]
        public float Temperatura { get; set; }

        [Column("VL_PRESSAO")]
        public float Pressao { get; set; }

        [Column("VL_UMIDADE")]
        public float Umidade { get; set; }

        [Column("VL_VELOCIDADE_VENTO")]
        public float VelocidadeVento { get; set; }

        [Column("VL_NUVENS")]
        public float Nuvens { get; set; }

        [Column("FL_ESP32")]
        public float Esp32 { get; set; }

        public Endereco Endereco { get; set; }
    }
}