using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.Contracts;

namespace Learning_Management_System.Models
{
    public class Ttttt
    {
        [PersonalData]
        public virtual IKey? UserId { get; set; }
    }
}
