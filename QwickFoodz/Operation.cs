using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;

namespace QwickFoodz
{
    public static  class Operation
    {
        //static default list
      public static  List<CustomerDetails> customerDetailsList=new List<CustomerDetails>();
      public static List<FoodDetails> foodDetailsList=new List<FoodDetails>();
      public static List<OrderDetails>orderDetailsList=new List<OrderDetails>();
      public  static List<ItemDetails> itemDetailsList=new List<ItemDetails>();
      public static CustomerDetails CurrentLoggedInUser;


      //methods

      //Main Menu
      public static void MainMenu(){
        bool isExit=false;
        do{
          System.Console.WriteLine("------------Main Menu----------");
          System.Console.WriteLine("1. Customer Registration\n2. Customer Login \n3. Exit");
          System.Console.Write("Enter Your Choice : ");
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
              isExit=true;
              break;
            }
            default:
            {
              System.Console.WriteLine("Invalid User Input Please Enter Correct Input");
              break;
            }
          }
        }while(!isExit);
      }

      //registration
      public static void Registration(){
        System.Console.WriteLine("-----------Registration Page----------");
    
        System.Console.Write("Enter the Name : ");
        string name=Console.ReadLine();
        System.Console.Write("Enter the Father Name : ");
        string fatherName=Console.ReadLine();
        System.Console.Write("Enter the Gender (Male Female Transgender) : ");
        Gender gender=Enum.Parse<Gender>(Console.ReadLine(),true);
        System.Console.Write("Enter the Mobile No : ");
        string mobileNo=Console.ReadLine();
        System.Console.Write("Enter the DOB dd/MM/yyyy : ");
        DateTime dob=DateTime.ParseExact(Console.ReadLine(),"dd/MM/yyyy",null);
        System.Console.Write("Enter the MailID : ");
        string mailID=Console.ReadLine();
        System.Console.Write("Enter the  Balance : ");
        double  balance=double.Parse(Console.ReadLine());
        System.Console.Write("Enter Your Location : ");
        string location=Console.ReadLine();
        CustomerDetails customer=new CustomerDetails(balance,name,fatherName,gender,mobileNo,dob,mailID,location);
        System.Console.WriteLine("----------------------------------------------------------------");
        System.Console.WriteLine(" Customer Registration Successful Your Customer ID : "+customer.CustomerID);
        System.Console.WriteLine("-----------------------------------------------------------------");
      }

      //login
      public static void Login(){
        bool isValid=false;
        System.Console.WriteLine("---------Login Page-------------");
        System.Console.Write("Enter the Customer ID : ");
        string customerID=Console.ReadLine();
        foreach(CustomerDetails  customer in customerDetailsList){
          if(customer.CustomerID.Equals(customerID)){
            isValid=true;
            CurrentLoggedInUser=customer;
            SubMenu();
            break;
          }
        }
        if(!isValid){
          System.Console.WriteLine("Invalid  User Id ");
        }
      }
      public static void SubMenu(){
        bool isMainMenu=false;
        do{
          System.Console.WriteLine("------SubMenu----------");
          System.Console.WriteLine("1. Show Profile\n2. Order Food\n3. Cancel Order\n4. Modify Order\n5. Order History\n6. Recharge Wallet\n7. Show Wallet Balance\n8. Exit");
          System.Console.Write("Enter Your Choice : ");
          int choice=int.Parse(Console.ReadLine());
          switch(choice){
            case 1:
            {
              ShowProfile();
              break;
            }
            case 2:
            {
              OrderFood();
              break;
            }
            case 3:
            {
              CancelOrder();
              break;
            }
            case 4:
            {
              ModifyOrder();
              break;
            }
            case 5:
            {
              OrderHistory();
              break;
            }
            case 6:
            {
              RechargeWallet();
              break;
            }
            case 7:
            {
              ShowWalletBalance();
              break;
            }
            case 8:
            {
              isMainMenu=true;
              System.Console.WriteLine("---------Going to Main Menu----------");
              break;
            }
            default:
            {
              System.Console.WriteLine("Invalid Input ");
              break;
            }

          }

        }while(!isMainMenu);
      }
      //Show Profile
      public static void ShowProfile(){
        System.Console.WriteLine("------Customer Profile----------");
        System.Console.WriteLine($"Customer ID : {CurrentLoggedInUser.CustomerID}\nWallet Balance : {CurrentLoggedInUser.walletBalance}\nName : {CurrentLoggedInUser.Name}\nFather Name :{CurrentLoggedInUser.FatherName}\nGender : {CurrentLoggedInUser.Gender}\nMobile : {CurrentLoggedInUser.MoblieNo}\nDOB : {CurrentLoggedInUser.DOB.ToString("dd/MM/yyyy")}\nMailId : {CurrentLoggedInUser.MailID}\nLocation : {CurrentLoggedInUser.Location}");
      }
      //Order food
      public static void OrderFood(){
        System.Console.WriteLine("------Ordering Food---------");
        //creating order details object
        OrderDetails order=new OrderDetails(CurrentLoggedInUser.CustomerID,0,DateTime.Now,OrderStatus.Initiated);
        //creating local item list
        List<ItemDetails> localItemList=new List<ItemDetails>();
        double totalPrice=0;
        //displaying food items
        foreach(FoodDetails food in foodDetailsList){
          System.Console.WriteLine($"Food ID : {food.FoodID} | FoodName : {food.FoodName} | Price Per Quantity : {food.PricePerQuantity} | Quantity : {food.QuantityAvailable}");
        }
        bool  isContinue=false;
       do{
        //asking food id
        System.Console.Write("Enter the food ID : ");
        string foodID=Console.ReadLine();
        System.Console.Write("Enter the food quantity : ");
        int quantity=int.Parse(Console.ReadLine());
        bool  isValidID=false;
        bool isQuantityAvailable=false;
        
        foreach(FoodDetails food in foodDetailsList){
          if(food.FoodID.Equals(foodID.ToUpper())){
            isValidID=true;
            if(food.QuantityAvailable>quantity){
              
              ItemDetails item=new ItemDetails(order.OrderID,food.FoodID,quantity,quantity*food.PricePerQuantity);
              food.QuantityAvailable=food.QuantityAvailable-quantity;
              //adding items to the temporary list
              itemDetailsList.Add(item);
              //calculating total price
              totalPrice=totalPrice+quantity*food.PricePerQuantity;
              
              
            }
          }
          else{
            isQuantityAvailable=true;
          }
        }
        if(!isValidID){
          System.Console.WriteLine("Invalid food ID");
        }
        else if(isValidID && !isQuantityAvailable){
          System.Console.WriteLine("Food Quantity is not available");
        }
        System.Console.Write("Do you want to Buy again ? (Yes): ");
        string choose=Console.ReadLine();
        if(choose.ToLower()=="yes"){
          isContinue=true;
        }
        else if(choose.ToLower()!="yes"){
          //confirming the order
          System.Console.WriteLine("Do you want to confirm the order : (Yes)/no : ");
          string confirm=Console.ReadLine();
          if(confirm.ToLower()=="yes"){
            //checking balance
            if(CurrentLoggedInUser.walletBalance>totalPrice){
              System.Console.WriteLine("Order Placed Successfully");
              order.OrderStatus=OrderStatus.Ordered;
              order.TotalPrice=totalPrice;
              CurrentLoggedInUser.DeductBalance(totalPrice);
             
              
              break;
            }
            else{
              System.Console.Write("Insufficient Balance Are you going to recharge (Yes/No) : ");
              string option=Console.ReadLine();
              //if not having balance
              if(option.ToLower()=="yes"){
                double Recharge=CurrentLoggedInUser.walletBalance+(totalPrice-CurrentLoggedInUser.walletBalance);
                CurrentLoggedInUser.WalletRecharge(Recharge);
                System.Console.WriteLine("Order Placed Successfully");
                 order.OrderStatus=OrderStatus.Ordered;
                 order.TotalPrice=totalPrice;
                 CurrentLoggedInUser.DeductBalance(totalPrice);
                 itemDetailsList.AddRange(localItemList);
                 break;
              }
              else{
                foreach(ItemDetails item in itemDetailsList){
                  if(order.OrderID==item.OrderID){
                    foreach(FoodDetails food in foodDetailsList){
                      if (item.FoodID==food.FoodID){
                        food.QuantityAvailable=food.QuantityAvailable+quantity;
                      }
                    }
                  }
                }
              }
            }
           
          }
          else{
            foreach(ItemDetails item in itemDetailsList){
                  if(order.OrderID==item.OrderID){
                    foreach(FoodDetails food in foodDetailsList){
                      if (item.FoodID==food.FoodID){
                        food.QuantityAvailable=food.QuantityAvailable+quantity;
                      }
                    }
                  }
                }
          }
          
        }
        }while(isContinue);

      }
      //CAncel Order
      public static void CancelOrder(){
        //showing order details list
        System.Console.WriteLine("---------Cancelling Order----------");
        
        foreach(OrderDetails order in orderDetailsList){
          //display the  order details
          if(CurrentLoggedInUser.CustomerID==order.CustomerID && order.OrderStatus==OrderStatus.Ordered){

            System.Console.WriteLine($"Order ID : {order.OrderID} | Customer ID : {order.CustomerID} | Total Price : {order.TotalPrice} | Date of Order : {order.DateOfOrder.ToString("dd/MM/yyyy")} | Order Status : {order.OrderStatus}");
            

          }
        
        }
          //ASK User to remove the id
            System.Console.Write("Enter the order ID to cancel : ");
            string orderID=Console.ReadLine();
            bool isValid=false;
            foreach(OrderDetails order in orderDetailsList){
              if(orderID.Equals(order.OrderID)){
                isValid=true;
                order.OrderStatus=OrderStatus.Cancelled;
                CurrentLoggedInUser.walletBalance=order.TotalPrice+CurrentLoggedInUser.walletBalance;
                System.Console.WriteLine("Order Cancelled Successfully");
              }
            }
            if(!isValid){
              System.Console.WriteLine("Invalid Order ID");
            }
      }
      //modify Order
      public static  void ModifyOrder(){
        System.Console.WriteLine("-------Modifying the Order--------");
        //display  the order details of the current user
        foreach(OrderDetails order in orderDetailsList){
          if(CurrentLoggedInUser.CustomerID==order.CustomerID && order.OrderStatus==OrderStatus.Ordered){
            System.Console.WriteLine($"Order ID : {order.OrderID} | Customer ID : {order.CustomerID} | Total Price : {order.TotalPrice} | Date Of Order : {order.DateOfOrder} | OrderStatus : {order.OrderStatus}");
          }
        }
        //asking order id
        System.Console.Write("Enter the Order ID : ");
        string orderID=Console.ReadLine();
         foreach(OrderDetails order in orderDetailsList){
          //validating
          if(CurrentLoggedInUser.CustomerID==order.CustomerID.ToUpper() && order.OrderStatus==OrderStatus.Ordered){
            //fetching  item details
            foreach(ItemDetails item in itemDetailsList){
              System.Console.WriteLine($"ITem ID : {item.ItemID} | Order ID : {item.OrderID} | Food ID :{item.FoodID} | Purchase Count : {item.PurchaseCount} | Price of Order : {item.PriceOfOrder}");
            }
            System.Console.Write("Enter the  item ID :");
            string  itemID=Console.ReadLine();
            foreach(ItemDetails item in itemDetailsList){
              //validating item id
              if(item.ItemID==itemID.ToUpper()){
                System.Console.Write("Enter the quantity : ");
                int quantity=int.Parse(Console.ReadLine());
                if(quantity-item.PurchaseCount>0){
                   if(CurrentLoggedInUser.walletBalance>item.PriceOfOrder+(quantity-item.PurchaseCount)*item.PriceOfOrder){
                    System.Console.WriteLine("Modified Successfully of the order id of "+order.OrderID);
                    CurrentLoggedInUser.walletBalance=CurrentLoggedInUser.walletBalance-(quantity-item.PurchaseCount)*item.PriceOfOrder;
                   }
                   else{
                    System.Console.WriteLine("Insufficient balance \nDo you want to recharge ? ");
                    string recharge=Console.ReadLine();
                    if(recharge.ToLower()=="yes"){
                      System.Console.WriteLine("You need to recharge the amount of "+(quantity-item.PurchaseCount)*item.PriceOfOrder);
                      CurrentLoggedInUser.DeductBalance((quantity-item.PurchaseCount)*item.PriceOfOrder);
                      System.Console.WriteLine("Order Modified Successfully ");
                      order.TotalPrice=(quantity-item.PurchaseCount)*item.PriceOfOrder;
                    }
                   
                   }
                }
                else{
                  System.Console.WriteLine("order modified successfully and order id of "+order.OrderID);
                  CurrentLoggedInUser.walletBalance=CurrentLoggedInUser.walletBalance+(item.PurchaseCount-quantity)*item.PriceOfOrder;
                  order.TotalPrice=CurrentLoggedInUser.walletBalance+(item.PurchaseCount-quantity)*item.PriceOfOrder;
                }
              }
            }
          }
        }

      }
      //Order History
      public static void OrderHistory(){
        System.Console.WriteLine("----------Order History-------");
        foreach(OrderDetails order in orderDetailsList){
          if(order.CustomerID==CurrentLoggedInUser.CustomerID){
            System.Console.WriteLine($"Order ID : {order.OrderID} | Customer ID : {order.CustomerID} | Total Price : {order.TotalPrice} | Date of Order : {order.DateOfOrder.ToString("dd/MM/yyyy")} | Order Status : {order.OrderStatus}");
           
          }
        }
      }
      //recharge wallet
      public static void RechargeWallet(){
        System.Console.WriteLine("----------Wallet Recharge------");
        System.Console.Write("Enter the amount to recharge : ");
        double money=double.Parse(Console.ReadLine());
        CurrentLoggedInUser.WalletRecharge(money);
      System.Console.WriteLine("Successfully recharged");
      }
      //Show wallet balance
      public static void ShowWalletBalance(){
        System.Console.WriteLine("------------Wallet Balance---------");
        System.Console.WriteLine("Wallet Balance is : "+CurrentLoggedInUser.walletBalance);
      
      }
      //load default data
      public static void LoadDefaultData(){
        //customer details default data
      
        CustomerDetails customer1=new CustomerDetails(10000,"Ravi","Ettaparajan",Gender.Male,"974774646",new DateTime(1999,11,11),"ravi@gmail.com","chennai");
        CustomerDetails customer=new CustomerDetails(15000,"Baskaran","Sethurajan",Gender.Male,"847575775",new DateTime(1999,11,11),"baskaran@gmail.com","Chennai");
        customerDetailsList.AddRange(new List<CustomerDetails>{customer1,customer});

          //food details default data
          FoodDetails food =new  FoodDetails("Chicken Briyani 1Kg",100,12);
          FoodDetails food1 =new FoodDetails("Mutton Briyani 1Kg",150,10);
          FoodDetails food2 =new FoodDetails("Veg Full Meals",80,30);
          FoodDetails food3 =new FoodDetails("Noodles",100,40);
          FoodDetails food4 =new FoodDetails("Dosa",40,40);
          FoodDetails food5 =new FoodDetails("Idly (2 pieces)",20,50);
          FoodDetails food6 =new FoodDetails("Pongal",40,20);
          FoodDetails food7 =new FoodDetails("Vegetable Briyani",80,15);
          FoodDetails food8 =new FoodDetails("Lemon Rice",50,30);
          FoodDetails food9 =new FoodDetails("Veg Pulav",120,30);
         FoodDetails food10 =new FoodDetails("Chicken 65 (200 Grams)",75,30);
         foodDetailsList.AddRange(new List<FoodDetails>{food,food1,food2,food3,food4,food5,food6,food7,food8,food9,food10});

         //order details default data
         OrderDetails order  =new OrderDetails("CID1001",580,new DateTime(2022,11,26),OrderStatus.Ordered);
         OrderDetails order1 =new OrderDetails("CID1002",870,new DateTime(2022,11,26),OrderStatus.Ordered);
         OrderDetails order2 =new OrderDetails("CID1001",820,new DateTime(2022,11,26),OrderStatus.Cancelled);
         orderDetailsList.AddRange(new List<OrderDetails>{order,order1,order2});

         //item details default data
         ItemDetails item =new ItemDetails("OID3001","FID2001",2,200);
         ItemDetails item1=new ItemDetails("OID3001","FID2002",2,300);
         ItemDetails item2=new ItemDetails("OID3001","FID2003",1,80);
         ItemDetails item3=new ItemDetails("OID3002","FID2001",1,100);
         ItemDetails item4=new ItemDetails("OID3002","FID2002",4,600);
         ItemDetails item5=new ItemDetails("OID3002","FID2010",1,120);
         ItemDetails item6=new ItemDetails("OID3002","FID2009",1,50);
         ItemDetails item7=new ItemDetails("OID3003","FID2002",2,300);
         ItemDetails item8=new ItemDetails("OID3003","FID2008",4,320);
         ItemDetails item9=new ItemDetails("OID3003","FID2001",2,200);
         itemDetailsList.AddRange(new List<ItemDetails>{item,item1,item2,item3,item4,item5,item6,item7,item8,item9});
         
      }
        
    }
}