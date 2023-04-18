using GraphQL.Types;
namespace BibrusServer.GraphQL.Types
{
    public class Info
    {
        public string FirstName;
        public string LastName;
        public string Class;
        public string City;
        public string Road;
        public string BuildingNumber;
        public string SchoolName;
        public string SchoolPhone;
        public string SchoolAddress;
    }
    public class InfoType : ObjectGraphType<Info>
    {
        public InfoType()
        {
            Field(x => x.FirstName);
            Field(x => x.LastName);
            Field(x => x.Class);
            Field(x => x.City);
            Field(x => x.Road);
            Field(x => x.BuildingNumber);
            Field(x => x.SchoolName);
            Field(x => x.SchoolPhone);
            Field(x => x.SchoolAddress); 
        }
    }
}
