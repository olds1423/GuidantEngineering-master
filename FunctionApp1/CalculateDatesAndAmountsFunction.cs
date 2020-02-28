using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FunctionApp1.Messages;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionApp1
{
    public class CalculateDatesAndAmountsFunction
    {
        [FunctionName("CalculateDatesAndAmountsFunction")]
        public async Task Run([QueueTrigger("messagetomom", Connection = "")]MessageToMom myQueueItem, 
            [Queue("outputletter")] IAsyncCollector<FormLetter> letterCollector,
            ILogger log)
        {
            log.LogInformation($"{myQueueItem.Greeting} {myQueueItem.HowMuch} {myQueueItem.HowSoon}");
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");

            //TODO parse flattery list into comma separated string
            var flatteryList = String.Join(",", myQueueItem.Flattery);
            Console.WriteLine(flatteryList);

            //TODO populate Header with salutation comma separated string and "Mother"
            var headerSalutation = myQueueItem.Greeting +", Mother";
            Console.WriteLine(headerSalutation);

            //TODO calculate likelihood of receiving loan based on this decision tree
            // 100 percent likelihood (initial value) minus the probability expressed from the quotient of howmuch and the total maximum amount ($10000)
            var likelihoodOfLoan = 100; //percent minus the probability expressed? (myQueueItem.HowMuch / 10000) or does it mean (10000 / myQueueItem.HowMuch )


            //TODO calculate approximate actual date of loan receipt based on this decision tree
            // funds will be made available 10 business days after day of submission  
            // business days are weekdays, there are no holidays that are applicable

            //so if submitted on a weekday it will take two business weeks, which would be 14 days later accounting for two weekends 
            //but only 12 days if submitted on a sunday because it would be available on the second friday following?
            //if it were submitted on a saturday it would take 13 days because of the sunday the first week, 5 business days, one weeked, then available at the end of the next business week 



            //TODO use new values to populate letter values per the following:
            //Body:"Really need help: I need $5523.23 by December 12,2020"
            //ExpectedDate = calculated date
            //RequestedDate = howsoon
            //Heading=Greeting
            //Likelihood = calculated likelihood

            await letterCollector.AddAsync(new FormLetter { });
            //add in in fields calculated above here as well as body in string form
        }

        
    }


    
}