using System.Collections.Generic;

namespace BusinessLayer
{
    public class User
    {
        private int _id;
        private string _firstName;
        private string _lastName;
        private string _email;
        private List<Wallet> _wallets;
        private List<SharedWallet> _sharedWallets;
        private List<Category> _categories;

        public int Id
        {
            get => _id;
            private set => _id = value;
        }

        public string FirstName
        {
            get => _firstName;
            set => _firstName = value;
        }

        public string LastName
        {
            get => _lastName;
            set => _lastName = value;
        }

        // public string FullName
        // {
        //     get
        //     {
        //         string result = FirstName;
        //         if (!string.IsNullOrWhiteSpace(LastName))
        //         {
        //             if (!string.IsNullOrWhiteSpace(FirstName))
        //                 result += ", ";
        //
        //             result += LastName;
        //         }
        //
        //         return result;
        //     }
        // }

        public string Email
        {
            get => _email;
            set => _email = value;
        }

        public List<Wallet> Wallets
        {
            get => _wallets;
            set => _wallets = value;
        }

        public List<SharedWallet> SharedWallets
        {
            get => _sharedWallets;
            set => _sharedWallets = value;
        }

        public List<Category> Categories
        {
            get => _categories;
            set => _categories = value;
        }


        public User()
        {
            Wallets = new List<Wallet>();
            SharedWallets = new List<SharedWallet>();
            Categories = new List<Category>();
        }

        public User(int id) : this()
        {
            Id = id;
        }


        /**
         * @param index  begins with 1
         */
        public SharedWallet ShareWallet(int index)
        {
            return new SharedWallet(_wallets[index - 1]);
        }

        public bool Validate()
        {
            bool res = true;

            if (string.IsNullOrWhiteSpace(_firstName))
                res = false;
            if (string.IsNullOrWhiteSpace(_lastName))
                res = false;
            if (string.IsNullOrWhiteSpace(_email))
                res = false;

            return res;
        }
    }
}
