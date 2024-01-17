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
    public static class BookViewModelExtension
    {
        public static Book ToBook(this BookViewModel book)
        {
            return new Book()
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
