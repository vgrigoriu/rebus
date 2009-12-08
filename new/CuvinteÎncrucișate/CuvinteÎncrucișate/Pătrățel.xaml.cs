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
using System.Diagnostics;

namespace CuvinteÎncrucișate
{
    /// <summary>
    /// Interaction logic for Pătrățel.xaml
    /// </summary>
    public partial class Pătrățel : UserControl
    {
        public Pătrățel()
        {
            InitializeComponent();
            this.GotFocus += new RoutedEventHandler(PătrățelGotFocus);
            this.LostFocus += new RoutedEventHandler(PătrățelLostFocus);
            this.MouseDown += new MouseButtonEventHandler(PătrățelMouseDown);
            this.KeyUp += new KeyEventHandler(PătrățelKeyUp);
            this.PreviewKeyDown += new KeyEventHandler(PătrățelPreviewKeyDown);
        }

        void PătrățelPreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Right:
                    if (this.Right != null)
                    {
                        this.Right.Focus();
                    }
                    e.Handled = true;
                    return;
                case Key.Left:
                    if (this.Left != null)
                    {
                        this.Left.Focus();
                    }
                    e.Handled = true;
                    return;
                case Key.Up:
                    if (this.Up != null)
                    {
                        this.Up.Focus();
                    }
                    e.Handled = true;
                    return;
                case Key.Down:
                    if (this.Down != null)
                    {
                        this.Down.Focus();
                    }
                    e.Handled = true;
                    return;
            }
        }

        internal Pătrățel Left { get; set; }
        internal Pătrățel Right { get; set; }
        internal Pătrățel Up { get; set; }
        internal Pătrățel Down { get; set; }

        void PătrățelKeyUp(object sender, KeyEventArgs e)
        {
            char litera;
            switch (e.Key)
            {
                case Key.A:
                    litera = 'A';
                    break;
                case Key.B:
                    litera = 'B';
                    break;
                case Key.C:
                    litera = 'C';
                    break;
                case Key.D:
                    litera = 'D';
                    break;
                case Key.E:
                    litera = 'E';
                    break;
                case Key.F:
                    litera = 'F';
                    break;
                case Key.G:
                    litera = 'G';
                    break;
                case Key.H:
                    litera = 'H';
                    break;
                case Key.I:
                    litera = 'I';
                    break;
                case Key.J:
                    litera = 'J';
                    break;
                case Key.K:
                    litera = 'K';
                    break;
                case Key.L:
                    litera = 'L';
                    break;
                case Key.M:
                    litera = 'M';
                    break;
                case Key.N:
                    litera = 'N';
                    break;
                case Key.O:
                    litera = 'O';
                    break;
                case Key.P:
                    litera = 'P';
                    break;
                case Key.Q:
                    litera = 'Q';
                    break;
                case Key.R:
                    litera = 'R';
                    break;
                case Key.S:
                    litera = 'S';
                    break;
                case Key.T:
                    litera = 'T';
                    break;
                case Key.U:
                    litera = 'U';
                    break;
                case Key.V:
                    litera = 'V';
                    break;
                case Key.W:
                    litera = 'W';
                    break;
                case Key.X:
                    litera = 'X';
                    break;
                case Key.Y:
                    litera = 'Y';
                    break;
                case Key.Z:
                    litera = 'Z';
                    break;
                case Key.Space:
                    litera = default(char);
                    break;
                default:
                    // don't change anything if we don't know what it is
                    return;
            }
            this.Literă = litera;
        }

        void PătrățelMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!this.IsFocused)
            {
                this.Focus();
            }
        }

        void PătrățelLostFocus(object sender, RoutedEventArgs e)
        {
            this.border.Background = Brushes.White;
        }

        void PătrățelGotFocus(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("cucu");
            Keyboard.Focus(this);
            this.border.Background = Brushes.PeachPuff;
        }



        public char Literă
        {
            get
            {
                string text = this.textBox.Text;
                return text.SingleOrDefault();
            }
            set
            {
                Trace.Assert("ABCDEFGHIJKLMNOPQRSTUVWXYZ".Contains(value) || value == default(char));
                if (value == default(char))
                {
                    this.textBox.Text = string.Empty;
                }
                else
                {
                    this.textBox.Text = value.ToString();
                }
            }
        }
    }
}
