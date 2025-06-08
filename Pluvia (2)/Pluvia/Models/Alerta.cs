
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

   
    namespace Pluvia.Models
{
     [Table("T_PL_ALERTA")]
    public class Alerta
    {
        [Key]
        [Column("ID_ALERTA")]
        public int Id { get; set; }

        [ForeignKey("Usuario")]
        [Column("ID_USUARIO")]
        public int UsuarioId { get; set; }

        [ForeignKey("Endereco")]
        [Column("ID_ENDERECO")]
        public int EnderecoId { get; set; }

        [Column("DS_DESCRICAO_ALERTA")]
        public string Descricao { get; set; }

        [Column("DT_HORARIO")]
        public DateTime DataHorario { get; set; }

        [Column("FL_STATUS")]
        public int Status { get; set; }

        public Usuario Usuario { get; set; }
        public Endereco Endereco { get; set; }
    }
}