using System;
using System.Collections.Generic;
using System.IO;

namespace BusinessLayer
{
    public class Transaction
    {
        private int _id;
        private decimal _sum;
        private string _currency;
        private Category _category;
        private string _description;
        private DateTime _date;
        private List<FileStream> _files;

        public int Id
        {
            get => _id;
            private set => _id = value;
        }

        public decimal Sum
        {
            get => _sum;
            set => _sum = value;
        }

        public string Currency
        {
            get => _currency;
            set => _currency = value;
        }

        public Category Category
        {
            get => _category;
            set => _category = value;
        }

        public string Description
        {
            get => _description;
            set => _description = value;
        }

        public DateTime Date
        {
            get => _date;
            set => _date = value;
        }

        public List<FileStream> Files
        {
            get => _files;
            set => _files = value;
        }


        public Transaction()
        {
            Files = new List<FileStream>();
        }

        public Transaction(int id) : this()
        {
            Id = id;
        }

        ~Transaction()
        {
            foreach (var file in _files)
                file.Close();
        }


        public bool Validate()
        {
            bool res = true;

            if (string.IsNullOrWhiteSpace(_currency))
                res = false;
            if (_category == null)
                res = false;

            return res;
        }
    }
}
