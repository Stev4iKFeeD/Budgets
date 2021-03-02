using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class Wallet
    {
        private int _id;
        private string _name;
        private string _description;
        private string _currency;
        private decimal _initialBalance;
        private List<Transaction> _transactions;
        private List<Category> _categories;

        public int Id
        {
            get => _id;
            private set => _id = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Description
        {
            get => _description;
            set => _description = value;
        }

        public string Currency
        {
            get => _currency;
            set => _currency = value;
        }

        public decimal InitialBalance
        {
            get => _initialBalance;
            set => _initialBalance = value;
        }

        // ???
        // public List<Transaction> Transactions
        // {
        //     get => _transactions;
        //     set => _transactions = value;
        // }

        public List<Category> Categories
        {
            get => _categories;
            set => _categories = value;
        }


        public Wallet()
        {
            // Transactions = new List<Transaction>();
            _transactions = new List<Transaction>();
            Categories = new List<Category>();
        }

        public Wallet(int id) : this()
        {
            Id = id;
        }


        public decimal CurrentBalance()
        {
            decimal res = _initialBalance;
            foreach (var transaction in _transactions)
                res += transaction.Sum;

            return res;
        }

        public decimal Incomes(DateTime since, DateTime to)
        {
            decimal res = 0;
            foreach (var transaction in _transactions)
                if (since <= transaction.Date && transaction.Date <= to)
                    if (transaction.Sum > 0)
                        res += transaction.Sum;

            return res;
        }

        public decimal IncomesForCurrentMonth()
        {
            DateTime today = DateTime.Today;
            return Incomes(new DateTime(today.Year, today.Month, 1), today);
        }

        public decimal Expenses(DateTime since, DateTime to)
        {
            decimal res = 0;
            foreach (var transaction in _transactions)
                if (since <= transaction.Date && transaction.Date <= to)
                    if (transaction.Sum < 0)
                        res += transaction.Sum;

            return Math.Abs(res);
        }

        public decimal ExpensesForCurrentMonth()
        {
            DateTime today = DateTime.Today;
            return Expenses(new DateTime(today.Year, today.Month, 1), today);
        }

        public bool AddTransaction(Transaction transaction)
        {
            if (_categories.Count != 0)
                if (!_categories.Contains(transaction.Category))
                    return false;

            _transactions.Add(transaction);
            return true;
        }

        public bool RemoveTransaction(Transaction transaction)
        {
            return _transactions.Remove(transaction);
        }

        /**
         * @param index  begins with 1
         */
        public bool RemoveTransaction(int index)
        {
            if (index < 1 || index > _transactions.Count)
                return false;

            _transactions.RemoveAt(index - 1);
            return true;
        }

        /**
         * @param index  begins with 1
         */
        public Transaction GetTransaction(int index)
        {
            if (index < 1 || index > _transactions.Count)
                return null;

            return _transactions[index - 1];
        }

        /**
         * @param from  begins with 1
         * @param to    inclusive
         */
        public List<Transaction> GetTransactions(int from, int to)
        {
            int firstIndex = Math.Min(from, to) - 1;
            int count = Math.Max(from, to) - firstIndex;
            if (firstIndex < 0 || count > /*_transactions.Count*/ 10)
                return null;
            
            return _transactions.GetRange(firstIndex, count);
        }

        public List<Transaction> GetTenRecentlyAddedTransactions()
        {
            return GetTransactions(Math.Max(1, _transactions.Count - 9), _transactions.Count);
        }

        public bool Validate()
        {
            bool res = true;

            if (string.IsNullOrWhiteSpace(_name))
                res = false;
            if (string.IsNullOrWhiteSpace(_currency))
                res = false;

            return res;
        }
    }
}
