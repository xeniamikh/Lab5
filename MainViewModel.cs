using BooksCatalog;
using BooksCatalogUI.Extensions;
using BooksCatalogWebApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BooksCatalogUI.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        BooksComponent _booksComponent;
        public MainViewModel() 
        {
            _booksComponent = new BooksComponent();
            Books = new ObservableCollection<Book>(_booksComponent.BookCatalog.Books.ToList());
            
        }

        private ObservableCollection<Book> _books;
        public ObservableCollection<Book> Books
        {
            get { return _books; }
            set { SetValueAndRaisePropertyChangedEvent(ref _books, value); }
        }

        private ICommand _addBookCommand;
        public ICommand AddBookCommand
        {
            get { return _addBookCommand ?? (_addBookCommand = new RelayCommand(x => AddBook())); }
        }

        public void AddBook()
        {
            AddBookViewModel viewModel = new AddBookViewModel();
      
            AddBookWindow addBookWindow = new AddBookWindow()
            {
                DataContext = viewModel
               
            };

            if (addBookWindow.ShowDialog() == true)
            {
                AddBook(viewModel);
            }
        }

        public void AddBook(AddBookViewModel viewModel)
        {   
            var book = new Book()
            {
                Author = viewModel.Author,
                Title = viewModel.Title,
                Brief = viewModel.Brief,
                Genre = viewModel.Genre,
                ISBN = viewModel.ISBN,
                BookId = Guid.NewGuid().ToString(),
                Date = viewModel.Date,
            };

            Books.Add(book);
            _booksComponent.AddBook(book);
        }

        private ICommand _findBooksCommand;
        public ICommand FindBooksCommand
        {
            get { return _findBooksCommand ?? (_findBooksCommand = new RelayCommand(x => FindBooks())); }
        }

        private void FindBooks()
        {
            FindBookViewModel viewModel = new FindBookViewModel();
            FindBooksWindow window = new FindBooksWindow()
            {
                DataContext = viewModel
            };

            if (window.ShowDialog() == true)
            {
                Enum.TryParse(viewModel.SelectedCategory, out Category category);
                Books = new ObservableCollection<Book> (_booksComponent.FindBooksByCategory(category, viewModel.KeyWords).ToList());
            }
        }

        private ICommand _exitCommand;
        public ICommand ExitCommand
        {
            get { return _exitCommand ?? (_exitCommand = new RelayCommand(x => Exit())); }
        }

        private void Exit()
        {
            
            Application.Current.MainWindow.Close();
        }

        private ICommand _showAllCommand;
        public ICommand ShowAllCommand
        {
            get { return _showAllCommand ?? (_showAllCommand = new RelayCommand(x => ShowAll())); }
        }

        public void ShowAll()
        {
            Books = new ObservableCollection<Book>(_booksComponent.BookCatalog.Books);
        }
    }
}
