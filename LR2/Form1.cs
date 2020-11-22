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
using System.Xml.Xsl;

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
            XmlNodeList chnodes = xroot.SelectNodes("Notebook");

            for (int i = 0; i < chnodes.Count; i++)
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

            if (!comboBoxCompany.Items.Contains(nod.SelectSingleNode("@Company").Value))
            {
                comboBoxCompany.Items.Add(nod.SelectSingleNode("@Company").Value);
            }
            if (!comboBoxModel.Items.Contains(nod.SelectSingleNode("@Model").Value))
            {
                comboBoxModel.Items.Add(nod.SelectSingleNode("@Model").Value);
            }
            if (!comboBoxPrice.Items.Contains(nod.SelectSingleNode("@Price").Value))
            {
                comboBoxPrice.Items.Add(nod.SelectSingleNode("@Price").Value);
            }
            if (!comboBoxRating.Items.Contains(nod.SelectSingleNode("@Rating").Value))
            {
                comboBoxRating.Items.Add(nod.SelectSingleNode("@Rating").Value);
            }
        }

        class Notebooks 
        {
            public string Company = "";
            public string Model = "";
            public string Price = "";
            public string Size = "";
            public string Rating = "";
            public string Quality = "";
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
            checkBoxCompany.Checked = false;
            checkBoxModel.Checked = false;
            checkBoxPrice.Checked = false;
            checkBoxQuality.Checked = false;
            checkBoxRating.Checked = false;
            checkBoxSize.Checked = false;
            radioButtondom.Checked = false;
            radioButtonLtx.Checked = false;
            radioButtonsax.Checked = false;
            comboBoxCompany.SelectedIndex = -1;
            comboBoxModel.SelectedIndex = -1;
            comboBoxPrice.SelectedIndex = -1;
            comboBoxQuality.SelectedIndex = -1;
            comboBoxRating.SelectedIndex = -1;
            comboBoxSize.SelectedIndex = -1;
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
            if (checkBoxPrice.Checked) { nb.Price = comboBoxPrice.SelectedItem.ToString(); }
            if (checkBoxRating.Checked) { nb.Rating = comboBoxRating.SelectedItem.ToString(); }
            if (checkBoxQuality.Checked) { nb.Quality = comboBoxQuality.SelectedItem.ToString(); }
            if (checkBoxSize.Checked) { nb.Size = comboBoxSize.SelectedItem.ToString(); }

            XMLStrategy an = new XmlDOMStrategy();

            if (radioButtondom.Checked) an = new XmlDOMStrategy();
            if (radioButtonsax.Checked) an = new XmlSAXStrategy();
            if (radioButtonLtx.Checked) an = new XmlLTXStrategy();

            List<Notebooks> res = an.Search(nb);
            CreateXMLTransform(res);
            foreach (Notebooks ntb in res)
            {
                rtb.Text += "Компанія - " + ntb.Company + "\n";
                rtb.Text += "Модель - " + ntb.Model + "\n";
                rtb.Text += "Ціна - " + ntb.Price + "\n";
                rtb.Text += "Рейтинг - " + ntb.Rating + "\n";
                rtb.Text += "Стан - " + ntb.Quality + "\n";
                rtb.Text += "Розмір екрана - " + ntb.Size + "\n";
                rtb.Text +=  "\n\n\n";
            }
        }

        private void transform_Click(object sender, EventArgs e)
        {
            transform1();
        }

        private void transform1()
        {
            XslCompiledTransform xct = new XslCompiledTransform();
            xct.Load(@"C:\Users\user\source\repos\LR2\xsldoc.xsl");
            string link1 = @"C:\Users\user\source\repos\LR2\helpxml.xml";
            string link2 = @"C:\Users\user\source\repos\LR2\helpxml.html";
            xct.Transform(link1, link2);
        }
        private void CreateXMLTransform(List<Notebooks> list)
        {
            var xmlDoc = new XmlDocument();
            XmlElement el;
            int childCounter;

            xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null));

            el = xmlDoc.CreateElement("Notebooks");
            xmlDoc.AppendChild(el);

            int size = list.Count();

            for (childCounter = 0; childCounter < size; childCounter++)
            {
                XmlElement childelmt;
                XmlAttribute childattr;

                childelmt = xmlDoc.CreateElement("Notebook");

                childattr = xmlDoc.CreateAttribute("Company");
                childattr.Value = list[childCounter].Company;
                childelmt.Attributes.Append(childattr);

                childattr = xmlDoc.CreateAttribute("Model");
                childattr.Value = list[childCounter].Model;
                childelmt.Attributes.Append(childattr);

                childattr = xmlDoc.CreateAttribute("Price");
                childattr.Value = list[childCounter].Price;
                childelmt.Attributes.Append(childattr);

                childattr = xmlDoc.CreateAttribute("Rating");
                childattr.Value = list[childCounter].Rating;
                childelmt.Attributes.Append(childattr);

                childattr = xmlDoc.CreateAttribute("Quality");
                childattr.Value = list[childCounter].Quality;
                childelmt.Attributes.Append(childattr);

                childattr = xmlDoc.CreateAttribute("Size");
                childattr.Value = list[childCounter].Size;
                childelmt.Attributes.Append(childattr);

                el.AppendChild(childelmt);
            }

            xmlDoc.Save(@"C:\Users\user\source\repos\LR2\helpxml.xml");
        }

        private void checkBoxCompany_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
