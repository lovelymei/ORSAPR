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
            buildButton.Enabled = false;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void CheckColor()
        {
            if ((fullHeightTextBox.BackColor == Color.LightGreen) && (lowerDiameterTextBox.BackColor == Color.LightGreen)
                && (upperDiameterTextBox.BackColor == Color.LightGreen) && (upperHeightTextBox.BackColor == Color.LightGreen)
                && (lowerHeightTextBox.BackColor == Color.LightGreen))
                buildButton.Enabled = true;
        }


        private void CheckDiameter()
        {
            if ((lowerDiameterTextBox.Text != null) && (upperDiameterTextBox.Text != null))
            {
                int firstValue = 0, secondValue = 0;
                int.TryParse(lowerDiameterTextBox.Text, out firstValue);
                int.TryParse(upperDiameterTextBox.Text, out secondValue);
                if (firstValue > secondValue)
                {
                    lowerDiameterTextBox.BackColor = Color.LightCoral;
                }
            }
        }

        private void CheckNull()
        {
            if (fullHeightTextBox.Text == "")
                fullHeightTextBox.BackColor = Color.White;

            if (lowerDiameterTextBox.Text == "")
                lowerDiameterTextBox.BackColor = Color.White;

            if (upperDiameterTextBox.Text == "")
                upperDiameterTextBox.BackColor = Color.White;

            if (upperHeightTextBox.Text == "")
                upperHeightTextBox.BackColor = Color.White;

            if (lowerHeightTextBox.Text == "")
                lowerHeightTextBox.BackColor = Color.White;
        }

        private void CheckHeight()
        {
            if ((upperHeightTextBox.Text != null) && (lowerHeightTextBox.Text != null))
            {
                int firstValue = 0, secondValue = 0;
                int.TryParse(upperHeightTextBox.Text, out firstValue);
                int.TryParse(lowerHeightTextBox.Text, out secondValue);
                if (firstValue > secondValue)
                {
                    upperHeightTextBox.BackColor = Color.LightCoral;
                }
            }
        }

        private void fullHeightTextBox_TextChanged(object sender, EventArgs e)
        {
            int value = 0;
            int.TryParse(fullHeightTextBox.Text, out value);

            if ((value > 10000) || (value < 10))
            {
                fullHeightTextBox.BackColor = Color.LightCoral;
                buildButton.Enabled = false;
            }
            else
            {
                fullHeightTextBox.BackColor = Color.LightGreen;
            }
            CheckColor();
            CheckNull();
        }

        private void lowerDiameterTextBox_TextChanged(object sender, EventArgs e)
        {
            int value;
            int.TryParse(lowerDiameterTextBox.Text, out value);


            if ((value > 500) || (value < 5))
            {
                lowerDiameterTextBox.BackColor = Color.LightCoral;
                buildButton.Enabled = false;
            }
            else
            {
                lowerDiameterTextBox.BackColor = Color.LightGreen;
            }
            CheckColor();
            CheckDiameter();
            CheckNull();
        }

        private void upperDiameterTextBox_TextChanged(object sender, EventArgs e)
        {
            int value;
            int.TryParse(upperDiameterTextBox.Text, out value);

            if ((value > 100) || (value < 3))
            {
                upperDiameterTextBox.BackColor = Color.LightCoral;
                buildButton.Enabled = false;
            }
            else
            {
                upperDiameterTextBox.BackColor = Color.LightGreen;
            }
            CheckColor();
            CheckDiameter();
            CheckNull();
        }

        private void upperHeightTextBox_TextChanged(object sender, EventArgs e)
        {

            int value;
            int.TryParse(upperHeightTextBox.Text, out value);

            if ((value > 150) || (value < 2))
            {
                upperHeightTextBox.BackColor = Color.LightCoral;
                buildButton.Enabled = false;
            }
            else
            {
                upperHeightTextBox.BackColor = Color.LightGreen;
            }
            CheckColor();
            CheckHeight();
            CheckNull();
        }

        private void lowerHeightTextBox_TextChanged(object sender, EventArgs e)
        {
            int value;
            int.TryParse(lowerHeightTextBox.Text, out value);

            if ((value > 100) || (value < 3))
            {
                lowerHeightTextBox.BackColor = Color.LightCoral;
                buildButton.Enabled = false;
            }
            else
            {
                lowerHeightTextBox.BackColor = Color.LightGreen;
            }
            CheckColor();
            CheckHeight();
            CheckNull();
        }

        private void buildButton_Click(object sender, EventArgs e)
        {
            int textFirst;
            int.TryParse(lowerDiameterTextBox.Text, out textFirst);
            int textSecond;
            int.TryParse(upperDiameterTextBox.Text, out textSecond);
            if (textFirst > textSecond)
            {
                MessageBox.Show("Диаметр нижнего основания не может быть больше диаметра верхнего", "Ошибка валидации", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            int.TryParse(upperHeightTextBox.Text, out textFirst);
            int.TryParse(lowerHeightTextBox.Text, out textSecond);
            if (textFirst > textSecond)
            {
                MessageBox.Show("Высота нижнего основания не может быть больше высоты верхнего", "Ошибка валидации", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }


            BuildRook();
        }

        private void BuildRook()
        {
            _rookInfo = new RookInfo
            {
                FullHeight = int.Parse(fullHeightTextBox.Text),
                LowerBaseDiameter = int.Parse(lowerDiameterTextBox.Text),
                UpperBaseDiameter = int.Parse(upperDiameterTextBox.Text),
                LowerBaseHeight = int.Parse(lowerHeightTextBox.Text),
                UpperBaseHeight = int.Parse(upperHeightTextBox.Text)
            };

            _manager = new Manager();
            _manager.InitializeComponent(_rookInfo);
        }
    }
}
