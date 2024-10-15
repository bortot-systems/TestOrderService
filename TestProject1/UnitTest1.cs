using TestOrderService.Models;
using TestOrderService.Services;

namespace TestProject1
{
    public class UnitTest1
    {
        public class OrderPackingServiceTests
        {
            [Fact]
            public void ProcessOrders_ShouldPackProductsCorrectly()
            {
                // Arrange
                var service = new OrderPackingService();
                var orders = new OrdersRequest
                {
                    Orders = new List<Order>
                    {
                        new Order
                        {
                            OrderId = 1,
                            Products = new List<Product>
                            {
                                new Product { ProductId = "1", Dimensions = new Dimensions { Height = 10, Width = 10, Length = 10 } },
                                new Product { ProductId = "2", Dimensions = new Dimensions { Height = 15, Width = 15, Length = 15 } },
                                new Product { ProductId = "3", Dimensions = new Dimensions { Height = 35, Width = 15, Length = 25 } }
                            }
                        }
                    }
                };

                // Act
                var result = service.ProcessOrders(orders);

                // Print
                foreach (var packingResult in result)
                {
                    Console.WriteLine($"Order ID: {packingResult.OrderId}");
                    foreach (var box in packingResult.Boxes)
                    {
                        Console.WriteLine($"  Box ID: {box.BoxId}");
                        Console.WriteLine($"  Products: {string.Join(", ", box.Products)}");
                    }
                }

                // Assert
                Assert.NotEmpty(result);
                Assert.Equal(1, result.Count);
                Assert.NotEmpty(result[0].Boxes);
            }
        }
    }
}