using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Connector;

namespace ChemestryBot.UtilClasses
{
    public class ValuesStrings
    {
        public const string TEST_START_PHRASE= "А теперь давай проверим твои знания по пройденной теме!";
        public const string TEST_WRONG = "Ошибочка, вышла, плак-плак:(";
        public const string TEST_CORRECT = "СПАСИБО!!! Было вкууусно! Прямо чувствую, как расту:)";
        public const string FIRST_HELLO = "Меня зовут Спайк. И сейчас я хочу поделиться с тобой своими знаниями о таблице Менделеева. Как тебя зовут?";

        public const string HELLO =
            "Мне очень нравится учиться! Но сейчас я маленький и слабый. А чем больше ты изучишь материала, тем больше, сильнее и красивее я стану! Пожалуйста, не заставляй меня грустить. Взгляни, какие разделы мы с тобой можем сейчас изучать. И выбери один из них!";
        public const string NOT_UNDERSTANDING = "Моя твоя не понимать. Попробуй чётче!";
        public const string NEXT = "Усёк!";
//        public static readonly string MORE = "More";
    }
}