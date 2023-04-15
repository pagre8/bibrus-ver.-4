using BibrusServer.GraphQL.Types;
using BibrusServer.Models;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace BibrusServer.GraphQL
{
    public class BibrusQuery: ObjectGraphType
    {
        public BibrusQuery(BibrusDbContext _context) {

            Field<ListGraphType<StudentType>>(
                "StudentId",
                resolve: context => _context.Students.ToList()
                );

        }

    }
}
