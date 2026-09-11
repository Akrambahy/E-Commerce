using System.Collections.Generic;

namespace E_Commerce
{
    internal class Program

    {


        void RollbackProduct(int id, Dictionary<int, string> productNames, Dictionary<int, decimal> productPrices, Dictionary<int, int> productStocks)
        {

            if (productNames.ContainsKey(id)) productNames.Remove(id);
            if (productPrices.ContainsKey(id)) productPrices.Remove(id);
            if (productStocks.ContainsKey(id)) productStocks.Remove(id);


        }




        bool AddProduct(int id, string name, decimal price, int stock, Dictionary<int, string> productNames, Dictionary<int, decimal> productPrices, Dictionary<int, int> productStocks)
        {
            if (price <= 0 || stock < 0 || (productNames.ContainsKey(id) || productPrices.ContainsKey(id) || productStocks.ContainsKey(id))) return false;
            if (!productNames.TryAdd(id, name)) { return false; }
            if (!productPrices.TryAdd(id, price)) { RollbackProduct(id, productNames, productPrices, productStocks); return false; }
            if (!productStocks.TryAdd(id, stock)) { RollbackProduct(id, productNames, productPrices, productStocks); return false; }
            return true;

        }
        void GetProductBySubName(string name, Dictionary<int, string> productNames, Dictionary<int, decimal> productPrices, Dictionary<int, int> productStocks)
        {


            List<KeyValuePair<int, string>> products = productNames.Where(product => product.Value.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();

            foreach (KeyValuePair<int, string> product in products)
            {
                Console.WriteLine($"id :{product.Key}  , Name : {product.Value} , Price : {productPrices[product.Key]} , Stocks  : {productStocks[product.Key]}\n");
            }

        }
        void GetProductByLessOrEqualPrice(decimal price, Dictionary<int, string> productNames, Dictionary<int, decimal> productPrices, Dictionary<int, int> productStocks)
        {

            List<KeyValuePair<int, decimal>> products = productPrices.Where(product => product.Value <= price).ToList();

            foreach (KeyValuePair<int, decimal> product in products)
            {
                Console.WriteLine($"id :{product.Key}  , Name : {productNames[product.Key]} , Price : {productPrices[product.Key]} , Stocks  : {productStocks[product.Key]}\n");
            }

        }
        void SortProducts(Dictionary<int, string> productNames, Dictionary<int, decimal> productPrices, Dictionary<int, int> productStocks)
        {

            List<KeyValuePair<int, decimal>> aProductPrices = productPrices.OrderByDescending(price => price.Value).ToList();
            //    List<KeyValuePair<int , string>> aProductNames = productNames.OrderByDescending(Name => productPrices.GetValueOrDefault(Name.Key)).ToList();
            //    List<KeyValuePair<int , int>> aProductStocks = productStocks.OrderByDescending(Stock => productPrices[Stock.Key]).ToList();

            foreach (KeyValuePair<int, decimal> product in aProductPrices)
            {
                Console.WriteLine($"id :{product.Key}  , Name : {productNames[product.Key]} , Price : {productPrices[product.Key]} , Stocks  : {productStocks[product.Key]}\n");
            }

        }




        void PrintProduct(int id, Dictionary<int, string> productNames, Dictionary<int, decimal> productPrices, Dictionary<int, int> productStocks)
        {
            if (!(productNames.ContainsKey(id) && productPrices.ContainsKey(id) && productStocks.ContainsKey(id))) { Console.WriteLine("Product Not Found "); return; }
            Console.WriteLine($"Prouduct Id : {id}");
            Console.WriteLine($"Prouduct Name : {productNames.GetValueOrDefault(id)}");
            Console.WriteLine($"Prouduct Price : {productPrices.GetValueOrDefault(id)}");
            Console.WriteLine($"Prouduct Stock : {productStocks.GetValueOrDefault(id)}");

        }
        bool UpdateProduct(int id, string name, decimal price, int stock, Dictionary<int, string> productNames, Dictionary<int, decimal> productPrices, Dictionary<int, int> productStocks)
        {
            if (!IsAvilbleProduct(id, productNames, productPrices, productStocks) || stock < 0 || price <= 0) return false;

            productNames[id] = name;
            productPrices[id] = price;
            productStocks[id] = stock;
            return true;

        }
        bool IsAvilbleProduct(int id, Dictionary<int, string> productNames, Dictionary<int, decimal> productPrices, Dictionary<int, int> productStocks)
        {
            return (productNames.ContainsKey(id) && productPrices.ContainsKey(id) && productStocks.ContainsKey(id));

        }


        bool RemoveProduct(int id, Dictionary<int, string> productNames, Dictionary<int, decimal> productPrices, Dictionary<int, int> productStocks)
        {
            if (!IsAvilbleProduct(id, productNames, productPrices, productStocks)) return false;
            string oldName = productNames.GetValueOrDefault(id);
            decimal oldPrice = productPrices.GetValueOrDefault(id);
            int oldStock = productStocks.GetValueOrDefault(id);

            if (!productNames.Remove(id)) { return false; }
            if (!productPrices.Remove(id)) { productNames[id] = oldName; return false; }
            if (!productStocks.Remove(id)) { productNames[id] = oldName; productPrices[id] = oldPrice; return false; }

            return true;

        }


        bool TryGetProduct(int id, out string? name, out decimal price, out int stock, Dictionary<int, string> productNames, Dictionary<int, decimal> productPrices, Dictionary<int, int> productStocks)
        {
            if (!IsAvilbleProduct(id, productNames, productPrices, productStocks))
            {
                name = null;
                price = 0;
                stock = 0;
                return false;
            }


            name = productNames.GetValueOrDefault(id);
            price = productPrices.GetValueOrDefault(id);
            stock = productStocks.GetValueOrDefault(id);

            return true;

        }
        void RollbackCustomer(int id, Dictionary<int, string> customerNames, Dictionary<int, string> customerEmails, Dictionary<int, string> customerPhones)

        {

            if (customerNames.ContainsKey(id)) customerNames.Remove(id);
            if (customerEmails.ContainsKey(id)) customerEmails.Remove(id);
            if (customerPhones.ContainsKey(id)) customerPhones.Remove(id);


        }

        void RollbackRemovedCustomer(int id, Dictionary<int, string> customerNames, Dictionary<int, string> customerEmails, Dictionary<int, string> customerPhones)

        {



        }

        bool AddCustomer(int id, string name, string email, string phone, Dictionary<int, string> customerNames, Dictionary<int, string> customerEmails, Dictionary<int, string> customerPhones)
        {
            if (name.Length == 0 || email.Length == 0 || phone.Length == 0 || (customerNames.ContainsKey(id) || customerEmails.ContainsKey(id) || customerPhones.ContainsKey(id))) return false;
            if (!customerNames.TryAdd(id, name)) { return false; }
            if (!customerEmails.TryAdd(id, email)) { RollbackCustomer(id, customerNames, customerEmails, customerPhones); return false; }
            if (!customerPhones.TryAdd(id, phone)) { RollbackCustomer(id, customerNames, customerEmails, customerPhones); return false; }
            return true;

        }


        bool IsAvailableCustomer(int id, Dictionary<int, string> customerNames, Dictionary<int, string> customerEmails, Dictionary<int, string> customerPhones
     )
        {
            return (customerNames.ContainsKey(id) && customerEmails.ContainsKey(id) && customerPhones.ContainsKey(id));
        }
        bool TryGetCustomer(
            int id,
            out string? name,
            out string? email,
            out string? phone,

Dictionary<int, string> customerNames, Dictionary<int, string> customerEmails, Dictionary<int, string> customerPhones
)
        {
            if (!IsAvailableCustomer(id, customerNames, customerPhones, customerEmails))
            {
                name = default;
                phone = default;
                email = default;
                return false;
            }

            name = customerNames.GetValueOrDefault(id);
            phone = customerPhones.GetValueOrDefault(id);
            email = customerEmails.GetValueOrDefault(id);
            return true;

        }
        bool UpdateCustomer(int id, string name, string email, string phone, Dictionary<int, string> customerNames, Dictionary<int, string> customerEmails, Dictionary<int, string> customerPhones)
        {
            if (name.Length == 0 || email.Length == 0 || phone.Length == 0 || !IsAvailableCustomer(id, customerNames, customerEmails, customerPhones)) return false;
            customerNames[id] = name;
            customerEmails[id] = email;
            customerPhones[id] = phone;
            return true;

        }



        bool RemoveCustomer(int id, Dictionary<int, string> customerNames, Dictionary<int, string> customerEmails, Dictionary<int, string> customerPhones)
        {
            if (!IsAvailableCustomer(id, customerNames, customerEmails, customerPhones)) return false;
            string oldName = customerNames[id];
            string oldEmail = customerEmails[id];
            string oldPhone = customerPhones[id];
            if (!customerNames.Remove(id)) { return false; }
            if (!customerEmails.Remove(id)) { customerNames[id] = oldName; return false; }
            if (!customerPhones.Remove(id)) { customerNames[id] = oldName; customerEmails[id] = oldEmail; return false; }
            return true;
        }


        bool IsExistInCart(int productId, Dictionary<int, int> cart)
        {
            return (cart.ContainsKey(productId));
        }


        bool AddToCart(int productId, int quantity, Dictionary<int, int> cart, Dictionary<int, string> productNames, Dictionary<int, decimal> productPrices, Dictionary<int, int> productStocks)
        {


            if (!IsAvilbleProduct(productId, productNames, productPrices, productStocks) || quantity <= 0 || quantity > productStocks.GetValueOrDefault(productId)) return false;
            if (cart.TryGetValue(productId, out int currentQuantity) && quantity + currentQuantity <= productStocks[productId]) { cart[productId] += quantity; return true; }
            if (!cart.TryAdd(productId, quantity)) { return false; }
            return true;

        }





        bool RemoveFromCart(int productId, int quantity, Dictionary<int, int> cart)

        {
            if (!cart.TryGetValue(productId, out int currentQuantity) || quantity <= 0 || quantity > currentQuantity) return false;
            if (quantity == currentQuantity) { cart.Remove(productId); return true; }
            cart[productId] -= quantity;
            return true;

        }

        decimal CalculateCartTotal(Dictionary<int, int> cart, Dictionary<int, decimal> productPrices)
        {
            decimal total = 0m;




            foreach (KeyValuePair<int, int> product in cart)
            {

                if (!productPrices.TryGetValue(product.Key, out decimal currentPrice))
                {
                    throw new ArgumentException($"product {product.Key} not has value");
                }

                total += (currentPrice * product.Value);

            }

            return total;
        }

        enum DiscountType
        {

            NoDiscount,
            Percentage10,
            Percentage20,
            Fixed500

        }



        decimal ApplyDiscount(decimal total, DiscountType discountType)
        {
            if (total <= 0) throw new ArgumentException("total Is Zero");

            switch (discountType)
            {
                case DiscountType.NoDiscount:

                    return total;

                case DiscountType.Percentage10:
                    return (total - (total * (decimal)0.10));
                case DiscountType.Percentage20:
                    return (total - (total * (decimal)0.20));
                case DiscountType.Fixed500:
                    return (total - 500);
                default:
                    throw new ArgumentOutOfRangeException("discount exception");



            }


        }



        bool UpdateStock(Dictionary<int, int> cart, Dictionary<int, int> productStocks)
        {

            foreach (KeyValuePair<int, int> product in cart)
            {
                if (productStocks[product.Key] < product.Value) return false;

            }
            foreach (KeyValuePair<int, int> product in cart)
            {
                productStocks[product.Key] -= product.Value;
            }
            return true;

        }

        bool Checkout(int orderId, Dictionary<int, Order> orders, int customerId, DateTime orderDate, out Order order, Dictionary<int, int> cart, Dictionary<int, decimal> productPrices, DiscountType discountType, Dictionary<int, int> productStocks)
        {
            Order currentOrder = new Order();

            decimal finalTotal = ApplyDiscount(CalculateCartTotal(cart, productPrices), discountType);
            if (!CreateOrder(orderId, orders, customerId, finalTotal, orderDate, cart, out currentOrder) )
            {
                order = default;
                return false;
            }
            if (!UpdateStock(cart, productStocks)) {
                 orders.Remove(orderId);
                 order=default;
                return false;
                throw new ArgumentException("error on stocks");
                }

            order = currentOrder;


            return true;
        }

        struct Order
        {
            public int OrderId;
            public int CustomerId;
            public decimal Total;
            public DateTime OrderDate;
            public Dictionary<int, int> Items;
        }


        bool IsExistOrder(int orderId, Dictionary<int, Order> orders)
        {
            return orders.ContainsKey(orderId);
        }




        bool CreateOrder(int orderId, Dictionary<int, Order> orders, int customerId, decimal total, DateTime orderDate, Dictionary<int, int> items, out Order order)
        {
            if (IsExistOrder(orderId, orders))
            {
                order = default;
                return false;
            }

            Order currentOrder = new Order();
            currentOrder.OrderId = orderId;
            currentOrder.CustomerId = customerId;
            currentOrder.Total = total;
            currentOrder.OrderDate = orderDate;
            currentOrder.Items = new Dictionary<int, int>(items);


            if (!orders.TryAdd(orderId, currentOrder))
            {
                order = default;
                return false;
            }

            order = currentOrder;

            return true;
        }
        bool TryRemoveOrder(int orderId, Dictionary<int, Order> orders,  Dictionary<int, int> productStocks)
        {
            if (!orders.TryGetValue(orderId , out Order order))
            {
                return false;
            }
     
            
 Dictionary<int, int> oldProductStocks= new Dictionary<int, int> (productStocks);

           
 foreach (KeyValuePair<int , int> item in order.Items)
            {
                if(!productStocks.ContainsKey(item.Key)){
                    productStocks.Clear();

             foreach (KeyValuePair<int , int> productStock in oldProductStocks)
            {
              

                productStocks.Add(productStock.Key,productStock.Value);
            
            }
                
                 return false ;}
                productStocks[item.Key]+=item.Value;
            }
            if (!orders.Remove(orderId))
            {
                productStocks.Clear();

             foreach (KeyValuePair<int , int> productStock in oldProductStocks)
            {
              

                productStocks.Add(productStock.Key,productStock.Value);
            
            }
     
                return false ;
            }

            return true;
        }



        bool TryGetOrder(int orderId, Dictionary<int, Order> orders, out Order order)
        {
            if (!orders.TryGetValue(orderId, out order))
            {
              
                return false ;
            }
                  
          
           return true ;
            

        }




        void PrintItems(Dictionary<int, int> items ){
             
             if(items.Count==0)
            {
              Console.WriteLine("items not found");
              return;
              
            }

            else
            {
                foreach(KeyValuePair<int , int> item in items)
                Console.WriteLine($"Product Id: {item.Key} | Quantity: {item.Value}");
                
            }
        }
        void PrintOrder(int orderId,Dictionary<int, Order> orders ){
             Order order;
             if(!TryGetOrder(orderId,orders,out order))
            {
              Console.WriteLine("order not found");
              return;
              
            }

            else
            {
                Console.WriteLine($"Order Id {order.OrderId}");
                Console.WriteLine($"Customer Id {order.CustomerId}");
                Console.WriteLine($"Total {order.Total}");
                Console.WriteLine($"Order Date {order.OrderDate}");
                Console.WriteLine($"Order Items : ");
                PrintItems(order.Items);
            }
        }





        bool ChangeCartQuantity(int productId,int newQuantity,  Dictionary<int, int> cart, Dictionary<int, int> productStocks)
        {
            if(!IsExistInCart(productId,cart) || newQuantity <= 0 ||! productStocks.TryGetValue(productId,out int stock)|| newQuantity > stock) return false ;
             
             cart[productId]= newQuantity;
             return true;

        }



// Rules
// Product لازم يكون موجود في الـ cart.
// newQuantity لازم تكون أكبر من 0.
// newQuantity مينفعش تتعدى الـ stock.
// لو كل حاجة صحيحة → نحدث الكمية.
// لو أي validation فشل → الـ cart مايتغيرش.














































        static void Main(string[] args)
        {
            Dictionary<int, string> productNames = new Dictionary<int, string> { };
            Dictionary<int, decimal> productPrices = new Dictionary<int, decimal> { };
            Dictionary<int, int> productStocks = new Dictionary<int, int> { };


            Dictionary<int, string> customerNames = new Dictionary<int, string>();
            Dictionary<int, string> customerEmails = new Dictionary<int, string> { };
            Dictionary<int, string> customerPhones = new Dictionary<int, string> { };


            Dictionary<int, int> cart = new Dictionary<int, int>();
            Dictionary<int, Order> orders = new Dictionary<int, Order>();
        }
    }
}
