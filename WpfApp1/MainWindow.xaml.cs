using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1;

public partial class MainWindow : Window
{ 
    private int selectedLabel { get; set; }
    
    public MainWindow()
    {
        InitializeComponent();
        TypeComboBox.ItemsSource = new []
        {
            new Type{ name = "Элипс", code = 0 },
            new Type{ name = "Многоугольник", code = 1 },
        };
    }

    private void ComboBox_SelectionChange(object sender, SelectionChangedEventArgs e)
    {
        if(TypeComboBox.SelectedItem is Type type) 
        {
            selectedLabel = type.code;
            TextBox.IsEnabled = true;
            if (selectedLabel == 0)
            {
                TextBlock.Text = "Введите координату центра фигуры и длины сторон через space";
            }
            else
            {
                TextBlock.Text = "Введите координаты фигуры через ,space";
            }
        }
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        if (selectedLabel == 0)
        {
            string[] coordinate = TextBox.Text.Split(' ');
            if (coordinate.Length != 4 || coordinate.Length % 2 == 1)
            {
                TextBlock.Text = "Ошибка ввода координат";
                return;
            } 
            
            Canvas.SetLeft(Ellipse, int.Parse(coordinate[0]));
            Canvas.SetTop(Ellipse, int.Parse(coordinate[1]));
            
            Ellipse.Width = double.Parse(coordinate[2]);
            Ellipse.Height = double.Parse(coordinate[3]);
            
            Ellipse.Visibility = Visibility.Visible;
            return;
        }
        
        string[] coordinatePolygon = TextBox.Text.Split(' ');
        PointCollection pointCollection = new PointCollection();
        if (coordinatePolygon.Length < 6 || coordinatePolygon.Length % 2 == 1)
        {
            TextBlock.Text = "Ошибка ввода координат";
            return;
        } 
        
        for (int i = 0; i < coordinatePolygon.Length; i += 2)
        {
            pointCollection.Add(new Point(Convert.ToDouble(coordinatePolygon[i]), Convert.ToDouble(coordinatePolygon[i+1]))); 
        }
        Polygon.Points = pointCollection;
        Polygon.Visibility = Visibility.Visible;
    }

    private void TextBox_TextChanged(object sender, EventArgs e)
    {
        Button.IsEnabled = true;
    }

    private void ColorPicker_SelectedColorChanged(object sender, EventArgs e)
    {
        if(ColorPicker.SelectedColor.HasValue)
        {
            if (selectedLabel == 0)
            {
                Ellipse.Fill = new SolidColorBrush(ColorPicker.SelectedColor.Value);
                return;
            }
            Polygon.Fill = new SolidColorBrush(ColorPicker.SelectedColor.Value);
        }
    }
}

class Type
{
    public string name { get; set; }
    public int code { get; set; }
}

class TypeColor
{
    public string name { get; set; }
    public string code { get; set; }
}