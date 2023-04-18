using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Json;

namespace Bibrusik
{
    public partial class InformacjeUczen : Form
    {
        public int id;
        public InformacjeUczen(int id)
        {
            InitializeComponent();
            this.id = id;

        }
        public class Info
        {
            public string FirstName;
            public string LastName;
            public string Class;
            public string City;
            public string Road;
            public string BuildingNumber;
            public string SchoolName;
            public string SchoolPhone;
            public string SchoolAddress;
        }
        Info info = new Info();
        HttpClient Http = new HttpClient();

        private void user_surname_Click(object sender, EventArgs e)
        {
            
        
        }

        private void surname_Click(object sender, EventArgs e)
        {
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }


        public async Task FetchData(int id)
        {
            var postBody = new
            {
                query =
                "mutation{" +
                "   getData(" +
                $"       id: {id}" +
                "   ){" +
                "       firstName," +
                "       lastName," +
                "       class," +
                "       city," +
                "       road," +
                "       buildingNumber" +
                "       schoolName," +
                "       schoolPhone," +
                "       schoolAddress" +
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
                parsedResponse = parsedResponse.GetProperty("data").GetProperty("getData");

                

                info.BuildingNumber = parsedResponse.GetProperty("buildingNumber").GetString();
                info.City = parsedResponse.GetProperty("city").GetString();
                info.Class = parsedResponse.GetProperty("class").GetString();
                info.FirstName = parsedResponse.GetProperty("firstName").GetString();
                info.LastName = parsedResponse.GetProperty("lastName").GetString();
                info.Road = parsedResponse.GetProperty("road").GetString();
                info.SchoolAddress = parsedResponse.GetProperty("schoolAddress").GetString();
                info.SchoolPhone = parsedResponse.GetProperty("schoolPhone").GetString();
                info.SchoolName = parsedResponse.GetProperty("schoolName").GetString();
                Console.WriteLine(info.FirstName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);

            };

        }
        private async void InformacjeUczen_Load(object sender, EventArgs e)
        {
            await FetchData(id);
            name.Text = info.FirstName;
            surname.Text = info.LastName;
            label2.Text = info.Class;
            city.Text = info.City;
            street.Text = info.Road;
            building.Text = info.BuildingNumber;
            school.Text = info.SchoolName;
            number.Text = info.SchoolPhone;
            adress.Text = info.SchoolAddress;
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            Zmienhaslo form = new Zmienhaslo(id);
            form.ShowDialog();

        }
    }
}
