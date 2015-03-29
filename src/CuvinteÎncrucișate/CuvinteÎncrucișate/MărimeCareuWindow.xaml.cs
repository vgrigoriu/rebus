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
using System.Windows.Shapes;

namespace CuvinteÎncrucișate
{
    /// <summary>
    /// Interaction logic for MărimeCareuWindow.xaml
    /// </summary>
    public partial class MărimeCareuWindow : Window
    {
        public MărimeCareuWindow()
        {
            InitializeComponent();
            this.okButton.Click += new RoutedEventHandler(okButtonClick);
        }

        void okButtonClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            Close();
        }

        public int? Orizontal
        {
            get
            {
                int orizontal;
                if (int.TryParse(this.orizontal.Text, out orizontal))
                {
                    return orizontal;
                }

                return null;
            }
        }

        public int? Vertical
        {
            get
            {
                int vertical;
                if (int.TryParse(this.vertical.Text, out vertical))
                {
                    return vertical;
                }

                return null;
            }
        }
    }
}
