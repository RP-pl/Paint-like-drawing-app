using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFAppTest
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class ColorDialog : Window
    {
        public Color Resault = Colors.Black;
        public ColorDialog()
        {
            InitializeComponent();
            base.Width = 200;
            base.Height = 150;
            for(int i=1;i<255;i+=35)
            {
                for (int j = 1; j < 255; j+=35)
                {
                    for (int k = 1; k < 255; k+=35)
                    {
                        StackPanel sp = new StackPanel();
                        sp.Orientation = Orientation.Horizontal;
                        Rectangle r = new Rectangle();
                        r.Width = 10;
                        r.Height = 10;
                        r.Fill = new SolidColorBrush(Color.FromRgb((byte)i, (byte)j, (byte)k));
                        sp.Children.Add(r);
                        Label l = new Label();
                        l.Content = Color.FromRgb((byte)i, (byte)j, (byte)k);
                        sp.Children.Add(l);
                        CB.Items.Add(sp);
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (CB.SelectedItem != null)
            {
                Resault = ((SolidColorBrush)((Rectangle)((StackPanel)CB.SelectedItem).Children[0]).Fill).Color;
            }
            DialogResult = true;
        }
    }
}
