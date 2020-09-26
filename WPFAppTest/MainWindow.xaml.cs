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
using System.Windows.Ink;
using System.IO;
using System.Security.Permissions;
using Xceed.Wpf.Toolkit;
using System.Globalization;

namespace WPFAppTest
{
    public class HeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Double val = (Double)value;
            val = val + 67;
            return (object)val;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Double val = (Double)value;
            val = val - 67;
            return (object)val;
        }
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            canvas.Background = Brushes.White;
            DrawingAttributes dr = new DrawingAttributes();
            dr.Color = Colors.SpringGreen;
            canvas.DefaultDrawingAttributes = dr;
            Binding b = new Binding("Width");
            b.Source = canvas;
            b.Mode = BindingMode.TwoWay;
            Win.SetBinding(Window.WidthProperty,b);
            Binding h = new Binding("Height");
            h.Source = canvas;
            h.Converter = new HeightConverter();
            h.Mode = BindingMode.TwoWay;
            Win.SetBinding(Window.HeightProperty, h);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            canvas.Strokes.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.Filter = "Plik|*.png";
            dialog.ShowDialog();
            try
            {
                FileStream fs = new FileStream(dialog.FileName, FileMode.Create);
                RenderTargetBitmap rtb = new RenderTargetBitmap((int)canvas.Width, (int)canvas.Height, 96d, 96d, PixelFormats.Default);
                rtb.Render(canvas);
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(rtb));
                encoder.Save(fs);
                fs.Close();
            }
            catch (ArgumentException ex)
            {
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Owner = this;
            cd.ShowDialog();
            DrawingAttributes dr = canvas.DefaultDrawingAttributes;
            dr.Color = cd.Resault;
            canvas.DefaultDrawingAttributes = dr;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            StrokeDialog sd = new StrokeDialog();
            sd.Owner = this;
            sd.ShowDialog();
            DrawingAttributes dr = canvas.DefaultDrawingAttributes;
            dr.Height = sd.ResaultHeight;
            dr.Width = sd.ResaultWidth;
            canvas.DefaultDrawingAttributes = dr;
        }
    }
}
