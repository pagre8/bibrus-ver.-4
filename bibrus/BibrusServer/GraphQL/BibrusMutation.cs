using BibrusServer.GraphQL.Inputs;
using BibrusServer.GraphQL.Types;
using BibrusServer.Models;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Components.Forms;
using Serilog.Debugging;
using System.Runtime.CompilerServices;
using System.Threading.RateLimiting;

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

                    var User = _context.Users.Single(u => u.CredentialsId == credential.Id);

                    return User.Id;
    
                }
            );

            Field<ListGraphType<StringGraphType>>(
                "GetName",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "Id" }),
                resolve: context =>
                {
                    var UId = context.GetArgument<int>("id");
                    var User = _context.Users.Find(UId);
                    List<string> values = new List<string>
                    {
                        User.FirstName,
                        User.Type
                    };
                    return values;
                }
                );

            Field<ListGraphType<ClassesType>>(
                "GetClasses",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }),
                resolve: context =>
                {
                    var Uid = context.GetArgument<int>("id");
                    var employee = _context.Employees.Single(a => a.UserId == Uid);

                    var subjects = _context.Subjects.Where(s => s.EmployeeId == employee.Id).ToList();

                    List<Class> classes = new List<Class>();
                    List<ClassValues> values = new List<ClassValues>();

                    foreach(var subject in subjects)
                    {
                        classes = _context.Classes.Where(c => c.Id == subject.Id).ToList();
                    }
                    
                    foreach(var c in classes)
                    {
                        ClassValues tmp = new ClassValues();
                        tmp.Id = c.Id;
                        tmp.Name = c.Name;
                        values.Add(tmp);
                    }
                    return values;
                }
            );

            Field<InfoType>(
                "GetData",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name="id"}),
                resolve: context =>
                {
                    Info info = new Info();
                    var Uid = context.GetArgument<int>("id");
                    var User = _context.Users.Find(Uid);
                    info.FirstName = User.FirstName;
                    info.LastName = User.LastName;
                    
                    var student = _context.Students.Single(s => s.UserId == Uid);
                    var _class = _context.Classes.Find(student.ClassId);
                    info.Class = _class.Name;
                    var address = _context.Addresses.Find(User.AddressId);
                    info.City = address.City;
                    info.Road = address.Road;
                    info.BuildingNumber = address.HomeNumber;

                    var school = _context.Schools.First();
                    info.SchoolName = school.Name;

                    var schooladdress = _context.Addresses.Find(school.AddressId);
                    info.SchoolPhone = schooladdress.PhoneNumber;
                    info.SchoolAddress = schooladdress.City + " " + schooladdress.Road + " " + schooladdress.HomeNumber;

                    return info;
                }
                );

            Field<StringGraphType>(
                "UpdatePassword",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name="id"},new QueryArgument<NonNullGraphType<StringGraphType>> { Name="password"}),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    var updatedPassword = context.GetArgument<string>("password");
                    var user = _context.Users.Find(id);
                    var credentials = _context.Credentials.Find(user.CredentialsId);
                    credentials.Password = updatedPassword;
                    _context.SaveChanges();
                    return "Updated Successfully";
                }
                );

            Field<BooleanGraphType>(
                "CheckLogin",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name="login"}),
                resolve: context =>
                {
                    var login = context.GetArgument<string>("login");
                    return _context.Credentials.Any(c => c.Login == login);
                }
                );

            Field<StringGraphType>(
                "AddStudent",
                arguments: new QueryArguments
                (
            new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "firstName" },
            new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "lastName" },
            new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "email" },
            new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "phone" },
            new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "login" },
            new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "password" },
            new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "pesel" },
            new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "pFirstName" },
            new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "pLastName" },
            new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "pEmail" },
            new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "ppesel" },
            new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "pPhone" },
            new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "pLogin" },
            new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "pPassword" },
            new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "city" },
            new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "road" },
            new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "homeNumber" },
            new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "voivoidship" },
            new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "postalCode" },
            new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "pCity" },
            new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "pRoad" },
            new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "pHomeNumber" },
            new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "pVoivoidship" },
            new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "pPostalCode" }
                ),
                resolve: context =>
                {
                    var firstName = context.GetArgument<string>("firstName");
                    var lastName = context.GetArgument<string>("lastName");
                    var email = context.GetArgument<string>("email");
                    var phone = context.GetArgument<string>("phone");
                    var login = context.GetArgument<string>("login");
                    var password = context.GetArgument<string>("password");
                    var pesel = context.GetArgument<string>("pesel");
                    var pFirstName = context.GetArgument<string>("pFirstName");
                    var pLastName = context.GetArgument<string>("pLastName");
                    var pEmail = context.GetArgument<string>("pEmail");
                    var ppesel = context.GetArgument<string>("ppesel");
                    var pPhone = context.GetArgument<string>("pPhone");
                    var pLogin = context.GetArgument<string>("pLogin");
                    var pPassword = context.GetArgument<string>("pPassword");
                    var city = context.GetArgument<string>("city");
                    var road = context.GetArgument<string>("road");
                    var homeNumber = context.GetArgument<string>("homeNumber");
                    var voivoidship = context.GetArgument<string>("voivoidship");
                    var postalCode = context.GetArgument<string>("postalCode");
                    var pCity = context.GetArgument<string>("pCity");
                    var pRoad = context.GetArgument<string>("pRoad");
                    var pHomeNumber = context.GetArgument<string>("pHomeNumber");
                    var pVoivoidship = context.GetArgument<string>("pVoivoidship");
                    var pPostalCode = context.GetArgument<string>("pPostalCode");

                    var address = new Address
                    {
                        City = city,
                        Road = road,
                        HomeNumber = homeNumber,
                        Voivoidship = voivoidship,
                        PostalCode = postalCode
                    };

                    var Paddress = new Address
                    {
                        City = pCity,
                        Road = pRoad,
                        HomeNumber = pHomeNumber,
                        Voivoidship = pVoivoidship,
                        PostalCode = pPostalCode
                    };

                    _context.Addresses.Add(address);
                    _context.SaveChanges();

                    _context.Addresses.Add(Paddress);
                    _context.SaveChanges()
                    ;
                    var Credentials = new Credential
                    {
                        Login = login,
                        Password = password
                    };

                    var PCredentials = new Credential
                    {
                        Login = pLogin,
                        Password = pPassword
                    };

                    _context.Credentials.Add(PCredentials);
                    _context.SaveChanges();

                    _context.Credentials.Add(Credentials);
                    _context.SaveChanges();

                    var User = new User
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Email = email,
                        Pesel = pesel,
                        Address = address,
                        Type = "ucz"  
                    };
                    
                    _context.Users.Add(User);
                    _context.SaveChanges();

                    var PUser = new User
                    {
                        FirstName = pFirstName,
                        LastName = pLastName,
                        Email = pEmail,
                        Pesel = ppesel,
                        Address = Paddress,
                        Type = "rod"
                    };

                    _context.Users.Add(PUser);
                    _context.SaveChanges();

                    var Student = new Student
                    {
                        User=User,
                        ParentUser = PUser,
                    };

                    _context.Students.Add(Student);

                    _context.SaveChanges();

                    return "Added Successfully";
                }
                );

            Field<StringGraphType>(
                "AddGrade",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "studentId"},
                    new QueryArgument<NonNullGraphType<DateGraphType>> { Name="date"},
                    new QueryArgument<NonNullGraphType<IntGraphType>> {  Name="grade"},
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name="weight"},
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name="comment"},
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name="categorty"},
                    new QueryArgument<NonNullGraphType<BooleanGraphType>> { Name="avg"},
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name="userid"},
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name="semester"}
                    ),
                resolve: context=>
                {
                    var studentId = context.GetArgument<int>( "studentId" );
                    var date = context.GetArgument<DateTime>( "date" );
                    var _grade = context.GetArgument<int>("grade");
                    var weight = context.GetArgument<int>("weight");
                    var comment = context.GetArgument<string>("comment");
                    var category = context.GetArgument<string>("category");
                    var avg = context.GetArgument<bool>("avg");
                    var userId = context.GetArgument<int>("userid");
                    var semester = context.GetArgument<int>("semester");

                    Console.WriteLine(studentId);

                    var employee = _context.Employees.Single(e=>e.UserId==userId);

                    var subject = _context.Subjects.Single(s => s.EmployeeId == employee.Id);

                    var student = _context.Students.Find(studentId);

                    var grade = new Grade
                    {
                        Student = student,
                        Subject = subject,
                        Value = _grade,
                        Weight = weight,
                        Date = date,
                        Comment = comment,
                        Category = category,
                        CountToAvg = avg,
                        Semester = semester
                    };

                    _context.Grades.Add(grade);

                    _context.SaveChanges();

                    return "Added Successfully";
                }   
                );

            Field<ListGraphType<StudentsNamesType>>(
                "GetStudentsName",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    List<Student> tmp =  _context.Students.Where(s => s.ClassId == id).ToList();

                    List<StudentsName> values = new List<StudentsName>();

                    foreach ( var s in tmp)
                    {
                        var user = _context.Users.Find(s.UserId);
                        StudentsName studentName = new StudentsName();
                        studentName.Id = s.Id;
                        studentName.Name = user.FirstName +" "+ user.LastName;
                        values.Add(studentName);
                    }
                    return values;
                }
                );

            Field<ListGraphType<StudentAttendancetype>>(
                "getAttendance",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name="id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    var attendance = _context.Attandances.Where(x => x.StudentId == id).ToList();
                    List<StudentAttendance> studentAttendances = new List<StudentAttendance>();
                    foreach(var tmp in attendance)
                    {
                        var subject = _context.Subjects.Find(tmp.SubjectId);
                        var employee = _context.Employees.Find(tmp.EmployeeId);

                        var user = _context.Users.Find(employee.UserId);

                        StudentAttendance _studentattendance = new StudentAttendance
                        {
                            date = tmp.Date,
                            hour = tmp.Hour,
                            subject = subject.Name,
                            value = tmp.Value,
                            issuer = user.FirstName + " " + user.LastName
                        };
                        studentAttendances.Add(_studentattendance);
                    }
                    return studentAttendances;
                }
                );

            Field<StringGraphType>(
                "addAttendance",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<DateGraphType>> { Name = "date" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "studentid" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "subject" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "value" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "hour" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "userid" }
                ),
                resolve: context =>
                {
                    var date = context.GetArgument<DateTime>("date");

                    var studentId = context.GetArgument<int>("studentid");
                    var student = _context.Students.Find(studentId);

                    var subjectName = context.GetArgument<string>("subject");
                    var subject = _context.Subjects.Single(s => s.Name == subjectName);

                    var value = context.GetArgument<string>("value");
                    var hour = context.GetArgument<int>("hour");

                    var userid = context.GetArgument<int>("userid");
                    var user = _context.Users.Find(userid);
                    var employee = _context.Employees.Single(e => e.UserId == userid);
                    Attandance attendance = new Attandance()
                    {
                        Date = date,
                        Value = value,
                        Student = student,
                        Subject = subject,
                        Hour = hour,
                        Employee = employee
                    };

                    _context.Attandances.Add(attendance);
                    _context.SaveChanges();
                    return "Added Successfully";
                }
                );


            Field<ListGraphType<StudentGradeType>>(
                "getGrades",
                arguments:
                new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    var grades = _context.Grades.Where(g => g.StudentId == id).ToList();
                    List<StudentGrade> studentGrades = new List<StudentGrade>();
                    foreach (var grade in grades)
                    {
                        StudentGrade grade1 = new StudentGrade()
                        {
                            avg = grade.CountToAvg,
                            category = grade.Category,
                            semester = grade.Semester,
                            value = grade.Value,
                            comment = grade.Comment,
                            date = grade.Date,
                            weight = grade.Weight
                        };
                        studentGrades.Add(grade1);
                    }
                    return studentGrades;
                });

            Field<StringGraphType>(
                "addClass",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "year" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "subjectId" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "employeeId" },
                    new QueryArgument<NonNullGraphType<ListGraphType<IntGraphType>>> { Name = "studentid" }
                    ),
                resolve: context =>
                {
                    var name = context.GetArgument<string>("name");
                    var year = context.GetArgument<string>("year");
                    var employeeId = context.GetArgument<int>("employeeId");
                    var employee = _context.Employees.Find(employeeId);

                    var subjectId = context.GetArgument<int>("subjectId");
                    var subject = _context.Subjects.Find(subjectId);

                    var studentsids = context.GetArgument<List<int>>("studentid");
                    var students = new List<Student>();
                    foreach (var studentid in studentsids)
                    {
                        students.Add(_context.Students.Find(studentid));
                    }



                    Class _class = new Class()
                    {
                        Employee = employee,
                        Name = name,
                        Subject = subject,
                        Year = year,
                        Students = students
                    };

                    _context.Classes.Add(_class);
                    _context.SaveChanges();

                   // var classid = _context.Classes.Single(c => c.Employee == employee && c.Year == year && c.EmployeeId == employeeId).Id;

                    //_context.SaveChanges();

                    return "added Successfully";
                }

                );
        } 

    }
}
