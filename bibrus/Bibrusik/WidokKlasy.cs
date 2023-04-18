using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;

namespace Bibrusik
{
    public partial class WidokKlasy : Form
    {
        int id;
        HttpClient Http = new HttpClient();
        public WidokKlasy(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        public class Student
        {
            public string name { get; set; }
            public int StudentId { get; set; }
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
        public class StudentGrade
        {
            public int value;
            public int weight;
            public DateTime date;
            public string comment;
            public string category;
            public bool avg;
            public int semester;
        }
        List<StudentGrade> grades = new List<StudentGrade>();

        async void add(int i, string name, int student_id, int subject_id)
        {

            FlowLayoutPanel flowLayoutPanel1 = new FlowLayoutPanel();
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.Location = new System.Drawing.Point(112, 24);
            flowLayoutPanel1.MaximumSize = new System.Drawing.Size(200, 0);
            flowLayoutPanel1.Size = new System.Drawing.Size(200, 0);

            FlowLayoutPanel flowLayoutPanel2 = new FlowLayoutPanel();
            flowLayoutPanel2.AutoSize = true;
            flowLayoutPanel2.Location = new System.Drawing.Point(112, 24);
            flowLayoutPanel2.MaximumSize = new System.Drawing.Size(200, 0);
            flowLayoutPanel2.Size = new System.Drawing.Size(200, 0);


            Button button1 = new Button();
            button1.BackColor = System.Drawing.SystemColors.Info;
            button1.BackgroundImage = global::Bibrusik.Properties.Resources.pixil_frame_0__21_;
            button1.Location = new System.Drawing.Point(350, 19);
            button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            button1.Size = new System.Drawing.Size(40, 40);
            button1.TabIndex = 0;
            button1.Click += (object sender, EventArgs e) =>
            {
                DodawanieOcen dodawanieOcen = new DodawanieOcen(student_id,id);
                dodawanieOcen.ShowDialog();
                reload(sender, e);
            };

            Button button2 = new Button();
            button2.BackColor = System.Drawing.SystemColors.Info;
            button2.BackgroundImage = global::Bibrusik.Properties.Resources.pixil_frame_0__21_;
            button2.Location = new System.Drawing.Point(350, 19);
            button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            button2.Size = new System.Drawing.Size(40, 40);
            button2.TabIndex = 0;
            button2.Click += (object sender, EventArgs e) =>
            {
                DodawanieOcen dodawanieOcen = new DodawanieOcen(student_id, id);
                dodawanieOcen.ShowDialog();
                reload(sender, e);
            };




            Label surname1 = new Label();
            surname1.AutoSize = true;
            surname1.Location = new System.Drawing.Point(6, 24);
            surname1.MaximumSize = new System.Drawing.Size(50, 13);
            surname1.Size = new System.Drawing.Size(100, 13);
            surname1.Text = name;

            Label surname2 = new Label();
            surname2.AutoSize = true;
            surname2.Location = new System.Drawing.Point(6, 24);
            surname2.MaximumSize = new System.Drawing.Size(50, 13);
            surname2.Size = new System.Drawing.Size(100, 13);
            surname2.Text = name;

            Label avg1 = new Label();
            avg1.AutoSize = true;
            avg1.Location = new System.Drawing.Point(300, 24);
            avg1.MaximumSize = new System.Drawing.Size(50, 13);
            avg1.Size = new System.Drawing.Size(50, 13);


            Label avg2 = new Label();
            avg2.AutoSize = true;
            avg2.Location = new System.Drawing.Point(300, 24);
            avg2.MaximumSize = new System.Drawing.Size(50, 13);
            avg2.Size = new System.Drawing.Size(50, 13);

            Label label3 = new Label();
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(290, 10);
            label3.MaximumSize = new System.Drawing.Size(50, 13);
            label3.Size = new System.Drawing.Size(50, 13);
            label3.Text = "średnia:";

            Label label4 = new Label();
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(290, 10);
            label4.MaximumSize = new System.Drawing.Size(50, 13);
            label4.Size = new System.Drawing.Size(50, 13);
            label4.Text = "średnia:";


            GroupBox groupBox1 = new GroupBox();
            GroupBox groupBox2 = new GroupBox();


            await FetchGrades(student_id);


           foreach(var grade in grades)
            {
                int[,] grades1 = new int[2, 8];
                int[,] grades2 = new int[2, 8];
                //pierwszy wymiar zawiera sumę i ilość ocen danej wagi, a drugi wymiar wagę
                    Label label = new Label();
                    label.Text = grade.value.ToString();
                    label.Size = new System.Drawing.Size(13, 13);
                    label.BackColor = System.Drawing.Color.SandyBrown;
                    ToolTip toolTip = new ToolTip();
                    toolTip.ReshowDelay = 500;
                    toolTip.ShowAlways = true;
                string text = "Ocena: " + grade.value + "\nWaga: " + grade.weight + "\nKategoria: " + grade.category + "\nKomentarz: " + grade.comment + "\nSemestr: " + grade.semester + "\nLicz do średniej: " + grade.avg + "\nData dodania: " + grade.date;
                    toolTip.SetToolTip(label, text);
                    if (grade.semester == 1)
                    {
                        flowLayoutPanel1.Controls.Add(label);
                        if (grade.avg)
                        {
                            if (grade.weight != 10)
                            {
                                grades1[0, grade.weight] += grade.value;
                                grades1[1, grade.weight] += 1;
                            }
                            else
                            {
                                grades1[0, 7] += grade.value;
                                grades1[1, 7] += 1;
                            }
                        }
                    }
                    else
                    {
                        flowLayoutPanel2.Controls.Add(label);
                        if (grade.avg)
                        {
                            if (grade.weight != 10)
                            {
                            grades2[0, grade.weight] += grade.value;
                            grades2[1, grade.weight] += 1;
                        }
                            else
                            {
                            grades2[0, 7] += grade.value;
                            grades2[1, 7] += 1;
                        }
                        }
                    }


                float wavg1 = (grades1[0, 1] + grades1[0, 2] * 2 + grades1[0, 3] * 3 + grades1[0, 4] * 4 + grades1[0, 5] * 5 + grades1[0, 6] * 6 + grades1[0, 7] * 10);
                wavg1 /= (grades1[1, 1] + grades1[1, 2] * 2 + grades1[1, 3] * 3 + grades1[1, 4] * 4 + grades1[1, 5] * 5 + grades1[1, 6] * 6 + grades1[1, 7] * 10);
                float wavg2 = (grades2[0, 1] + grades2[0, 2] * 2 + grades2[0, 3] * 3 + grades2[0, 4] * 4 + grades2[0, 5] * 5 + grades2[0, 6] * 6 + grades2[0, 7] * 10);
                wavg2 /= (grades2[1, 1] + grades2[1, 2] * 2 + grades2[1, 3] * 3 + grades2[1, 4] * 4 + grades2[1, 5] * 5 + grades2[1, 6] * 6 + grades2[1, 7] * 10);


                avg1.Text = wavg1.ToString();
                avg2.Text = wavg2.ToString();
                groupBox1.Controls.Add(avg1);
                groupBox2.Controls.Add(avg2);
                groupBox1.Controls.Add(label3);
                groupBox2.Controls.Add(label4);

            }
            grades.Clear();


            groupBox1.AutoSize = true;
            groupBox1.BackColor = System.Drawing.Color.Transparent;
            groupBox1.Location = new System.Drawing.Point(12, 40 + 80 * i);
            groupBox1.Size = new System.Drawing.Size(410, 61);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(flowLayoutPanel1);
            groupBox1.Controls.Add(surname1);
            this.Controls.Add(groupBox1);

            groupBox2.AutoSize = true;
            groupBox2.BackColor = System.Drawing.Color.Transparent;
            groupBox2.Location = new System.Drawing.Point(430, 40 + 80 * i);
            groupBox2.Size = new System.Drawing.Size(410, 61);
            groupBox2.Controls.Add(flowLayoutPanel2);
            groupBox2.Text = "II   semestr";
            groupBox2.Controls.Add(surname2);

            groupBox2.Controls.Add(button2);

            //groupBox2.Controls.Add(label2);
            this.Controls.Add(groupBox2);

        }

        public async Task FetchGrades(int id)
        {
            grades.Clear();
            var postBody = new
            {
                query =
                "mutation{" +
                "   getGrades(" +
                $"       id: {id}" +
                "   ){" +
                "       avg," +
                "       category," +
                "       comment," +
                "       date," +
                "       semester," +
                "       value," +
                "       weight" +
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

                parsedResponse = parsedResponse.GetProperty("data").GetProperty("getGrades");

                foreach (var item in parsedResponse.EnumerateArray())
                {
                    StudentGrade grade = new StudentGrade
                    {
                        avg = item.GetProperty("avg").GetBoolean(),
                        category = item.GetProperty("category").GetString(),
                        comment = item.GetProperty("comment").GetString(),
                        date = item.GetProperty("date").GetDateTime(),
                        semester = item.GetProperty("semester").GetInt32(),
                        value = item.GetProperty("value").GetInt32(),
                        weight = item.GetProperty("weight").GetInt32(),
                    };
                    grades.Add(grade);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

        }

        void reload(object sender, EventArgs e)
        {
            Controls.Clear();
            InitializeComponent();
            WidokKlasy_Load(sender, e);
            students.Clear();
            grades.Clear();
        }

        private async void WidokKlasy_Load(object sender, EventArgs e)
        {
            await FetchData();
            int i=0;
            foreach(var student in students)
            {
                add(i,student.name,student.StudentId,0);
                i++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GrupoweDodawanieOcen grupoweDodawanieOcen = new GrupoweDodawanieOcen();
            grupoweDodawanieOcen.ShowDialog();
            reload(sender, e);
        }
    }
}
