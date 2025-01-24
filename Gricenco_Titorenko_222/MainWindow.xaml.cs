using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gricenco_Titorenko_222
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка на корректность ввода
                if (string.IsNullOrWhiteSpace(InputX.Text) || string.IsNullOrWhiteSpace(InputM.Text))
                {
                    MessageBox.Show("Введите значения для x и m.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!double.TryParse(InputX.Text, out double x) || !double.TryParse(InputM.Text, out double m))
                {
                    MessageBox.Show("Некорректные данные. Введите числа.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Выбор функции f(x)
                double fx;
                if (SinhButton.IsChecked == true)
                {
                    fx = Math.Sinh(x); // Гиперболический синус
                }
                else if (SquareButton.IsChecked == true)
                {
                    fx = Math.Pow(x, 2); // Квадрат числа
                }
                else // ExpButton
                {
                    fx = Math.Exp(x); // Экспонента
                }

                // Вычисление значения j
                double j;
                if (-1 < m && m < x)
                {
                    j = Math.Sin(5 * fx + 3 * m * Math.Abs(fx));
                    ResultBox.Text = j.ToString("F4");
                }
                else if (m < x)
                {
                    j = Math.Cos(3 * fx + 5 * m * Math.Abs(fx));
                    ResultBox.Text = j.ToString("F4");
                }
                else if (x == m)
                {
                    j = Math.Pow(fx + m, 2);
                    ResultBox.Text = j.ToString("F4");
                }
                else
                {
                    string result = "Функция не определена при таких значениях";
                    ResultBox.Text = result;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            // Очистка полей
            InputX.Clear();
            InputM.Clear();
            ResultBox.Clear();
            SinhButton.IsChecked = true;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите выйти?", "Подтверждение выхода", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            base.OnClosing(e);
        }
    }
}
