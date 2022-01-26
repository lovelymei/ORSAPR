using KompasApi;
using Rook;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ChessRookUI
{
    /// <summary>
    /// Главное окно приложения
    /// </summary>
    public partial class MainWindow : Form
    {
        /// <summary>
        /// Зеленый цвет 
        /// </summary>
        private readonly Color _successColor = Color.LightGreen;

        /// <summary>
        /// Красный цвет 
        /// </summary>
        private readonly Color _errorColor = Color.LightCoral;

        /// <summary>
        /// Белый цвет
        /// </summary>
        private readonly Color _emptyColor = Color.White;

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
        /// Потокобезопасный вызов асинхронного метода
        /// </summary>
        private BackgroundWorker _backgroundWorker =
            new BackgroundWorker();

        /// <summary>
        /// Конструктор MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            buildButton.Enabled = false;

            _rookInfo = new RookInfo();
            _modelCreator = new ModelCreator();
            
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
                "Эта величина должна быть больше величины Е");
            toolTip.SetToolTip(upperHeightTextBox, 
                "Эта величина должна быть меньше величины D");
        }

        /// <summary>
        /// Общий метод валидации
        /// </summary>
        private void Check()
        {
            while (true)
            {
                Thread.Sleep(100);

                CheckDepencies(lowerHeightTextBox, upperHeightTextBox,
                    _rookInfo.LOWER_BASE_HEIGHT_MIN, _rookInfo.LOWER_BASE_HEIGHT_MAX,
                    _rookInfo.UPPER_BASE_HEIGHT_MIN, _rookInfo.UPPER_BASE_HEIGHT_MAX);

                CheckDepencies(lowerDiameterTextBox, upperDiameterTextBox,
                    _rookInfo.LOWER_BASE_DIAMETER_MIN, _rookInfo.LOWER_BASE_DIAMETER_MAX,
                    _rookInfo.UPPER_BASE_DIAMETER_MIN, _rookInfo.UPPER_BASE_DIAMETER_MAX);

                CheckHeight();

                CheckColor();
            }
        }

        /// <summary>
        /// Валидация одного текстового поля
        /// </summary>
        /// <param name="textBox">textbox,значение в котором будет проверяться</param>
        /// <param name="min">минимальное значение</param>
        /// <param name="max">максимальное значение</param>
        /// <returns></returns>
        private Color CheckTextBox(TextBox textBox, int min,int max)
        {
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                var isParsed = int.TryParse(textBox.Text, out int value);
                var isCorrect = _rookInfo.Validation(value, min, max);
                
                if (isParsed && isCorrect)
                {
                    return _successColor;
                }
                else
                {
                    return _errorColor;
                }
            }
            return _emptyColor;
        }

        /// <summary>
        /// Проверка высоты ладьи
        /// </summary>
        private void CheckHeight()
        {
            var fullHeightColor = CheckTextBox(fullHeightTextBox, 
                _rookInfo.FULL_HEIGHT_MIN, _rookInfo.FULL_HEIGHT_MAX);

            if (fullHeightColor == _successColor
                && upperHeightTextBox.BackColor == _successColor
                && lowerHeightTextBox.BackColor == _successColor)
            {
                int.TryParse(upperHeightTextBox.Text, out int upper);
                int.TryParse(lowerHeightTextBox.Text, out int lower);
                int.TryParse(fullHeightTextBox.Text, out int full);

                if ((upper*2 + lower) >= full)
                {
                    DrawRed(fullHeightTextBox);
                    DrawRed(lowerHeightTextBox);
                    DrawRed(upperHeightTextBox);
                    return;
                }

                DrawGreen(fullHeightTextBox);
                DrawGreen(lowerHeightTextBox);
                DrawGreen(upperHeightTextBox);

                return;
            }

            fullHeightTextBox.BackColor = fullHeightColor;
            return;
        }

        /// <summary>
        /// Проверка цвета
        /// </summary>
        private void CheckColor()
        {
            if (_textBoxes.All(t => t.BackColor == _successColor))
            {
                buildButton.Enabled = true;
            }
        }

        /// <summary>
        /// Проверка на соблюдение условий зависимости
        /// </summary>
        /// <param name="greaterBox">TextBox, в котором должно быть значение больше</param>
        /// <param name="lessBox">TextBox, в котором должно быть значение меньше</param>
        /// <param name="greaterMin"> Минимальное значение для большего </param>
        /// <param name="greaterMax"> Максимальное значение для большего</param>
        /// <param name="lessMin"> Минимальное значение для меньшего </param>
        /// <param name="lessMax"> Максимальное значение для большего </param>
        /// <returns></returns>
        private void CheckDepencies(TextBox greaterBox, TextBox lessBox,
            int greaterMin, int greaterMax, int lessMin, int lessMax)
        {
            var first = CheckTextBox(greaterBox, greaterMin, greaterMax);
            var second = CheckTextBox(lessBox, lessMin, lessMax);

            if (first == _successColor && second == _successColor)
            {
                int.TryParse(greaterBox.Text, out int greater);
                int.TryParse(lessBox.Text, out int less);

                if (less > greater)
                {
                    DrawRed(lessBox);
                    DrawRed(greaterBox);
                    return;
                }
                else
                {
                    DrawGreen(lessBox);
                    DrawGreen(greaterBox);
                    return;
                }
            }
            else
            {
                greaterBox.BackColor = first;
                lessBox.BackColor = second;
            }
        }

        /// <summary>
        /// Перекрашивание textbox в красный
        /// </summary>
        /// <param name="box"> textbox, который надо перекрасить </param>
        private void DrawRed(TextBox box)
        {
            box.BackColor = _errorColor;
            buildButton.Enabled = false;
        }

        /// <summary>
        /// Перекрашивание textbox в зеленый
        /// </summary>
        /// <param name="box"> textbox, который надо перекрасить </param>
        private void DrawGreen(TextBox box)
        {
            box.BackColor = _successColor;
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
                UpperBaseHeight = int.Parse(upperHeightTextBox.Text),
                HasFillet = filletCheckBox.Checked
            };

            _modelCreator.CreateRook(_rookInfo);    
        }

        /// <summary>
        /// Обработчик события загрузки формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            _backgroundWorker.DoWork += (obj, ea) =>
                Check();
            _backgroundWorker.RunWorkerAsync();
        }
    }
}
