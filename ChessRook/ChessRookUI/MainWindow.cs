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
            _rookInfo = new RookInfo();
            _manager = new Manager();
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
                if (firstValue < secondValue)
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
                if (firstValue < secondValue)
                {
                    upperHeightTextBox.BackColor = Color.LightCoral;
                }
            }
        }

        private void fullHeightTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int value = 0;
                int.TryParse(fullHeightTextBox.Text, out value);
                var isCorrect = _manager.Validation(value, 10, 10000);
                if (isCorrect)
                {
                    fullHeightTextBox.BackColor = Color.LightGreen;
                }
                else
                {
                    fullHeightTextBox.BackColor = Color.LightCoral;
                    buildButton.Enabled = false;
                }
            }
            catch
            {
                fullHeightTextBox.BackColor = Color.LightCoral;
            }
           
            CheckColor();
            CheckNull();
        }

        private void lowerDiameterTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int value = 0;
                int.TryParse(lowerDiameterTextBox.Text, out value);
                var isCorrect = _manager.Validation(value, 5, 500);
                if (isCorrect)
                {
                    lowerDiameterTextBox.BackColor = Color.LightGreen;
                }
                else
                {
                    lowerDiameterTextBox.BackColor = Color.LightCoral;
                    buildButton.Enabled = false;
                }
            }
            catch
            {
                lowerDiameterTextBox.BackColor = Color.LightCoral;
                buildButton.Enabled = false;
            }

            CheckColor();
            CheckDiameter();
            CheckNull();
        }

        private void upperDiameterTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int value = 0;
                int.TryParse(upperDiameterTextBox.Text, out value);
                var isCorrect = _manager.Validation(value, 3, 100);
                if (isCorrect)
                {
                    upperDiameterTextBox.BackColor = Color.LightGreen;
                }
                else
                {
                    upperDiameterTextBox.BackColor = Color.LightCoral;
                    buildButton.Enabled = false;
                }
            }
            catch
            {
                upperDiameterTextBox.BackColor = Color.LightCoral;
                buildButton.Enabled = false;
            }

            CheckColor();
            CheckDiameter();
            CheckNull();
        }

        private void upperHeightTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int value = 0;
                int.TryParse(upperHeightTextBox.Text, out value);
                var isCorrect = _manager.Validation(value, 2, 150);
                if (isCorrect)
                {
                    upperHeightTextBox.BackColor = Color.LightGreen;
                }
                else
                {
                    upperHeightTextBox.BackColor = Color.LightCoral;
                    buildButton.Enabled = false;
                }
            }
            catch
            {
                upperHeightTextBox.BackColor = Color.LightCoral;
                buildButton.Enabled = false;
            }

            CheckColor();
            CheckHeight();
            CheckNull();
        }

        private void lowerHeightTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int value = 0;
                int.TryParse(lowerHeightTextBox.Text, out value);
                var isCorrect = _manager.Validation(value, 2, 150);
                if (isCorrect)
                {
                    lowerHeightTextBox.BackColor = Color.LightGreen;
                }
                else
                {
                    lowerHeightTextBox.BackColor = Color.LightCoral;
                    buildButton.Enabled = false;
                }
            }
            catch
            {
                lowerHeightTextBox.BackColor = Color.LightCoral;
                buildButton.Enabled = false;
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

            _manager.InitializeComponent(_rookInfo);
        }
    }
}
