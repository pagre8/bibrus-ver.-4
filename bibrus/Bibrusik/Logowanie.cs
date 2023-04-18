using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Net.Http.Json;
using System.Security.Cryptography;

namespace Bibrusik
{
    public partial class Logowanie : Form
    {
        HttpClient Http = new HttpClient();

        public int id=0;

        public Logowanie()
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

        private void cancel_button_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void password_textbox_TextChanged(object sender, EventArgs e)
        {

        }

        private async void login_button_Click(object sender, EventArgs e)
        {
            if(login_textbox.Text == "" || password_textbox.Text == "")
            {
                MessageBox.Show("Nieprawidłowy login lub hasło","Błąd",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else 
            {
                string login = login_textbox.Text;
                string password = password_textbox.Text;

                password = ComputeSha256Hash(password);

                await FetchData(login,password);

                if (id != 0)
                {
                    Close();
                }
                
            }
        }

        public async Task FetchData(string _login, string _password)
        {
            var postBody = new
            {
                query =
                "mutation{" +
                "   login(" +
                $"       login: \"{_login}\"," +
                $"       password: \"{_password}\"" +
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
                
                var parsedResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
                
                parsedResponse = parsedResponse.GetProperty("data").GetProperty("login");
                int _id = parsedResponse.Deserialize<int>();
                if(_id==0)
                {
                    MessageBox.Show("Nieprawidłowy login lub hasło", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                id = _id;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);

            };
            
        }

    }
}
