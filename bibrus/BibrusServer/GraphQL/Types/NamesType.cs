using GraphQL.Types;
namespace BibrusServer.GraphQL.Types
{
    public class StudentsName
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class StudentsNamesType: ObjectGraphType<StudentsName>
    {
        public StudentsNamesType() 
        {
            Field(x => x.Id);
            Field(x => x.Name);
        }
    }
}
