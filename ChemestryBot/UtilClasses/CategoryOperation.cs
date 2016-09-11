using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChemestryBot.Interface;
using Microsoft.Bot.Connector;

namespace ChemestryBot.UtilClasses
{
    [Serializable]
    public class CategoryOperation : IContentInterface
    {
        public Activity GetContent(CodeClass code)
        {

            if (code.IndexArray[code.PointAt] < MessagesController.BotCategories[code.PointAt].GetCount())
            {
                Unit temp = MessagesController.BotCategories[code.PointAt].Units[code.IndexArray[code.PointAt]];
                Activity activity = new Activity(ActivityTypes.Message);
                
                activity.Text = temp.description;
                activity.Summary = temp.description;
                if (!String.IsNullOrEmpty(temp.imageUrl))
                {
                    var att = new Attachment()
                    {
                        ContentUrl = temp.imageUrl,
                        ContentType = "image/png",
                        Name = temp.name
                    };
//                    List<CardImage> cardImages = new List<CardImage>();
//                    cardImages.Add(new CardImage(temp.imageUrl));
//                    List<CardAction> cardButtons = new List<CardAction>();
//                    CardAction plButton = new CardAction()
//                    {
////                        Value = "",
////                        Type = "imBack",// "openUrl",
////                        Title = "Next"
//                    };
//                    cardButtons.Add(plButton);
//                    HeroCard plCard = new HeroCard()
//                    {
//                        Title = temp.name,
//                        Subtitle = temp.description,
//                        Images = cardImages,
//                        Buttons = cardButtons
//                    };
//                    Attachment plAttachment = plCard.ToAttachment();
                    List<Attachment> attachementList = new List<Attachment>();
                    attachementList.Add(att);
                    activity.Attachments = attachementList;
//                    List<Attachment> attachementList = new List<Attachment>();
//                    Attachment attachment = new Attachment("image/jpg");
//                    attachment.ContentUrl = temp.imageUrl;
//                    activity.Attachments = attachementList;
                }

                return activity;
            }
            return null;

        }

        public CodeClass GetNextCode(CodeClass code)
        {
            if (code.IndexArray[code.PointAt] < MessagesController.BotCategories[code.PointAt].GetCount())
            {
                code.IndexArray[code.PointAt]++;
            }
            return code;
        }
    }
}