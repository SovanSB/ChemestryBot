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
                var actions = new List<CardAction>();
                actions.Add(AttachmentCreation.CreateCardAction(ValuesStrings.NEXT, "Next"));
                
                List<CardImage> imageList = new List<CardImage>();
//                activity.Text = temp.description;
//                activity.Summary = temp.description;
                if (!String.IsNullOrEmpty(temp.imageUrl))
                {
                    CardImage image = new CardImage(temp.imageUrl);
                    imageList.Add(image);
                    
                    
//                    List<Attachment> attachementList = new List<Attachment>();
//                    Attachment attachment = new Attachment("image/jpg");
//                    attachment.ContentUrl = temp.imageUrl;
//                    activity.Attachments = attachementList;
                }

                var attachment = AttachmentCreation.CreateHeroCardAttachment(temp.name, null, temp.description,
                                        imageList, actions);
                List<Attachment> attachementList = new List<Attachment>();
                attachementList.Add(attachment);
                activity.Attachments = attachementList;
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