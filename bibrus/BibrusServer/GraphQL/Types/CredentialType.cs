using BibrusServer.Models;
using GraphQL.Types;
using System.Runtime.ExceptionServices;

namespace BibrusServer.GraphQL.Types
{
    public class CredentialType : ObjectGraphType<Credential>
    {
        public CredentialType()
        {
            Name = "Credential";

            Field(x => x.Id, type: typeof(IdGraphType)).Description("The ID of the credential.");
            Field(x => x.Login, nullable: true).Description("The login associated with the credential.");
            Field(x => x.Password, nullable: true).Description("The password associated with the credential.");
            Field<ListGraphType<UserType>>("users", resolve: context => context.Source.Users, description: "The list of users associated with the credential.");
        }
    }
}
