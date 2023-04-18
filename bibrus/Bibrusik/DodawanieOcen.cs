using System;

using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Json;

namespace Bibrusik
{
    public partial class DodawanieOcen : Form
    {
        public int StudentId;
        public int UserId;
        HttpClient Http = new HttpClient();
        public class Info
        {
            public DateTime Date;
            public int Grade;
            public int Weight;
            public string Category;
            public string comment;
            public bool avg;
            public int semester;
        }


        public DodawanieOcen(int _StudentId, int _UserId)
        {
            StudentId = _StudentId;
            UserId = _UserId;
            InitializeComponent();
        }

        private void DodawanieOcen_Load(object sender, EventArgs e)
        {

        }
        public async Task SendData(Info info)
        {
            var postBody = new
            {
                query =
                "mutation{" +
                "   addGrade(" +
                $"       avg: {(info.avg).ToString().ToLower()}," +
                $"       categorty: \"{info.Category}\"," +
                $"       comment: \"{info.comment}\"," +
                $"       date: \"{info.Date.ToString("yyyy-MM-dd")}\"," +
                $"       grade: {info.Grade}," +
                $"       semester: {info.semester}," +
                $"       studentId: {StudentId}," +
                $"       userid: {UserId}," +
                $"       weight: {info.Weight}" +
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
                MessageBox.Show("Dodano pomyślnie", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);

            };
        }
        private void add_button_Click(object sender, EventArgs e)
        {
            Info info = new Info
            {
                Date = data.Value,
                Grade = int.Parse(ocena.Text),
                Weight = int.Parse(weight_combobox.Text),
                comment = kom.Text,
                Category = kategoria.Text,
                avg = count_checkbox.Checked,
                semester= int.Parse(semester_combobox.Text),
            };
            SendData(info);
            Close();
        }

        private void semester_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void semester_combobox_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
