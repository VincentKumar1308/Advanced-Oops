using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeteriaManagement
{
    public static class Operation
    {
        static UserDetails CurrentLoggedInUser;
        static List<UserDetails> userDetailsList = new List<UserDetails>();
        static List<FoodDetails> foodDetailsList = new List<FoodDetails>();
        static List<OrderDetails> orderDetailsList = new List<OrderDetails>();
        static List<CartItem> cartItemList=new List<CartItem>();
        //mainmenu
        public static void MainMenu()
        {
            bool IsExit = false;
            do
            {
                System.Console.WriteLine("1. User Registration\n2.User Loging\n3.Exit");
                System.Console.Write("Enter Your Choice : ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
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
                            IsExit = true;
                            break;
                        }
                }
            } while (!IsExit);
        }
        //registration
        public static void Registration()
        {
            System.Console.WriteLine("Welcome  to the Registration Page");
            System.Console.Write("Enter Your Name        : ");
            string name = Console.ReadLine();
            System.Console.Write("Enter Your Father Name : ");
            string fatherName = Console.ReadLine();
            System.Console.Write("Enter Your Mobile Number : ");
            long mobileNumber = long.Parse(Console.ReadLine());
            System.Console.Write("Enter Your mailID : ");
            string mailID = Console.ReadLine();
            System.Console.Write("Enter Your Gender : (Male Female Others) : ");
            Gender gender = Enum.Parse<Gender>(Console.ReadLine(), true);
            System.Console.Write("Enter Your Balance : ");
            long balance = long.Parse(Console.ReadLine());
            System.Console.Write("Enter Your Work Station Number : ");
            string workStationNumber = Console.ReadLine();
            UserDetails user = new UserDetails(name, fatherName, mobileNumber, mailID, gender, workStationNumber, balance);
            userDetailsList.Add(user);
            System.Console.WriteLine("Registration is successful  and User ID is : " + user.UserID);
            Console.ReadKey();

        }

//login
        public static void Login()
        {
            System.Console.WriteLine("Welcome to the Login  Page ");
            System.Console.WriteLine();
            System.Console.Write("Enter Your User ID : ");
            string userID = Console.ReadLine();
            bool isValid = false;
            foreach (UserDetails user in userDetailsList)
            {
                if (userID == user.UserID)
                {
                    CurrentLoggedInUser = user;
                    SubMenu();
                    isValid = true;

                }
            }
            if (!isValid
            )
            {
                System.Console.WriteLine("Invalid User ID");
            }
        }
        //submenu
        public static void SubMenu()
        {
            bool isSubMenu = false;
            do
            {
                System.Console.WriteLine("-------Submenu---------");
                System.Console.WriteLine("1.Show My Profile\n2.Food Order\n3. Modify Order\n4. Cancel Order\n5. Order History\n6. Wallet Recharge\n7. Show Wallet Balance\n8. Exit");
                System.Console.Write("Enter Your Choice : ");
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        {

                            Profile();
                            break;
                        }
                    case 2:
                        {
                            FoodOrder();
                            break;
                        }
                    case 3:
                    {
                        ModifyOrder();
                        break;
                    }
                    case 4:
                {
                    CancelOrder();
                    break;
                }
                case 5:
                {
                    OrderHistory();
                    break;
                }
                case 6:
                {
                    Recharge();
                    break;
                }
                case 7:
                {
                    WalletBalance();
                    break;
                }
                case 8:
                {
                    isSubMenu=true;
                    System.Console.WriteLine("Going to the main menu");
                    break;
                }
                default : {
                    System.Console.WriteLine("Invalid option ");
                    break;
                }

                }
            } while (!isSubMenu);
        }

        //Food order
        public static void FoodOrder()
        {
            //DISPLAYING  THE FOOD DETAILS
            System.Console.WriteLine($"{"Food ID",-10} | {"Food Name",-10} | {"Food Price",-10} | {"Available Quantity",-10}");
            foreach (FoodDetails food in foodDetailsList)
            {
                System.Console.WriteLine($"{food.FoodID,-10} | {food.FoodName,-10} | {food.FoodPrice,-10} | {food.Quantity,-10}");
            }
            //1. creating a temporary local cart Item List
            List<CartItem> tempCartList = new List<CartItem>();

            //2. Creating order object with current user ID
            OrderDetails order = new OrderDetails(CurrentLoggedInUser.UserID, DateTime.Now, 0, OrderStatus.Initiated);
            bool isContinue=false;
             bool isValidID = false;
             bool isCount=false;
            do
            {
                //3. Ask the user to pick a product using food ID
                System.Console.Write("Enter the food ID : ");
                string foodID = Console.ReadLine();
               
                foreach (FoodDetails food in foodDetailsList)
                {
                    if (foodID.Equals(food.FoodID))
                    {
                        isValidID = true;
                        // 3 Asking the quanntity
                        System.Console.Write("Enter the quantity : ");
                        int quantity = int.Parse(Console.ReadLine());
                        //calculating the price of the order
                        double price = quantity * food.FoodPrice;
                        if (quantity > food.Quantity)
                        {
                            //4. quantity higher than the availability
                            System.Console.WriteLine("Sorry Food currently not available");
                            break;

                        }
                        else if (quantity < food.Quantity)
                        {
                            //4. reducing the quantity
                            food.Quantity -= quantity;
                            CartItem cart = new CartItem(order.OrderID, food.FoodID, price, quantity);
                            isCount=true;
                            //adding to the temp cart list
                            tempCartList.Add(cart);
                        }
                    }
                }
                if (!isValidID)
                {
                    System.Console.WriteLine(" Invalid fOOD ID ");
                }

                //do you want to pick the another food product
                System.Console.WriteLine("Do you want to pick another food product  ? (Yes): ");
                string purchaseAgain=Console.ReadLine();
                if(purchaseAgain.ToLower()=="yes"){
                  isContinue=true;   
                }
                else{
                    isContinue=false;
                }
            } while (isContinue);
            if(isValidID && isCount){
            //asking to confirm the purchase
            System.Console.Write("Do you want to confirm the Purchase ? (Yes) : ");
            string purchaseConfirm=Console.ReadLine();
            if(purchaseConfirm.ToLower()=="yes"){
                //checking whether he is having  sufficient balance or not
                foreach(CartItem cart in tempCartList){
                     //insufficient balance
                     if(cart.OrderPrice>CurrentLoggedInUser.WalletBalance){
                        //asking to recharge
                        System.Console.WriteLine("Insufficient Balance are you willing to recharge ? : (Yes) ");
                        string willingToRecharge=Console.ReadLine();
                        if(willingToRecharge.ToLower()=="yes"){
                            CurrentLoggedInUser.WalletRecharge(cart.OrderPrice-CurrentLoggedInUser.WalletBalance+1);
                             CurrentLoggedInUser.DeductAmount(cart.OrderPrice);
                            cartItemList.AddRange(tempCartList);
                            order.TotalPrice=cart.OrderPrice;
                            order.OrderStatus=OrderStatus.Ordered;
                            orderDetailsList.Add(order);
                            System.Console.WriteLine("Order placed successfully and order id is "+order.OrderID);
                            break;
                        }
                        else{
                            System.Console.WriteLine("Exiting without Order due to insufficient balance");
                            foreach(FoodDetails  food in foodDetailsList){
                                if(food.FoodID.Equals(cart.FoodID)){
                                    food.Quantity+=cart.OrderQuantity;
                                }
                            }
                            break;
                        }
                     }
                     else{
                        CurrentLoggedInUser.DeductAmount(cart.OrderPrice);
                        cartItemList.AddRange(tempCartList);
                        order.TotalPrice=cart.OrderPrice;
                        order.OrderStatus=OrderStatus.Ordered;
                        orderDetailsList.Add(order);
                        System.Console.WriteLine("Order placed successfully and order id is "+order.OrderID);
                        break;
                     }
                     }
                }
            }
            else{
                //rejection of the purchase
                foreach (CartItem cart in tempCartList)
                {
                    foreach (FoodDetails food in foodDetailsList)
                    {
                        if (food.FoodID.Equals(cart.FoodID))
                        {
                            food.Quantity += cart.OrderQuantity;
                        }
                    }
                }
               
            }

        }
         
        //Modify Order
        public static void ModifyOrder(){
            bool isOrderHistory=false;
            System.Console.WriteLine($"{"Order ID",-10} | {"User ID",-10} |  {"Order Date",-10} | {"Total Price",-10} | {"Order Status",-10}");
            foreach(OrderDetails order in orderDetailsList){
                //showing order details of the current logged in user
                if(order.UserID==CurrentLoggedInUser.UserID && order.OrderStatus==OrderStatus.Ordered){
                    isOrderHistory=true;
                    System.Console.WriteLine($"{order.OrderID,-10} | {order.UserID} | {order.OrderDate.ToString("dd/MM/yyyy"),-10} | {order.OrderStatus,-10}");
                }
            }
            //not having order history
            if(!isOrderHistory){
                System.Console.WriteLine("You don't have the order history");
            }
            else if(isOrderHistory){
                System.Console.Write("Enter the Order ID : ");
                string orderID=Console.ReadLine();
                foreach(OrderDetails order in orderDetailsList){
                    //checking the order id
                    if(order.UserID==CurrentLoggedInUser.UserID && order.OrderStatus==OrderStatus.Ordered){
                        bool isCorrectOrderID=false;
                        //displaying the cart items of the order id
                        System.Console.WriteLine($"{"Item ID",-10} | {"Order ID",-10} | {"Food ID",-10} | {"Order Price",-10} | {"Order Quantity",-10} | ");
                        foreach(CartItem cart in cartItemList){
                            if(cart.OrderID.Equals(orderID)){
                                isCorrectOrderID=true;
                                //showing cart items if order id matches
                                System.Console.WriteLine($"{cart.ItemID,-10} | {cart.OrderID,-10} | {cart.FoodID,-10} | {cart.OrderPrice,-10} | {cart.OrderQuantity,-10}");
                            }
                            
                        }
                       
                        if(isCorrectOrderID){
                             //asking for the item id from the user
                            System.Console.Write("Enter the item ID : ");
                            string itemID=Console.ReadLine();
                            bool isItemIDAvailable=false;
                            foreach(CartItem cart in cartItemList){
                                //checking the item id in the cart list
                                if(cart.ItemID==itemID && cart.OrderID==order.OrderID){
                                    isItemIDAvailable=true;
                                    //asking for the quantity to modify
                                    System.Console.Write("Enter the quantity to modify : ");
                                   int quantity=int.Parse(Console.ReadLine());
                                       if(quantity>cart.OrderQuantity){
                                    //if wallet balance is lesser than the order balance
                                    if(CurrentLoggedInUser.WalletBalance<(quantity-cart.OrderQuantity)*cart.OrderPrice){
                                        System.Console.Write("Insuffuiceint balance are want to recharge (Yes) : ");
                                        string recharge=Console.ReadLine();
                                        if(recharge=="yes"){
                                            CurrentLoggedInUser.WalletRecharge((quantity-cart.OrderQuantity)*cart.OrderPrice);
                                            System.Console.WriteLine("Order modified successfully");
                                            order.TotalPrice=(quantity-cart.OrderQuantity)*cart.OrderPrice;
                                            CurrentLoggedInUser.DeductAmount(order.TotalPrice);
                                            break;
                                        }
                                        else{
                                            System.Console.WriteLine("Exiting due to insufficient balance");
                                            break;
                                        }

                                    }
                                    else{
                                        System.Console.WriteLine("Order modified successfully");
                                        order.TotalPrice=(quantity-cart.OrderQuantity)*cart.OrderPrice;
                                        CurrentLoggedInUser.DeductAmount(order.TotalPrice);
                                        break;
                                    }

                                }
                                else{
                                    //if quantity lesser than the previous order
                                    System.Console.WriteLine("Order modified successfully");
                                    CurrentLoggedInUser.WalletRecharge((cart.OrderQuantity-quantity)*cart.OrderPrice);
                                    order.TotalPrice=quantity*cart.OrderPrice;
                                    cart.OrderPrice=order.TotalPrice;
                                    break;
                                }
                            }
                       

                                }
                                 if(!isItemIDAvailable){
                                System.Console.WriteLine("Invalid Item id ");
                                break;
                            }
                            }
                        }          

                    }
                }
               
                 
            }
            //cance order
            public static void CancelOrder(){
                System.Console.WriteLine($"{"Order ID",-10} | {"User ID",-10} | {"Order Date",-10} | {"Total Price",-10} | {"Order Status",-10}");
                foreach(OrderDetails order in orderDetailsList){
                    if(CurrentLoggedInUser.UserID==order.UserID && order.OrderStatus==OrderStatus.Ordered){
                        System.Console.WriteLine($"{order.OrderID,-10} | {order.UserID,-10} | {order.OrderDate.ToString("dd/MM/yyyy"),-10} | {order.TotalPrice,-10} | {order.OrderStatus,-10}");
                    }
                }
                System.Console.Write("Enter the Order ID to cancel : ");
                string orderID=Console.ReadLine();
                bool  isValidOrderID=false;
                 foreach(OrderDetails order in orderDetailsList){
                    if(CurrentLoggedInUser.UserID==order.UserID && order.OrderStatus==OrderStatus.Ordered){
                       if(order.OrderID==orderID){
                        isValidOrderID=true;
                        order.OrderStatus=OrderStatus.Cancelled;
                        foreach(CartItem cart in cartItemList){
                            foreach(FoodDetails food in foodDetailsList){
                                if(cart.FoodID==food.FoodID){
                                    food.Quantity=food.Quantity+cart.OrderQuantity;
                                    CurrentLoggedInUser.WalletRecharge(food.FoodPrice);                        
                                }
                            }
                            
                            System.Console.WriteLine("Order Cancelled successfully");
                            break;
                        }
                       }
                    }
                }
                if(!isValidOrderID){
                    System.Console.WriteLine("Invalid Order ID");
                }

            }
        
        //order history
        public static void OrderHistory(){
            System.Console.WriteLine($"{"Order ID",-10} | {"User ID",-10} | {"Order Date",-10} | {"Total Price",-10} | {"Order Status",-10}");
            foreach(OrderDetails order in orderDetailsList){
                if(CurrentLoggedInUser.UserID==order.UserID){
                    System.Console.WriteLine($"{order.OrderID,-10} | {order.UserID,-10} | {order.OrderDate,-10} | {order.TotalPrice,-10} | {order.OrderStatus,-10}");
                }
            }
        }

        //Recharge
        public static void Recharge(){
            System.Console.Write("Do you want to Recharge ? (Yes/No) : ");
            string money=Console.ReadLine();
            System.Console.Write("Enter the amount to recharge : ");
            if(money.ToLower()=="yes"){
            double amount=double.Parse(Console.ReadLine());
            CurrentLoggedInUser.WalletRecharge(amount);
            System.Console.WriteLine("Successfully Recharged");
        }
        }
        
        //Show Wallet Balance
        public static void WalletBalance(){
            System.Console.WriteLine("Balance amount is : "+CurrentLoggedInUser.WalletBalance);
        }
        //user Profile
        public static void Profile()
        {
            System.Console.WriteLine("UserID : " + CurrentLoggedInUser.UserID + "\nUser Name : " + CurrentLoggedInUser.UserName + "\nFather Name : " + CurrentLoggedInUser.FatherName + "\nMobile No : " + CurrentLoggedInUser.MobileNumber + "\nMailID : " + CurrentLoggedInUser.MailID + "\nGender : " + CurrentLoggedInUser.Gender + "Work Station Number : " + CurrentLoggedInUser.WorkStationNumber + " Balance : " + CurrentLoggedInUser.WalletBalance);
        }

        //loading default datas
        public static void LoadDefaultData()
        {
            //user details
            UserDetails user = new UserDetails("Ravichandran", "Ettapparajan", 8857777575, "ravi@gmail.com", Gender.Male, "WS101", 400);
            userDetailsList.Add(user);
            UserDetails user1 = new UserDetails("Baskaran", "Sethurajan", 9577747744, "baskaran@gmail.com", Gender.Male, "WS105", 500);
            userDetailsList.Add(user1);

            //food  details
            FoodDetails food1 = new FoodDetails("Coffee", 20, 100);
            FoodDetails food2 = new FoodDetails("Tea", 15, 100);
            FoodDetails food3 = new FoodDetails("Biscuit", 10, 100);
            FoodDetails food4 = new FoodDetails("Juice", 50, 100);
            FoodDetails food5 = new FoodDetails("Puff", 40, 100);
            FoodDetails food6 = new FoodDetails("Milk", 10, 100);
            FoodDetails food7 = new FoodDetails("PopCorn", 20, 20);
            foodDetailsList.AddRange(new List<FoodDetails> { food1, food2, food3, food4, food5, food6, food7 });

            //order details
            OrderDetails order = new OrderDetails("SF1001", DateTime.Now, 70, OrderStatus.Ordered);
            OrderDetails order1 = new OrderDetails("SF1002", DateTime.Now, 100, OrderStatus.Ordered);
            orderDetailsList.AddRange(new List<OrderDetails> { order, order1 });
            

            //CART item details
            CartItem cart=new CartItem("OID1001","FID101",20,1);
            CartItem car1t=new CartItem("OID1001","FID103",10,1);
            CartItem cart2=new CartItem("OID1001","FID105",40,1);
            CartItem cart3=new CartItem("OID1002","FID103",10,1);
            CartItem cart4=new CartItem("OID1002","FID103",10,1);
            CartItem cart5=new CartItem("OID1002","FID105",40,1);
            cartItemList.AddRange(new List<CartItem>{cart,car1t,cart2,cart3,cart4,cart5});

        }

    }
}