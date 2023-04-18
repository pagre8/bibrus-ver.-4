using BibrusServer.Models;
using GraphQL.Language.AST;
using GraphQL.Types;
namespace BibrusServer.GraphQL.Types
{
    public class ClassValues
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public class ClassesType: ObjectGraphType<ClassValues>
    {
        public ClassesType()
        {
            Field(x => x.Id);
            Field(x=>x.Name);
        }
    }
}
