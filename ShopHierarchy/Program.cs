namespace ShopHierarchy
{
    using Microsoft.EntityFrameworkCore;
    using ShopHierarchy.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        static void Main(string[] args)
        {
            using (var db = new ShopDbContext.ShopDbContext())
            {
                PrepareDatabase(db);
                SaveSalesman(db);
                SaveItems(db);
                ProcessCommand(db);
                //PrintSalesmanWithCustomerCount(db);
                //PrintCustomersWithOrderAndReviewsCount(db);                
            }
        }


        private static void PrepareDatabase(ShopDbContext.ShopDbContext db)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }

        private static void SaveSalesman(ShopDbContext.ShopDbContext db)
        {
            var salesmen = Console.ReadLine().Split(';');

            foreach (var salesman in salesmen)
            {
                db.Add(new SalesMan { Name = salesman });

            }
            db.SaveChanges();
        }

        private static void ProcessCommand(ShopDbContext.ShopDbContext db)
        {
            while (true)
            {
                var line = Console.ReadLine();
                var customerId = 0;
                var isParsedCustomerId = int.TryParse(line, out customerId);

                if (line == "END")
                {
                    break;
                }

                var parts = line.Split('-');
                var command = parts[0];
                var arguments = parts[1];

                switch (command)
                {
                    case "register":
                        RegisterCustomer(db, arguments);
                        break;
                    case "order":
                        SaveOrder(db, arguments);
                        break;
                    case "review":
                        SaveReview(db, arguments);
                        break;

                    default:
                        break;
                }
            }
        }

        private static void SaveReview(ShopDbContext.ShopDbContext db, string arguments)
        {
            var parts = arguments.Split(';');            
            var customerId = int.Parse(parts[0]);
            var itemId = int.Parse(parts[1]);

            db.Add(new Review
            {
                CustomerId = customerId,
                ItemId = itemId
            });
            db.SaveChanges();
        }

        private static void SaveOrder(ShopDbContext.ShopDbContext db, string arguments)
        {
            var parts = arguments.Split(';');
            var customerId = int.Parse(parts[0]);
            var order = new Order
            {
                CustomerId = customerId,
            };

            for (int i = 1; i < parts.Length; i++)
            {
                var itemId = int.Parse(parts[i]);
                order.Items.Add(new ItemOrder { ItemId = itemId});
            }            

            db.Add(order);
            db.SaveChanges();
        }

        private static void RegisterCustomer(ShopDbContext.ShopDbContext db, string arguments)
        {
            var parts = arguments.Split(';');
            var customerName = parts[0];
            var salesmanId = int.Parse(parts[1]);

            db.Add(new Customer
            {
                Name = customerName,
                SalesmanId = salesmanId,
            });

            db.SaveChanges();
        }

        private static void PrintSalesmanWithCustomerCount(ShopDbContext.ShopDbContext db)
        {
            var salesmenData = db.Salesmans
                .Select(s => new
                {
                    s.Name,
                    Customers = s.Customers.Count
                })
                .OrderByDescending(s => s.Customers)
                .ThenBy(s => s.Name)
                .ToList();

            foreach (var salesman in salesmenData)
            {
                Console.WriteLine($"{salesman.Name} - {salesman.Customers} customers");
            }
        }

        private static void PrintCustomersWithOrderAndReviewsCount(ShopDbContext.ShopDbContext db)
        {
            var customerData = db
                .Customers
                .Select(c => new
                {
                    c.Name,
                    Orders = c.Orders.Count,
                    Reviews = c.Reviews.Count
                })
                .OrderByDescending(c => c.Orders)
                .ThenByDescending(c => c.Reviews)
                .ToList();

            foreach (var customer in customerData)
            {
                Console.WriteLine(customer.Name);
                Console.WriteLine($"Orders: {customer.Orders}");
                Console.WriteLine($"Reviews: {customer.Reviews}");
            }
        }
        private static void SaveItems(ShopDbContext.ShopDbContext db)
        {
            while (true)
            {
                var line = Console.ReadLine();

                if (line == "END")
                {
                    break;
                }

                var parts = line.Split(';');
                var itemName = parts[0];
                var itemPrice = decimal.Parse(parts[1]);

                db.Add(new Item
                {
                    Name = itemName,
                    Price = itemPrice
                });
                                
            }
            db.SaveChanges();
        }
        private static void PrintNumberOfItemsOrderAndCountOfReviews(ShopDbContext.ShopDbContext db, int customerId)
        {
            var customer = db.Customers
                             .Where(c => c.Id == customerId)
                             .Select(c => new
                             {
                                 Orders = c.Orders
                                           .Select(o => new
                                           {
                                               o.Id,
                                               Items = o.Items.Count
                                           })
                                           .OrderBy(o => o.Id),
                                 Reviews = c.Reviews.Count
                             })
                             .FirstOrDefault();

            foreach (var order in customer.Orders)
            {
                Console.WriteLine($"order {order.Id}: {order.Items} items");
            }

            Console.WriteLine($"reviews: {customer.Reviews}");
        }

    }
}
