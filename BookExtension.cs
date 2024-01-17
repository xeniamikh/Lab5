using BooksCatalog;
using BooksCatalogUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BooksCatalogUI.Extensions
{
    public static class BookExtension
    {
        public static BookViewModel ToBookViewModel(this Book book)
        {
            return new BookViewModel()
            {
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                BookId = book.BookId,
                Brief = book.Brief,
                Date = book.Date,
                ISBN = book.ISBN
            };
        }
    }
}
