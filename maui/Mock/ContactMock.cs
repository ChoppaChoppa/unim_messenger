using System;
using System.Collections.Generic;
using maui.Models;

namespace maui.Mock
{
    public static class ContactMock
    {

        public static List<ContactModel> GetContacts()
        {
            return new List<ContactModel>()
            {
               new ContactModel
               {
                   ID = 1,
                   Name = "Martin L. King",
                   Messages = new List<Message>()
                   {
                       new Message
                       {
                           MessageFrom = 1,
                           MessageText = "Hello"
                       },
                       new Message
                       {
                           MessageFrom = 0,
                           MessageText = "Hi"
                       },
                       new Message
                       {
                           MessageFrom = 1,
                           MessageText = "How are you?"
                       },
                       new Message
                       {
                           MessageFrom = 0,
                           MessageText = "I'm fine"
                       },
                       new Message
                       {
                           MessageFrom = 0,
                           MessageText = "what do you need?"
                       },
                       new Message
                       {
                           MessageFrom = 0,
                           MessageText = "Go to Meeasdjoaijd aosdjia oisjdo aisjdoaisjdaosidudsnfisundfisudnfisdunfisdufnisdunfisudnfisdunfsidunfsidufnshhhhhhisudhfisudhfsidufhisdufhisudfhsiudhfisudhfsiduhfsiudhfisudhfisudfhsidufhsdiufhsdiuhfsdiufhisudhfisuhfisudfhisudhfisduhfsidufhsidufhsidufhsiudhfsidufhisudhfisudhfisudfhsiudhfisudfhsiduhfsiudhfisudhfsidufhsidufhsiudhfisudhfisudhfisudhfisudhfsiduhfiusdhfisudhfisduhfisudhfisudhfisudfhsiufhsdiufhsdiufhisufhsiufhsiduhfiusdhfiusdhfisudfhisduhfisdufhidufnsidufnsidunfsidufnsidufnsiudnfsiudnfisdunfisdunfiorungf ci reungicugyniusynfiusydbgiuseyrbgcmisuyebrgiusdfbgaosijdoajting"
                       },
                   },
                   LastMessage = "Go to Meeting"
               },
               new ContactModel
               {
                   Name = "MAUI",
                   LastMessage = "I'm C# and GO developer",
                   Messages = new List<Message>()
               },
               new ContactModel
               {
                   Name = "Michel Jordan",
                   LastMessage = "Go to play basketball",
                   Messages = new List<Message>()
               },
               new ContactModel
               {
                   Name = "KFC",
                   LastMessage = "Your order '1234567890' was create and wating you",
                   Messages = new List<Message>()
               },
               new ContactModel
               {
                   Name = "Boris",
                   LastMessage = "Boris ebola zhuadela",
                   Messages = new List<Message>()
               },
               new ContactModel
               {
                   Name = "Инал",
                   LastMessage = "Invalidate",
                   Messages = new List<Message>()
               },
            };
        }
    }

}
