using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BankAccounts_DotnetCore3._1version.Models
{
    public class BankAccount
    {
        //[Key]
        public int BankAccountID { get; set; }
        public string AccountNumber { get; set; }
        public string AccountHolder { get; set; }
        public int BankID { get; set; }
        public string IFSC { get; set; }
    }
}
