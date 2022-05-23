using MySql.Data.MySqlClient;
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
    public partial class Form1 : Form
    {
        UInt16 page, pages;
        public Form1()
        {
            InitializeComponent();
            InitializeSEngine();
            InitializeResults();
  
            pout();
        }

        public class Model
        {
            public UInt32 modelId, BrandId, CPUId, OSId, RAMId, ROMId;
            public string name = null, description= null;
        }
        public class Phone
        {
            public Model model;
            public string Brand, CPU, OS, RAM, ROM;
            public List<string> photos = new List<string>();

            public Phone(Model model)
            {
                this.model = model;
            }

            public UInt32 getBrandid()
                { return model.BrandId; }
            public UInt32 getCPUId()
                { return model.CPUId; }
            public UInt32 getOSId()
                { return model.OSId; }
            public UInt32 getRAMId()
                { return model.RAMId; }
            public UInt32 getROMId()
                { return model.ROMId; }
        }
        public List<Phone> phones = new List<Phone>();
        public void InitializeSEngine() 
        {
            try
            {
                //defalut items
                BrandCBox.Items.Add("всички");
                modelCBox.Items.Add("всички");
                cameraCBox.Items.Add("всички");

                BrandCBox.SelectedItem = "всички";
                modelCBox.SelectedItem = "всички";
                cameraCBox.SelectedItem = "всички";

                modelCBox.Enabled = false;

                //connection to Data Base
                DBConnection conn = new DBConnection();
                conn.open();
                //get Brand items
                string query = "SELECT Марки FROM марки";

                MySqlCommand command = new MySqlCommand(query, conn.conn);

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    BrandCBox.Items.Add(reader.GetString("Марки"));
                }
                reader.Close();
                //get camera items
                query = "SELECT Мегапиксели FROM мегапиксели";

                command = new MySqlCommand(query, conn.conn);

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    cameraCBox.Items.Add(reader.GetString("Мегапиксели"));
                }
                reader.Close();
                conn.close();
            }
            catch
            {
                MessageBox.Show("ГРЕШКА: InitializeSEngine() се чупи", "ГРЕШКА",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void InitializeResults()
        {

         // try
         // {
                //connection to Data Base
                DBConnection conn = new DBConnection();
                conn.open();
                //get phones

                string query = "SELECT * FROM модели";

                MySqlCommand command = new MySqlCommand(query, conn.conn);

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Model model = new Model();
                    model.modelId = reader.GetUInt32("idМодели");
                    model.BrandId = reader.GetUInt32("idМарки");
                    model.CPUId = reader.GetUInt32("idПроцесор");
                    model.OSId = reader.GetUInt32("idОС");
                    model.RAMId = reader.GetUInt32("idОперативна памет");
                    model.ROMId = reader.GetUInt32("idСторидж");
                    model.name = reader.GetString("Модел");
                    try
                    {
                        model.description = reader.GetString("Описание");
                    }
                    catch
                    {
                        //MessageBox.Show("ГРЕШКА: " + model.name.ToString() + " has no description", "ГРЕШКА",
                        //                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        model.description = "N/A";
                    }
                    phones.Add(new Phone(model));
                }
                reader.Close();
                //get other phone data

                for (int i = 0; i < phones.Count; ++i)
                {
                    //get phone Brand
                    query = "SELECT Марки FROM марки WHERE idМарки=@BrandId ;";

                    command = new MySqlCommand(query, conn.conn);
                    command.Parameters.Add("@BrandId", MySqlDbType.UInt32).Value = phones[i].getBrandid();

                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        try { phones[i].Brand = reader.GetString("Марки"); }
                        catch { phones[i].Brand = "N/A"; }
                    }
                    else phones[i].Brand = "N/A";



                    reader.Close();
                    
                    //get phone CPU
                    query = "SELECT Процесор FROM процесор WHERE idПроцесор=@CPUId ;";

                    command = new MySqlCommand(query, conn.conn);
                    command.Parameters.Add("@CPUId", MySqlDbType.UInt32).Value = phones[i].getCPUId();

                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        try { phones[i].CPU = reader.GetString("Процесор"); }
                        catch { phones[i].CPU = "N/A"; }
                    }
                    else  phones[i].CPU = "N/A";

                    reader.Close();
                    //get phone OS
                    query = "SELECT ОС FROM ос WHERE idОС=@OSId ;";

                    command = new MySqlCommand(query, conn.conn);
                    command.Parameters.Add("@OSId", MySqlDbType.UInt32).Value = phones[i].getOSId();
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        try{ phones[i].OS = reader.GetString("ОС");}
                        catch { phones[i].OS = "N/A"; }
                    }
                    else phones[i].OS = "N/A";

                    reader.Close();
                    //get phone RAM
                    query = "SELECT размер FROM ram WHERE idRam = @RAMId ;";

                    command = new MySqlCommand(query, conn.conn);
                    command.Parameters.Add("@RAMId", MySqlDbType.UInt32).Value = phones[i].getRAMId();

                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        try { phones[i].RAM = reader.GetString("размер"); }
                        catch { phones[i].RAM = "N/A"; }
                    }
                    else phones[i].RAM = "N/A";


                    reader.Close();
                    //get phone ROM
                    query = "SELECT Размер FROM сторидж WHERE idСторидж = @ROMId ";

                    command = new MySqlCommand(query, conn.conn);
                    command.Parameters.Add("@ROMId", MySqlDbType.UInt32).Value = phones[i].getROMId();

                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        try { phones[i].ROM = reader.GetString("Размер"); }
                        catch { phones[i].ROM = "N/A"; }
                    }
                    else phones[i].ROM = "N/A";

                    reader.Close();
                    //get photos

                    query = "SELECT DISTINCT Път FROM снимка JOIN галерия ON снимка.idСнимка = галерия.idСнимка where галерия.idМодели = @modelId ;";

                    command = new MySqlCommand(query, conn.conn);
                    command.Parameters.Add("@modelId", MySqlDbType.UInt32).Value = phones[i].model.modelId;

                    reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        phones[i].photos.Add(reader.GetString("Път"));
                    }
                    if (!phones[i].photos.Any())
                        phones[i].photos.Add(("default.jpg"));
                    reader.Close();
                }
            pages = (UInt16)(phones.Count / 3 + Convert.ToUInt16(phones.Count % 3 != 0));
            page = 1;
            previousBtn.Enabled = false;
            nextBtn.Enabled = page < pages;
            pageLable.Text = page.ToString();
            pagesLabel.Text = pages.ToString();

            conn.close();
           /* }
            catch
            {
                MessageBox.Show("ГРЕШКА: InitializeResults() се счупи", "ГРЕШКА",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        public void pout()
        {


            List<PictureBox> pics = new List<PictureBox>();
            pics.Add(pictureBox1);
            pics.Add(pictureBox2);
            pics.Add(pictureBox3);

            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();

            List<RichTextBox> text = new List<RichTextBox>();
            text.Add(richTextBox1);
            text.Add(richTextBox2);
            text.Add(richTextBox3);

            for (int i = 0; i < 3; ++i)
            {
                if((i + (3 * (page - 1)))<phones.Count)
                {
                    pics[i].Visible = true;
                    text[i].Visible = true;
                    //MessageBox.Show(@"photos\" + phones[(i + (3 * (page - 1)))].photos[0]+"\ti= "+i.ToString()+"\ti2= "+(i + (3 * (page - 1))).ToString(), "ГРЕШКА",MessageBoxButtons.OK);
                    pics[i].ImageLocation = @"photos\"  + phones[(i + (3 * (page - 1)))].photos[0];
                    pics[i].SizeMode = PictureBoxSizeMode.StretchImage;
                    text[i].Text = "Смартфон :" +
                        phones[i + (3 * (page - 1))].Brand + " " +
                        phones[i + (3 * (page - 1))].model.name +
                        "\n Процесор " +
                        phones[i + (3 * (page - 1))].CPU + 
                        "\n Операционна Система " + phones[i + (3 * (page - 1))].OS +
                        "\n RAM памет " + phones[i + (3 * (page - 1))].RAM +
                        "\n Сторидж " + phones[i + (3 * (page - 1))].ROM;
                }
                else
                {
                    pics[i].Visible = false;
                    text[i].Visible = false;
                }
            }

            pictureBox1.ImageLocation = @"Image\1.jpg";

        }
        private void BrandCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (BrandCBox.Text != "всички")
                {
                    //connection to Data Base
                    DBConnection conn = new DBConnection();
                    conn.open();
                    //get Brand id
                    string query = "SELECT idМарки FROM марки WHERE Марки=@Brand";

                    MySqlCommand command = new MySqlCommand(query, conn.conn);
                    command.Parameters.Add("@Brand", MySqlDbType.VarChar, 45).Value = BrandCBox.Text;

                    MySqlDataReader reader = command.ExecuteReader();
                    UInt32 brandId;
                    if (reader.Read())
                    {
                        brandId = reader.GetUInt32("idМарки");
                    }
                    else
                    {
                        reader.Close();
                        MessageBox.Show("ГРЕШКА: Индексът към марката не е открит", "ГРЕШКА",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    reader.Close();

                    //get models
                    modelCBox.Items.Clear();
                    modelCBox.Items.Add("всички");
                    modelCBox.SelectedItem = "всички";

                    query = "SELECT Модел FROM модели WHERE idМарки=@BrandId";

                    command = new MySqlCommand(query, conn.conn);
                    command.Parameters.Add("@BrandId", MySqlDbType.UInt32).Value = brandId;

                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        modelCBox.Items.Add(reader.GetString("Модел"));
                    }

                    reader.Close();
                    conn.close();
                    modelCBox.Enabled = true;
                }
                else
                {
                    modelCBox.Items.Clear();
                    modelCBox.Items.Add("всички");
                    modelCBox.SelectedItem = "всички";
                    modelCBox.Enabled = false;
                }
            }
            catch
            {
                MessageBox.Show("ГРЕШКА: BrandCBox_SelectedIndexChanged(object sender, EventArgs e) се счупи", "ГРЕШКА",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            if(page<pages)
            {
                ++page;
                nextBtn.Enabled = page<pages;
                previousBtn.Enabled = true;
                pout();
            }
            else nextBtn.Enabled = false;
        }

        private void previousBtn_Click(object sender, EventArgs e)
        {
            if (page > 0)
            {
                --page;
                nextBtn.Enabled = true;
                previousBtn.Enabled = page > 0;
                pout();
            }
            else nextBtn.Enabled = false;
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            
        }
    }
}