using BooksCatalog;
using BooksCatalogWebApi;

namespace BookCatalogTests
{
    public class BookComponentTests
    {
        [Fact]
        public void NewBookAddedToBooks()
        {
            var books = new List<Book>();
            var component = new BooksComponent(books);
            int count = component.BookCatalog.Books.Count;
            Assert.Equal(0, count);
            var book = new Book("Title", "Author", "0000-000", "key, work, word", DateTime.Now, "Roman");
            component.AddBook(book);
            Assert.Single(component.BookCatalog.Books);
        }

        [Theory]
        [InlineData(Category.Title, "Title", 2)]
        [InlineData(Category.Author, "Somebody", 1)]
        [InlineData(Category.Author, "Author2", 1)]
        [InlineData(Category.ISBN, "0200-000", 1)]
        [InlineData(Category.Keyword, "key, work", 2)]
        public void FindBooksReturnsRightResults(Category category, string keyword, int count)
        {
            var component = GetBookComponent();
            var books = component.FindBooksByCategory(category, keyword);
            Assert.True(books.Any());
            Assert.Equal(count, books.Count());
        }

        [Fact]
        public void FindBooksReturnsSortedResults()
        {
            var component = GetBookComponent();
            var books = component.FindBooksByCategory(Category.Keyword, "work, goods");
            Assert.True(books.Any());
            Assert.Equal(2, books.Count());
            Assert.Equal("Test", books.First().Title);
            Assert.Equal("Title", books.Last().Title);
        }

        [Fact]
        private void GetReaderWriterReturnsCorrectResult()
        {
            var component = GetBookComponent();
            IReaderWriter readerWriter = component.GetReaderWriterBySavingType(SavingType.SqLite);
            Assert.True(readerWriter is SqLiteReaderWriter);
            readerWriter = component.GetReaderWriterBySavingType(SavingType.Xml);
            Assert.True(readerWriter is XmlReaderWriter);
            readerWriter = component.GetReaderWriterBySavingType(SavingType.JSon);
            Assert.True(readerWriter is JsonReaderWriter);
        }

        private BooksComponent GetBookComponent()
        {
            var books = new List<Book>
            {
                new Book("Title", "Author", "0000-000", "key, work, word", DateTime.Now, "Roman"),
                new Book("Title 2", "Author2", "0200-000", "sea sky grass", DateTime.Now, "Poem"),
                new Book("Test", "Somebody", "1000-000", "goods product work", DateTime.Now, "Detective")
            };
            var component = new BooksComponent(books);
            return component;
        }
    }
}