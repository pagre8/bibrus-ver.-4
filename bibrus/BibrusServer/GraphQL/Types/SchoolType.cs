using BibrusServer.Models;
using GraphQL.Types;

namespace BibrusServer.GraphQL.Types
{
    public class SchoolType : ObjectGraphType<School>
    {
        public SchoolType()
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("The ID of the school.");
            Field(x => x.Name, nullable: true).Description("The name of the school.");
            Field<AddressType>("address", resolve: context => context.Source.Address,description:"The address of the school.");
        }
    }

}
