﻿using System.Drawing;
using System.IO;

namespace BusinessLayer
{
    public class Category
    {
        private int _id;
        private string _name;
        private string _description;
        private Color _color;
        private FileStream _icon;

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

        public Color Color
        {
            get => _color;
            set => _color = value;
        }

        public FileStream Icon
        {
            get => _icon;
            set => _icon = value;
        }


        public Category()
        {

        }

        public Category(int id) : this()
        {
            Id = id;
        }

        ~Category()
        {
            _icon?.Close();
        }


        public bool Validate()
        {
            bool res = true;

            if (string.IsNullOrWhiteSpace(_name))
                res = false;
            if (_color.IsEmpty)
                res = false;

            return res;
        }
    }
}
