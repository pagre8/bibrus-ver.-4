using GraphQL.Types;

namespace BibrusServer.GraphQL.Types
{
    public class StudentGrade
    {
        public int value;
        public int weight;
        public DateTime date;
        public string comment;
        public string category;
        public bool avg;
        public int semester;
    }

    public class StudentGradeType:ObjectGraphType<StudentGrade>
    {
        public StudentGradeType()
        {
            Field(x => x.value);
            Field(x => x.weight);
            Field(x => x.date);
            Field(x => x.comment);
            Field(x => x.category);
            Field(x => x.avg);
            Field(x => x.semester);
        }
    }
}
