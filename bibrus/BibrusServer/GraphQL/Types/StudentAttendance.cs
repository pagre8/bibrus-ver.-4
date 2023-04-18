using GraphQL.Types;

namespace BibrusServer.GraphQL.Types
{
    public class StudentAttendance
    {
        public string subject { get; set; }
        public string value { get; set; }
        public DateTime date { get; set; }
        public int hour { get; set; }
        public string issuer { get; set; }
    }
    
    public class StudentAttendancetype : ObjectGraphType<StudentAttendance>
    {
        public StudentAttendancetype()
        {
            Field(x => x.date);
            Field(x => x.subject);
            Field(x => x.value);
            Field(x => x.hour);
            Field(x => x.issuer);
        }
    }
}
