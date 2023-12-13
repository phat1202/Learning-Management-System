using System.ComponentModel;

namespace Learning_Management_System.Models
{
    public class BaseInfo
    {
        public BaseInfo()
        {
            UpdatedAt = DateTime.Now;
            CreatedAt = DateTime.Now;
        }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }

        [DefaultValue(true)]
        public bool IsActive { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
