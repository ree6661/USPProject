using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FindPhone
{
    public partial class Galery : Form
    {
        List<String> galery;
        PictureBox pb;
        int picture=-1;
        public Galery(List<String> photos)
        {
            InitializeComponent();
            pb = pictureBox1;
            pictureBox1 = new PictureBox();
            galery = photos;
            updateSearch(1);
            loadImage(picture);
        }
        public void loadImage(int index) 
        {
            pb.Load(@"photos\" + galery[index]);
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        public void updateSearch(int update)
        {
            picture += update;
            nextBtn.Enabled = picture < galery.Count-1;
            previousBtn.Enabled = picture > 0;
        }
        private void nextBtn_Click(object sender, EventArgs e)
        {
            if(picture<galery.Count)
            {
                updateSearch(1);
                loadImage(picture);
            }
        }

        private void previousBtn_Click(object sender, EventArgs e)
        {
            if (picture > 0)
            {
                updateSearch(-1);
                loadImage(picture);
            }
        }
    }
}
