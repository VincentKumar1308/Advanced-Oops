using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryShop
{
    public static class Operation
    {
        //properties
        public static List<ProductDetails> productDetailsList=new List<ProductDetails>();
        public static List<BookingDetails> bookingDetailsList=new List<BookingDetails>();
        public static List<OrderDetails> orderDetailsList=new List<OrderDetails>();
        public static List<CustomerRegistration> custmerRegistrationList=new List<CustomerRegistration>();
        public static CustomerRegistration CurrentLoggedInUser;



        //mainMenu
        public static void MainMenu(){
            bool isMainMenu=true;
            do{
                System.Console.WriteLine("---------MainMenu---------");
                System.Console.WriteLine("1. Customer Registration\n2. Customer Login\n3. Exit");
                System.Console.Write("Enter Your Option : ");
                int choice=int.Parse(Console.ReadLine());
                switch(choice){
                    case 1:
                    {
                        Registration();
                        break;
                    }
                    case 2:
                    {
                        Login();
                        break;
                    }
                    case 3:
                    {
                        System.Console.WriteLine("Thank You");
                        isMainMenu=false;
                        break;
                    }
                }
            }while(isMainMenu);
        }

        //registration
        public static void Registration(){
            System.Console.WriteLine("Welcome to the registration page");
            System.Console.Write("Enter the wallet Balance : ");
            double balance=double.Parse(Console.ReadLine());
             System.Console.Write("Enter the Name : ");
            string name=Console.ReadLine();
             System.Console.Write("Enter the Father Name : ");
            string fatherName=Console.ReadLine();
             System.Console.Write("Enter the Gender (Male female Others): ");
             Gender gender=Enum.Parse<Gender>(Console.ReadLine(),true);
              System.Console.Write("Enter the Mobile Number : ");
             long mobile=long.Parse(Console.ReadLine());
              System.Console.Write("Enter the Date of Birth dd/MM/yyyy : ");
              DateTime dob=DateTime.ParseExact(Console.ReadLine(),"dd/MM/yyyy",null);
               System.Console.Write("Enter the Mail ID : ");
              string mailID=Console.ReadLine();
             
            CustomerRegistration customer=new CustomerRegistration(balance,name,fatherName,gender,mobile,dob,mailID);
            custmerRegistrationList.Add(customer);
            System.Console.WriteLine("Registration Successful and Customer id is "+customer.CustomerID);

        }

        //login page
        public static void  Login(){
            System.Console.WriteLine("Welcome to the login page");
            System.Console.Write("Enter the  Customer ID : ");
            string customerID=Console.ReadLine();
            bool isValid=false;
            foreach(CustomerRegistration customer in custmerRegistrationList){
                if(customer.CustomerID==customerID){
                    isValid=true;
                    CurrentLoggedInUser=customer;
                    SubMenu();
                    break;
                }
            }
            if(!isValid){
                System.Console.WriteLine("Invalid User ID ");
            }

        }

        //SubMenu
        public static void SubMenu()
        {
            bool isSubMenu = true;
            do
            {
                System.Console.WriteLine("1.Show Customer Details\n2. Show Product Details\n3. Wallet Recharge\n4. Take Order\n5.Modify Order Quantity\n6.Cancel Order\n7.Show Balance\n8.Exit");
                System.Console.Write("Enter Your choice : ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        {
                            ShowDetails();
                            break;
                        }
                    case 2:
                        {
                            ShowProductDetails();
                            break;
                        }
                    case 3:
                        {
                            WalletRecharge();
                            break;
                        }
                    case 4:
                        {
                            TakeOrder();
                            break;
                        }
                    case 5:
                        {
                            //ModifyOrderQuantity();
                            break;
                        }
                    case 6:
                        {
                            //CancelOrder();
                            break;
                        }
                    case 7:
                        {
                            //ShowBalance();
                            break;
                        }
                    case 8:
                        {
                            System.Console.WriteLine("-------Thank You-------");
                            isSubMenu = false;
                            break;
                        }
                }
            } while (isSubMenu);
        }

        //show customer details
        public static void ShowDetails()
        {
            System.Console.WriteLine($"Customer ID : {CurrentLoggedInUser.CustomerID} | Wallet Balance : {CurrentLoggedInUser.WalletBalance} | Name : {CurrentLoggedInUser.Name} | Father Name : {CurrentLoggedInUser.FatherName} Gender : {CurrentLoggedInUser.Gender} | Mobile : {CurrentLoggedInUser.MobileNo} | DOB : {CurrentLoggedInUser.DOB} | MAILID : {CurrentLoggedInUser.MailID}");
        }
        //show Product Details
        public static void ShowProductDetails()
        {
            foreach (ProductDetails product in productDetailsList)
            {
                System.Console.WriteLine($"Product ID : {product.ProductID} | Product Name : {product.ProductName} | Quantity Available : {product.QuantityAvailable} | Price Per Quantity : {product.PricePerQuantity}");
            }
        }

        //Wallet Recharge
        public static void WalletRecharge()
        {
            System.Console.WriteLine("Do You want to REcharge (yes)");
            string choice = Console.ReadLine();
            if (choice.ToLower() == "yes")
            {
                System.Console.Write("Enter the amount  to Recharge  : ");
                int money = int.Parse(Console.ReadLine());
                CurrentLoggedInUser.WalletRecharge(money);
                System.Console.WriteLine("The Wallet Balance is : " + CurrentLoggedInUser.WalletBalance);
            }
        }

        //Take Order
        public static void TakeOrder()
        {
            //a
            System.Console.Write("Do you want to purchase (yes) : ");
            string purchase = Console.ReadLine();
            if (purchase.ToLower() == "yes")
            {
                BookingDetails book = new BookingDetails(CurrentLoggedInUser.CustomerID, 0, DateTime.Now, BookingDetails.BookingStatus.Initated);
                //b 
                List<OrderDetails> tempOrderList = new List<OrderDetails>();
                bool isPurchase = false;
                do
                {

                    //c show product details
                    ShowProductDetails();
                    //d. asking the product from user
                    System.Console.Write("Enter the Product ID : ");
                    bool isValidID = false;
                    string productID = Console.ReadLine();
                    foreach (ProductDetails product in productDetailsList)
                    {
                        if (product.ProductID == productID)
                        {
                            isValidID = true;
                            //d. getting quantity
                            System.Console.Write("Enter the no of quantity to purchase : ");
                            int quantity = int.Parse(Console.ReadLine());
                            //e. checking the availability of the stock
                            if (product.QuantityAvailable < quantity)
                            {
                                System.Console.WriteLine("Quantity not available ");
                                break;
                            }
                            else
                            {
                                //f. create order object
                                OrderDetails order = new OrderDetails(book.BookingID, product.ProductID, quantity, product.PricePerQuantity * quantity);
                                product.QuantityAvailable = product.QuantityAvailable - quantity;
                                tempOrderList.Add(order);
                                book.TotalPrice=product.PricePerQuantity * quantity;
                                System.Console.WriteLine("Product is successfully ordered to cart");
                            }


                        }
                    }
                    if (!isValidID)
                    {
                        System.Console.WriteLine("Invalid  Product ID");
                        
                    }
                    //g do you want to continue
                    System.Console.Write("Do you want to continue ? yes : ");
                    string choice = Console.ReadLine();
                    if (choice.ToLower() == "yes")
                    {
                        isPurchase = true;
                    }
                } while (isPurchase);
                //do you want to confirm order
                System.Console.Write("Do you want to confirm order : Yes/No");
                string confirm = Console.ReadLine();
                if (confirm.ToLower() == "yes")
                {
                    if(CurrentLoggedInUser.WalletBalance>book.TotalPrice){
                        System.Console.WriteLine("Booking Successful ");
                        book.bookingStatus=BookingDetails.BookingStatus.Booked;
                        foreach(OrderDetails temp in tempOrderList){
                            orderDetailsList.Add(temp);
                        }
                        CurrentLoggedInUser.WalletBalance=CurrentLoggedInUser.WalletBalance-book.TotalPrice;
                    }
                    else{
                        bool isRecharge=false;
                        do{
                        System.Console.WriteLine("Insufficient balance recharge with "+book.TotalPrice);
                        System.Console.WriteLine("Do you want to recharge : yes ");
                        string choice=Console.ReadLine();
                        if(choice.ToLower()=="yes"){
                            isRecharge=true;
                            WalletRecharge();
                        }
                        else if(choice.ToLower()=="no"){
                            isRecharge=false;
                            System.Console.WriteLine("Cart Removed Successfully");
                             
                            
                        }
                        }while(isRecharge);
                    }

                }
            }
        }
        //load default data
        public static void LoadDefaultData()
        {
            //default  customerDetails
            CustomerRegistration customer = new CustomerRegistration(10000, "Ravi", "Ettapparajan", Gender.Male, 974774646, new DateTime(1999, 11, 11), "ravi@gmail.com");
            CustomerRegistration customer1 = new CustomerRegistration(10000, "Baskarn", "Sethurajan", Gender.Male, 847575775, new DateTime(1999, 11, 11), "baskaran@gmail.com");
            custmerRegistrationList.AddRange(new List<CustomerRegistration> { customer, customer1 });

            //product details default data
            ProductDetails product = new ProductDetails("Sugar", 20, 40);
            ProductDetails product1 = new ProductDetails("Rice", 100, 50);
            ProductDetails product2 = new ProductDetails("Milk", 10, 40);
            ProductDetails product3 = new ProductDetails("Coffee", 10, 10);
            ProductDetails product4 = new ProductDetails("Tea", 10, 10);
            ProductDetails product5 = new ProductDetails("Masala Powder", 10, 20);
            ProductDetails product6 = new ProductDetails("Salt", 10, 10);
            ProductDetails product7 = new ProductDetails("Turmeric Powder", 10, 25);
            ProductDetails product8 = new ProductDetails("Chilli Powder", 10, 20);
            ProductDetails product9 = new ProductDetails("GroundNut Oil", 10, 140);
            productDetailsList.AddRange(new List<ProductDetails> { product, product1, product2, product3, product4, product5, product6, product7, product8, product9 });

            //booking details default data
            BookingDetails book = new BookingDetails("CID1001", 220, new DateTime(2022, 07, 26), BookingDetails.BookingStatus.Booked);
            BookingDetails book1 = new BookingDetails("CID1002", 400, new DateTime(2022, 07, 26), BookingDetails.BookingStatus.Booked);
            BookingDetails book2 = new BookingDetails("CID1003", 280, new DateTime(2022, 07, 26), BookingDetails.BookingStatus.Cancelled);
            BookingDetails book3 = new BookingDetails("CID1004", 0, new DateTime(2022, 07, 26), BookingDetails.BookingStatus.Initated);


            //order details default data
            OrderDetails order = new OrderDetails("BID3001", "PID2001", 2, 80);
            OrderDetails order1 = new OrderDetails("BID3001", "PID2003", 1, 40);
            OrderDetails order2 = new OrderDetails("BID3001", "PID2003", 1, 40);
            OrderDetails order3 = new OrderDetails("BID3002", "PID2001", 1, 40);
            OrderDetails order4 = new OrderDetails("BID3002", "PID2002", 4, 200);
            OrderDetails order5 = new OrderDetails("BID3002", "PID2010", 1, 140);
            OrderDetails order6 = new OrderDetails("BID3002", "PID2009", 1, 20);
            OrderDetails order7 = new OrderDetails("BID3003", "PID2002", 2, 100);
            OrderDetails order8 = new OrderDetails("BID3003", "PID2008", 4, 100);
            OrderDetails order9 = new OrderDetails("BID3003", "PID2001", 2, 80);
            orderDetailsList.AddRange(new List<OrderDetails> { order, order1, order2, order3, order4, order5, order6, order7, order8, order9 });
        }
    }
}
