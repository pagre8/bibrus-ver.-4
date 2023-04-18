using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http.Json;
using System.Net.Http;

namespace Bibrusik
{
    public partial class Zmienhaslo : Form
    {
        int id;
        HttpClient Http = new HttpClient();
        public Zmienhaslo(int id)
        {
            InitializeComponent();
            this.id = id;
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

        public async Task SendData(int id,string password)
        {
            var postBody = new
            {
                query =
                "mutation{" +
                "   updatePassword(" +
                $"       id:{id}," +
                $"       password:\"{password}\"" +
                "   )" +
                "}"
            };
            Console.WriteLine(postBody);
            try
            {
                var response = await Http.PostAsJsonAsync("https://localhost:7151/graphql", postBody);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }
                var parsedResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
                Console.WriteLine(parsedResponse.ToString());
                MessageBox.Show("Zmieniono pomyślnie", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);

            };
        }

        private void pass_textbox_TextChanged(object sender, EventArgs e)
        {
       
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void submit_button_Click(object sender, EventArgs e)
        {
            
         if (string.IsNullOrEmpty(pass_textbox.Text) && string.IsNullOrEmpty(repeat_pass_textbox.Text))
         {
             MessageBox.Show("Hasło nie może być puste","Błąd",MessageBoxButtons.OK, MessageBoxIcon.Error);
             return;
         }
            
        if (pass_textbox.Text.Contains(" "))
        {
            MessageBox.Show("Hasło nie może zawierać spacji", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
            if (pass_textbox.Text.Length < 8)
            {
                MessageBox.Show("Hasło musi zawierać co najmniej 8 znaków", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        if(pass_textbox.Text != repeat_pass_textbox.Text)
        {
            MessageBox.Show("Hasła nie są zgodne", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
       
        SendData(id, ComputeSha256Hash(pass_textbox.Text));
            
        }
    }
}
