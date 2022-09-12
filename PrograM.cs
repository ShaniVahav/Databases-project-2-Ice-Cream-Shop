﻿using System;
using System.Data;
using System.Diagnostics;//used for Stopwatch class

using MySql.Data;
using MySql.Data.MySqlClient;

using MySqlAccess;
using BusinessLogic;
using System.Collections;
using BusinessEntities;
using System.Reflection.PortableExecutable;

// See https://aka.ms/new-console-template for more information

Console.WriteLine("Please create tables first, by pressing '1'");

Stopwatch stopwatch = new Stopwatch();
int sales_amount = 0;
int sum_price = 0;

int userInput = -1;
int price;
int round_number;

int package = -1;
var toppingsArraylist = new ArrayList();
int iceCreamBallsNumber = 0;                                    /// משתנה של כמות הכדורים 
var fDict = new Dictionary<int, int>();  ///// פה אני יוצר את המילון 
for (int i = 1; i < 11; i++)
{
    fDict.Add(i, 0);
}
Console.WriteLine("1 - create tables");
userInput = Int32.Parse(Console.ReadLine());


if (userInput == 1)
    Logic.createTables();

NEW_ORDER:
round_number = 0;
price = 0;
package = -1;
toppingsArraylist.Clear();
iceCreamBallsNumber = 0;

Console.WriteLine("\nHi! Welcome to our Ice Cream shop\n");
Console.WriteLine("Please choose a task:");
Console.WriteLine("1 - order an ice cream");
Console.WriteLine("2 - exit");
Console.WriteLine("3 - Goto sales summary");
userInput = Int32.Parse(Console.ReadLine());

if (userInput == 2)
{
    System.Environment.Exit(0);
}
if (userInput == 3)
{
      SUMMARY:
                    Console.WriteLine("Please choose a task:");
                    Console.WriteLine("1 - View daily report");
                    Console.WriteLine("2 - View incomplete sales");
                    userInput = Int32.Parse(Console.ReadLine());

                    if(userInput == 1)
                    {
                        Console.WriteLine("Number of sales: " + sales_amount);
                        Console.WriteLine("Total sales amount: " + sum_price + " nis");
                        double avg = sum_price/sales_amount;
                        Console.WriteLine("Average sale amount: " + avg + " nis");
                    }

                    if(userInput == 2)
                    {
                      MySqlAccess.MySqlAccess.get_incompleteSales();
                    }

                    goto NEW_ORDER;
}


// create a sale
DateTime date = DateTime.Now;
Sale s = new Sale(date, price);
MySqlAccess.MySqlAccess.insertObject_Sale(s);

    
ANOTHER_ORDER:

round_number++;
package = -1;
toppingsArraylist.Clear();
iceCreamBallsNumber = 0; 

for (int i = 1; i < 11; i++)
{
    fDict[i] = 0;
}


    Console.WriteLine("Please choose a package:");
    Console.WriteLine("1 - regular cone");
    Console.WriteLine("2 - special cone");
    Console.WriteLine("3 - box");
    userInput = Int32.Parse(Console.ReadLine());
    create_an_order.flavours(ref fDict, ref iceCreamBallsNumber);

    switch (userInput)
    {
        case 1:
            package = 11;
            create_an_order.toppings_for_regular(ref fDict, ref iceCreamBallsNumber, ref toppingsArraylist);
            break;
        case 2:
            package = 12;
            create_an_order.toppings_for_special(ref fDict, ref toppingsArraylist);
            break;
        case 3:
            package = 13;
            create_an_order.toppings_for_box(ref fDict, ref toppingsArraylist);
            break;
    }
/// insert the round of the order to data base 

Console.WriteLine("Do you want to edit your order?");
Console.WriteLine("1 - No");
Console.WriteLine("2 - Yes");
userInput = Int32.Parse(Console.ReadLine());
if(userInput == 2  )
{
    goto NEW_ORDER;
}

/// insert the round of the order to data base 
BusinessLogic.Logic.fillTableOrder(ref toppingsArraylist, round_number, ref fDict, package);

// calculate the price:
if (package == 11)
{
    switch(iceCreamBallsNumber)
    {
        case 1:
            price += 7;
            break;
        case 2:
            price += 12;
            break;
        case 3:
            price += 18;
            break;
    }
}

if (package == 12)
{
    switch(iceCreamBallsNumber)
    {
        case 1:
            price += 9;
            break;
        case 2:
            price += 14;
            break;
        case 3:
            price += 20;
            break;
    }
}

if (package == 13)
{
    switch(iceCreamBallsNumber)
    {
        case 1:
            price += 12;
            break;
        case 2:
            price += 17;
            break;
        case 3:
            price += 23;
            break;
        DEFAULT:
            price += 23 + (iceCreamBallsNumber-3)*6;
    }
}
price += toppingsArraylist.Count*2;

Console.WriteLine("\nPlease choose a task:");
Console.WriteLine("1 - Pay (After payment, the order cannot be canceled)");
Console.WriteLine("2 - Delete");
Console.WriteLine("3 - Add another order");
Console.WriteLine("4 - Get most common ingredient");
Console.WriteLine("5 - Get most common flavour");
userInput = Int32.Parse(Console.ReadLine());

    switch (userInput)
    {
        case 1:  // chose to pay
            sum_price += price; 
            sales_amount++;         
            // update the price in data base 
            MySqlAccess.MySqlAccess.update_price(price);

            Console.WriteLine("Please choose a task:");
            Console.WriteLine("1 - Check the bill");
            Console.WriteLine("2 - New Order");
            Console.WriteLine("3 - exit");
            userInput = Int32.Parse(Console.ReadLine());

            switch (userInput)
            {
                case 1:
                    edit.bill(date, price);
                    goto NEW_ORDER;
                    break;
                case 2:
                    goto NEW_ORDER;
                case 3:
                    Console.WriteLine("Thanks! Hope to see you next time");
                    System.Environment.Exit(0);
                    break;
            }

            break;

        case 2:
            BusinessLogic.edit.delete();
            Console.WriteLine("Thank you for your time");
            goto NEW_ORDER;
            break;

        case 3:
            goto ANOTHER_ORDER;
            break;
         case 4 :
           BusinessLogic.Logic.getMostCommonIN(userInput);
        break;
        case 5:
             BusinessLogic.Logic.getMostCommonIN(userInput);
             break ;
    }