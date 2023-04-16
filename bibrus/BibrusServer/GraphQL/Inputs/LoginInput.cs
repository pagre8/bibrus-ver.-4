using BibrusServer.Models;
using GraphQL.Types;
namespace BibrusServer.GraphQL.Inputs
{
    public class LoginInput:InputObjectGraphType<Credential>
    {
        public LoginInput() 
        {
        Name = "Credentials";
            Field<NonNullGraphType<StringGraphType>>("login");
            Field<NonNullGraphType<StringGraphType>>("password");
        }
    }
}
