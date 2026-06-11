using FinalExam.Models.Base;

namespace FinalExam.Models
{
    public class Position : BaseEntity
    {
        public string Name { get; set; }
        public List<Member> Members { get; set; }
    }
}
