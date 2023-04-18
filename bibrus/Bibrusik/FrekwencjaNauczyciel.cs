using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Json;


namespace Bibrusik
{
    public partial class FrekwencjaNauczyciel : Form
    {
        HttpClient Http = new HttpClient();
        int id;
        public FrekwencjaNauczyciel(int _classid)
        {
            InitializeComponent();
            id = _classid;
        }

        public class Student
        {
            public string name { get; set; }
            public int StudentId { get; set; }
        }
        public class StudentAttendance
        {
            public string subject { get; set; }
            public string value { get; set; }
            public string date { get; set; }
            public int hour { get; set; }
            public string issuer { get; set; }
        }
        List<Student> students { get; set; } = new List<Student>();

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
        List<StudentAttendance> attendances = new List<StudentAttendance>();
        public async Task FetchAttendance(int id)
        {
        attendances.Clear();
            var postBody = new
            {
                query =
                "mutation{" +
                "   getAttendance(" +
                $"       id: {id}" +
                "   ){" +
                "       date," +
                "       hour," +
                "       issuer," +
                "       subject," +
                "       value" +
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

                parsedResponse = parsedResponse.GetProperty("data").GetProperty("getAttendance");

                foreach (var item in parsedResponse.EnumerateArray())
                {
                    StudentAttendance _attendance = new StudentAttendance
                    {
                        date = item.GetProperty("date").GetDateTime().ToString("dd.MM.yyyy"),
                        hour = item.GetProperty("hour").GetInt32(),
                        issuer = item.GetProperty("issuer").GetString(),
                        subject = item.GetProperty("subject").GetString(),
                        value = item.GetProperty("value").GetString()
                    };
                    attendances.Add(_attendance);
                }    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);

            };
           
        }

        async void add(int i, string name, int student_id)
        {
            
            FlowLayoutPanel flowLayoutPanel1 = new FlowLayoutPanel();
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.Location = new System.Drawing.Point(112, 24);
            flowLayoutPanel1.MaximumSize = new System.Drawing.Size(200, 0);
            flowLayoutPanel1.Size = new System.Drawing.Size(200, 0);

            Label name1 = new Label();
            name1.AutoSize = true;
            name1.MaximumSize = new System.Drawing.Size(105, 13);
            name1.Location = new System.Drawing.Point(6, 24);
            name1.Size = new System.Drawing.Size(105, 13);
            name1.Text = name;


            GroupBox groupBox1 = new GroupBox();
                await FetchAttendance(student_id);
                foreach (var attendance in attendances)
                {
                    Label label = new Label();
                    label.Text = attendance.value;
                    label.Size = new System.Drawing.Size(19, 19);
                    label.BackColor = System.Drawing.Color.SandyBrown;
                    ToolTip toolTip = new ToolTip();
                    toolTip.ReshowDelay = 500;
                    toolTip.ShowAlways = true;
                    flowLayoutPanel1.Controls.Add(label);
                    string text = "Rodzaj: " + attendance.value + "\nData: " + attendance.date + "\nPrzedmiot: " + attendance.subject + "\nGodzina lekcyjna: " + attendance.hour + "\nNauczyciel: " + attendance.issuer;
                    toolTip.SetToolTip(label, text);
                }
            attendances.Clear();
            

            groupBox1.AutoSize = true;
            groupBox1.BackColor = System.Drawing.Color.Transparent;
            groupBox1.Location = new System.Drawing.Point(12, 40 + 80 * i);
            groupBox1.Size = new System.Drawing.Size(410, 61);
            groupBox1.Controls.Add(flowLayoutPanel1);
            groupBox1.Controls.Add(name1);
            this.Controls.Add(groupBox1);
        }
        private async void FrekwencjaNauczyciel_Load(object sender, EventArgs e)
        {
            await FetchData();
            int i = 0;
            foreach(var student in students)
            {
                add(i,student.name,student.StudentId);
                
                i++;
            }
        }

        void reload(object sender, EventArgs e)
        {
            Controls.Clear();
            InitializeComponent();
            FrekwencjaNauczyciel_Load(sender,e);
            students.Clear();
            attendances.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5(id);
            form5.ShowDialog();
            reload(sender, e);
        }
    }
}
