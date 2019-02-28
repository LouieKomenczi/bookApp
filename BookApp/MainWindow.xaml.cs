using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using Newtonsoft.Json;

namespace BookApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Book> books = new ObservableCollection<Book>();
        ObservableCollection<Book> matchingBook = new ObservableCollection<Book>();

        string[] genres = { "adventure", "autobiography", "fiction", "sci-fi", "history", "other" };

        internal ObservableCollection<Book> Books { get => books; set => books = value; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbxFilter.ItemsSource = genres;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            //Book b1 = new Book();
            //b1.Author = "John";
            //b1.Title = "Book of John";
            //b1.Genres = "autobiography";
            //book.Add(b1);
            AddBook addBook = new AddBook();
            addBook.Owner = this;
            addBook.ShowDialog();
            lbxBooks.ItemsSource = books;

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string json = JsonConvert.SerializeObject(Books, Formatting.Indented);

            using (StreamWriter sw = new StreamWriter(@"e:\temp\books.json"))
            {
                sw.Write(json);
            }

            MessageBox.Show("book list saved");
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            using (StreamReader sr = new StreamReader(@"e:\temp\books.json"))
            {
                string json = sr.ReadToEnd();
                Books = JsonConvert.DeserializeObject<ObservableCollection<Book>>(json);

                lbxBooks.ItemsSource = Books;
            }

            MessageBox.Show("book list loaded");
        }

        private void CbxFilter_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string filterGenre = cbxFilter.SelectedItem as string;

            if(filterGenre != null)
            {
                matchingBook.Clear();
                foreach(Book b in Books)
                {
                    if (b.Genres.Equals(filterGenre))
                    {
                        matchingBook.Add(b);
                    }
                }
            }

            lbxBooks.ItemsSource = matchingBook;
        }

        private void TbxSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string search = tbxSearch.Text;

            if (!String.IsNullOrEmpty(search))
            {
                matchingBook.Clear();
                foreach (Book b in Books)
                {
                    if (b.Title.IndexOf(search, 0, StringComparison.CurrentCultureIgnoreCase) != -1)
                    {
                        matchingBook.Add(b);
                    }
                }
            }

            lbxBooks.ItemsSource = matchingBook;
            
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {           
            string deleteThisBook = lbxBooks.SelectedItem.ToString();

            if (!String.IsNullOrEmpty(deleteThisBook))
            {
                foreach(Book b in Books)
                {
                    string current = b.Title + ":" + b.Author + " - " + b.Genres;
                    if (current.Equals(deleteThisBook))
                    {
                        Books.Remove(b);
                        break;
                    }
                }
            }
            lbxBooks.ItemsSource = Books;

        }
    }
}
