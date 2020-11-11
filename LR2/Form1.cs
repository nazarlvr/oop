using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace LR2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ReadXml();
        }

        public void ReadXml()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Users\user\source\repos\LR2\Gadgets.xml");
            XmlElement xroot = doc.DocumentElement;
            XmlNodeList chnodes = xroot.SelectNodes("Gadget");

            for (int i = 0; i < chnodes.Count; ++i)
            {
                XmlNode n = chnodes.Item(i);
                AddNewItem(n);
            }
            CombosetupSize();
            ComboSetupQuality();

        }

        private void ComboSetupQuality()
        {
            comboBoxQuality.Items.Add("Новий");
            comboBoxQuality.Items.Add("Б/У");
        }

        private void CombosetupSize()
        {
            comboBoxSize.Items.Add("11");
            comboBoxSize.Items.Add("14");
            comboBoxSize.Items.Add("15.6");
            comboBoxSize.Items.Add("17");
        }

        private void AddNewItem(XmlNode nod)
        {

            if (!comboBoxCompany.Items.Contains(nod.SelectSingleNode(@"Company").Value))
            {
                comboBoxCompany.Items.Add(nod.SelectSingleNode(@"Company").Value);
            }
            if (!comboBoxModel.Items.Contains(nod.SelectSingleNode(@"Model").Value))
            {
                comboBoxModel.Items.Add(nod.SelectSingleNode(@"Model").Value);
            }
            if (!comboBoxQuality.Items.Contains(nod.SelectSingleNode(@"Price").Value))
            {
                comboBoxQuality.Items.Add(nod.SelectSingleNode(@"Price").Value);
            }
            if (!comboBoxQuality.Items.Contains(nod.SelectSingleNode(@"Rating").Value))
            {
                comboBoxQuality.Items.Add(nod.SelectSingleNode(@"Rating").Value);
            }


        }

        class Notebooks 
        {
            public string Company;
            public string Model;
            public string Price;
            public string Size;
            public string Rating;
            public string Quality;
        }

        interface XMLStrategy
        {
            List<Notebooks> Search (Notebooks nb);
        }

        class XmlDOMStrategy : XMLStrategy
        {
            public List<Notebooks> Search (Notebooks nb)
            {
                List<Notebooks> res = new List<Notebooks>();
                XmlDocument doc = new XmlDocument();
                doc.Load(@"C:\Users\user\source\repos\LR2\Gadgets.xml");

                XmlNode nod = doc.DocumentElement;
                foreach(XmlNode node in nod.ChildNodes)
                {
                    string Company = "";
                    string Model = "";
                    string Price = "";
                    string Rating = "";
                    string Size = "";
                    string Quality = "";

                    foreach (XmlAttribute att in node.Attributes)
                    {
                        if (att.Name.Equals("Company") && (att.Value.Equals(nb.Company) || nb.Company.Equals(String.Empty)))
                        {
                            Company = att.Value;
                        }

                        if (att.Name.Equals("Model") && (att.Value.Equals(nb.Model) || nb.Model.Equals(String.Empty)))
                        {
                            Model = att.Value;
                        }

                        if (att.Name.Equals("Price") && (att.Value.Equals(nb.Price) || nb.Price.Equals(String.Empty)))
                        {
                            Price = att.Value;
                        }

                        if (att.Name.Equals("Rating") && (att.Value.Equals(nb.Rating) || nb.Rating.Equals(String.Empty)))
                        {
                            Rating = att.Value;
                        }                     

                        if (att.Name.Equals("Quality") && (att.Value.Contains(nb.Quality) || nb.Quality.Equals(String.Empty)))
                        {
                            Quality = att.Value;
                        }

                        if (att.Name.Equals("Size") && (att.Value.Contains(nb.Size) || nb.Size.Equals(String.Empty)))
                        {
                            Size = att.Value;
                        }

                    }

                    if (Company != "" && Model != "" && Price != "" && Quality != "" && Rating != "" && Size != "")
                    {
                        Notebooks newnb = new Notebooks();
                        newnb.Company = Company;
                        newnb.Model = Model;
                        newnb.Price = Price;
                        newnb.Quality = Quality;
                        newnb.Rating = Rating;
                        newnb.Size = Size;

                        res.Add(newnb);
                    }
                }
                return res;
            }
        }

        class XmlSAXStrategy : XMLStrategy
        {
            public List<Notebooks> Search (Notebooks nb)
            {
                List<Notebooks> SAXres = new List<Notebooks>();
                var xmlReader = new XmlTextReader(@"C:\Users\user\source\repos\LR2\Gadgets.xml");

                while (xmlReader.Read())
                {
                    if (xmlReader.HasAttributes)
                    {
                        while (xmlReader.MoveToNextAttribute())
                        {
                            string Company = "";
                            string Model = "";
                            string Price = "";
                            string Rating = "";
                            string Quality = "";
                            string Size = "";

                            if (xmlReader.Name.Equals("Company") && (xmlReader.Value.Equals(nb.Company) || nb.Company.Equals(String.Empty)))
                            {
                                Company = xmlReader.Value;

                                xmlReader.MoveToNextAttribute();
                                if (xmlReader.Name.Equals("Model") && (xmlReader.Value.Equals(nb.Model) || nb.Model.Equals(String.Empty)))
                                {
                                    Model = xmlReader.Value;

                                    xmlReader.MoveToNextAttribute();
                                    if (xmlReader.Name.Equals("Price") && (xmlReader.Value.Equals(nb.Price) || nb.Price.Equals(String.Empty)))
                                    {
                                        Price = xmlReader.Value;

                                        xmlReader.MoveToNextAttribute();
                                        if (xmlReader.Name.Equals("Rating") && (xmlReader.Value.Equals(nb.Rating) || nb.Rating.Equals(String.Empty)))
                                        {
                                            Rating = xmlReader.Value;

                                            xmlReader.MoveToNextAttribute();
                                            if (xmlReader.Name.Equals("Quality") && (xmlReader.Value.Contains(nb.Quality) || nb.Quality.Equals(String.Empty)))
                                            {
                                                Quality = xmlReader.Value;

                                                xmlReader.MoveToNextAttribute();
                                                if (xmlReader.Name.Equals("Size") && (xmlReader.Value.Contains(nb.Quality) || nb.Size.Equals(String.Empty)))
                                                {
                                                    Size = xmlReader.Value;
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            if (Company != "" && Model != "" && Price != "" && Rating != "" && Quality != "" && Size != "")
                            {
                                Notebooks newnb = new Notebooks();
                                newnb.Company = Company;
                                newnb.Model = Model;
                                newnb.Price = Price;
                                newnb.Quality = Quality;
                                newnb.Rating = Rating;
                                newnb.Size = Size;

                                SAXres.Add(newnb);
                            }
                        }
                    }
                }

                xmlReader.Close();
                return SAXres;
            }
        }

        class XmlLTXStrategy : XMLStrategy 
        {
            public List<Notebooks> Search(Notebooks nb)
            {
                List<Notebooks> LTXres = new List<Notebooks>();
                var doc = XDocument.Load(@"C:\Users\user\source\repos\LR2\Gadgets.xml");
                var result = from obj in doc.Descendants("Notebook")
                             where
                             (
                             (obj.Attribute("Company").Value.Equals(nb.Company) || nb.Company.Equals(String.Empty)) &&
                             (obj.Attribute("Model").Value.Equals(nb.Model) || nb.Model.Equals(String.Empty)) &&
                             (obj.Attribute("Price").Value.Equals(nb.Price) || nb.Price.Equals(String.Empty)) &&
                             (obj.Attribute("Rating").Value.Equals(nb.Rating) || nb.Rating.Equals(String.Empty)) &&
                             (obj.Attribute("Quality").Value.Contains(nb.Quality) || nb.Quality.Equals(String.Empty)) &&
                             (obj.Attribute("Size").Value.Contains(nb.Size) || nb.Size.Equals(String.Empty))
                             )
                             select new
                             {
                                 company = (string)obj.Attribute("Company"),
                                 model = (string)obj.Attribute("Model"),
                                 price = (string)obj.Attribute("Price"),
                                 rating = (string)obj.Attribute("Rating"),
                                 quality = (string)obj.Attribute("Quality"),
                                 size = (string)obj.Attribute("Size")
                             };
                foreach(var n in result)
                {
                    Notebooks newnb = new Notebooks();
                    newnb.Company = n.company;
                    newnb.Model = n.model;
                    newnb.Price = n.price;
                    newnb.Rating = n.rating;
                    newnb.Quality = n.quality;
                    newnb.Size = n.size;

                    LTXres.Add(newnb);
                }
                return LTXres;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            rtb.Text = "";
        }

        private void checkBoxSize_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Find_Click(object sender, EventArgs e)
        {
            find();
        }
        private void find()
        {
            rtb.Text = "";
            Notebooks nb = new Notebooks();

            if (checkBoxCompany.Checked) { nb.Company = comboBoxCompany.SelectedItem.ToString(); }
            if (checkBoxModel.Checked) { nb.Model = comboBoxModel.SelectedItem.ToString(); }
            if (checkBoxPrice.Checked) { nb.Company = comboBoxPrice.SelectedItem.ToString(); }
            if (checkBoxRating.Checked) { nb.Company = comboBoxRating.SelectedItem.ToString(); }
            if (checkBoxQuality.Checked) { nb.Company = comboBoxQuality.SelectedItem.ToString(); }
            if (checkBoxSize.Checked) { nb.Company = comboBoxSize.SelectedItem.ToString(); }

            XMLStrategy an = new XmlDOMStrategy();

            if (radioButtondom.Checked) an = new XmlDOMStrategy();
            if (radioButtonsax.Checked) an = new XmlSAXStrategy();
            if (radioButtonLtx.Checked) an = new XmlLTXStrategy();
            

        }
    }
}
