using BibrusServer.Models;
using GraphQL.Types;

namespace BibrusServer.GraphQL.Types
{
    public class SubjectType : ObjectGraphType<Subject>
    {
        public SubjectType()
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("The ID of the subject.");
            Field(x => x.Name, nullable: true).Description("The name of the subject.");
            Field(x => x.EmployeeId, nullable: true, type: typeof(IdGraphType)).Description("The ID of the employee who teaches the subject.");

            Field<ListGraphType<AttandanceType>>(
                "attandances",
                resolve: context => context.Source.Attandances,
                description: "The attandances of the subject."
            );

            Field<ListGraphType<ClassType>>(
                "classes",
                resolve: context => context.Source.Classes,
                description: "The classes of the subject."
            );

            Field<EmployeeType>(
                "employee",
                resolve: context => context.Source.Employee,
                description: "The employee who teaches the subject."
            );

            Field<ListGraphType<GradeType>>(
                "grades",
                resolve: context => context.Source.Grades,
                description: "The grades of the subject."
            );
        }
    }
}
