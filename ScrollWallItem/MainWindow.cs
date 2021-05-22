using System;
using System.Windows.Forms;

namespace GeodeExampleCSharp
{
    public partial class MainWindow : Form
    {
        public event EventHandler OnWallItemMove;
        public ScrollWallItem swi;
        public bool isFlash = true; // default = flash
        bool isBigSteps;
        public MainWindow()
        {
            InitializeComponent();
            #region Scroll event
            textBox1.MouseWheel += new MouseEventHandler(TextBox1_ScrollWheel);
            textBox2.MouseWheel += new MouseEventHandler(textBox2_ScrollWheel);
            textBox3.MouseWheel += new MouseEventHandler(textBox3_ScrollWheel);
            textBox4.MouseWheel += new MouseEventHandler(textBox4_ScrollWheel);
            #endregion
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            swi = new ScrollWallItem(this);
            TopMost = true;
        }

        #region textbox1
        private void TextBox1_ScrollWheel(object sender, MouseEventArgs e)
        {
            HandledMouseEventArgs handledArgs = e as HandledMouseEventArgs;
            handledArgs.Handled = true;

            if (handledArgs.Delta < 0)
            {
                if (isBigSteps == false)
                    swi.w1++;
                else
                    swi.w1 += 10;
            }

            else
            {
                if (isBigSteps == false)
                    swi.w1--;
                else
                    swi.w1 -= 10;
            }

            textBox1.Text = swi.w1.ToString();
        }
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            OnWallItemMove?.Invoke(this, null);
        }
        #endregion

        #region textbox2
        private void textBox2_ScrollWheel(object sender, MouseEventArgs e)
        {
            HandledMouseEventArgs handledArgs = e as HandledMouseEventArgs;
            handledArgs.Handled = true;

            if (handledArgs.Delta < 0)
            {
                if (isBigSteps == false)
                    swi.w2++;
                else
                    swi.w2 += 10;
            }

            else
            {
                if (isBigSteps == false)
                    swi.w2--;
                else
                    swi.w2 -= 10;
            }
            textBox2.Text = swi.w2.ToString();
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            OnWallItemMove?.Invoke(this, null);
        }
        #endregion

        #region textbox3
        private void textBox3_ScrollWheel(object sender, MouseEventArgs e)
        {
            HandledMouseEventArgs handledArgs = e as HandledMouseEventArgs;
            handledArgs.Handled = true;

            if (handledArgs.Delta < 0)
            {
                if (isBigSteps == false)
                    swi.l1++;
                else
                    swi.l1 += 10;
            }

            else
            {
                if (isBigSteps == false)
                    swi.l1--;
                else
                    swi.l1 -= 10;
            }

            textBox3.Text = swi.l1.ToString();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            OnWallItemMove?.Invoke(this, null);
        }
        #endregion

        #region textbox4
        private void textBox4_ScrollWheel(object sender, MouseEventArgs e)
        {
            HandledMouseEventArgs handledArgs = e as HandledMouseEventArgs;
            handledArgs.Handled = true;


            if (handledArgs.Delta < 0)
            {
                if (isBigSteps == false)
                    swi.l2++;
                else
                    swi.l2 += 10;
            }

            else
            {
                if (isBigSteps == false)
                    swi.l2--;
                else
                    swi.l2 -= 10;
            }

            textBox4.Text = swi.l2.ToString();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            OnWallItemMove?.Invoke(this, null);
        }
        #endregion

        #region rotate
        private void button1_Click(object sender, EventArgs e)
        {
            if (swi.rotation == "l") swi.rotation = "r";
            else swi.rotation = "l";

            OnWallItemMove.Invoke(this, null);
        }
        #endregion

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            isBigSteps = !isBigSteps;
        }

        private void unityRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (unityRadioButton.Checked)
                isFlash = false;
        }

        private void flashRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (flashRadioButton.Checked)
                isFlash = true;
        }
    }
}