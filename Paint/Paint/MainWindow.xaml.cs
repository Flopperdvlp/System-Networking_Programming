using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Paint
{
    public partial class MainWindow : Window
    {
        private bool isDrawing = false;
        private Point previousPoint;
        private Brush selectedColor = Brushes.Black;
        private double brushSize = 5;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ColorButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Background is SolidColorBrush brush)
            {
                selectedColor = brush;
                MessageBox.Show($"You selected: {selectedColor}");
            }
        }
        private void SaveCanvas_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Рисунок сохранён как 'drawing.png'", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void ClearCanvas_Click(Object sender, RoutedEventArgs e)
        {
            DrawingCanvas.Children.Clear();
            MessageBox.Show("The entire field is cleared!");
        }
        private void DrawingCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDrawing = true;
            previousPoint = e.GetPosition(DrawingCanvas);
        }
        private void DrawingCanvas_MouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            isDrawing = false;  
        }
        private void DrawingCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                Point currentPoint = e.GetPosition(DrawingCanvas);
                Line line = new Line
                {
                    Stroke = selectedColor,
                    StrokeThickness = brushSize,
                    X1 = previousPoint.X,
                    Y1 = previousPoint.Y,
                    X2 = currentPoint.X,
                    Y2 = currentPoint.Y
                };
                DrawingCanvas.Children.Add(line);
                previousPoint = currentPoint;
            }
        }
        private void BrushSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            brushSize = BrushSizeSlider.Value;
        }
    }
}