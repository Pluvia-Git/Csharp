using Pluvia.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("T_PL_USUARIO")]
public class Usuario
{
    [Key]
    [Column("ID_USUARIO")]
    public int Id { get; set; }

    [ForeignKey("Endereco")]
    [Column("ID_ENDERECO")]
    public int EnderecoId { get; set; }

    [Column("ID_NOME")]
    public string Nome { get; set; }

    [Column("ID_EMAIL")]
    public string Email { get; set; }

    [Column("CD_CPF")]
    public string Cpf { get; set; }

    [Column("CD_SENHA")]
    public string Senha { get; set; }

    public Endereco Endereco { get; set; }

    public ICollection<Alerta> Alertas { get; set; }
}
