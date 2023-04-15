using BibrusServer.Models;
using GraphQL.Types;

namespace BibrusServer.GraphQL.Types
{
    public class StudentType : ObjectGraphType<Student>
    {
        public StudentType()
        {
            Name = "Student";
            Description = "A student in the school.";

            Field(x => x.Id, type: typeof(IdGraphType)).Description("The ID of the student.");
            Field(x => x.UserId, nullable: true, type: typeof(IdGraphType)).Description("The ID of the user associated with the student.");
            Field(x => x.ClassId, nullable: true, type: typeof(IdGraphType)).Description("The ID of the class the student belongs to.");
            Field(x => x.ParentUserId, nullable: true, type: typeof(IdGraphType)).Description("The ID of the user associated with the student's parent.");

            Field<ListGraphType<AttandanceType>>("attandances", resolve: context => context.Source.Attandances, description: "The attandances of the student.");
            Field<ListGraphType<GradeType>>("grades", resolve: context => context.Source.Grades, description: "The grades of the student.");
            Field<ClassType>("class", resolve: context => context.Source.Class, description: "The class the student belongs to.");
            Field<UserType>("parentUser", resolve: context => context.Source.ParentUser, description: "The user associated with the student's parent.");
            Field<UserType>("user", resolve: context => context.Source.User, description: "The user associated with the student.");
        }
    }

}
