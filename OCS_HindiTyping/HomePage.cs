using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace OCS_HindiTyping
{
    public partial class HomePage : Form
    {
        int i;
        int cnt;
        int wrong;
        //bool isShift;
        string lession_practice;
        StringReader reader;
        char[] charsRead;
        Dictionary<Button, string> dict = new Dictionary<Button, string>();
        public HomePage()
        {
            wrong = 0;
            InitializeComponent();
            cnt = 1;
            dict.Add(button1, "~`");
            dict.Add(button2, "!1");
            dict.Add(button3, "@2");
            dict.Add(button4, "#3");
            dict.Add(button5, "$4");
            dict.Add(button6, "%5");
            dict.Add(button7, "^6");
            dict.Add(button8, "&7");
            dict.Add(button9, "*8");
            dict.Add(button10, "(9");
            dict.Add(button11, ")0");
            dict.Add(button12, "_-");
            dict.Add(button13, "+=");
            btnShowHide.Image.Tag = "1";
            btnShowHide_Click(null, null);
            lession_practice = "practice";
        }



        private void loadData(int num)
        {
           
           // isShift = false;
            string startupPath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName, ("Content\\" + lession_practice + "0" + num + ".txt"));
            // Read the file as one string. 
            //string text = System.IO.File.ReadAllText(startupPath);

            //var path = Path.GetFullPath("\\Content\\" + lession_practice + "0" + num + ".txt");
            label1.Text = File.ReadAllText(startupPath);
            label1.Text = label1.Text.Replace("�", "");
            label1.Text = label1.Text.Replace("  ", " ");
            label1.Text = label1.Text.Replace("\n", " ");
            label1.Text = label1.Text.Replace("\t", " ");
            label2.Text = "";
            reader = null;
            charsRead = null;
            charsRead = new char[label1.Text.Length];
            reader = new StringReader(label1.Text);
            reader.Read(charsRead, 0, label1.Text.Length);

            //label1.Size = new Size(this.Width-15, label1.Height);
                     
            //groupBox2.Size = new Size(this.Width-10, groupBox2.Height);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadData(1);

            // textBox1.Text = File.ReadAllText("E:\\DotNet-Practices\\OCS_HindiTyping\\OCS_HindiTyping\\Content\\practice01.txt");
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var isShiftPressed = false;
            //if ((keyData == (Keys.ShiftKey | Keys.Shift)))
            //{
            //    isShift = true;
            //    return true;
            //}
            //else
            //    isShift = false;


            // StringBuilder reformattedText = new StringBuilder();
            // using (StringWriter writer = new StringWriter(reformattedText))
            // {
            int length = charsRead.Length;
            Console.WriteLine(charsRead[i]);
            Console.WriteLine(((char)keyData).ToString());
            if (keyData == Keys.Back && label2.Text.Length > 0)
            {
                i--;
                label2.Text = label2.Text.Substring(0, label2.Text.Length - 1);
                return base.ProcessCmdKey(ref msg, keyData);
            }
            Keys code = keyData & Keys.KeyCode;
            Keys Ktype = (Keys)code;
            Keys kfile = (Keys)charsRead[i];
            KeysConverter kc = new KeysConverter();
            string keyChar = kc.ConvertToString(keyData);
            if (keyChar.Contains("Shift"))
                isShiftPressed = true;
            else
                isShiftPressed = false;
            chageCharBySign(ref keyChar);
            keyChar = keyChar.Replace("Shift+", "");
            var charValue = charsRead[i].ToString();//.ToUpper();
            charValue = charValue.Replace("\r", "Enter");
            charValue = charValue.Replace("\n", "Enter");
            keyChar = keyChar.Replace("Oemtilde", "~");
            if (keyChar == "Oemtilde")
            {
            }
            if (isShiftPressed)
            {
            }

            //if (charValue == ((char)keyData).ToString())
            //{
            //}
            if (charValue == keyChar.ToLower() || (charValue == keyChar.ToUpper() && isShiftPressed))
            {
                if (charValue == "Enter")
                {
                    label2.Text = "";
                    label2.Left = label1.Left;
                    label2.Width = 0;
                    label2.Top += 22;
                }
                label2.Text += charsRead[i].ToString();
                i++;
                //if (char.IsUpper(charsRead[i]))
                //    isShift = true;
                setImage(charsRead[i].ToString());
            }
            else
            {
                wrong = wrong + 1;
                label52.Text = wrong.ToString();
            }
            if (label2.Width >= label1.Width)
            {
                label2.Text = "";
                label2.Left = label1.Left;
                label2.Width = 0;
                label2.Top += 22;
            }
            // }
            //if ((keyData == (Keys.ShiftKey | Keys.Shift)))
            //    isShift = true;
            //else
            //    setImage(((char)keyData).ToString());
           // isShift = false;
            return base.ProcessCmdKey(ref msg, keyData);
        }

        static void SetText(TextBox textBox, string str)
        {
            Graphics graphics = textBox.CreateGraphics();
            SizeF size = graphics.MeasureString(str, textBox.Font);
            graphics.Dispose();
            textBox.Width = (int)Math.Round(size.Width);
            textBox.Text = str;

        }

        private void setImage(string tag, bool shifPressed = false)
        {

            var aa = dict.Where(a => a.Value.Contains(tag)).FirstOrDefault().Key;
            if (aa != null)
            {
                Button btn = (Button)aa;
                btn.Image = Resources.down;
            }
            Control allBtns = null;
            List<Button> btnList = new List<Button>();
            for (int i = 0; i < 70; i++)
            {
                allBtns = this.groupBox1.Controls.Find("button" + i, false).SingleOrDefault();
                if (allBtns != null)
                {
                    btnList.Add((Button)allBtns);
                }
            }
            if (tag == " ")
            {
                foreach (var item in btnList)
                {
                    item.Image = null;
                }
                button58.BackColor = Color.LightGreen;
            }
            else
            {
                foreach (var item in btnList)
                {

                    Console.WriteLine(item.Tag.ToString().IndexOf(tag));
                    if (item.Tag.ToString().Contains(tag) && item.Tag.ToString().Length < 3)
                    {
                        if (char.IsUpper(tag, 0) || shifPressed)
                            item.Image = Resources.up;
                        else
                            item.Image = Resources.down;
                        //if (isShift)
                        //{
                        //    item.Image = Resources.up;
                        //}
                        //else
                        //    item.Image = Resources.down;
                    }
                    else
                    {
                        // if (item.TabIndex != 0 && item.TabIndex != 16)
                        item.Image = null;
                        item.BackColor = Color.Transparent;
                    }
                }
            }
            //isShift = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnShowHide_Click(object sender, EventArgs e)
        {
            if (btnShowHide.Image.Tag.ToString() == "0")
            {
                btnShowHide.Image = Resources.toogleoff;
                btnShowHide.Image.Tag = "1";
            }
            else
            {
                btnShowHide.Image = Resources.toggleon;
                btnShowHide.Image.Tag = "0";
            }
            Control allBtns = null;
            List<Label> btnList = new List<Label>();
            for (int i = 0; i < 55; i++)
            {
                allBtns = this.groupBox1.Controls.Find("Label" + i, false).SingleOrDefault();
                if (allBtns != null)
                {
                    if (allBtns.Visible)
                        allBtns.Visible = false;
                    else
                        allBtns.Visible = true;
                    //btnList.Add((Label)allBtns);
                }
            }
            button58.Focus();
        }

        static void chageCharBySign(ref string sgn)
        {
            if (sgn.IndexOf("OemPlus", StringComparison.CurrentCultureIgnoreCase) >= 0)
                sgn = "=";
            else if (sgn.IndexOf("Shift+OemOpenBrackets", StringComparison.CurrentCultureIgnoreCase) >= 0)
                sgn = "+";

            if (sgn.IndexOf("OemMinus", StringComparison.CurrentCultureIgnoreCase) >= 0)
                sgn = "-";
            else if (sgn.IndexOf("Shift+OemMinus", StringComparison.CurrentCultureIgnoreCase) >= 0)
                sgn = "_";

            else if (sgn.IndexOf("OemOpenBrackets", StringComparison.CurrentCultureIgnoreCase) >= 0)
                sgn = "[";
            else if (sgn.IndexOf("Shift+OemOpenBrackets", StringComparison.CurrentCultureIgnoreCase) >= 0)
                sgn = "{";

            else if (sgn.IndexOf("OemCloseBrackets", StringComparison.CurrentCultureIgnoreCase) >= 0)
                sgn = "}";
            else if (sgn.IndexOf("Shift+OemCloseBrackets", StringComparison.CurrentCultureIgnoreCase) >= 0)
                sgn = "]";

            else if (sgn.IndexOf("OemPipe", StringComparison.CurrentCultureIgnoreCase) >= 0 || sgn.IndexOf("Oem5", StringComparison.CurrentCultureIgnoreCase) >= 0)
                sgn = "|";
            else if (sgn.IndexOf("Shift+OemPipe", StringComparison.CurrentCultureIgnoreCase) >= 0 || sgn.IndexOf("Shift+Oem5", StringComparison.CurrentCultureIgnoreCase) >= 0)
                sgn = "\\";


            else if (sgn.IndexOf("OemSemicolon", StringComparison.CurrentCultureIgnoreCase) >= 0 || sgn == "Oem1" || sgn == "oem1")
                sgn = ";";
            else if (sgn.IndexOf("Shift+OemSemicolon", StringComparison.CurrentCultureIgnoreCase) >= 0 || sgn.IndexOf("Shift+Oem1", StringComparison.CurrentCultureIgnoreCase) >= 0)
                sgn = ":";

            else if (sgn.IndexOf("OemQuotes", StringComparison.CurrentCultureIgnoreCase) >= 0 || sgn == "Oem7" || sgn == "oem7")
                sgn = "'";
            else if (sgn == "Shift+OemQuotes" || sgn.IndexOf("Shift+Oem7", StringComparison.CurrentCultureIgnoreCase) >= 0)
            {
                sgn = "#";
                sgn = sgn.Replace('#', '"');
            }
            else if (sgn.IndexOf("OemComma", StringComparison.CurrentCultureIgnoreCase) >= 0)
                sgn = ",";
            else if (sgn.IndexOf("Shift+OemComma", StringComparison.CurrentCultureIgnoreCase) >= 0)
                sgn = "<";

            else if (sgn.IndexOf("OemPeriod", StringComparison.CurrentCultureIgnoreCase) >= 0)
                sgn = ".";
            else if (sgn.IndexOf("Shift+OemPeriod", StringComparison.CurrentCultureIgnoreCase) >= 0)
                sgn = ">";

            else if (sgn.IndexOf("OemQuestion", StringComparison.CurrentCultureIgnoreCase) >= 0)
                sgn = "/";
            else if (sgn.IndexOf("Shift+OemQuestion", StringComparison.CurrentCultureIgnoreCase) >= 0)
                sgn = "?";

            else if (sgn.IndexOf("Space", StringComparison.CurrentCultureIgnoreCase) >= 0)
                sgn = " ";
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (cnt < 7)
            {
                wrong = 0; label52.Text = "0";
                i = 0;
                cnt += 1;
                loadData(cnt);
            }
            button58.Focus();
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            if (cnt > 1)
            {
                wrong = 0;
                label52.Text = "0";
                i = 0;
                cnt -= 1;
                loadData(cnt);
            }
            button58.Focus();
        }

        private void rdbPractice_CheckedChanged(object sender, EventArgs e)
        {
            wrong = 0; label52.Text = "0";
            i = 0;
            lblCourse.Text = "Practice";
            lession_practice = "practice";
            cnt = 1;
            loadData(1);
        }

        private void rdbLession_CheckedChanged(object sender, EventArgs e)
        {
            wrong = 0; label52.Text = "0";
            i = 0;
            lblCourse.Text = "Lesson";
            lession_practice = "lesson";
            cnt = 1;
            loadData(1);
        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
