using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace Bibrusik
{
    public partial class DodawanieKlasy : Form
    {
        HttpClient Http = new HttpClient();
        public DodawanieKlasy()
        {
            InitializeComponent();
        }

        public class StudentsName
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        List<StudentsName> Students = new List<StudentsName>();

        List<StudentsName> teachers = new List<StudentsName>();

        List<StudentsName> subjects = new List<StudentsName>();

        public class Class
        {
            public int employeeid;
            public string name;
            public int studentid;
            public int subjecyid;
            public string year;
        }

        public async Task SendData(Class _class, List<StudentsName> ids)
        {
            string _ids="";
            if (ids.Count == 1)
            {
                _ids = ids[0].Id.ToString();
            }
            else
            {
                foreach (var id in ids)
                {
                    _ids += id.Id + ",";
                }
            }
            var postBody = new
            {
                query =
                "mutation{" +
                "   addClass(" +
                $"       employeeId: {_class.employeeid}," +
                $"       name: \"{_class.name}\"," +
                $"       studentid: [{_ids}]," +
                $"       subjectId: {_class.subjecyid}," +
                $"       year: \"{_class.year}\"" +
                "   )" +
                "}"
            };
            //Console.WriteLine(postBody);
            try
            {
                var response = await Http.PostAsJsonAsync("https://localhost:7151/graphql", postBody);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }
                MessageBox.Show("Dodano pomyślnie", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);

            };

        }

        public async Task FetchTeachers()
        {
            var postBody = new
            {
                query =
                "query{getAllTeachers{id,name}}"
            };
            //Console.WriteLine(postBody);
            try
            {
                var response = await Http.PostAsJsonAsync("https://localhost:7151/graphql", postBody);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }

                var parsedResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

                parsedResponse = parsedResponse.GetProperty("data").GetProperty("getAllTeachers");
                foreach (var item in parsedResponse.EnumerateArray())
                {
                    StudentsName teacher = new StudentsName
                    {
                        Id = item.GetProperty("id").GetInt32(),
                        Name = item.GetProperty("name").GetString()
                    };
                    comboBox1.Items.Add(teacher.Name);
                    teachers.Add(teacher);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);

            };

        }

        public async Task FetchSubjects()
        {
            var postBody = new
            {
                query =
                "query{getAllSubjects{id,name}}"
            };
            //Console.WriteLine(postBody);
            try
            {
                var response = await Http.PostAsJsonAsync("https://localhost:7151/graphql", postBody);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }

                var parsedResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

                parsedResponse = parsedResponse.GetProperty("data").GetProperty("getAllSubjects");
                foreach (var item in parsedResponse.EnumerateArray())
                {
                    StudentsName subject = new StudentsName
                    {
                       Id = item.GetProperty("id").GetInt32(),
                       Name = item.GetProperty("name").GetString()
                    };
                    subjects.Add(subject);
                    checkedListBox2.Items.Add(subject.Name);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

        }
        public async Task FetchStudents()
        {
            var postBody = new
            {
                query =
                "query{getFreeStudentsNames{id,name}}"
            };
            //Console.WriteLine(postBody);
            try
            {
                var response = await Http.PostAsJsonAsync("https://localhost:7151/graphql", postBody);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }

                var parsedResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

                parsedResponse = parsedResponse.GetProperty("data").GetProperty("getFreeStudentsNames");
                foreach (var item in parsedResponse.EnumerateArray())
                {
                    StudentsName students1 = new StudentsName
                    {
                        Id = item.GetProperty("id").GetInt32(),
                        Name = item.GetProperty("name").GetString()
                    };
                    checkedListBox1.Items.Add(students1.Name);
                    Students.Add(students1);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);

            };

        }

       

        private async void DodawanieKlasy_Load(object sender, EventArgs e)
        {
            await FetchStudents();
            await FetchSubjects();
            await FetchTeachers();
        }


        private async void add_button1_Click(object sender, EventArgs e)
        {
            Class _class = new Class()
            {
                year = rokSzkolny.Text,
                name = textBox2.Text
            };

            StudentsName selectedNauczyciel = new StudentsName()
            {
                Name = comboBox1.Text
            };

            foreach(var nauczyciel in teachers)
            {
                if (nauczyciel.Name == selectedNauczyciel.Name)
                {
                    selectedNauczyciel.Id = nauczyciel.Id;
                }
            }

            _class.employeeid = selectedNauczyciel.Id;

            List<StudentsName> students = new List<StudentsName>();


            for(int i = 0; i < checkedListBox1.CheckedItems.Count; i++)
            {
                students.Add(Students[i]);
            }

            for (int i = 0; i < checkedListBox2.CheckedItems.Count; i++)
            {
                _class.subjecyid = subjects[i].Id;
            }

            await SendData(_class, students);

            Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
