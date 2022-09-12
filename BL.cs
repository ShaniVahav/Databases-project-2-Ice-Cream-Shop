using System.Collections;

using BusinessEntities;


namespace BusinessLogic
{
  
    public class Logic
    {
        public static void getMostCommonIN(int id) { 
            string sql = null;
        switch(id){
            case 4 : 
                    sql = "select item FROM ice_cream_shop.ingredients join a on ice_cream_shop.ingredients.id_INGREDIENT="+
                                      "a.id_INGREDIENT order by a.amount DESC limit 1;";
                        MySqlAccess.MySqlAccess.searchMostCommon( sql);     
                        break;
            case 5:
                       sql = "select item FROM ice_cream_shop.ingredients join a on ice_cream_shop.ingredients.id_INGREDIENT="+
                                      "a.id_INGREDIENT WHERE a.id_INGREDIENT < 11 order by a.amount DESC limit 1 ;";
                        MySqlAccess.MySqlAccess.searchMostCommon( sql);     
                        break ;
                 }
          
           
        }


          public static ArrayList fillTableIN()
        {
            ArrayList a = new ArrayList { "choclate" , "vanila" , "mecupelet",
                                  "Banana" ,
                                   "orange",
                                  "coconut" ,
                                  "Oreo" ,
                                  "coffee" ,
                                  "strawberry",
                                  "mango",
                                  "regular_cone",
                                  "special_cone",
                                  "box",
                                  "TOPchoclate",
                                  "TOPchoclate",
                                  "TOP maple"
            };
            
            return a;
        }

        public static void createTables()
        {
            MySqlAccess.MySqlAccess.createTables();
        }

        public static void fillTableOrder(ref ArrayList toppings, int round_number, ref Dictionary<int, int> fdict, int package)
        {   
            iceCreamOrder a = new(package, fdict, toppings);
            MySqlAccess.MySqlAccess.insertObjectToOrders(a, round_number);
        }
    }


        // we add:
        public class create_an_order
        {

            public static void flavours(ref Dictionary<int, int> fDict, ref int iceCreamBallsNumber, int package)
            {
                int current_amount = 0;
                while (true)
                {
                    if(package != 3)
                    {
                        Console.WriteLine("You can choose between 1-3 balls");
                    }
                    if(package == 3)
                    {
                        Console.WriteLine("Choose tastes");
                    }
                    
                    Console.WriteLine("0- forward with the order\n" +
                                  "1 - chocolate\n" +
                                  "2 - vanilla\n" +
                                  "3 - mecupelet\n" +
                                  "4 - Banana\n" +
                                  "5 - orange\n" +
                                  "6 - coconut\n" +
                                  "7 - Oreo\n" +
                                  "8 - coffee\n" +
                                  "9 - strawberry\n" +
                                  "10 - mango\n");
                    int userInput = Int32.Parse(Console.ReadLine());
                    if (userInput == 0)
                        break;
                    fDict[userInput]++;
                    current_amount++;
                    iceCreamBallsNumber++;

                    if(package != 3)
                    {
                        if (current_amount == 3)
                        break;
                    }
                }
            }

            public static void toppings_for_regular(ref Dictionary<int, int> fDict, ref int number, ref ArrayList toppingsArraylist)
            {
                if (number < 2)
                    return;
                Dictionary<string , int> Tdict= new Dictionary<string, int>(); 
                Tdict.Add("chocolate",14);
                Tdict.Add("peanuts",15);
                Tdict.Add("maple",16);
                
                removeToppings(ref fDict, ref Tdict, ref toppingsArraylist);
            }             
            
            public static void toppings_for_special(ref Dictionary<int, int> fDict, ref ArrayList toppingsArraylist)
            {
                Dictionary<string , int> Tdict= new Dictionary<string, int>(); 
                Tdict.Add("chocolate",14);
                Tdict.Add("peanuts",15);
                Tdict.Add("maple",16);
              
                removeToppings(ref fDict, ref Tdict, ref toppingsArraylist);
            }

            public static void toppings_for_box(ref Dictionary<int, int> fDict, ref ArrayList toppingsArraylist)
            {
                toppings_for_special(ref fDict, ref toppingsArraylist);
            }

             public static void removeToppings(ref Dictionary<int, int> fDict, ref Dictionary<string,int> Tdict,ref ArrayList toppingsArraylist)
            {
                if (fDict[1] > 0 || fDict[3] > 0)
                    Tdict.Remove("chocolate");
                if (fDict[2] > 0)
                    Tdict.Remove("maple");

                foreach (var item in Tdict)
                { 
                    string key = item.Key;
                    int value = item.Value;
                    // addingCheak(key,value, ref toppingsArraylist);
                    Console.WriteLine($"Do you want topping of {key}?");
                    Console.WriteLine("1 - Yes");
                    Console.WriteLine("2 - No");
                    int userInput = Int32.Parse(Console.ReadLine());
                    if (userInput == 1)
                    {
                        toppingsArraylist.Add(value);
                    }
                }
            }

            // public static void addingCheak(string key , int value , ref ArrayList toppingsArraylist)
            // {
               
            //    // Console.WriteLine(toppingsArraylist.ToString());
            // } 

        }


        public class edit
        {
            public static void delete()
            {
                int id = MySqlAccess.MySqlAccess.getId();
                Console.WriteLine(" The id is "+ id);
                MySqlAccess.MySqlAccess.deleteOrderFromDB(id);
            }
        
            public static void bill(DateTime date, int price)
            {
                Console.WriteLine("Date: " + date);
                Console.WriteLine("Price: " + price + " nis");
                Console.WriteLine("Thank you! Hope to see you next time");
            }
        }
}
    
