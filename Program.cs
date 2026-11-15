namespace BudgetCalculator
{
    public class BudgetLogic
    { 
        public static void Main()
        {
            //First Run
            string appVer = "2.0";
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

        public static void GatherUserInformation()
        {
            Console.Clear();
            Console.Write("What's Your Name: ");
            string userName = Console.ReadLine() ?? "User";

            //Pass a Prompt and Bool to Helper Function to return a valid double
            double userBudget = ReadValidAmmount("Please Enter Budget: ",false);
            double gpuCost = ReadValidAmmount("GPU Cost: ");
            double cpuCost = ReadValidAmmount("CPU Cost: ");
            double ramCost = ReadValidAmmount("RAM Cost: ");

            //Send Results for Calculation
            CalculateBudget(gpuCost,cpuCost,ramCost,userBudget,userName);
        }
        //Potentially add another argument to allow specificying the type of Invaldi Number
        //Ex $Invalid {errorPrompt}, Please try again } | Not Important
        public static double ReadValidAmmount(string appPrompt, bool allowZero = true) //TODO Replace redundant TryParse logic
        {
            double ammount;
            Console.Write(appPrompt);

            while (!double.TryParse(Console.ReadLine() ?? "", out  ammount) || (allowZero == false && ammount == 0)) 
            {
                Console.Write($"Invalid Number, Please Try Again: ");
                continue;
            }
            return ammount;
        }

        public static void CalculateBudget(double gpuCost,double cpuCost,double ramCost,double userBudget, string userName)
        {
            double combinedUpgradeCost;
            double budgetRemainder;

            combinedUpgradeCost = gpuCost + cpuCost + ramCost;
            budgetRemainder =  userBudget - combinedUpgradeCost;

            DisplayUpgradeSummary
            (combinedUpgradeCost, budgetRemainder, gpuCost, cpuCost, ramCost, userBudget, userName);
        }

        public static void DisplayUpgradeSummary //God Bless the Argument Gods, We can do better TODO Cleanup
        (double combinedUpgradeCost,double budgetRemainder, double gpuCost, double cpuCost, double ramCost, double userBudget, string userName)
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
            else if(combinedUpgradeCost < 0)
            {
                budgetStatus = "You are a Wizard of Unknown Stregnths";
            }
            else
            {
                budgetStatus = "Within Budget";
            }

            double remainingBudgetPercentage = (budgetRemainder / userBudget) * 100;
            Console.WriteLine($"""

            ----- UPGRADE SUMMARY-----

            Customer: {userName}
            Budget: ${userBudget:F2}
            GPU Cost: ${gpuCost:F2}
            CPU Cost: ${cpuCost:F2}
            RAM Cost: ${ramCost:F2}

            Total Cost: ${combinedUpgradeCost:F2}
            Budget Status: {budgetStatus}

            Remaining Budget: ${budgetRemainder:F2}
            Remaining Budget Percentage: {remainingBudgetPercentage:F1}%
            """);
        }
    }
}