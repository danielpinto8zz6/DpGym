using System.ComponentModel.DataAnnotations.Schema;

namespace DpGym.Domain;

public class Member
{
    [Column("MemberId")]
    public int Id { get; set; }
}