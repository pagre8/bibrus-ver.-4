using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Json;
using System.Linq;

namespace Bibrusik
{
    public partial class Form5  : Form
    {
        HttpClient Http = new HttpClient();
        int id;
        public Form5(int classId)
        {
            InitializeComponent();
            id = classId;
        }

        public class Student
        {
            public string name { get; set; }
            public int StudentId { get; set; }
        }

        List<Student> students { get; set; } = new List<Student>();

        void add(int i, string name)
        {
            ComboBox comboBox = new ComboBox();
            comboBox.FormattingEnabled = true;
            comboBox.Items.AddRange(new object[] {
            "ob",
            "nb",
            "w",
            "d",
            "sp",
            "zw"});
            comboBox.Text = "nb";
            comboBox.TabIndex = i;
            comboBox.Location = new System.Drawing.Point(172, 14);
            comboBox.Size = new System.Drawing.Size(46, 21);

            Label name_label = new Label();
            name_label.Location = new System.Drawing.Point(12, 14);
            name_label.Margin = new System.Windows.Forms.Padding(15);
            name_label.Size = new System.Drawing.Size(105, 20);
            name_label.Text = name;



            GroupBox groupBox = new GroupBox();
            groupBox.Controls.Add(name_label);
            groupBox.Controls.Add(comboBox);
            groupBox.Location = new System.Drawing.Point(12, 12 + 45 * i);
            groupBox.Size = new System.Drawing.Size(240, 45);

            Controls.Add(groupBox);
        }

        public async Task FetchSubjects()
        {
            var postBody = new
            {
                query =
                "query{getAllSubjects}"
            };
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
                    comboBox2.Items.Add(item.GetString());
                }
                comboBox2.Text = comboBox2.Items[0].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);

            };
        }

        public async Task FetchData()
        {
            var postBody = new
            {
                query =
                "mutation{" +
                "   getStudentsName(" +
                $"       id: {id}" +
                "   ){" +
                "       id," +
                "       name" +
                "   }" +
                "}"
            };
            try
            {
                var response = await Http.PostAsJsonAsync("https://localhost:7151/graphql", postBody);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }

                var parsedResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

                parsedResponse = parsedResponse.GetProperty("data").GetProperty("getStudentsName");

                foreach (var item in parsedResponse.EnumerateArray())
                {
                    Student student = new Student
                    {
                        StudentId = item.GetProperty("id").GetInt32(),
                        name = item.GetProperty("name").GetString()
                    };
                    students.Add(student);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);

            };
        }
        public class Attendance
        {
            public DateTime date;
            public int hour;
            public int studentid;
            public int userid;
            public string value;
            public string subject;
        }

        private async void Form5_Load(object sender, EventArgs e)
        {
            await FetchSubjects();
            await FetchData();
            int i = 0;
            foreach (var student in students)
            {
                add(i, student.name);
                i++;
            }
            comboBox2.Text = comboBox2.Items[0].ToString();
        }
        bool check = true;
        public async Task SendData(Attendance attendance)
        {
            var postBody = new
            {
                query =
                "mutation{" +
                "   addAttendance(" +
                $"       date: \"{attendance.date.ToString("yyyy-MM-dd")}\"," +
                $"       hour: {attendance.hour}," +
                $"       studentid: {attendance.studentid}," +
                $"       subject: \"{attendance.subject}\"," +
                $"       userid: {attendance.userid}," +
                $"       value: \"{attendance.value}\"," +
                $"   )" +
                "}"
            };   
            try
            {
                var response = await Http.PostAsJsonAsync("https://localhost:7151/graphql", postBody);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                check = false;
            };
            
        }

        private async void add_button_Click(object sender, EventArgs e)
        {
            Attendance attendance = new Attendance()
            {
                date = data.Value,
                hour = int.Parse(comboBox1.Text),
                userid = id,
                subject = comboBox2.Text, 
            };
            int i =0;
            foreach (GroupBox box in Controls.OfType<GroupBox>().ToList())
            {
                foreach (ComboBox combo in box.Controls.OfType<ComboBox>())
                {
                    attendance.studentid = students[i].StudentId;
                    attendance.value = combo.Text;
                    await SendData(attendance);
                    i++;
                }
            }
            if (check)
            {
                MessageBox.Show("Dodano pomyślnie", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
        }
    }
}
