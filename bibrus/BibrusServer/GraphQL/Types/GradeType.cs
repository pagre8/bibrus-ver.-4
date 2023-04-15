using BibrusServer.Models;
using GraphQL.Types;

namespace BibrusServer.GraphQL.Types
{
    public class GradeType : ObjectGraphType<Grade>
    {
        public GradeType()
        {
            Field(x => x.Id);
            Field(x => x.StudentId, nullable: true);
            Field(x => x.Subjectid, nullable: true);
            Field(x => x.Value, nullable: true);
            Field(x => x.Weight, nullable: true);
            Field(x => x.Date, nullable: true);
            Field(x => x.Comment, nullable: true);
            Field(x => x.Category, nullable: true);
            Field(x => x.CountToAvg, nullable: true);
            Field(x => x.Semester, nullable: true);
            Field<StudentType>(
                "student",
                resolve: context => context.Source.Student
            );
            Field<SubjectType>(
                "subject",
                resolve: context => context.Source.Subject
            );
        }
    }
}
