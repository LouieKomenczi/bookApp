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
using System.Windows.Shapes;

namespace BookApp
{
    /// <summary>
    /// Interaction logic for AddBook.xaml
    /// </summary>
    public partial class AddBook : Window
    {
        string[] genres = { "adventure", "autobiography", "fiction", "sci-fi", "history", "other" };

        public AddBook()
        {
            InitializeComponent();
            cbxGenre.ItemsSource = genres;
        }

        private void BtnAddBook_Click(object sender, RoutedEventArgs e)
        {
            Book b = new Book();
            bool valid = true;          
            if (!String.IsNullOrEmpty(tbxTitle.Text))
            {
                 b.Title = tbxTitle.Text;                   
            }
            else
            {
                valid = false;
            }
            if (!String.IsNullOrEmpty(tbxAuthor.Text))
            {
                b.Author = tbxAuthor.Text;
            }
            else
            {
                valid = false;
            }
            if (cbxGenre.SelectedItem != null)
            {
                b.Genres = cbxGenre.SelectedItem as string;                   
            }
            else
            {
                valid = false;
            }

            if (valid)
            {
                MainWindow main = Owner as MainWindow;
                main.Books.Add(b);                
            }
            else
            {
                MessageBox.Show("book was not added, incomplet information...");
            }
            this.Close();

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
