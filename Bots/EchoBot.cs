// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.18.1

using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeoFriend.Bots
{
    public class EchoBot : ActivityHandler
    {
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var name = turnContext.Activity.Text;
            var f = System.IO.Path.Combine(System.Environment.CurrentDirectory,@"WorldCities.csv");
            var cd = new CountryData(f);
            var cap = cd.GetCapital(name);
            var replyText = cap == null 
                ? $"Sorry, I don't know the capital of {name}. \r\n Los siento, no se cual es la capital de {name} " 
                : $"The capital of {name} is {cap}!.";
            await turnContext.SendActivityAsync(replyText);

        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var welcomeText = "Hello, I am LilyBot.  I am your geography TA. Enter the name of a country/region and I will tell you its capital city  \r\n " +
                "Hola, soy LilyBot.  Te voy a ensenar las capitales del mundo.  Escribe solamente el nombre de un pais o region";
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
                }
            }
        }
    }
}
