using KompasApi;
using Rook;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ChessRookUI
{
    //TODO: XML
    public partial class MainWindow : Form
    {
        /// <summary>
        /// Зеленый цвет 
        /// </summary>
        private readonly Color SuccessColor = Color.LightGreen;

        /// <summary>
        /// Красный цвет 
        /// </summary>
        private readonly Color ErrorColor = Color.LightCoral;

        /// <summary>
        /// Белый цвет
        /// </summary>
        private readonly Color EmptyColor = Color.White;

        /// <summary>
        /// Данные для построения ладьи
        /// </summary>
        private RookInfo _rookInfo;

        /// <summary>
        /// Класс для построения модели ладьи
        /// </summary>
        private ModelCreator _modelCreator;

        /// <summary>
        /// Поля для ввода 
        /// </summary>
        private List<Control> _textBoxes;

        /// <summary>
        /// Конструктор MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            buildButton.Enabled = false;

            _rookInfo = new RookInfo();
            _modelCreator = new ModelCreator();

            //TODO: RSDN
            _textBoxes = Controls
                .Cast<Control>()
                .Where(c => c.GetType() == typeof(TextBox))
                .ToList();

            toolTip.SetToolTip(fullHeightTextBox, 
                "Должна быть больше величин D и Е, а также их суммы");
            toolTip.SetToolTip(lowerDiameterTextBox, 
                "Эта величина должна быть больше величины C");
            toolTip.SetToolTip(upperDiameterTextBox,
                "Эта величина должна быть меньше величины В");
            toolTip.SetToolTip(lowerHeightTextBox, 
                "Эта величина должна быть меньше величины Е");
            toolTip.SetToolTip(upperHeightTextBox, 
                "Эта величина должна быть больше величины D");
        }

        /// <summary>
        /// Проверка высоты ладьи
        /// </summary>
        private void CheckHeight()
        {
            if (!string.IsNullOrEmpty(fullHeightTextBox.Text) && 
                !string.IsNullOrEmpty(upperHeightTextBox.Text) && 
                !string.IsNullOrEmpty(lowerHeightTextBox.Text))
            {
                int.TryParse(upperHeightTextBox.Text, out int upper);
                int.TryParse(lowerHeightTextBox.Text, out int lower);
                int.TryParse(fullHeightTextBox.Text, out int full);

                if ((upper + lower) >= full)
                {
                    DrawRed(fullHeightTextBox);
                }
                else
                {
                    DrawGreen(fullHeightTextBox);
                }
            }
        }

        /// <summary>
        /// Проверка цвета
        /// </summary>
        private void CheckColor()
        {
            if (_textBoxes.All(t => t.BackColor == SuccessColor))
            {
                buildButton.Enabled = true;
            } 
        }
        
        /// <summary>
        /// Проверка на соблюдение зависимостей
        /// </summary>
        /// <param name="upperBox"> textbox </param>
        /// <param name="lowerBox"> textbox </param>
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

        /// <summary>
        /// Проверка на пустые поля 
        /// </summary>
        private void CheckNull()
        {
            foreach (var textbox in _textBoxes)
            {
                if (textbox.Text == "")
                {
                    textbox.BackColor = EmptyColor;
                }
            }
        }

        /// <summary>
        /// Перекрашивание textbox в красный
        /// </summary>
        /// <param name="box"> textbox, который надо перекрасить </param>
        private void DrawRed(TextBox box)
        {
            box.BackColor = ErrorColor;
            buildButton.Enabled = false;
        }

        /// <summary>
        /// Перекрашивание textbox в зеленый
        /// </summary>
        /// <param name="box"> textbox, который надо перекрасить </param>
        private void DrawGreen(TextBox box)
        {
            box.BackColor = SuccessColor;
        }

        /// <summary>
        /// Все проверки (мы против дублирования)
        /// </summary>
        private void Checking()
        {
            CheckDepencies(upperHeightTextBox, fullHeightTextBox);
            CheckDepencies(lowerHeightTextBox, fullHeightTextBox);
            CheckHeight();
            CheckDepencies(upperDiameterTextBox, lowerDiameterTextBox);
            CheckDepencies(lowerHeightTextBox, upperHeightTextBox);
            CheckNull();
            CheckColor();
        }

        /// <summary>
        /// Парсинг и валидация введенных значений
        /// </summary>
        /// <param name="box"> textbox, из которого берутся значения </param>
        /// <param name="min"> нижняя граница диапазона </param>
        /// <param name="max"> верхняяя граница диапазона </param>
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

        private void TextChangedChecking(TextBox box,int min,int max)
        {
            ParsingAndValidation(box, min, max);
            Checking();
        }

        //TODO: занести диапазоны в параметры
        //tODO: Дублирования
        /// <summary>
        /// Обработка события: изменение текста в fullHeightTextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fullHeightTextBox_TextChanged(object sender, EventArgs e)
        {
            TextChangedChecking(fullHeightTextBox, 
                _rookInfo.FULL_HEIGHT_MIN, _rookInfo.FULL_HEIGHT_MAX);
        }

        /// <summary>
        /// Обработка события: изменение текста в lowerDiameterTextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lowerDiameterTextBox_TextChanged(object sender, EventArgs e)
        {
            TextChangedChecking(lowerDiameterTextBox, 
                _rookInfo.LOWER_BASE_DIAMETER_MIN, _rookInfo.LOWER_BASE_DIAMETER_MAX);
        }

        /// <summary>
        /// Обработка события: изменение текста в upperDiameterTextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void upperDiameterTextBox_TextChanged(object sender, EventArgs e)
        {
            TextChangedChecking(upperDiameterTextBox, 
                _rookInfo.UPPER_BASE_DIAMETER_MIN, _rookInfo.UPPER_BASE_DIAMETER_MAX);
        }

        /// <summary>
        /// Обработка события: изменение текста в upperHeightTextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void upperHeightTextBox_TextChanged(object sender, EventArgs e)
        {
            TextChangedChecking(upperHeightTextBox, 
                _rookInfo.UPPER_BASE_HEIGHT_MIN, _rookInfo.UPPER_BASE_HEIGHT_MAX);
        }

        /// <summary>
        /// Обработка события: изменение текста в lowerHeightTextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lowerHeightTextBox_TextChanged(object sender, EventArgs e)
        {
            TextChangedChecking(lowerHeightTextBox, 
                _rookInfo.LOWER_BASE_HEIGHT_MIN, _rookInfo.LOWER_BASE_HEIGHT_MAX);
        }

        /// <summary>
        /// Обработка события: нажатие на кнопку  buildButton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buildButton_Click(object sender, EventArgs e)
        {
            BuildRook();
        }

        /// <summary>
        /// Построение ладьи
        /// </summary>
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
            _modelCreator.CreateRook(_rookInfo);
        }
    }
}
