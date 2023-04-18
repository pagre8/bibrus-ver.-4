using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;

namespace Bibrusik
{
    public partial class DodawanieUcznia : Form
    {
        HttpClient Http = new HttpClient();
        public DodawanieUcznia()
        {
            InitializeComponent();
        }
        string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public class Info
        {
            public string FirstName;
            public string LastName;
            public string Email;
            public string Phone;
            public string login;
            public string password;
            public string pesel;
            public string PFirstName;
            public string PLastName;
            public string PEmail;
            public string Ppesel;
            public string Pphone;
            public string PLogin;
            public string PPassword;
            public string City;
            public string Road;
            public string HomeNumber;
            public string Voivoidship;
            public string PostalCode;
            public string PCity;
            public string PRoad;
            public string PHomeNumber;
            public string PVoivoidship;
            public string PPostalCode;
        }
        public async Task SendData(Info _info)
        {
            var postBody = new
            {
                query =
                "mutation{" +
                "   addStudent(" +
                $"       firstName: \"{_info.FirstName}\"," +
                $"       lastName: \"{_info.LastName}\"," +
                $"       email: \"{_info.Email}\"," +
                $"       phone: \"{_info.Phone}\"," +
                $"       login: \"{_info.login}\"," +
                $"       password: \"{ComputeSha256Hash(_info.password)}\"," +
                $"       pesel: \"{_info.pesel}\"," +
                $"       pFirstName: \"{_info.PFirstName}\"," +
                $"       pLastName: \"{_info.PLastName}\"," +
                $"       pEmail: \"{_info.PEmail}\"," +
                $"       ppesel: \"{_info.Ppesel}\"," +
                $"       pPhone: \"{_info.Pphone}\"," +
                $"       pLogin: \"{_info.PLogin}\"," +
                $"       pPassword: \"{ComputeSha256Hash(_info.PPassword)}\"," +
                $"       city: \"{_info.City}\"," +
                $"       road: \"{_info.Road}\"," +
                $"       homeNumber: \"{_info.HomeNumber}\"," +
                $"       voivoidship: \"{_info.Voivoidship}\"," +
                $"       postalCode: \"{_info.PostalCode}\"," +
                $"       pCity: \"{_info.PCity}\"," +
                $"       pRoad: \"{_info.PRoad}\"," +
                $"       pHomeNumber: \"{_info.PHomeNumber}\"," +
                $"       pVoivoidship: \"{_info.PVoivoidship}\"," +
                $"       pPostalCode: \"{_info.PPostalCode}\"" +
                "   )" +
                "}"
            };
            try
            {
                var response = await Http.PostAsJsonAsync("https://localhost:7151/graphql", postBody);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }
                MessageBox.Show("Dodano pomyślnie", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);

            };

        }
        private void DodawanieUcznia_Load(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void add_button_Click(object sender, EventArgs e)
        {
            Info info = new Info
            {
                FirstName = textBox1.Text,
                LastName = textBox2.Text,
                Email = textBox5.Text,
                Phone = maskedTextBox1.Text,
                login = textBox8.Text,
                password = textBox7.Text,
                pesel = textBox9.Text,
                PFirstName = textBox12.Text,
                PLastName = textBox11.Text,
                PEmail = textBox10.Text,
                Pphone = maskedTextBox2.Text,
                PLogin = textBox21.Text,
                PPassword = textBox20.Text,
                City = textBox3.Text,
                Road = textBox6.Text,
                HomeNumber = textBox15.Text,
                PCity = textBox18.Text,
                PRoad = textBox19.Text,
                PHomeNumber = textBox17.Text,
                PVoivoidship = textBox4.Text,
                PPostalCode = textBox16.Text, 
                Voivoidship = textBox22.Text,
                PostalCode = textBox14.Text,
            };
            SendData(info);
            
        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
