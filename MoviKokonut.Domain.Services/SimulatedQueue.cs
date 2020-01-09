//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace MoviKokonut.Domain.Services
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Threading.Tasks;
    using MoviKokonut.Domain.Services.Interface;

    // For a detailed description on how to manage cross-database consistency,
    // see the documentation  "How the Shopping Application Creates an Order"
    // on http://go.microsoft.com/fwlink/p/?LinkID=317537 
    [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "We are simulating a queue, so this name is appropriate.")]
    public sealed class SimulatedQueue : IOrderQueue
    {
        private readonly IOrderProcessor orderProcessor;

        public SimulatedQueue(IOrderProcessor orderProcessor)
        {
            this.orderProcessor = orderProcessor;
        }
        
        public void Send(Guid trackingId)
        {
            // To make this guidance reference implementation easy to consume and deploy, 
            // we spin up a task to simulate a queue:
            Task.Run(() => this.DeliverMessage(trackingId));
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Since this simulates a background process, we need to catch all exceptions to make sure the Task completes properly.")]
        private void DeliverMessage(Guid trackingId)
        {
            // In a queue implementation, a separate process (for example, a Windows Azure worker role) should
            // get the trackingId from the queue and then process the message. 

            // You should setup your queuing logic as follows:
            // 1. If the method Execute runs successfully, the message should be removed from the queue.
            // 2. If the method Execute throws an exception, the message should NOT be removed from the queue so that 
            //    the method will get called again some time in the future.

            // The following code simulates the queuing logic to allow a maximum number of 5 tries to process each message.
            int maxNumberOfTimes = 5;
            for (int i = 1; i <= maxNumberOfTimes; i++)
            {
                try
                {
                    this.orderProcessor.Execute(trackingId);

                    // You may want to remove the message from the queue here
                    // ....

                    // Execute succeeded, break out of the loop...
                    break;
                }
                catch (Exception)
                {
                    // You may want to log the exception here 
                    // Log(...)
                    if (i < maxNumberOfTimes)
                    {
                        // Wait for 2 seconds to try again
                        Thread.Sleep(2000);
                    }
                    else // (i == maxNumberOfTimes)
                    {
                        // You know that the system is not working correctly since you have reached the maximum number of tries 
                        // to process the message. Manual intervention may be necessary at this point.
                        // You can log, send an email to operations, or call some other method to handle the situation 
                        // by adding your error handling code here:

                        // ...

                        // You may want to remove the message from the queue at this point, since you 
                        // have already resorted to an alternate method to handle the message

                        // ...

                        // You are now leaving the loop
                    }
                }
            }
        }
    }
}
