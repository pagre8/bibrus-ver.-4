using BibrusServer.Models;
using GraphQL.Types;

namespace BibrusServer.GraphQL.Types
{
    public class EmployeeType : ObjectGraphType<Employee>
    {
        public EmployeeType()
        {
            Name = "Employee";

            Field(x => x.Id, type: typeof(IdGraphType)).Description("The ID of the employee.");
            Field(x => x.Role, nullable: true).Description("The role of the employee.");
            Field<ListGraphType<AttandanceType>>("attandances", resolve: context => context.Source.Attandances, description: "The list of attandances associated with the employee.");
            Field<ListGraphType<ClassType>>("classes", resolve: context => context.Source.Classes, description: "The list of classes associated with the employee.");
            Field<ListGraphType<SubjectType>>("subjects", resolve: context => context.Source.Subjects, description: "The list of subjects associated with the employee.");
            Field<UserType>("user", resolve: context => context.Source.User, description: "The user associated with the employee.");
        }
    }

}
