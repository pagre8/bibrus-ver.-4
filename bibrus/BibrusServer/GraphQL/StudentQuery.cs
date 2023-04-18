using BibrusServer.GraphQL.Types;
using BibrusServer.Models;
using GraphQL.Introspection;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BibrusServer.GraphQL
{
    public class BibrusQuery: ObjectGraphType
    {

        public BibrusQuery(BibrusDbContext _context) {

            Field<ListGraphType<StudentType>>(
                "StudentId",
                resolve: context => _context.Students.ToList()
                );

            Field<ListGraphType<StudentsNamesType>>(
                "GetAllTeachers",
                resolve: context =>
                {
                    var Employees = _context.Employees.ToList();
                    List<StudentsName> names = new List<StudentsName>();
                    foreach (var employee in Employees)
                    {
                        var User = _context.Users.Find(employee.UserId);
                        StudentsName name = new StudentsName()
                        {
                            Id = employee.Id,
                            Name = User.FirstName + " " + User.LastName
                        };
                        names.Add(name);
                    }
                    return names;
                }
                );

            Field<ListGraphType<StudentsNamesType>>(
                "GetAllSubjects",
                resolve: context =>
                {
                    var subjects = _context.Subjects.ToList();
                    List<StudentsName> names = new List<StudentsName>();
                    foreach (var subject in subjects)
                    {
                        StudentsName name = new StudentsName()
                        {
                            Id = subject.Id,
                            Name = subject.Name
                        };
                        names.Add(name);
                    }
                    return names;
                }
                );

            Field<ListGraphType<StudentsNamesType>>(
                "GetFreeStudentsNames",
                resolve: context =>
                {
                    List<Student> students = _context.Students.Where(s => s.ClassId == null).ToList();
                    List<StudentsName> names = new List<StudentsName>();
                    foreach (Student student in students)
                    {
                        var user = _context.Users.Single(u => u.Id == student.UserId);
                        StudentsName studentsName = new StudentsName();
                        studentsName.Id = student.Id;
                        studentsName.Name = user.FirstName + " " + user.LastName;
                        names.Add(studentsName);
                    }
                    return names;
                }
                );

        }

    }
}
