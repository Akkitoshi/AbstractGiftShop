using AbstractGiftShopModel;
using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.Interfaces;
using AbstractGiftShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text.RegularExpressions;

namespace AbstractGiftShopServiceImplementDataBase.Implementations
{
    public class MessageInfoServiceDB : IMessageInfoService
    {
        private AbstractGiftShopDbContext context;
        public MessageInfoServiceDB(AbstractGiftShopDbContext context)
        {
            this.context = context;
        }
        public List<MessageInfoViewModel> GetList()
        {
            List<MessageInfoViewModel> result = context.MessageInfos
            .Where(rec => !rec.SClientId.HasValue)
            .Select(rec => new MessageInfoViewModel
            {
                MessageId = rec.MessageId,
                SClientFIO = rec.FromMailAddress,
                DateDelivery = rec.DateDelivery,
                Subject = rec.Subject,
                Body = rec.Body
            })
        .ToList();
            return result;
        }
        public MessageInfoViewModel GetElement(int id)
        {
            MessageInfo element = context.MessageInfos.Include(rec => rec.SClient)
            .FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new MessageInfoViewModel
                {
                    MessageId = element.MessageId,
                    SClientFIO = element.SClient.SClientFIO,
                    DateDelivery = element.DateDelivery,
                    Subject = element.Subject,
                    Body = element.Body
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(MessageInfoBindingModel model)
        {
            MessageInfo element = context.MessageInfos.FirstOrDefault(rec =>
           rec.MessageId == model.MessageId);
            if (element != null)
            {
                return;
            }
            var message = new MessageInfo
            {
                MessageId = model.MessageId,
                FromMailAddress = model.FromMailAddress,
                DateDelivery = model.DateDelivery,
                Subject = model.Subject,
                Body = model.Body
            };
            var mailAddress = Regex.Match(model.FromMailAddress, @"(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)");
            if (mailAddress.Success)
            {
                var client = context.SClients.FirstOrDefault(rec => rec.Mail ==
               mailAddress.Value);
                if (client != null)
                {
                    message.SClientId = client.Id;
                }
            }
            context.MessageInfos.Add(message);
            context.SaveChanges();
        }
    }
}