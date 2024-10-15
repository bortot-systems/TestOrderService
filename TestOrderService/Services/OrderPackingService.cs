using TestOrderService.Models;

namespace TestOrderService.Services
{
    public class OrderPackingService
    {
        private readonly List<Box> _availableBoxes;

        public OrderPackingService()
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
            return orders.Orders.Select(order => PackOrder(order)).ToList();
        }

        private OrderPackingResult PackOrder(Order order)
        {
            var packingResult = new OrderPackingResult
            {
                OrderId = order.OrderId,
                Boxes = new List<BoxAllocation>()
            };

            var productsToPack = order.Products.ToList();
            bool hasFitProducts = false;

            foreach (var box in _availableBoxes)
            {
                var allocatedProducts = PackProductsInBox(box, productsToPack);

                if (allocatedProducts.Any())
                {
                    packingResult.Boxes.Add(new BoxAllocation
                    {
                        BoxId = box.BoxId,
                        Products = allocatedProducts,
                        Message = null
                    });
                    hasFitProducts = true;
                }          
            }

            if (!hasFitProducts)
            {
                packingResult.Boxes.Add(new BoxAllocation
                {
                    BoxId = null,
                    Products = productsToPack.Select(p => p.ProductId).ToList(),
                    Message = "Produto não cabe em nenhuma caixa disponível."
                });
            }

            return packingResult;
        }

        private List<string> PackProductsInBox(Box box, List<Product> productsToPack)
        {
            var allocatedProducts = new List<string>();
            var remainingVolume = CalculateBoxVolume(box);

            for (int i = 0; i < productsToPack.Count; i++)
            {
                var product = productsToPack[i];
                var productVolume = CalculateProductVolume(product.Dimensions);

                if (productVolume <= remainingVolume && CanFitProductInBox(box, product.Dimensions))
                {
                    allocatedProducts.Add(product.ProductId);
                    remainingVolume -= productVolume;
                    productsToPack.RemoveAt(i);
                    i--;
                }
            }

            return allocatedProducts;
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
