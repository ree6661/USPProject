using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FindPhone
{
    public partial class Form1 : Form
    {
        UInt16 page, pages;
        public List<PictureBox> pics = new List<PictureBox>();
        public Form1()
        {
            InitializeComponent();
            InitializeSEngine();
            find();
            pics.Add(pictureBox1);
            pics.Add(pictureBox2);
            pics.Add(pictureBox3);

            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();

            pout();
        }
        public void pagesUpdate()
        {
            pages = (UInt16)(phones.Count / 3 + Convert.ToUInt16(phones.Count % 3 != 0));
            pages += Convert.ToUInt16(pages == 0);
            page = 1;
            previousBtn.Enabled = false;
            nextBtn.Enabled = page < pages;
            pageLable.Text = page.ToString();
            pagesLabel.Text = pages.ToString();
        }
        public class Model
        {
            public UInt32 modelId, BrandId, CPUId, OSId, RAMId, ROMId;
            public string name = null, description = "N/A";
        }
        public class Phone
        {
            public Model model;
            public string Brand = "N/A", CPU = "N/A", OS = "N/A", RAM = "N/A", ROM = "N/A", frontCams = "", backCams = "";
            public List<string> photos = new List<string>();

            public Phone() 
            {
                model=new Model();
            }
            public Phone(Model model)
            {
                this.model = model;
                frontCams = "";
                backCams = "";
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
        public string getDsc(Phone phone)
        {
            return "Смартфон :" +
                        phone.Brand + " " +
                        phone.model.name +
                        "\n Предни камери " + phone.frontCams +
                        "\n Задни камери " + phone.backCams +
                        "\n Процесор " +
                        phone.CPU +
                        "\n Операционна Система " + phone.OS +
                        "\n RAM памет " + phone.RAM +
                        "\n Сторидж " + phone.ROM +
                        "\n Описание " + phone.model.description;
        }
        public void pout()
        {
            List<RichTextBox> text = new List<RichTextBox>();
            text.Add(richTextBox1);
            text.Add(richTextBox2);
            text.Add(richTextBox3);
            
            for (int i = 0; i < 3; ++i)
            {
                if ((i + (3 * (page - 1))) < phones.Count)
                {
                    pics[i].Visible = true;
                    text[i].Visible = true;
                    pics[i].Load(@"photos\" + phones[(i + (3 * (page - 1)))].photos[0]);
                    pics[i].SizeMode = PictureBoxSizeMode.StretchImage;
                    text[i].Text = getDsc(phones[(i + (3 * (page - 1)))]);
                }
                else
                {
                    //pics[i].Load();
                    pics[i].Visible = false;
                    text[i].Visible = false;
                }
                pics.Add(pictureBox1);
                pics.Add(pictureBox2);
                pics.Add(pictureBox3);
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
            if (page < pages)
            {

                ++page;
                nextBtn.Enabled = page < pages;
                previousBtn.Enabled = true;
                pageLable.Text = page.ToString();
                pout();
            }
            else nextBtn.Enabled = false;
        }

        private void previousBtn_Click(object sender, EventArgs e)
        {
            if (page > 1)
            {
                nextBtn.Enabled = true;
                previousBtn.Enabled = --page > 1;
                pageLable.Text = page.ToString();
                pout();
            }
            else nextBtn.Enabled = false;
        }
        public void find() 
        {
            phones.Clear();
            string query = "SELECT марки.idМарки ,марки.Марки," +
                                "модели.idМодели, модели.Модел,модели.Описание," +
                                "процесор.idПроцесор,процесор.Процесор," +
                                "ос.idОС,ос.ОС,ram.idRAM,ram.RAM," +
                                "сторидж.idСторидж,сторидж.размер " +
                                "FROM phonedb.модели " +
                                "JOIN phonedb.марки ON марки.idМарки = модели.idМарки " +
                                "JOIN phonedb.процесор ON процесор.idПроцесор = модели.idПроцесор " +
                                "JOIN phonedb.ос ON ос.idОС = модели.idОС " +
                                "JOIN phonedb.ram ON ram.idRAM = модели.idRAM " +
                                "JOIN phonedb.сторидж ON сторидж.idСторидж = модели.idСторидж ",
                 brand = "",model = "", camera = "";

            if (cameraCBox.SelectedItem != "всички")
                query += "JOIN phonedb.камери ON камери.idМодели=модели.idМодели  " +
                            "JOIN phonedb.мегапиксели ON мегапиксели.idМегапиксели=камери.idМегапиксели ";

                if (modelCBox.SelectedItem != "всички")
                model = " WHERE модели.Модел = @Model";
            else if (BrandCBox.SelectedItem != "всички") 
                brand = " WHERE марки.Марки = @Brand";

            if (cameraCBox.SelectedItem != "всички")
            {
                if (brand == "" || model == "") camera = " AND ";
                else camera = " WHERE ";
                camera += "мегапиксели.Мегапиксели = @Camera GROUP BY модели.idМодели";
            }

            query += model + brand + camera + " ORDER BY модели.idМодели";

            //connection to Data Base
            DBConnection conn = new DBConnection();
            conn.open();

            MySqlCommand command = new MySqlCommand(query, conn.conn);
            
            command.Parameters.Add("@Brand", MySqlDbType.VarChar, 45).Value = BrandCBox.Text;
            command.Parameters.Add("@Model", MySqlDbType.VarChar, 45).Value = modelCBox.Text;
            command.Parameters.Add("@Camera", MySqlDbType.VarChar, 45).Value = cameraCBox.Text;

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Phone phone = new Phone();
                phone.model.modelId = reader.GetUInt32("idМодели");
                phone.model.BrandId = reader.GetUInt32("idМарки");
                phone.model.CPUId = reader.GetUInt32("idПроцесор");
                phone.model.OSId = reader.GetUInt32("idОС");
                phone.model.RAMId = reader.GetUInt32("idRAM"); 

                phone.model.ROMId = reader.GetUInt32("idСторидж");
                phone.model.name = reader.GetString("Модел");
                try
                {
                    phone.model.description = reader.GetString("Описание");
                }
                catch
                {
                    phone.model.description = "N/A";
                }
                try { phone.Brand = reader.GetString("Марки"); }
                catch { phone.Brand = "N/A"; }
                try { phone.CPU = reader.GetString("Процесор"); }
                catch { phone.CPU = "N/A"; }
                try { phone.OS = reader.GetString("ОС"); }
                catch { phone.OS = "N/A"; }
                try { phone.RAM = reader.GetString("RAM"); }
                catch { phone.RAM = "N/A"; }
                try { phone.ROM = reader.GetString("Размер"); }
                catch { phone.ROM = "N/A"; }
                phones.Add(phone);
            }
            reader.Close();
            //get photos

            query = "SELECT DISTINCT Път FROM снимка JOIN галерия ON снимка.idСнимка = галерия.idСнимка where галерия.idМодели = @modelId ;";
            command = new MySqlCommand(query, conn.conn);

            for (int i = 0; i < phones.Count; ++i)
            {
                if(searchBox.Text != "")
                    if(!(Regex.IsMatch(getDsc(phones[i]), searchBox.Text)))
                    {
                        phones.RemoveAt(i);
                        --i;
                        continue;
                    }
            command.Parameters.Clear();
                command.Parameters.Add("@modelId", MySqlDbType.UInt32).Value = phones[i].model.modelId;
                reader.Close();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    phones[i].photos.Add(reader.GetString("Път"));
                }
                if (!phones[i].photos.Any())
                    phones[i].photos.Add(("default.jpg"));
            }
            reader.Close();
            //get cams
            query = "SELECT SUM(if(камери.Предна=1 , 1, 0)) AS брой_предни ," +
                    "SUM(if(камери.Предна=0 , 1, 0)) AS брой_задни, Мегапиксели " +
                    "FROM мегапиксели JOIN камери " +
                    "ON камери.idМегапиксели = мегапиксели.idМегапиксели " +
                    "WHERE камери.idМодели=@modelId GROUP BY Мегапиксели";

            command = new MySqlCommand(query, conn.conn);
            for (int i = 0; i < phones.Count; ++i)
            {
                command.Parameters.Clear();
                command.Parameters.Add("@modelId", MySqlDbType.UInt32).Value = phones[i].model.modelId;
                reader.Close();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string camMp = reader.GetString("Мегапиксели");
                    int br = reader.GetInt32("брой_предни");
                    if (br != 0) phones[i].frontCams += br.ToString() + "x" + camMp;
                    br = reader.GetInt32("брой_задни");
                    if (br != 0) phones[i].backCams += br.ToString() + "x" + camMp;
                }
            }
            reader.Close();
            pagesUpdate();
            conn.close();
        }
        private void searchBtn_Click(object sender, EventArgs e)
        {
            find();
            pout();
        }
    }
}