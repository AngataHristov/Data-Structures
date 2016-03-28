namespace CollectionOfProducts
{
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class CollectionOfProducts
    {
        private IDictionary<int, Product> productsById;
        private OrderedDictionary<decimal, SortedSet<Product>> productsByPrice;
        private IDictionary<string, SortedSet<Product>> productsByTitle;
        private IDictionary<string, OrderedDictionary<decimal, SortedSet<Product>>> productsByTitleAndPrice;
        private IDictionary<string, OrderedDictionary<decimal, SortedSet<Product>>> productsBySupplierAndPrice;

        public CollectionOfProducts()
        {
            this.productsById = new Dictionary<int, Product>();
            this.productsByPrice = new OrderedDictionary<decimal, SortedSet<Product>>();
            this.productsByTitle = new Dictionary<string, SortedSet<Product>>();
            this.productsByTitleAndPrice = new Dictionary<string, OrderedDictionary<decimal, SortedSet<Product>>>();
            this.productsBySupplierAndPrice = new Dictionary<string, OrderedDictionary<decimal, SortedSet<Product>>>();
            this.Count = 0;
        }

        public int Count { get; private set; }

        public void Add(int id, string title, string supplier, decimal price)
        {
            var product = new Product(id, title, supplier, price);

            if (this.productsById.ContainsKey(id))
            {
                this.Remove(id);
            }

            this.productsById[id] = product;
            this.AddInProductsByPrice(product);
            this.AddInProductsByTitle(product);
            this.AddProductsByTitleAndPrice(product);
            this.AddProductsBySupplierAndPrice(product);

            this.Count++;
        }

        public bool Remove(int id)
        {
            if (!this.productsById.ContainsKey(id))
            {
                return false;
            }

            Product product = this.productsById[id];

            string productTitle = product.Title;
            decimal productPrice = product.Price;
            string productSupplier = product.Supplier;

            this.productsById.Remove(id);

            this.RemoveFromProductsByPrice(productPrice, product);
            this.RemoveFromProductsByTitle(productTitle, product);
            this.RemoveFromProductsByTitleAndPrice(productTitle, productPrice, product);
            this.RemoveFromProductsBySupplierAndPrice(productSupplier, productPrice, product);

            this.Count--;

            return true;
        }

        public IEnumerable<Product> FindProductsInPriceRange(decimal startPrice, decimal endPrice)
        {
            var productsInPriceRange = this.productsByPrice.Range(startPrice, true, endPrice, true);

            if (productsInPriceRange == null)
            {
                yield break;
            }

            foreach (KeyValuePair<decimal, SortedSet<Product>> productsByPrice in productsInPriceRange)
            {
                foreach (Product product in productsByPrice.Value)
                {
                    yield return product;
                }
            }
        }

        public IEnumerable<Product> FindProductsByTitle(string title)
        {
            if (!this.productsByTitle.ContainsKey(title))
            {
                return Enumerable.Empty<Product>();
            }

            return this.productsByTitle[title];

        }

        public IEnumerable<Product> FindProductsByTitleAndPrice(string title, decimal price)
        {
            if (!this.productsByTitleAndPrice.ContainsKey(title) || !this.productsByTitleAndPrice[title].ContainsKey(price))
            {
                return Enumerable.Empty<Product>();
            }

            var products = this.productsByTitleAndPrice[title][price];

            return products;
        }

        public IEnumerable<Product> FindProductsByTitleInPriceRange(string title, decimal startPrice, decimal endPrice)
        {
            if (!this.productsByTitleAndPrice.ContainsKey(title))
            {
                yield break;
            }

            var productsInPriceRange = this.productsByTitleAndPrice[title].Range(startPrice, true, endPrice, true);

            if (productsInPriceRange == null)
            {
                yield break;
            }

            foreach (KeyValuePair<decimal, SortedSet<Product>> productsByPrice in productsInPriceRange)
            {
                foreach (Product product in productsByPrice.Value)
                {
                    yield return product;
                }
            }
        }

        public IEnumerable<Product> FindProductsBySupplierAndPrice(string supplier, decimal price)
        {
            if (!this.productsBySupplierAndPrice.ContainsKey(supplier) || !this.productsBySupplierAndPrice[supplier].ContainsKey(price))
            {
                return Enumerable.Empty<Product>();
            }

            var products = this.productsBySupplierAndPrice[supplier][price];

            return products;
        }

        public IEnumerable<Product> FindProductsBySupplierInPriceRange(string supplier, decimal startPrice, decimal endPrice)
        {
            if (!this.productsBySupplierAndPrice.ContainsKey(supplier))
            {
                yield break;
            }

            var products = this.productsBySupplierAndPrice[supplier].Range(startPrice, true, endPrice, true);

            foreach (KeyValuePair<decimal, SortedSet<Product>> productsByPrice in products)
            {
                foreach (Product product in productsByPrice.Value)
                {
                    yield return product;
                }
            }
        }

        private void AddInProductsByPrice(Product product)
        {
            decimal productPrice = product.Price;

            if (!this.productsByPrice.ContainsKey(productPrice))
            {
                this.productsByPrice[productPrice] = new SortedSet<Product>();
            }

            this.productsByPrice[productPrice].Add(product);
        }

        private void AddInProductsByTitle(Product product)
        {
            string productTitle = product.Title;

            if (!this.productsByTitle.ContainsKey(productTitle))
            {
                this.productsByTitle[productTitle] = new SortedSet<Product>();
            }

            this.productsByTitle[productTitle].Add(product);
        }

        private void AddProductsByTitleAndPrice(Product product)
        {
            string productTitle = product.Title;
            decimal productPrice = product.Price;

            if (!this.productsByTitleAndPrice.ContainsKey(productTitle))
            {
                this.productsByTitleAndPrice[productTitle] = new OrderedDictionary<decimal, SortedSet<Product>>();
            }

            if (!this.productsByTitleAndPrice[productTitle].ContainsKey(productPrice))
            {
                this.productsByTitleAndPrice[productTitle][productPrice] = new SortedSet<Product>();
            }

            this.productsByTitleAndPrice[productTitle][productPrice].Add(product);
        }

        private void AddProductsBySupplierAndPrice(Product product)
        {
            string productSupplier = product.Supplier;
            decimal productPrice = product.Price;

            if (!this.productsBySupplierAndPrice.ContainsKey(productSupplier))
            {
                this.productsBySupplierAndPrice[productSupplier] = new OrderedDictionary<decimal, SortedSet<Product>>();
            }

            if (!this.productsBySupplierAndPrice[productSupplier].ContainsKey(productPrice))
            {
                this.productsBySupplierAndPrice[productSupplier][productPrice] = new SortedSet<Product>();
            }

            this.productsBySupplierAndPrice[productSupplier][productPrice].Add(product);

        }

        private void RemoveFromProductsByPrice(decimal productPrice, Product product)
        {
            this.productsByPrice[productPrice].Remove(product);
            if (this.productsByPrice[productPrice].Count == 0)
            {
                this.productsByPrice.Remove(productPrice);
            }
        }

        private void RemoveFromProductsByTitle(string productTitle, Product product)
        {
            this.productsByTitle[productTitle].Remove(product);
            if (this.productsByTitle[productTitle].Count == 0)
            {
                this.productsByTitle.Remove(productTitle);
            }
        }

        private void RemoveFromProductsByTitleAndPrice(string productTitle, decimal productPrice, Product product)
        {
            this.productsByTitleAndPrice[productTitle][productPrice].Remove(product);
            if (this.productsByTitleAndPrice[productTitle][productPrice].Count == 0)
            {
                this.productsByTitleAndPrice[productTitle].Remove(productPrice);
            }

            if (this.productsByTitleAndPrice[productTitle].Count == 0)
            {
                this.productsByTitleAndPrice.Remove(productTitle);
            }
        }

        private void RemoveFromProductsBySupplierAndPrice(string productSupplier, decimal productPrice, Product product)
        {
            this.productsBySupplierAndPrice[productSupplier][productPrice].Remove(product);
            if (this.productsBySupplierAndPrice[productSupplier][productPrice].Count == 0)
            {
                this.productsBySupplierAndPrice[productSupplier].Remove(productPrice);
            }

            if (this.productsBySupplierAndPrice[productSupplier].Count == 0)
            {
                this.productsBySupplierAndPrice.Remove(productSupplier);
            }
        }
    }
}
