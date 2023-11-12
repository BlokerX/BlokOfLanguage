using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BlokOfLanguage.Pages.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        bool isVisible = true;
        public bool IsVisible
        {
            get => isVisible;
            set
            {
                isVisible = value;
                OnPropertyChanged(nameof(IsVisible));
            }
        }

        public ICommand TapCommand { get; set; }

        public ViewModel()
        {
            isVisible = true;
            TapCommand = new Command(Show);
        }

        private void Show()
        {
            IsVisible = !IsVisible;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
