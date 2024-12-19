using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BankAccounts_DotnetCore3._1version.Models
{
    public class Bank
    {
        [Key]
        public int BankID { get; set; }
        public string BankName { get; set; }
    }
}
