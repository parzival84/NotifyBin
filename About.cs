using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrashBin
{
	public partial class About : Form
	{
		public About()
		{
			InitializeComponent();
			Descriptionlabel.Text = "TrashBin\nRecycle bin for your Microsoft Windows system tray area. \n © 2021 by Aaron Davis \n github.com/parzival84/TrashBin ";
		}
		private void Closebutton_Click(object sender, EventArgs e)
		{
			this.Close();
		}

        private void Descriptionlabel_Click(object sender, EventArgs e)
        {

        }

        private void Headlinelabel_Click(object sender, EventArgs e)
        {

        }
    }
}
