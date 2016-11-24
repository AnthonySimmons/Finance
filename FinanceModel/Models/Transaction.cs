using System;
using System.ComponentModel;

namespace FinanceModel.Models
{
    public class Transaction : INotifyPropertyChanged
    {
        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (value != _date)
                {
                    _date = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        private double _amount;
        public double Amount
        {
            get { return _amount; }
            set
            {
                if (value != _amount)
                {
                    _amount = value;
                    OnPropertyChanged(nameof(Amount));
                }
            }
        }

        private string _payee;
        public string Payee
        {
            get { return _payee; }
            set
            {
                if (value != _payee)
                {
                    _payee = value;
                    OnPropertyChanged(nameof(Payee));
                }
            }
        }

        private bool _isIncluded = true;
        public bool IsIncluded
        {
            get
            {
                return _isIncluded;
            }
            set
            {
                if (value != _isIncluded)
                {
                    _isIncluded = value;
                    OnPropertyChanged(nameof(IsIncluded));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
