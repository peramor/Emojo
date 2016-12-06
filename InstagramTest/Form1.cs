using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InstagramTest {
    public partial class Form1 : Form {

        InstagramGetter getter;

        public Form1() {
            InitializeComponent();
            getter = new InstagramGetter();
        }

        private async void button1_Click(object sender, EventArgs e) {
            Process.Start(getter.GetAuthLink());           
        }

        private async void button2_Click(object sender, EventArgs e) {
            List<InstagramData> data = await getter.GetData(textBox1.Text);
            label1.Text = data[0].Name;
        }
    }
}
