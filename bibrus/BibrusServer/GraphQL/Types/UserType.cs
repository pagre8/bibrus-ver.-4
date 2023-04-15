using BibrusServer.Models;
using GraphQL.Types;

namespace BibrusServer.GraphQL.Types
{
    public class UserType : ObjectGraphType<User>
    {
        public UserType()
        {
            Name = "User";

            Field(x => x.Id, type: typeof(IdGraphType)).Description("The ID of the user.");
            Field(x => x.FirstName).Description("The first name of the user.");
            Field(x => x.MiddleName, nullable: true).Description("The middle name of the user.");
            Field(x => x.LastName).Description("The last name of the user.");
            Field(x => x.Email).Description("The email address of the user.");
            Field(x => x.Type).Description("The type of the user (e.g. 'student' or 'teacher').");

            Field<AddressType>("address", resolve: context => context.Source.Address);
            Field<CredentialType>("credentials", resolve: context => context.Source.Credentials);

            Field<ListGraphType<EmployeeType>>("employees",
                resolve: context =>
                {
                    // Return the employees associated with this user
                    return context.Source.Employees;
                });

            Field<ListGraphType<StudentType>>("studentParentUsers",
                resolve: context =>
                {
                    // Return the students associated with this user as a parent
                    return context.Source.StudentParentUsers;
                });

            Field<ListGraphType<StudentType>>("studentUsers",
                resolve: context =>
                {
                    // Return the students associated with this user
                    return context.Source.StudentUsers;
                });
        }
    }
}
