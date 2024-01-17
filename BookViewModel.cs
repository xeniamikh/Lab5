using BooksCatalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksCatalogUI.ViewModel
{
    public class BookViewModel : BaseViewModel
    {
        private string bookId;
        private string title;
        private string author;
        private string iSBN;
        private string genre;
        private string brief = string.Empty;
        private DateTime date;

        public string BookId { get => bookId; set { SetValueAndRaisePropertyChangedEvent(ref bookId, value); } }
        public string Title { get => title; set { SetValueAndRaisePropertyChangedEvent(ref title, value); } }
        public string Author { get => author; set { SetValueAndRaisePropertyChangedEvent(ref author, value); } }

        public string ISBN { get => iSBN; set { SetValueAndRaisePropertyChangedEvent(ref iSBN, value); } }

        public string Brief { get => brief; set { SetValueAndRaisePropertyChangedEvent(ref brief, value); } }
        public DateTime Date { get => date; set { SetValueAndRaisePropertyChangedEvent(ref date, value); } }

        public string Genre { get => genre; set { SetValueAndRaisePropertyChangedEvent(ref genre, value); } }
    }
}
