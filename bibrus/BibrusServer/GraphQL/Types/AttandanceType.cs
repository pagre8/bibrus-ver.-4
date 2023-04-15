using BibrusServer.Models;
using GraphQL.Types;

namespace BibrusServer.GraphQL.Types
{
    public class AttandanceType : ObjectGraphType<Attandance>
    {
        public AttandanceType()
        {
            Name = "Attandance";

            Field(x => x.Id, type: typeof(IdGraphType)).Description("The ID of the attendance record.");
            Field(x => x.SubjectId, nullable: true).Description("The ID of the subject related to the attendance record.");
            Field(x => x.StudentId, nullable: true).Description("The ID of the student related to the attendance record.");
            Field(x => x.Value, nullable: true).Description("The value of the attendance record.");
            Field(x => x.Date, nullable: true).Description("The date of the attendance record.");
            Field(x => x.Hour, nullable: true).Description("The hour of the attendance record.");
            Field(x => x.EmployeeId, nullable: true).Description("The ID of the employee related to the attendance record.");
            Field<EmployeeType>("employee", resolve: context => context.Source.Employee, description: "The employee associated with the attendance record.");
            Field<StudentType>("student", resolve: context => context.Source.Student, description: "The student associated with the attendance record.");
            Field<SubjectType>("subject", resolve: context => context.Source.Subject, description: "The subject associated with the attendance record.");
        }

    }
}
