using TestOrderService.Models;

namespace TestOrderService.Services
{
    public class OrderPackingService2
    {
        private readonly List<Box> _availableBoxes;

        public OrderPackingService2()
        {
            _availableBoxes = new List<Box>
            {
                new Box { BoxId = "Caixa 1", Height = 30, Width = 40, Length = 80 },
                new Box { BoxId = "Caixa 2", Height = 80, Width = 50, Length = 40 },
                new Box { BoxId = "Caixa 3", Height = 50, Width = 80, Length = 60 }
            };
        }

        public List<OrderPackingResult> ProcessOrders(OrdersRequest orders)
        {
            var results = new List<OrderPackingResult>();

            foreach (var order in orders.Orders)
            {
                var packingResult = new OrderPackingResult
                {
                    OrderId = order.OrderId,
                    Boxes = new List<BoxAllocation>()
                };

                var productsToPack = new Queue<Product>(order.Products);

                bool productsPacked;
                do
                {
                    productsPacked = false;

                    foreach (var box in _availableBoxes)
                    {
                        var allocatedProducts = new List<string>();
                        var remainingVolume = CalculateBoxVolume(box);

                        var productsInCurrentBox = productsToPack.ToList();

                        foreach (var product in productsInCurrentBox)
                        {
                            var productVolume = CalculateProductVolume(product.Dimensions);
                            if (productVolume <= remainingVolume && CanFitProductInBox(box, product.Dimensions))
                            {
                                allocatedProducts.Add(product.ProductId);
                                productsToPack.Dequeue();
                                remainingVolume -= productVolume;
                                productsPacked = true;
                            }
                        }

                        if (allocatedProducts.Any())
                        {
                            packingResult.Boxes.Add(new BoxAllocation
                            {
                                BoxId = box.BoxId,
                                Products = allocatedProducts
                            });
                        }
                    }

                } while (productsPacked && productsToPack.Any());

                results.Add(packingResult);
            }

            return results;
        }

        private bool CanFitProductInBox(Box box, Dimensions productDimensions)
        {
            return productDimensions.Height <= box.Height &&
                   productDimensions.Width <= box.Width &&
                   productDimensions.Length <= box.Length;
        }

        private int CalculateBoxVolume(Box box)
        {
            return box.Height * box.Width * box.Length;
        }

        private int CalculateProductVolume(Dimensions dimensions)
        {
            return dimensions.Height * dimensions.Width * dimensions.Length;
        }
    }
}
