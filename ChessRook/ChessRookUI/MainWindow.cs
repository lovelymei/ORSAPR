using ChessRook;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ChessRookUI
{
    public partial class MainWindow : Form
    {
        private RookInfo _rookInfo;
        private Manager _manager;

        public MainWindow()
        {
            InitializeComponent();
            pictureBox1.Image = Image.FromFile("C:\\Users\\user\\source\\repos\\ORSAPR\\ChessRook\\1.png");
            pictureBox2.Image = Image.FromFile("C:\\Users\\user\\source\\repos\\ORSAPR\\ChessRook\\2.png");
            pictureBox3.Image = Image.FromFile("C:\\Users\\user\\source\\repos\\ORSAPR\\ChessRook\\3.png");
            button1.Enabled = false;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void CheckColor()
        {
            if ((textBox1.BackColor == Color.LightGreen) && (textBox2.BackColor == Color.LightGreen)
                && (textBox3.BackColor == Color.LightGreen) && (textBox4.BackColor == Color.LightGreen)
                && (textBox5.BackColor == Color.LightGreen))
                button1.Enabled = true;
        }


        private void CheckDiameter()
        {
            if ((textBox2.Text != null) && (textBox3.Text != null))
            {
                int firstValue = 0, secondValue = 0;
                int.TryParse(textBox2.Text, out firstValue);
                int.TryParse(textBox3.Text, out secondValue);
                if (firstValue > secondValue)
                {
                    textBox2.BackColor = Color.LightCoral;
                }
            }
        }

        private void CheckNull()
        {
            if (textBox1.Text == "")
                textBox1.BackColor = Color.White;

            if (textBox2.Text == "")
                textBox2.BackColor = Color.White;

            if (textBox3.Text == "")
                textBox3.BackColor = Color.White;

            if (textBox4.Text == "")
                textBox4.BackColor = Color.White;

            if (textBox5.Text == "")
                textBox5.BackColor = Color.White;
        }

        private void CheckHeight()
        {
            if ((textBox4.Text != null) && (textBox5.Text != null))
            {
                int firstValue = 0, secondValue = 0;
                int.TryParse(textBox4.Text, out firstValue);
                int.TryParse(textBox5.Text, out secondValue);
                if (firstValue > secondValue)
                {
                    textBox4.BackColor = Color.LightCoral;
                }
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int value = 0;
            int.TryParse(textBox1.Text, out value); 

            if ((value > 10000) || (value < 10))
            {
                textBox1.BackColor = Color.LightCoral;
                button1.Enabled = false;
            }
            else
            {
                textBox1.BackColor = Color.LightGreen;
            }
            CheckColor();
            CheckNull();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int value;
            int.TryParse(textBox2.Text, out value);
            

            if ((value > 500) || (value < 5))
            {
                textBox2.BackColor = Color.LightCoral;
                button1.Enabled = false;
            }
            else
            {
                textBox2.BackColor = Color.LightGreen;
            }
            CheckColor();
            CheckDiameter();
            CheckNull();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            int value;
            int.TryParse(textBox3.Text, out value);

            if ((value > 100) || (value < 3))
            {
                textBox3.BackColor = Color.LightCoral;
                button1.Enabled = false;
            }
            else
            {
                textBox3.BackColor = Color.LightGreen;
            }
            CheckColor();
            CheckDiameter();
            CheckNull();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            int value;
            int.TryParse(textBox4.Text, out value);

            if ((value > 150) || (value < 2))
            {
                textBox4.BackColor = Color.LightCoral;
                button1.Enabled = false;
            }
            else
            {
                textBox4.BackColor = Color.LightGreen;
            }
            CheckColor();
            CheckHeight();
            CheckNull();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            int value;
            int.TryParse(textBox5.Text, out value);

            if ((value > 100) || (value < 3))
            {
                textBox5.BackColor = Color.LightCoral;
                button1.Enabled = false;
            }
            else
            {
                textBox5.BackColor = Color.LightGreen;
            }
            CheckColor();
            CheckHeight();
            CheckNull();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int textFirst;
            int.TryParse(textBox2.Text, out textFirst);
            int textSecond;
            int.TryParse(textBox3.Text, out textSecond);
            if (textFirst > textSecond)
            {
                MessageBox.Show("Диаметр нижнего основания не может быть больше диаметра верхнего", "Ошибка валидации", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            int.TryParse(textBox4.Text, out textFirst);
            int.TryParse(textBox5.Text, out textSecond);
            if (textFirst > textSecond)
            {
                MessageBox.Show("Высота нижнего основания не может быть больше высоты верхнего","Ошибка валидации", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            //_rookInfo = new RookInfo
            //{
            //    FullHeight = int.Parse(textBox1.Text),
            //    LowerBaseDiameter
            //};
            
            _manager = new Manager();
            _manager.InitializeComponent(_rookInfo);
        }


    }
}
