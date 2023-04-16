using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bibrusik
{
    public partial class FrekwencjaNauczyciel : Form
    {
        public FrekwencjaNauczyciel()
        {
            InitializeComponent();
        }

        public class Student
        {
            public string name { get; set; }
            public int StudentId { get; set; }
        }

        List<Student> students { get; set; } = new List<Student>();

        

        private void FrekwencjaNauczyciel_Load(object sender, EventArgs e)
        {
            foreach (var student in students)
            {
                labe1.text = student.name;
            }
        }
    }
}
