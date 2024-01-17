using BooksCatalog;
using System.Collections.Generic;
using System;
using System.Linq;

namespace BooksCatalogUI.ViewModel
{
    public class FindBookViewModel : BaseViewModel
    {
        public FindBookViewModel()
        {
            SelectedCategory = Categories.First();
        }

        private string category;
        private string keyWords;

        public string SelectedCategory { get => category; set => category = value; }
        public string KeyWords { get => keyWords; set => keyWords = value; }

        public IEnumerable<string> Categories
        {
            get
            {
                return Enum.GetNames(typeof(Category)).ToList();
            }
        }
    }
}