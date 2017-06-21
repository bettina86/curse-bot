using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Luis;
using System.Collections.Generic;
using Microsoft.Bot.Builder.Luis.Models;

namespace CurseBot.Dialogs
{
    [LuisModel("9745f18f-4218-422d-b460-06f46108a92a", "cfa6434f40a94243a461f855ecc32abb")]
    [Serializable]
    public class RootLuisDialog : LuisDialog<object>
    {
        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"Não entendi seu porra!. Digite 'ajuda' se quiser alguma.";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("CursingByName")]
        public async Task CursingByName(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;

            EntityRecommendation name;

           if(result.TryFindEntity("Name", out name))
           {
                await context.PostAsync($"aff {name.Entity}, que nome lixo!");
            }
            else
            {
                await context.PostAsync($"aff que nome lixo!");
            }
                        

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("CursingByAge")]
        public async Task CursingByAge(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;

            EntityRecommendation age;

            if (result.TryFindEntity("Age", out age))
            {
                var ageInt = int.Parse(age.Entity);
                if(ageInt < 18)
                {
                    await context.PostAsync($"sai dae pirralho!");
                }
                else if(ageInt < 40)
                {
                    await context.PostAsync($"vai o brocha do carai!");
                }
                else
                {
                    await context.PostAsync($"seu véio podre!");
                }
            }

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("OnlyCursing")]
        public async Task OnlyCursing(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;

            await context.PostAsync(message.Text);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Help")]
        public async Task Help(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Iae seu otário!, se precisa de algo digue seu nome ou sua idade!");

            context.Wait(this.MessageReceived);
        }
    }
}