using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer
{
    public class Wallet
    {
        private int _id;
        private string _name;
        private string _description;
        private string _currency;
        private decimal _initialBalance;
        private decimal _currentWithoutInitialBalance;
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

        public decimal CurrentBalance
        {
            get => _currentWithoutInitialBalance + _initialBalance;
            private set => _currentWithoutInitialBalance = value;
        }

        // ???
        // public List<Transaction> Transactions
        // {
        //     get => _transactions;
        //     set => _transactions = value;
        // }

        // ???
        // public List<Category> Categories
        // {
        //     get => _categories;
        //     set => _categories = value;
        // }


        public Wallet()
        {
            // Transactions = new List<Transaction>();
            // Categories = new List<Category>();
            _transactions = new List<Transaction>();
            _categories = new List<Category>();
        }

        public Wallet(int id) : this()
        {
            Id = id;
        }


        // public decimal CurrentBalance()
        // {
        //     decimal res = _initialBalance;
        //     foreach (var transaction in _transactions)
        //         res += transaction.Sum;
        //
        //     return res;
        // }

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
            _currentWithoutInitialBalance += transaction.Sum;
            return true;
        }

        public bool RemoveTransaction(Transaction transaction)
        {
            _currentWithoutInitialBalance -= transaction.Sum;
            return _transactions.Remove(transaction);
        }

        /**
         * @param index  begins with 1
         */
        public bool RemoveTransaction(int index)
        {
            if (index < 1 || index > _transactions.Count)
                return false;

            _currentWithoutInitialBalance -= _transactions[index - 1].Sum;
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

        public bool AddCategory(Category category)
        {
            if (_categories.Contains(category))
                return false;

            _categories.Add(category);
            return true;
        }

        public bool RemoveCategory(Category category)
        {
            return _categories.Remove(category);
        }

        /**
         * @param index  begins with 1
         */
        public bool RemoveCategory(int index)
        {
            if (index < 1 || index > _categories.Count)
                return false;

            _categories.RemoveAt(index - 1);
            return true;
        }

        /**
         * @param index  begins with 1
         */
        public Category GetCategory(int index)
        {
            if (index < 1 || index > _categories.Count)
                return null;

            return _categories[index - 1];
        }

        // /**
        //  * @param from  begins with 1
        //  * @param to    inclusive
        //  */
        // public List<Category> GetCategories(int from, int to)
        // {
        //     int firstIndex = Math.Min(from, to) - 1;
        //     int count = Math.Max(from, to) - firstIndex;
        //     if (firstIndex < 0 || count > _categories.Count)
        //         return null;
        //
        //     return _categories.GetRange(firstIndex, count);
        // }

        public List<Category> GetCategories()
        {
            // return _categories.GetRange(0, _categories.Count);
            return _categories.ToList();
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
