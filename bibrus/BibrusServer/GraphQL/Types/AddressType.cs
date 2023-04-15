using BibrusServer.Models;
using GraphQL.Types;
namespace BibrusServer.GraphQL.Types
{
    public class AddressType: ObjectGraphType<Address>
    {
        public AddressType()
        {
            Name = "Address";

            Field(x => x.Id, type: typeof(IdGraphType)).Description("The ID of the address.");
            Field(x => x.City, nullable: true).Description("The city where the address is located.");
            Field(x => x.Road, nullable: true).Description("The name of the road where the address is located.");
            Field(x => x.HomeNumber, nullable: true).Description("The house number of the address.");
            Field(x => x.Voivoidship, nullable: true).Description("The voivodeship where the address is located.");
            Field(x => x.PostalCode, nullable: true).Description("The postal code of the address.");
            Field(x => x.PhoneNumber, nullable: true).Description("The phone number associated with the address.");
            Field<ListGraphType<SchoolType>>("schools", resolve: context => context.Source.Schools, description: "The list of schools associated with this address.");
            Field<ListGraphType<UserType>>("users", resolve: context => context.Source.Users, description: "The list of users associated with this address.");
        }
    }
}
