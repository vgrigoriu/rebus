using System;
using System.Collections.Generic;
using System.Linq;
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

namespace CuvinteÎncrucișate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindowLoaded;
            this.FileNewMenu.Click += new RoutedEventHandler(FileNewMenuClick);
        }

        void FileNewMenuClick(object sender, RoutedEventArgs e)
        {
            MărimeCareuWindow window = new MărimeCareuWindow();
            bool? returnValue = window.ShowDialog();
            if (returnValue.HasValue && returnValue.Value)
            {
                int? oriz = window.Orizontal;
                int? vert = window.Vertical;
                if (!oriz.HasValue)
                {
                    MessageBox.Show("Mărimea pe orizontală nu e-n regulă");
                    return;
                }
                if (!vert.HasValue)
                {
                    MessageBox.Show("Mărimea pe verticală nu e-n regulă");
                    return;
                }
                //MessageBox.Show("yeah: " + oriz.Value.ToString() + "x" + vert.Value.ToString());
                CreateCareu(oriz.Value, vert.Value);
            }
            else
            {
                MessageBox.Show("nay");
            }
        }

        private void CreateCareu(int orizontal, int vertical)
        {
            Pătrățel[,] patrate = new Pătrățel[orizontal, vertical];
            Grid grid = new Grid();
            this.Panel.Children.Clear();
            this.Panel.Children.Add(grid);
            for (int x = 0; x < orizontal; x++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int y = 0; y < vertical; y++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }

            // create them
            for (int x = 0; x < orizontal; x++)
            {
                for (int y = 0; y < vertical; y++)
                {
                    patrate[x, y] = new Pătrățel();
                }
            }

            // set their properties
            for (int x = 0; x < orizontal; x++)
            {
                for (int y = 0; y < vertical; y++)
                {
                    Pătrățel p = patrate[x, y];
                    if (x > 0)
                    {
                        p.Left = patrate[x - 1, y];
                    }
                    if (x < orizontal - 1)
                    {
                        p.Right = patrate[x + 1, y];
                    }
                    if (y > 0)
                    {
                        p.Up = patrate[x, y - 1];
                    }
                    if (y < vertical - 1)
                    {
                        p.Down = patrate[x, y + 1];
                    }

                    Grid.SetColumn(p, x);
                    Grid.SetRow(p, y);
                    grid.Children.Add(p);
                }
            }
            patrate[0, 0].Focus();
        }

        void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            Pătrățel p1 = new Pătrățel();
            p1.Literă = 'Q';
            this.Panel.Children.Add(p1);
            Pătrățel p2 = new Pătrățel();
            this.Panel.Children.Add(p2);
            p1.Down = p2;
            p2.Up = p1;
            p2.Focus();
        }
    }
}
