using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.AspNetCore.SignalR;
using SimpleChat.Hubs.Interfaces;
using SimpleChat.Models;

namespace SimpleChat.Hubs
{
    public class ChatHub : Hub<IChatHub>
    {
        public void Hello()
        {
            Clients.Caller.DisplayMessage("Hello from the SignalrDemoHub!");
        }
        
        public void SimulateDataProcessing()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            int progressPercentage = 0;
            var random = new Random();

            for (int i = 0; i < 10; i++)
            {
                int waitTimeMilliseconds = random.Next(100, 2500);
                Thread.Sleep(waitTimeMilliseconds);

                Clients.Caller.UpdateProgressBar(progressPercentage += 10);
            }

            stopwatch.Stop();

            Clients.Caller.DisplayProgressMessage($"Data processing complete, elapsed time: {stopwatch.Elapsed.TotalSeconds:0.0} seconds.");
        }

        public void SendMessage(Message message)
        {
            Clients.All.ReceiveMessage(message);
        }
    }
}