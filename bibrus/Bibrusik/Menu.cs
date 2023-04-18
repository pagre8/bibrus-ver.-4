using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Json;

namespace Bibrusik
{
    public partial class Menu : Form
    {

        public int UserId;

        HttpClient Http = new HttpClient();
        List<string> values;
        bool nauczyciel;
        public int selectedPage;
        public Menu(int _UserId)
        {
            InitializeComponent();
            
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer1_Tick);
            timer.Start();
            UserId = _UserId;
        }


        private async void Form15_Load(object sender, EventArgs e)
        {
            await FetchData();
            label8.Text = values[0];
            

        }

        public async Task FetchData()
        {
            var postBody = new
            {
                query =
                "mutation{" +
                "   getName(" +
                $"       id: {UserId}" +
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

                var parsedResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

                parsedResponse = parsedResponse.GetProperty("data").GetProperty("getName");
                values = parsedResponse.Deserialize<List<string>>();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);

            };

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            lbTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WyborKlasyfrekwencja wyborKlasyfrekwencja = new WyborKlasyfrekwencja(UserId, values[1]);
            wyborKlasyfrekwencja.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WyborKlasyoceny wyborKlasyoceny = new WyborKlasyoceny(UserId, values[1]);
            wyborKlasyoceny.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            InformacjeUczen form = new InformacjeUczen(UserId);

            form.ShowDialog();
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DodawanieUcznia dodawanieUcznia = new DodawanieUcznia();
            dodawanieUcznia.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DodawanieKlasy dodawanieKlasy = new DodawanieKlasy();
            dodawanieKlasy.ShowDialog();
        }
    }
    
}
