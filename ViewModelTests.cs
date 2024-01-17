using BooksCatalog;
using BooksCatalogUI.ViewModel;
using BooksCatalogWebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalogTests
{
    public class ViewModelTests
    {
        [Fact]
        public void NewBookAddedToBooks()
        {

            var viewModel = new MainViewModel();
            var expectedCount = viewModel.Books.Count +1;
            var addViewModel = new AddBookViewModel();
            addViewModel.Author = "Пушкин";
            addViewModel.Title = "Руслан и Людмила";
            addViewModel.BookId = Guid.NewGuid().ToString();
            addViewModel.Genre = "Баллада";
            viewModel.AddBook(addViewModel);
            Assert.Equal(expectedCount, viewModel.Books.Count);
        }

        [Fact]
        public void AddViewModelDefaultDatyIsToday()
        {
            var viewModel = new AddBookViewModel();
            Assert.Equal(viewModel.Date.ToShortDateString(), DateTime.Now.ToShortDateString());
        }

        [Fact]
        public void FindViewModelHasCategories()
        {
            var viewModel = new FindBookViewModel();
            Assert.Equal(4, viewModel.Categories.Count());
        }

        [Fact]
        public void FindViewModelSelectedCategoryIsAuthor()
        {
            var viewModel = new FindBookViewModel();
            Assert.Equal("Title", viewModel.SelectedCategory);
        }
    }
}
