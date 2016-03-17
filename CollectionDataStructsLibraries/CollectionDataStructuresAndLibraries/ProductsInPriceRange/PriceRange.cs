namespace ProductsInPriceRange
{
    using System;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class PriceRange
    {
        public static void Main()
        {
            OrderedBag<Product> products = new OrderedBag<Product>();

            int numberOfProducts = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfProducts; i++)
            {
                var tokens = Console.ReadLine()
                            .Split()
                            .ToArray();

                string productName = tokens[0];
                decimal price = decimal.Parse(tokens[1]);

                Product product = new Product(productName, price);

                products.Add(product);
            }

            decimal[] rangeTokens = Console.ReadLine()
                                    .Split()
                                    .Select(decimal.Parse)
                                    .ToArray();
            decimal startPrice = rangeTokens[0];
            decimal endPrice = rangeTokens[1];

            var currentRange = products.Range(new Product(null, startPrice), true, new Product(null, endPrice), true);

            if (currentRange.Count == 0)
            {
                Console.WriteLine("No products in this price range!");
                Environment.Exit(0);
            }

            foreach (var product in currentRange.Take(20))
            {
                Console.WriteLine(product);
            }

            Console.WriteLine();
        }
    }
}
