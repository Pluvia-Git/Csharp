using Pluvia.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("T_PL_ENDERECO")]
public class Endereco
{
    [Key]
    [Column("ID_ENDERECO")]
    public int Id { get; set; }

    [Column("ID_CIDADE")]
    public string? Cidade { get; set; }

    [Column("ID_BAIRRO")]
    public string? Bairro { get; set; }

    [Column("CD_CEP")]
    public string? Cep { get; set; }

    [Column("SG_ESTADO")]
    public string? Estado { get; set; }

    [Column("DS_LOGRADOURO")]
    public string? Logradouro { get; set; }

    [Column("VL_LATITUDE")]
    public float Latitude { get; set; }

    [Column("VL_LONGITUDE")]
    public float Longitude { get; set; }

    public ICollection<Usuario> Usuarios { get; set; }
    public ICollection<Clima> Climas { get; set; }
    public ICollection<Alerta> Alertas { get; set; }
}