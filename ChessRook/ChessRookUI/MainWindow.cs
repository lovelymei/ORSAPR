using ChessRook;
using Rook;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ChessRookUI
{
    public partial class MainWindow : Form
    {
        private RookInfo _rookInfo;
        private Manager _manager;

        private List<Control> _textBoxes;
        public MainWindow()
        {
            InitializeComponent();
            //TODO:
            pictureBox1.Image = Image.FromFile("C:\\Users\\user\\source\\repos\\ORSAPR\\ChessRook\\1.png");
            pictureBox2.Image = Image.FromFile("C:\\Users\\user\\source\\repos\\ORSAPR\\ChessRook\\2.png");
            pictureBox3.Image = Image.FromFile("C:\\Users\\user\\source\\repos\\ORSAPR\\ChessRook\\3.png");

            buildButton.Enabled = false;

            _rookInfo = new RookInfo();
            _manager = new Manager();

            _textBoxes = Controls.Cast<Control>().Where(c => c.GetType() == typeof(TextBox)).ToList();
        }

        private void CheckHeight()
        {
            if ((fullHeightTextBox.Text != null) && 
                (upperHeightTextBox.Text != null) && (lowerHeightTextBox.Text != null))
            {
                int.TryParse(upperHeightTextBox.Text, out int upper);
                int.TryParse(lowerHeightTextBox.Text, out int lower);
                int.TryParse(fullHeightTextBox.Text, out int full);

                if (upper > full)
                {
                    DrawRed(fullHeightTextBox);
                    DrawRed(upperHeightTextBox);
                }
                if (lower > full)
                {
                    DrawRed(fullHeightTextBox);
                    DrawRed(lowerHeightTextBox);
                }

                if ((upper + lower) > full)
                {
                    DrawRed(fullHeightTextBox);
                    DrawRed(upperHeightTextBox);
                    DrawRed(lowerHeightTextBox);
                }
                else
                {
                    DrawGreen(fullHeightTextBox);
                    DrawGreen(upperHeightTextBox);
                    DrawGreen(lowerHeightTextBox);
                }
            }
        }
        private void CheckColor()
        {
            //TODO: RSDN
            if (_textBoxes.All(t => t.BackColor == Color.LightGreen))
                buildButton.Enabled = true;
        }
        
        private void CheckDepencies(TextBox upperBox, TextBox lowerBox)
        {
            if ((upperBox.Text != null) || (lowerBox.Text != null))
            {
                int.TryParse(upperBox.Text, out int upper);
                int.TryParse(lowerBox.Text, out int lower);
                
                if (lower < upper)
                {
                    DrawRed(lowerBox);
                    DrawRed(upperBox);
                }
                else
                {
                    DrawGreen(lowerBox);
                    DrawGreen(upperBox);
                }
            }

        }

        private void CheckNull()
        {
            //TODO: Поработать со списком.
            foreach (var textbox in _textBoxes)
            {
                if (textbox.Text == "")
                {
                    textbox.BackColor = Color.White;
                }
            }
        }

        private void DrawRed(TextBox box)
        {
            box.BackColor = Color.LightCoral;
            buildButton.Enabled = false;
        }

        private void DrawGreen(TextBox box)
        {
            box.BackColor = Color.LightGreen;
        }

        private void Checking()
        {
            CheckHeight();
            CheckDepencies(upperDiameterTextBox, lowerDiameterTextBox);
            CheckDepencies(upperHeightTextBox, lowerHeightTextBox);
            CheckNull();
            CheckColor();
        }

        private void ParsingAndValidation(TextBox box, int min, int max)
        {
            var isParsed = int.TryParse(box.Text, out int value);
            var isCorrect = _rookInfo.Validation(value, min, max);
            if (isParsed && isCorrect)
            {
                DrawGreen(box);
            }
            else
            {
                DrawRed(box);
            }
        }
        private void fullHeightTextBox_TextChanged(object sender, EventArgs e)
        {
            ParsingAndValidation(fullHeightTextBox, 10, 10000);
            Checking();
        }

        private void lowerDiameterTextBox_TextChanged(object sender, EventArgs e)
        {
            ParsingAndValidation(lowerDiameterTextBox, 5, 500);
            Checking();
        }

        private void upperDiameterTextBox_TextChanged(object sender, EventArgs e)
        {
            ParsingAndValidation(upperDiameterTextBox, 3, 100);
            Checking();
        }

        private void upperHeightTextBox_TextChanged(object sender, EventArgs e)
        {
            ParsingAndValidation(upperHeightTextBox, 2, 150);
            Checking();
        }

        private void lowerHeightTextBox_TextChanged(object sender, EventArgs e)
        {
            ParsingAndValidation(lowerHeightTextBox, 2, 150);
            Checking();
        }

        private void buildButton_Click(object sender, EventArgs e)
        {
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
