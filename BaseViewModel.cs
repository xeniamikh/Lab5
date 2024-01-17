using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BooksCatalogUI
{
    public class BaseViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors
        {
            get { return _errors.Any(x => x.Value != null && x.Value.Count > 0); }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (!string.IsNullOrWhiteSpace(propertyName))
            {
                return _errors.ContainsKey(propertyName) && _errors[propertyName] != null && _errors[propertyName].Count > 0 ? _errors[propertyName].ToList() : null;
            }

            return null;
        }

        protected void SetValueAndValidate<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            SetValueAndRaisePropertyChangedEvent(ref field, value, propertyName);
            ValidateProperty(value, propertyName);
        }

        protected void SetValueAndRaisePropertyChangedEvent<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (!Equals(field, value))
            {
                field = value;
                RaisePropertyChanged(propertyName);
            }
        }

        protected void RaiseErrorChangedEvent([CallerMemberName] string propertyName = "")
        {
            RaiseErrorChangedEvent(this, new DataErrorsChangedEventArgs(propertyName));
        }

        protected void RaiseErrorChangedEvent(object sender, DataErrorsChangedEventArgs e)
        {
            if (ErrorsChanged != null)
            {
                ErrorsChanged(sender, e);
            }

            RaisePropertyChanged("HasErrors");
        }

        private void HandleValidationResults(IEnumerable<ValidationResult> validationResults)
        {
            var resultsByPropNames = from res in validationResults
                                     from mname in res.MemberNames
                                     group res by mname into g
                                     select g;

            foreach (var prop in resultsByPropNames)
            {
                var messages = prop.Select(r => r.ErrorMessage).ToList();
                _errors.Add(prop.Key, messages);
                RaiseErrorChangedEvent(this, new DataErrorsChangedEventArgs(prop.Key));
            }
        }

        public void ValidateProperty(object value, [CallerMemberName] string propertyName = "")
        {
            var validationContext = new ValidationContext(this, null, null) { MemberName = propertyName };
            var validationResults = new List<ValidationResult>();

            Validator.TryValidateProperty(value, validationContext, validationResults);

            if (_errors.ContainsKey(propertyName))
            {
                _errors.Remove(propertyName);
            }

            //RaiseErrorChangedEvent(propertyName);
            HandleValidationResults(validationResults);
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void RaisePropertyChanged(Expression<Func<object>> propertyExpression)
        {
            RaisePropertyChanged(propertyExpression.GetPropertyName());
        }
    }
}
