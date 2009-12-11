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
        }

        internal Pătrățel Left { get; set; }
        internal Pătrățel Right { get; set; }
        internal Pătrățel Up { get; set; }
        internal Pătrățel Down { get; set; }

        private bool eNegru;
        public bool ENegru
        {
            get
            {
                return this.eNegru;
            }
            set
            {
                this.eNegru = value;
                if (this.eNegru)
                {
                    this.border.Background = Brushes.Black;
                    this.border.BorderBrush = Brushes.White;
                }
                else
                {
                    this.border.Background = Brushes.White;
                    this.border.BorderBrush = Brushes.Black;
                }
            }
        }

        public char Literă
        {
            get
            {
                Trace.Assert(!this.ENegru);
                string text = this.textBox.Text;
                return text.SingleOrDefault();
            }
            set
            {
                Trace.Assert(!this.ENegru);
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
