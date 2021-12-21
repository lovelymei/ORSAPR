using KompasApi;
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

            buildButton.Enabled = false;

            _rookInfo = new RookInfo();
            _manager = new Manager();

            _textBoxes = Controls.Cast<Control>().Where(c => c.GetType() == typeof(TextBox)).ToList();

            toolTip.SetToolTip(fullHeightTextBox, "Должна быть больше величин D и Е, а также их суммы");
            toolTip.SetToolTip(lowerDiameterTextBox, "Эта величина должна быть больше величины C");
            toolTip.SetToolTip(upperDiameterTextBox, "Эта величина должна быть меньше величины В");
            toolTip.SetToolTip(lowerHeightTextBox, "Эта величина должна быть больше величины Е");
            toolTip.SetToolTip(upperHeightTextBox, "Эта величина должна быть меньше величины D");
        }

        private void CheckHeight()
        {
            if (!string.IsNullOrEmpty(fullHeightTextBox.Text) && 
                !string.IsNullOrEmpty(upperHeightTextBox.Text) && 
                !string.IsNullOrEmpty(lowerHeightTextBox.Text))
            {
                int.TryParse(upperHeightTextBox.Text, out int upper);
                int.TryParse(lowerHeightTextBox.Text, out int lower);
                int.TryParse(fullHeightTextBox.Text, out int full);

                if ((upper + lower) > full)
                {
                    DrawRed(fullHeightTextBox);
                }
                else
                {
                    DrawGreen(fullHeightTextBox);
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
            if (!string.IsNullOrEmpty(upperBox.Text) &&
                !string.IsNullOrEmpty(lowerBox.Text))
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
            CheckDepencies(upperHeightTextBox, fullHeightTextBox);
            CheckDepencies(lowerHeightTextBox, fullHeightTextBox);
            CheckHeight();
            CheckDepencies(lowerDiameterTextBox, upperDiameterTextBox);
            CheckDepencies(lowerHeightTextBox, upperHeightTextBox);
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
