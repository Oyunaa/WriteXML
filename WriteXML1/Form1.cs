using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WriteXML1
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=Oyunjargal_g;Initial Catalog=MICS;User Id=qw;Password=123456");
        DataSet ds = null;
        SqlCommand cmd = null;
        SqlDataAdapter sda = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            string str = "";
            XmlDocument doc = new XmlDocument();
            doc.Load(@"\test.xml");



           //< qstn >
           //     < preQTxt >
           //     test
           //     </ preQTxt >

           //     < qstnLit >
           //     During any of the antenatal visits for your pregnancy with(name), did you receive the following counselling ?

           //     </ qstnLit >

           //     < postQTxt >
           //     test1
           //     </ postQTxt >

           //     < ivuInstr >
           //     test2
           //     </ ivuInstr >
           // </ qstn >

            str = "select * from [MICS].[dbo].[question] where YearCode = '2015' and AimagCode is null ";
            try
            {
                ds = new DataSet();
                con.Open();
                cmd = new SqlCommand(str, con);
                sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
                //cmd.ExecuteNonQuery();
            }
            catch (Exception ex){ }
            finally
            {
                con.Dispose();
                con.Close();
            }

            //create node and add value
          



            XmlNodeList aNodes = doc.SelectNodes("/dataDscr/var");


            try
            {
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            // loop through all AID nodes
                            foreach (XmlNode aNode in aNodes)
                            {
                                XmlAttribute nameAttribute = aNode.Attributes["name"];

                                XmlAttribute filesAttribute = aNode.Attributes["files"];


                                string currentValue = nameAttribute.Value;
                                string currentFilesValue = filesAttribute.Value;


                                XmlNode qstnLit = null;
                                XmlNode preQTxt = null;
                                XmlNode postQTxt = null;
                                XmlNode ivuInstr = null;







                                if (nameAttribute != null)
                                {

                                    if (currentValue != "")
                                    {

                                        //CM6 == CM6
                                        //if (!currentValue.Contains("_"))   //currentValueLen[0] == ds.Tables[0].Rows[i]["name"].ToString()
                                        //{
                                            if (currentValue == ds.Tables[0].Rows[i]["name"].ToString() && currentFilesValue == ds.Tables[0].Rows[i]["files"].ToString())
                                            {

                                                if (ds.Tables[0].Rows[i]["LiteralQuestion"].ToString() != "" || ds.Tables[0].Rows[i]["PreQuestion"].ToString() != "" || ds.Tables[0].Rows[i]["PostQuestion"].ToString() != "" || ds.Tables[0].Rows[i]["InterviewerInstruction"].ToString() != "")
                                                {
                                                    XmlNode node = doc.CreateNode(XmlNodeType.Element, "qstn", null);
                                                    //node.InnerText = "How many of the following animals does this household have?";

                                                    if (ds.Tables[0].Rows[i]["LiteralQuestion"].ToString() != "")
                                                    {
                                                        qstnLit = doc.CreateElement("qstnLit");
                                                        qstnLit.InnerText = ds.Tables[0].Rows[i]["LiteralQuestion"].ToString();

                                                        node.AppendChild(qstnLit);
                                                    }
                                                    if (ds.Tables[0].Rows[i]["PreQuestion"].ToString() != "")
                                                    {
                                                        preQTxt = doc.CreateElement("preQTxt");
                                                        preQTxt.InnerText = ds.Tables[0].Rows[i]["PreQuestion"].ToString();

                                                        node.AppendChild(preQTxt);
                                                    }
                                                    if (ds.Tables[0].Rows[i]["PostQuestion"].ToString() != "")
                                                    {
                                                        postQTxt = doc.CreateElement("postQTxt");
                                                        postQTxt.InnerText = ds.Tables[0].Rows[i]["PostQuestion"].ToString();

                                                        node.AppendChild(postQTxt);
                                                    }

                                                    if (ds.Tables[0].Rows[i]["InterviewerInstruction"].ToString() != "")
                                                    {
                                                        ivuInstr = doc.CreateElement("ivuInstr");
                                                        ivuInstr.InnerText = ds.Tables[0].Rows[i]["InterviewerInstruction"].ToString();

                                                        node.AppendChild(ivuInstr);
                                                    }

                                                    aNode.AppendChild(node);

                                                }





                                                //XmlNode a = doc.SelectSingleNode("/dataDscr/var");   

                                               



                                            }
                                       // }
                                        //else // CM5_A != CM5    
                                        //{
                                        //    string[] currentValueLen = currentValue.Split('_');

                                           


                                        //    if (currentValueLen[0] == ds.Tables[0].Rows[i]["name"].ToString() && currentFilesValue == ds.Tables[0].Rows[i]["files"].ToString())
                                        //    {
                                        //        if ( ds.Tables[0].Rows[i]["PreQuestion"].ToString() != "" || ds.Tables[0].Rows[i]["PostQuestion"].ToString() != "" || ds.Tables[0].Rows[i]["InterviewerInstruction"].ToString() != "")
                                        //        {

                                        //            XmlNode node = doc.CreateNode(XmlNodeType.Element, "qstn", null);
                                        //            //node.InnerText = "How many of the following animals does this household have?";

                                        //            //if (ds.Tables[0].Rows[i]["LiteralQuestion"].ToString() != "")
                                        //            //{
                                        //            //    qstnLit = doc.CreateElement("qstnLit");
                                        //            //    qstnLit.InnerText = ds.Tables[0].Rows[i]["LiteralQuestion"].ToString();

                                        //            //    node.AppendChild(qstnLit);
                                        //            //}
                                        //            if (ds.Tables[0].Rows[i]["PreQuestion"].ToString() != "")
                                        //            {
                                        //                preQTxt = doc.CreateElement("preQTxt");
                                        //                preQTxt.InnerText = ds.Tables[0].Rows[i]["PreQuestion"].ToString();

                                        //                node.AppendChild(preQTxt);
                                        //            }
                                        //            if (ds.Tables[0].Rows[i]["PostQuestion"].ToString() != "")
                                        //            {
                                        //                postQTxt = doc.CreateElement("postQTxt");
                                        //                postQTxt.InnerText = ds.Tables[0].Rows[i]["PostQuestion"].ToString();

                                        //                node.AppendChild(postQTxt);
                                        //            }

                                        //            if (ds.Tables[0].Rows[i]["InterviewerInstruction"].ToString() != "")
                                        //            {
                                        //                ivuInstr = doc.CreateElement("ivuInstr");
                                        //                ivuInstr.InnerText = ds.Tables[0].Rows[i]["InterviewerInstruction"].ToString();

                                        //                node.AppendChild(ivuInstr);
                                        //            }


                                        //            //XmlNode a = doc.SelectSingleNode("/dataDscr/var");   

                                        //            aNode.AppendChild(node);
                                        //        }



                                        //    }
                                        //}



                                    }
                                }
                            }

                        }



                    }
                }
                // save the XmlDocument back to disk
                doc.Save(@"C:\Users\oyunjargal_g\Desktop\test.xml");
            }
            catch (XmlException exc)
            {
                //invalid file
                MessageBox.Show(exc.ToString());
            }
            finally
            {
                MessageBox.Show("Бичигдэж дууслаа");
            }



   
        }
    }
}
