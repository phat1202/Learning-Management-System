using System.ComponentModel;

namespace Learning_Management_System.Models
{
    public class BaseInfo
    {
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        [DefaultValue(true)]
        public bool IsActive { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
