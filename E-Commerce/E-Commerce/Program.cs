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
     
     
             bool IsExistInCart( int productId , Dictionary<int, int> cart)
        {
            return (cart.ContainsKey(productId));
        }


        bool AddToCart( int productId,int quantity, Dictionary<int, int> cart,Dictionary<int, string> productNames, Dictionary<int, decimal> productPrices, Dictionary<int, int> productStocks)
        {


            if(!IsAvilbleProduct(productId , productNames , productPrices , productStocks) || quantity <= 0  || quantity>productStocks.GetValueOrDefault(productId) ) return false;
           if(cart.TryGetValue(productId, out int currentQuantity) && quantity + currentQuantity <= productStocks[productId] ) {cart[productId] +=quantity; return true ; }
           if(! cart.TryAdd(productId , quantity)) {return false;}
           return true ;

        }





        bool RemoveFromCart( int productId,int quantity, Dictionary<int, int> cart)

        {
               if(! cart.TryGetValue(productId , out int currentQuantity)|| quantity <= 0  || quantity>currentQuantity ) return false;
           if(quantity==currentQuantity ) {cart.Remove(productId);     return true ;}
           cart[productId]-=quantity;
           return true ;
          
        }

        decimal CalculateCartTotal(Dictionary<int , int> cart , Dictionary<int, decimal> productPrices)
        {
            decimal total = 0m;


           

     foreach (KeyValuePair<int , int > product in cart)
            {

   if (!productPrices.TryGetValue(product.Key, out decimal currentPrice))
                {
                    throw new ArgumentException($"product {product.Key} not has value");
                }

                total += (currentPrice * product.Value);

            }            

            return total;
        }

        static void Main(string[] args)
        {
            Dictionary<int, string> productNames = new Dictionary<int, string> { };
            Dictionary<int, decimal> productPrices = new Dictionary<int, decimal> { };
            Dictionary<int, int> productStocks = new Dictionary<int, int> { };


            Dictionary<int, string> customerNames = new Dictionary<int, string> ();
            Dictionary<int, string> customerEmails = new Dictionary<int, string> { };
            Dictionary<int, string> customerPhones = new Dictionary<int, string> { };


            Dictionary<int, int> cart = new Dictionary<int, int>();
        }
    }
}
