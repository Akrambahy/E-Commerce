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

        void RollbackRemovedCustomer(int id,  Dictionary<int, string> customerNames, Dictionary<int, string> customerEmails, Dictionary<int, string> customerPhones)

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


    bool IsAvailableCustomer(int id ,Dictionary <int , string> customerNames, Dictionary <int , string> customerEmails , Dictionary <int , string> customerPhones
 )
        {
            return (customerNames.ContainsKey(id)&& customerEmails.ContainsKey(id)&& customerPhones.ContainsKey(id));
        }
        bool TryGetCustomer(
            int id,
            out string? name,
            out string? email,
            out string? phone,
        
Dictionary <int , string> customerNames, Dictionary <int , string> customerEmails , Dictionary <int , string> customerPhones
)
        {
            if(!IsAvailableCustomer(id, customerNames , customerPhones , customerEmails))
            {
                name=default;
                phone =default;
                email=default;
                return false ;
            }
            
            name=customerNames.GetValueOrDefault(id);
                phone =customerPhones.GetValueOrDefault(id);
                email=customerEmails.GetValueOrDefault(id);
                return true ;

        }
bool UpdateCustomer(int id, string name, string email, string phone, Dictionary<int, string> customerNames, Dictionary<int, string> customerEmails, Dictionary<int, string> customerPhones)
        {
            if (name.Length == 0 || email.Length == 0 || phone.Length == 0 ||  ! IsAvailableCustomer(id,customerNames , customerEmails ,customerPhones)) return false;
 customerNames[id] = name;
 customerEmails[id] = email;
 customerPhones[id] = phone;
            return true;

        }       

            

        bool RemoveCustomer(int id ,  Dictionary<int, string> customerNames, Dictionary<int, string> customerEmails, Dictionary<int, string> customerPhones)
        {
            if(!IsAvailableCustomer(id,customerNames ,customerEmails , customerPhones)) return false ;
             string oldName=customerNames[id];
             string oldEmail=customerEmails[id];
             string oldPhone=customerPhones[id];
            if(!customerNames.Remove(id)) {return false ;}
            if(!customerEmails.Remove(id)) {customerNames[id] = oldName ; return false ;}
            if(!customerPhones.Remove(id)) {customerNames[id] = oldName ; customerEmails[id] = oldEmail ; return false ;}
            return true ;
        }
        
         static void Main(string[] args)
        {
            Dictionary<int, string> productNames = new Dictionary<int, string> { };
            Dictionary<int, decimal> productPrices = new Dictionary<int, decimal> { };
            Dictionary<int, int> productStocks = new Dictionary<int, int> { };


            Dictionary<int, string> customerNames = new Dictionary<int, string> { };
            Dictionary<int, string> customerEmails = new Dictionary<int, string> { };
            Dictionary<int, string> customerPhones = new Dictionary<int, string> { };
        }
    }
}
