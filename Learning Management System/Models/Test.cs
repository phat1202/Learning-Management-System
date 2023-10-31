//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace Learning_Management_System.Models
//{
//    public class Test 
//    {
//        [Key]
//        public int? TestId { get; set; }
//        [ForeignKey(nameof(Course.CourseId))]
//        public int? CourseId { get; set; }
//        public DateTime? DateExam { get; set; }
//        public int? TestMinute { get; set; }
//    }
//    public class TestScore
//    {
//        [Key]
//        public int? ScoreId { get; set; }
//        [ForeignKey(nameof(Test.TestId))]
//        public int? TestId { get; set; }
//        [ForeignKey(nameof(ApplicationUser.Id))]
//        public string? StudentId { get; set; }
//        public float? Score { get; set; }
//    }
//    public class TestBase
//    {
//    }
//}
