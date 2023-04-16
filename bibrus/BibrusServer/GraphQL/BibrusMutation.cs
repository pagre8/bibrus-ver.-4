using BibrusServer.GraphQL.Inputs;
using BibrusServer.Models;
using GraphQL;
using GraphQL.Types;
using Serilog.Debugging;

namespace BibrusServer.GraphQL
{
    public class BibrusMutation: ObjectGraphType
    {
        public BibrusMutation(BibrusDbContext _context) 
        {
            Field<IntGraphType>(
                "Login",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "Login" }, new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "Password" }),
                resolve: context =>
                {
                    var login = context.GetArgument<string>("Login");
                    var password = context.GetArgument<string>("Password");
                    Credential credential;
                    try
                    {
                        credential = _context.Credentials.Single(a => a.Login == login && a.Password == password);
                    }
                    catch(Exception ex)
                    { 
                        Console.WriteLine(ex.Message);
                        return 0;
                    }
                    if(credential==null) 
                    {
                        return 0;
                    }

                    return credential.Id;
    
                }
            );

            Field<ListGraphType<StringGraphType>>(
                "GetName",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "Id" }),
                resolve: context =>
                {
                    var UId = context.GetArgument<int>("id");
                    var User = _context.Users.Find(UId);
                    List<string> values = new List<string>();
                    values.Add(User.FirstName);
                    values.Add(User.Type);
                    return values;
                }
                );
        } 

    }
}
