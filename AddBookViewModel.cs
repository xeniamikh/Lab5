using System;
using System.Diagnostics.Tracing;
using System.Windows.Input;

namespace BooksCatalogUI.ViewModel
{
    public class AddBookViewModel : BaseViewModel
    {
        public AddBookViewModel()
        {
            Date = DateTime.Now;
        }

        public string BookId { get => bookId; set => bookId = value; }
        public string Title { get => title; set => title = value; }
        public string Author { get => author; set => author = value; }

        public string ISBN { get => iSBN; set => iSBN = value; }

        public string Brief { get => brief; set => brief = value; }
        public DateTime Date { get => date; set => date = value; }

        public string Genre { get => genre; set => genre = value; }

        private string bookId;
        private string title;
        private string author;
        private string iSBN;
        private string brief = string.Empty;
        private DateTime date;
        private string genre;

    }
}