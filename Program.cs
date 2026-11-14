namespace BudgetCalculator
{
    public class BudgetLogic
    { 
        public static void Main()
        {
            GatherUserInformation();
        }

        public static void GatherUserInformation()
        {
            Console.Write("What's Your Name: ");
            string userName = Console.ReadLine() ?? "User";

            //Validate User Inputs Valid Number
            Console.Write("\nWhat's Your Budget: ");
            if (!double.TryParse(Console.ReadLine() ?? "", out double userBudget))
            {
                Console.WriteLine("Invalid Budget. Please enter a number.");
                return;
            }

            Console.Write("\nGPU Cost: ");
            if (!double.TryParse(Console.ReadLine() ?? "", out double gpuCost))
            {
                Console.WriteLine("Invalid GPU Cost. Please enter a number.");
                return;
            }

            Console.Write("\nCPU Cost: ");
            if (!double.TryParse(Console.ReadLine() ?? "", out double cpuCost))
            {
                Console.WriteLine("Invalid CPU Cost. Please enter a number.");
                return;
            }
            
            Console.Write("\nRAM Cost: ");
            if (!double.TryParse(Console.ReadLine() ?? "", out double ramCost))
            {
                Console.WriteLine("Invalid RAM Cost. Please enter a number.");
                return;
            }

            //Validate Numbers are Valid
            if (userBudget <= 0)
            {
                Console.WriteLine("Budget must be greater than Zero.");
                return;
            }

            if (gpuCost < 0 || cpuCost < 0 || ramCost < 0 )
            {
                Console.WriteLine("Please Verify All Fields are not Empty.");
                return;
            }

            //Send Results Over | Not Best Practice
            CalculateBudget(gpuCost,cpuCost,ramCost,userBudget,userName);
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

        public static void DisplayUpgradeSummary //God Bless the Argument Gods
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