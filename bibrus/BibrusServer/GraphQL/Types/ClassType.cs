using BibrusServer.Models;
using GraphQL.Types;

namespace BibrusServer.GraphQL.Types
{
    public class ClassType : ObjectGraphType<Class>
    {
        public ClassType()
        {
            Name = "Class";

            Field(x => x.Id, type: typeof(IdGraphType)).Description("The ID of the class.");
            Field(x => x.Name, nullable: true).Description("The name of the class.");
            Field(x => x.Year, nullable: true).Description("The year of the class.");
            Field(x => x.SubjectId, nullable: true).Description("The ID of the subject related to the class.");
            Field(x => x.EmployeeId, nullable: true).Description("The ID of the employee related to the class.");
            Field<EmployeeType>("employee", resolve: context => context.Source.Employee, description: "The employee associated with the class.");
            Field<SubjectType>("subject", resolve: context => context.Source.Subject, description: "The subject associated with the class.");
            Field<ListGraphType<StudentType>>("students", resolve: context => context.Source.Students, description: "The list of students in the class.");
        }
    }
}
