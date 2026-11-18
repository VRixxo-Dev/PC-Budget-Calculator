using System.ComponentModel.Design;

namespace BudgetCalculator
{
    public class BudgetLogic
    { 
        public static void Main()
        {
            //First Run
            string appVer = "1.0";
            Console.WriteLine($"PC-Budget-Calculator: Version {appVer}\n");

            //Start App
            string menuSelection;
            //We could create a helper method to validate menuSelection, Backlog
            do
            {
                GatherUserInformation();
                Console.Write("\nWould you like to Calculate Another Upgrade? (Y/N): ");
                menuSelection = (Console.ReadLine() ?? String.Empty).Trim().ToUpper();
            }
            while(menuSelection == "Y");
        }

        private static void GatherUserInformation()
        {
            Console.Clear();
            Console.Write("What's Your Name: ");
            string userName = Console.ReadLine() ?? "User";
            double userBudget = ReadValidAmount("Please Enter Your Budget: ",false);

            // Change ONLY this array to add components!!!
            string[] componentNames = ["GPU","CPU","RAM"];
            double[] componentCost = new double[componentNames.Length]; 

            for (int i = 0; i < componentNames.Length; i++)
            {
                componentCost[i] = ReadValidAmount($"{componentNames[i]} Cost: ");
            }

            //Send Results for Calculation
            CalculateBudget(componentCost, componentNames, userBudget,userName);
        }

        //Potentially add another argument to allow specificying the type of Invaldi Number
        //Ex $Invalid {errorPrompt}, Please try again } | Not Important
         private static double ReadValidAmount(string prompt, bool allowZero = true) //TODO Replace redundant TryParse logic
        {
            double amount;
            Console.Write(prompt);

            while (!double.TryParse(Console.ReadLine() ?? "", out  amount) 
                || amount < 0
                || (!allowZero && amount == 0))
            {
                Console.Write("Invalid Number, Please Try Again: ");
            }
            return amount;
        }

        private static void CalculateBudget(double[] componentCost,string[] componentNames,double userBudget, string userName)
        {
            double combinedUpgradeCost = 0;
            double budgetRemainder;

            foreach(double cost in componentCost)
            {
                combinedUpgradeCost += cost;
            }

            budgetRemainder =  userBudget - combinedUpgradeCost;

            DisplayUpgradeSummary
            (combinedUpgradeCost, budgetRemainder, componentCost,componentNames ,userBudget, userName);
        }

        private static void DisplayUpgradeSummary 
        (double combinedUpgradeCost, double budgetRemainder, double[] componentCost, string[] componentNames, double userBudget, string userName)
        {
            string budgetStatus;
            if(combinedUpgradeCost > userBudget)
            {
                budgetStatus = "Over Budget";
            }
            else if(combinedUpgradeCost == userBudget)
            {
                budgetStatus = "Right On Target";
            }
            else
            {
                budgetStatus = "Within Budget";
            }
            /* TODO Split into 3 Parts | Heading | Components (For Loop) | Total Summary
            */
            double remainingBudgetPercentage = (budgetRemainder / userBudget) * 100;
            
            Console.WriteLine($"""
            
            ----- UPGRADE SUMMARY-----

            Customer: {userName}
            Budget: ${userBudget:F2}

            """);

            // For Loop Goes Here
            for (int i = 0;i < componentNames.Length;i++)
            {
                Console.WriteLine($"{componentNames[i]} Cost: {componentCost[i]:F2}");
            }

            Console.WriteLine($"""
        
            Total Cost: ${combinedUpgradeCost:F2}
            Budget Status: {budgetStatus}

            Remaining Budget: ${budgetRemainder:F2}
            Remaining Budget Percentage: {remainingBudgetPercentage:F1}%
            """);
        }
    }
}