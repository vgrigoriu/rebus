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
using System.IO;
using System.Text.RegularExpressions;

namespace CuvinteÎncrucișate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Pătrățel selectedPătrățel;
        private Pătrățel SelectedPătrățel
        {
            get
            {
                return this.selectedPătrățel;
            }
            set
            {
                if (value != this.selectedPătrățel)
                {
                    if (this.selectedPătrățel != null)
                    {
                        this.selectedPătrățel.border.BorderThickness = new Thickness(1);
                    }
                    this.selectedPătrățel = value;
                    if (this.selectedPătrățel != null)
                    {
                        this.selectedPătrățel.border.BorderThickness = new Thickness(3);
                    }
                    OnNewFiltru();
                    EvidențiazăRîndSauColoană();
                }
            }
        }

        private void EvidențiazăRîndSauColoană()
        {
            for (int i = 0; i < this.patrate.GetLength(0); i++)
            {
                for (int j = 0; j < this.patrate.GetLength(1); j++)
                {
                    if (this.patrate[i, j] == this.SelectedPătrățel)
                    {
                        if (this.direcție == Direcție.Orizontal)
                        {
                            ColoreazăRîndul(j);
                            return;
                        }
                        else
                        {
                            ColoreazăColoana(i);
                            return;
                        }
                    }
                }
            }
        }

        private void ColoreazăColoana(int coloană)
        {
            for (int i = 0; i < this.patrate.GetLength(0); i++)
            {
                for (int j = 0; j < this.patrate.GetLength(1); j++)
                {
                    if (!this.patrate[i, j].ENegru)
                    {
                        if (i == coloană)
                        {
                            this.patrate[i, j].border.Background = new SolidColorBrush(Color.FromArgb(15, 0, 0, 15));
                        }
                        else
                        {
                            this.patrate[i, j].border.Background = Brushes.White;
                        }
                    }
                }
            }
        }

        private void ColoreazăRîndul(int rînd)
        {
            for (int i = 0; i < this.patrate.GetLength(0); i++)
            {
                for (int j = 0; j < this.patrate.GetLength(1); j++)
                {
                    if (!this.patrate[i, j].ENegru)
                    {
                        if (j == rînd)
                        {
                            this.patrate[i, j].border.Background = new SolidColorBrush(Color.FromArgb(15, 0, 0, 15));
                        }
                        else
                        {
                            this.patrate[i, j].border.Background = Brushes.White;
                        }
                    }
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindowLoaded;
            this.FileNewMenu.Click += new RoutedEventHandler(FileNewMenuClick);
            this.PreviewKeyDown += new KeyEventHandler(MainWindowPreviewKeyDown);
            this.KeyUp += new KeyEventHandler(MainWindowKeyUp);
        }

        void MainWindowKeyUp(object sender, KeyEventArgs e)
        {
            char litera;
            switch (e.Key)
            {
                case Key.Escape:
                    if (this.SelectedPătrățel != null)
                    {
                        this.SelectedPătrățel.ENegru = !this.SelectedPătrățel.ENegru;
                    }
                    OnNewFiltru();
                    return;
                case Key.CapsLock:
                    if (this.direcție == Direcție.Orizontal)
                    {
                        this.direcție = Direcție.Vertical;
                    }
                    else
                    {
                        this.direcție = Direcție.Orizontal;
                    }
                    OnNewFiltru();
                    EvidențiazăRîndSauColoană();
                    return;
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

            if (this.SelectedPătrățel != null && !this.SelectedPătrățel.ENegru)
            {
                this.SelectedPătrățel.Literă = litera;
                Pătrățel următorul = UrmătorulNeNegru();
                bool următorulLîngă = următorul == După(this.SelectedPătrățel);
                if (următorulLîngă && litera != default(char))
                {
                    OnFiltruModified();
                }
                if (următorul != null)
                {
                    this.SelectedPătrățel = următorul;
                }
                if (!următorulLîngă || litera == default(char))
                {
                    OnNewFiltru();
                }
            }
        }

        private Pătrățel UrmătorulNeNegru()
        {
            Pătrățel p = După(this.SelectedPătrățel);
            while (p != null && p.ENegru)
            {
                p = După(p);
            }
            return p;
        }

        void MainWindowPreviewKeyDown(object sender, KeyEventArgs e)
        {
            Pătrățel p;
            switch (e.Key)
            {
                case Key.Right:
                    p = FindPătrățelRight();
                    if (p != null)
                    {
                        this.SelectedPătrățel = p;
                    }
                    e.Handled = true;
                    return;
                case Key.Left:
                    p = FindPătrățelLeft();
                    if (p != null)
                    {
                        this.SelectedPătrățel = p;
                    }
                    e.Handled = true;
                    return;
                case Key.Up:
                    p = FindPătrățelUp();
                    if (p != null)
                    {
                        this.SelectedPătrățel = p;
                    }
                    e.Handled = true;
                    return;
                case Key.Down:
                    p = FindPătrățelDown();
                    if (p != null)
                    {
                        this.SelectedPătrățel = p;
                    }
                    e.Handled = true;
                    return;
            }
        }

        private Pătrățel FindPătrățelRight()
        {
            Pătrățel p = this.SelectedPătrățel.Right;
            /*while (p != null && p.ENegru)
            {
                p = p.Right;
            }*/

            return p;
        }

        private Pătrățel FindPătrățelLeft()
        {
            Pătrățel p = this.SelectedPătrățel.Left;
            /*while (p != null && p.ENegru)
            {
                p = p.Left;
            }*/

            return p;
        }
        private Pătrățel FindPătrățelUp()
        {
            Pătrățel p = this.SelectedPătrățel.Up;
            /*while (p != null && p.ENegru)
            {
                p = p.Up;
            }*/

            return p;
        }
        private Pătrățel FindPătrățelDown()
        {
            Pătrățel p = this.SelectedPătrățel.Down;
            /*while (p != null && p.ENegru)
            {
                p = p.Down;
            }*/

            return p;
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

        private Pătrățel[,] patrate;
        private void CreateCareu(int orizontal, int vertical)
        {
            this.patrate = new Pătrățel[orizontal, vertical];
            Grid grid = new Grid();
            grid.Width = (orizontal + 1) * 32;
            grid.Height = (vertical + 1) * 32;
            FocusManager.SetIsFocusScope(grid, true);
            this.Panel.Children.Clear();
            this.Panel.Children.Add(grid);
            for (int x = 0; x < orizontal + 1; x++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int y = 0; y < vertical + 1; y++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }

            // add numbers on upper & left side
            for (int row = 1; row < vertical + 1; row++)
            {
                TextBlock number = new TextBlock();
                number.TextAlignment = TextAlignment.Center;
                number.Text = row.ToString();
                Grid.SetRow(number, row);
                Grid.SetColumn(number, 0);
                grid.Children.Add(number);
            }
            for (int column = 1; column < orizontal + 1; column++)
            {
                TextBlock number = new TextBlock();
                number.TextAlignment = TextAlignment.Center;
                number.Text = column.ToString();
                Grid.SetRow(number, 0);
                Grid.SetColumn(number, column);
                grid.Children.Add(number);
            }

            // create them
            for (int x = 0; x < orizontal; x++)
            {
                for (int y = 0; y < vertical; y++)
                {
                    this.patrate[x, y] = new Pătrățel();
                }
            }

            // set their properties
            for (int x = 0; x < orizontal; x++)
            {
                for (int y = 0; y < vertical; y++)
                {
                    Pătrățel p = this.patrate[x, y];
                    if (x > 0)
                    {
                        p.Left = this.patrate[x - 1, y];
                    }
                    if (x < orizontal - 1)
                    {
                        p.Right = this.patrate[x + 1, y];
                    }
                    if (y > 0)
                    {
                        p.Up = this.patrate[x, y - 1];
                    }
                    if (y < vertical - 1)
                    {
                        p.Down = this.patrate[x, y + 1];
                    }

                    Grid.SetColumn(p, x + 1);
                    Grid.SetRow(p, y + 1);
                    grid.Children.Add(p);
                }
            }
            this.SelectedPătrățel = this.patrate[0, 0];
        }

        IEnumerable<string> allWords;
        void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            this.allWords = File.ReadAllLines("words.ro-ro.txt");
            this.allWords = this.allWords.Select(s => s.ToUpperInvariant());
            AfiseazaLista(this.allWords);
            CreateCareu(10, 10);
            //this.listaCuvinte.ItemsSource = new string[] { "ANA", "ARE", "MERE" };
        }

        private void AfiseazaLista(IEnumerable<string> cuvinte)
        {
            this.listaCuvinte.ItemsSource = null;
            this.listaCuvinte.Items.Clear();
            int cîteCuvinte = cuvinte.Count();
            if (cîteCuvinte == 0)
            {
                this.listaCuvinte.Items.Add("NICI UN CUVÎNT");
            }
            else if (cîteCuvinte < 100)
            {
                this.listaCuvinte.ItemsSource = cuvinte;
            }
            else
            {
                this.listaCuvinte.Items.Add(cîteCuvinte.ToString() + " CUVINTE");
            }
        }

        enum Direcție
        {
            Orizontal,
            Vertical
        }

        private Direcție direcție = Direcție.Orizontal;

        private Pătrățel Înainte(Pătrățel p)
        {
            if (this.direcție == Direcție.Orizontal)
            {
                return p.Left;
            }
            else
            {
                return p.Up;
            }
        }

        private Pătrățel După(Pătrățel p)
        {
            if (this.direcție == Direcție.Orizontal)
            {
                return p.Right;
            }
            else
            {
                return p.Down;
            }
        }

        string filtru;
        IEnumerable<string> cuvintePotrivite;
        private void OnNewFiltru()
        {
            string filtruNou = GetFiltru();
            if (this.filtru != filtruNou)
            {
                this.filtru = filtruNou;
                this.filtruTextBlock.Text = this.filtru;
                if (!string.IsNullOrEmpty(this.filtru))
                {
                    this.cuvintePotrivite = (from s in this.allWords
                                           where Regex.IsMatch(s, filtru)
                                           select s).ToArray();
                    AfiseazaLista(this.cuvintePotrivite);
                }
            }
        }

        private void OnFiltruModified()
        {
            string filtruNou = GetFiltru();
            if (this.filtru != filtruNou)
            {
                this.filtru = filtruNou;
                this.filtruTextBlock.Text = this.filtru;
                if (!string.IsNullOrEmpty(this.filtru))
                {
                    ModificaLista(this.filtru);
                }
            }
        }

        private void ModificaLista(string p)
        {
            this.cuvintePotrivite = (from s in this.cuvintePotrivite
                                                where Regex.IsMatch(s, p)
                                                select s).ToArray();
            AfiseazaLista(this.cuvintePotrivite);
        }

        private string GetFiltru()
        {
            // găsește începutul
            Pătrățel curent = this.SelectedPătrățel;
            if (curent.ENegru)
            {
                return string.Empty;
            }
            bool laÎnceput = false;
            while (!laÎnceput)
            {
                Pătrățel înainte = Înainte(curent);
                if (înainte != null && !înainte.ENegru)
                {
                    curent = înainte;
                }
                else
                {
                    laÎnceput = true;
                }
            }

            // sari peste spații
            int nrSpații = 0;
            while (curent != null && !curent.ENegru && curent.Literă == default(char))
            {
                curent = După(curent);
                nrSpații++;
            }

            // construiește filtrul
            StringBuilder filtru = new StringBuilder();
            filtru.Append("^");
            if (nrSpații != 0)
            {
                filtru.Append(".{0," + nrSpații.ToString() + "}");
            }
            nrSpații = 0;
            while (curent != null && !curent.ENegru)
            {
                if (curent.Literă == default(char))
                {
                    nrSpații++;
                }
                else
                {
                    filtru.Append('.', nrSpații);
                    filtru.Append(curent.Literă);
                    nrSpații = 0;
                }

                curent = După(curent);
            }
            if (nrSpații != 0)
            {
                filtru.Append(".{0," + nrSpații.ToString() + "}");
            }
            filtru.Append("$");
            return filtru.ToString();
        }
    }
}
