using BibrusServer.Models;
using GraphQL.Types;
namespace BibrusServer.GraphQL
{
    public class BibrusSchema : Schema, ISchema
    {
        public BibrusSchema(BibrusDbContext context) : base()
        {
        Query = new BibrusQuery(context);

        Mutation = new BibrusMutation(context);
        }
    }
}
