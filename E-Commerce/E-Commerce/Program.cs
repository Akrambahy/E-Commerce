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


        bool AddProduct(int id , string name , decimal price , int stock , Dictionary<int, string> productNames , Dictionary<int, decimal> productPrices , Dictionary<int, int> productStocks)
        {
            if (price <= 0 || stock < 0 || (productNames.ContainsKey(id) || productPrices.ContainsKey(id) || productStocks.ContainsKey(id))) return false ;
            if (!productNames.TryAdd(id, name) )  { return false; }
            if (! productPrices.TryAdd(id, price) )  { RollbackProduct(id, productNames, productPrices, productStocks); return false; }
            if (! productStocks.TryAdd(id, stock))   { RollbackProduct(id, productNames, productPrices, productStocks); return false; }
            return true;

        }

       
   void PrintProduct(int id , Dictionary<int, string> productNames , Dictionary<int, decimal> productPrices , Dictionary<int, int> productStocks)
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
        bool IsAvilbleProduct(int id , Dictionary<int, string> productNames , Dictionary<int, decimal> productPrices , Dictionary<int, int> productStocks)
        {
            return (productNames.ContainsKey(id) && productPrices.ContainsKey(id) && productStocks.ContainsKey(id)); 

        }


        bool RemoveProduct(int id , Dictionary<int, string> productNames, Dictionary<int, decimal> productPrices, Dictionary<int, int> productStocks)
        {
            if (!IsAvilbleProduct(id, productNames, productPrices, productStocks) ) return false;
            string oldName = productNames.GetValueOrDefault(id);
            decimal oldPrice = productPrices.GetValueOrDefault(id);
            int oldStock = productStocks.GetValueOrDefault(id);

           if (! productNames.Remove(id)) { return false; }
            if (!productPrices.Remove(id)) { productNames[id] = oldName ; return false; }
           if(! productStocks.Remove(id)){ productNames[id] = oldName; productPrices[id] = oldPrice; return false; }

            return true;

        }


        bool TryGetProduct(int id, out string?name, out decimal price, out int stock, Dictionary<int, string> productNames, Dictionary<int, decimal> productPrices, Dictionary<int, int> productStocks)
        {
            if (!IsAvilbleProduct(id, productNames, productPrices, productStocks))
            {
                name = null;
               price = 0;
               stock = 0;
                return false; 
            }


             name =  productNames.GetValueOrDefault(id);
             price = productPrices.GetValueOrDefault(id);
             stock = productStocks.GetValueOrDefault(id);

            return true;

        }
        static void Main(string[] args )
        {
            Dictionary<int, string> productNames = new Dictionary<int, string> { };
            Dictionary<int, decimal> productPrices = new Dictionary<int, decimal> { };
            Dictionary<int, int> productStocks = new Dictionary<int, int> { };

        }
    }
}
