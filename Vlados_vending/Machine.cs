using System;
using System.Collections.Generic;
using System.Text;

namespace Vlados_vending
{
    public class Machine
    {
        List<Product> products = new List<Product> {
            new Product() { name = "Tea", price = 20 },
            new Product() { name = "Cookies", price = 40 },
            new Product() { name = "Coffee", price = 25 },
            new Product() { name = "Cheesecake", price = 100 }
        };

        private int userMoney = 0;
        private Product userChoice = null;

        public void Start()
        {
            Console.WriteLine("VladOS vending corp. 1984.");
            AskMoney();
            AskChoice();
            Sell();
        }

        private void Sell()
        {
            if (userMoney > 0 && userChoice != null)
            {
                if (userChoice.price > userMoney)
                {
                    Console.WriteLine("Insufficient funds for this one.");
                    Console.WriteLine("Would you like to add up? Y | N.");
                    ConsoleKeyInfo yn = Console.ReadKey();
                    if (yn.Key == ConsoleKey.Y)
                    {
                        ReaskMoney();
                        Sell();
                    }
                    else
                    {
                        Console.WriteLine("Operation declined");
                    }
                }
                else if (userChoice.price == userMoney)
                {
                    Console.WriteLine($"Here's your {userChoice.name}");
                }
                else
                {
                    Console.WriteLine($"Here's your {userChoice.name}");
                    Console.WriteLine($"Here's your change {userMoney - userChoice.price}");
                    CountChange(userMoney - userChoice.price);
                }
            }
        }

        private void AskChoice()
        {

            for (int i = 0; i < products.Count; i++)
            {
                Console.WriteLine($"We've got: {products[i].name}, at position: {i}, for: {products[i].price}");
            }
            if (products.Count > 1)
            {
                Console.WriteLine($"Choose one between 0 and {products.Count - 1}.");
            }
            else
            {
                Console.WriteLine($"Choose one.");
            }

            int userChosePos = int.Parse(Console.ReadLine());

            if (userChosePos > products.Count - 1 || userChosePos < 0)
            {
                Console.WriteLine("No such index! Try again.");
                AskChoice();
            }

            userChoice = products[userChosePos];
        }

        private void AskMoney()
        {
            Console.WriteLine("Deposit money.");
            int money = int.Parse(Console.ReadLine());

            if (money < 1)
            {
                Console.WriteLine("Ammount of money is too low. Try again.");
                AskMoney();
            }
            else
            {
                userMoney = money;
            }
        }

        private void ReaskMoney()
        {
            Console.WriteLine("Add money.");
            int money = int.Parse(Console.ReadLine());

            if (money < 1)
            {
                Console.WriteLine("Ammount of money is too low. Try again.");
                AskMoney();
            }
            else
            {
                userMoney += money;
            }
        }

        private void CountChange(int change)
        {
            List<int> coinsChange = new List<int> { };
            while (change > 0)
            {
                int coinWorth = change > Coins.Tenner ? Coins.Tenner
                    : change < Coins.Tenner && change > Coins.Fiver ? Coins.Fiver
                    : change < Coins.Fiver && change > Coins.TwoCents ? Coins.TwoCents
                    : change < Coins.TwoCents ? Coins.Cent
                    : 0;
                change -= coinWorth;
                coinsChange.Add(coinWorth);
                Console.WriteLine($"Change is turned in by {coinWorth}");
                Console.WriteLine(change);
            }
            Console.WriteLine("Recounting...");
            string recounted = "";
            int sum = 0;
            for (int i = 0; i < coinsChange.Count; i++)
            {
                if (i != coinsChange.Count - 1)
                {
                    recounted += coinsChange[i].ToString() + " + ";
                }
                else
                {
                    recounted += coinsChange[i].ToString() + " = ";
                }
                sum += coinsChange[i];
            }
            Console.WriteLine($"{recounted}{sum}");
            Console.WriteLine("Have a nice day! <3");
        }

    }
}
