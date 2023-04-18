using System;
using System.Drawing;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;

namespace Bibrusik
{
    public partial class WyborKlasyfrekwencja : Form
    {
        HttpClient Http = new HttpClient();
        int id;
        public WyborKlasyfrekwencja(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        public class Class
        {
            public string name { get; set; }
            public int ClassId { get; set; }
        }

        List<Class> classes { get; set; } = new List<Class>();

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        void add(int i, string name,int classId)
        {
            Label label1 = new Label();
            label1.Location = new System.Drawing.Point(6, 24);
            label1.Size = new Size(50, 12);
            label1.Text = name;

            Button button2 = new Button();
            button2.BackColor = System.Drawing.SystemColors.Info;
            button2.Location = new System.Drawing.Point(176, 19);
            button2.Size = new System.Drawing.Size(75, 23);
            button2.Text = "Wybierz";
            button2.Click += (object sender, EventArgs e) =>
            {
                FrekwencjaNauczyciel frekwencjaNauczyciel = new FrekwencjaNauczyciel(classId);
                frekwencjaNauczyciel.ShowDialog();
            };



            GroupBox groupBox = new GroupBox();
            groupBox.Size = new System.Drawing.Size(257, 54);
            groupBox.Location = new System.Drawing.Point(80, 12 + 55 * i);
            groupBox.BackColor = System.Drawing.Color.Transparent;
            groupBox.Controls.Add(label1);
            groupBox.Controls.Add(button2);
            this.Controls.Add(groupBox);
        }

        public async Task FetchData()
        {
            var postBody = new
            {
                query =
                "mutation{" +
                "   getClasses(" +
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

                parsedResponse = parsedResponse.GetProperty("data").GetProperty("getClasses");

                foreach (var item in parsedResponse.EnumerateArray())
                {
                    Class student = new Class
                    {
                        ClassId = item.GetProperty("id").GetInt32(),
                        name = item.GetProperty("name").GetString()
                    };
                    classes.Add(student);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);

            };
        }

        private async void WyborKlasy_Load(object sender, EventArgs e)
            {
            await FetchData();
            int i=0;
            foreach (var _class in classes)
            {
                add(i, _class.name,_class.ClassId);
                i++;
            }
            }
        }
    }

