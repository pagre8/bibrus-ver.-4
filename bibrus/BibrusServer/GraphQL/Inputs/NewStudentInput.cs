using GraphQL.Types;

namespace BibrusServer.GraphQL.Inputs
{
    public class NewStudent
    {
        public string FirstName;
        public string LastName;
        public string Email;
        public string Nr;
        public string Phone;
        public string login;
        public string password;
        public string pesel;
        public string PFirstName;
        public string PLastName;
        public string PEmail;
        public string Ppesel;
        public string Pphone;
        public string PLogin;
        public string PPassword;
        public string City;
        public string Road;
        public string HomeNumber;
        public string Voivoidship;
        public string PostalCode;
        public string PCity;
        public string PRoad;
        public string PHomeNumber;
        public string PVoivoidship;
        public string PPostalCode;
    }

    public class NewStudentInput : InputObjectGraphType<NewStudent>
    {
        public NewStudentInput()
        {
            Name = "NewStudentInput";
            Field<NonNullGraphType<StringGraphType>>("firstName");
            Field<NonNullGraphType<StringGraphType>>("lastName");
            Field<NonNullGraphType<StringGraphType>>("email");
            Field<NonNullGraphType<StringGraphType>>("nr");
            Field<NonNullGraphType<StringGraphType>>("phone");
            Field<NonNullGraphType<StringGraphType>>("login");
            Field<NonNullGraphType<StringGraphType>>("password");
            Field<NonNullGraphType<StringGraphType>>("pesel");
            Field<NonNullGraphType<StringGraphType>>("pFirstName");
            Field<NonNullGraphType<StringGraphType>>("pLastName");
            Field<NonNullGraphType<StringGraphType>>("pEmail");
            Field<NonNullGraphType<StringGraphType>>("ppesel");
            Field<NonNullGraphType<StringGraphType>>("pPhone");
            Field<NonNullGraphType<StringGraphType>>("pLogin");
            Field<NonNullGraphType<StringGraphType>>("pPassword");
            Field<NonNullGraphType<StringGraphType>>("city");
            Field<NonNullGraphType<StringGraphType>>("road");
            Field<NonNullGraphType<StringGraphType>>("homeNumber");
            Field<NonNullGraphType<StringGraphType>>("voivoidship");
            Field<NonNullGraphType<StringGraphType>>("postalCode");
            Field<NonNullGraphType<StringGraphType>>("pCity");
            Field<NonNullGraphType<StringGraphType>>("pRoad");
            Field<NonNullGraphType<StringGraphType>>("pHomeNumber");
            Field<NonNullGraphType<StringGraphType>>("pVoivoidship");
            Field<NonNullGraphType<StringGraphType>>("pPostalCode");
        }
    }

}
