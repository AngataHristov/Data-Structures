namespace CollectionOfProducts
{
    using System;

    public class Product : IComparable<Product>
    {
        private int id;
        private string title;
        private string supplier;
        private decimal price;

        public Product(int id, string title, string supplier, decimal price)
        {
            this.Id = id;
            this.Title = title;
            this.Supplier = supplier;
            this.Price = price;
        }

        public int Id
        {
            get
            {
                return this.id;
            }

            private set
            {
                this.id = value;
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }

            private set
            {
                this.ValidatePropertyNotNull(value, "Title");
                this.title = value;
            }
        }

        public string Supplier
        {
            get
            {
                return this.supplier;
            }

            private set
            {
                this.ValidatePropertyNotNull(value, "Supplier");
                this.supplier = value;
            }
        }

        public decimal Price
        {
            get
            {
                return this.price;
            }

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Price can not be negative.");
                }

                this.price = value;
            }
        }

        public int CompareTo(Product other)
        {
            return this.Id.CompareTo(other.Id);
        }

        public override string ToString()
        {
            return string.Format("{0}. {1}, {2}, {3:f2}", this.Id, this.Title, this.Supplier, this.Price);
        }

        private void ValidatePropertyNotNull(string propertyValue, string propertyName)
        {
            if (string.IsNullOrEmpty(propertyValue))
            {
                throw new ArgumentNullException(
                    string.Format("{0} can not be null or empty", propertyName));
            }
        }
    }
}
