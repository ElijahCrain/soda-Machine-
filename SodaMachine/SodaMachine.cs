using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class SodaMachine
    {
        //Member Variables (Has A)
        private List<Coin> _register;
        private List<Can> _inventory;
        
        

        //Constructor (Spawner)
        public SodaMachine()
        {
            _register = new List<Coin>();
            _inventory = new List<Can>();
            FillInventory();
            FillRegister();
        }

        //Member Methods (Can Do)

        //A method to fill the sodamachines register with coin objects.
        public void FillRegister()
        {
			for (int i = 0; i <= 20 ; i++)
			{
                Quarter quarter = new Quarter();
                _register.Add(quarter);
			}
			for (int i = 0; i <= 10; i++)
			{
                Dime dime = new Dime();
                _register.Add(dime);
			}
			for (int i = 0; i <= 20; i++)
			{
                Nickel nickel = new Nickel();
                _register.Add(nickel);
			}
			for (int i = 0; i <= 50; i++)
			{
                Penny penny = new Penny();
                _register.Add(penny);
			}

        }
        //A method to fill the sodamachines inventory with soda can objects.
        public void FillInventory()
        {
			for (int i = 0; i <=12; i++)
			{
                Cola cola = new Cola();
                _inventory.Add(cola);
            }
			for (int i = 0; i <= 12; i++)
			{
                RootBeer rootBeer = new RootBeer();
                _inventory.Add(rootBeer);
            }
			for (int i = 0; i <= 12; i++)
			{
                OrangeSoda orangeSoda = new OrangeSoda();
                _inventory.Add(orangeSoda);
			}
            

            //for loop to fill inventory
        }
        //Method to be called to start a transaction.
        //Takes in a customer which can be passed freely to which ever method needs it.
        public void BeginTransaction(Customer customer)
        {
            bool willProceed = UserInterface.DisplayWelcomeInstructions(_inventory);
            if (willProceed)
            {
                Transaction(customer);
            }
        }
        
        //This is the main transaction logic think of it like "runGame".  This is where the user will be prompted for the desired soda.
        //grab the desired soda from the inventory.
        //get payment from the user.
        //pass payment to the calculate transaction method to finish up the transaction based on the results.
        private void Transaction(Customer customer)
        {
           
        }
        //Gets a soda from the inventory based on the name of the soda.
        private Can GetSodaFromInventory(string nameOfSoda)
        {
			for (int i = 0; i < _inventory.Count; i++)
			{
                if(_inventory[i].Name == nameOfSoda)
				{
                    _inventory.RemoveAt(i);
                    return _inventory[i];
					
				}
				else
				{
					Console.WriteLine("We dont have soda");
				}

			}
            return null; 
        }

        //This is the main method for calculating the result of the transaction.
        //It takes in the payment from the customer, the soda object they selected, and the customer who is purchasing the soda.
        //This is the method that will determine the following:
        //If the payment is greater than the price of the soda, and if the sodamachine has enough change to return: Despense soda, 
        //and change to the customer.
        //If the payment is greater than the cost of the soda, but the machine does not have ample change: Despense payment back to the customer.
        //If the payment is exact to the cost of the soda:  Despense soda.
        //If the payment does not meet the cost of the soda: despense payment back to the customer.
        private void CalculateTransaction(List<Coin> payment, Can chosenSoda, Customer customer)
        {
            double totalValue = TotalCoinValue(payment);
            double totalchange = DetermineChange(totalValue, chosenSoda.Price);
            List<Coin> changelist = GatherChange(totalchange);

            if (totalValue > chosenSoda.Price && GatherChange(totalchange) != null )
			{
                GetSodaFromInventory(chosenSoda.Name);
                customer.AddCanToBackpack(chosenSoda);
                customer.AddCoinsIntoWallet(changelist);

			}
            else if (totalValue > chosenSoda.Price && GatherChange(totalchange) == null)
			{
                customer.AddCoinsIntoWallet(payment);
			}
            else if (totalValue == chosenSoda.Price)
			{
                DepositCoinsIntoRegister(payment);
                customer.AddCanToBackpack(chosenSoda);
			}
			else if (totalValue != chosenSoda.Price)
			{

                customer.AddCoinsIntoWallet(payment);
			}
        }
        //Takes in the value of the amount of change needed.
        //Attempts to gather all the required coins from the sodamachine's register to make change.
        //Returns the list of coins as change to despense.
        //If the change cannot be made, return null.
        public List<Coin> GatherChange(double changeValue)
        {
            // changeValue : 75

            // Create a temp list of coin
            List<Coin> coinsChange = new List<Coin>();
             while (changeValue > .0)
			{
                if (changeValue >= .25)
				{
					if (RegisterHasCoin("Quarter"))
					{
                        Coin quarter = GetCoinFromRegister("Quarter");
                        coinsChange.Add(quarter);
                        changeValue -=.25;
                         continue;
					}   
                    
                    
				}
                if (changeValue >= .10)
				{
                   if (RegisterHasCoin("Dime")) 
					{
                        Coin dime = GetCoinFromRegister("Dime");
                        coinsChange.Add(dime);
                        changeValue -= .10;
                        continue;
                    }

                   
                    
				}
                if (changeValue >= .5)
				{
                   if (RegisterHasCoin("Nickel"))
					{
                        Coin nickel = GetCoinFromRegister("Nickel");
                        coinsChange.Add(nickel);
                        changeValue -= .5;
                        continue;
                    }
                    
                    
				}
                if (changeValue > .01)
				{
                   if (RegisterHasCoin("Penny"))
					{
                        Coin penny = GetCoinFromRegister("Penny");
                        coinsChange.Add(penny);
                        changeValue -= .01;
                        continue;
                    }
                    
                   
				}
                return null;
			}return coinsChange; 
                    
            
        }
        //Reusable method to check if the register has a coin of that name.
        //If it does have one, return true.  Else, false.
        private bool RegisterHasCoin(string name)
        {
            foreach (Coin coinType in _register)
            {
                if ( name == coinType.Name)
                {
                    return true;
                }
				
            }
            return false;
        }
        //Reusable method to return a coin from the register.
        //Returns null if no coin can be found of that name.
        private Coin GetCoinFromRegister(string name)
        {
			foreach(Coin coinType in _register)
			{
                if (name == coinType.Name )
				{
                    return coinType;
				}
			   
				
            }
            return null;
            
        }
        //Takes in the total payment amount and the price of can to return the change amount.
        private double DetermineChange(double totalPayment, double canPrice)
        {
            double correctChange = totalPayment - canPrice;
                return correctChange;
        }
        //Takes in a list of coins to returnt he total value of the coins as a double.
        private double TotalCoinValue(List<Coin> payment)
        {
            double paymentTotal = 0;
            foreach(Coin coin in payment)
			{
                paymentTotal += coin.Value;
			}
            return paymentTotal;
        }
        //Puts a list of coins into the soda machines register.
        private void DepositCoinsIntoRegister(List<Coin> coins)
        {
         foreach(Coin coin in coins )
			{
                _register.Add(coin);
			}
            
        }
    }
}
