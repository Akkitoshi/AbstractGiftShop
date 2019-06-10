using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.ViewModel;
using System.Collections.Generic;

namespace AbstractGiftShopServiceDAL.Interfaces
{
    public interface IMessageInfoService
    {
        List<MessageInfoViewModel> GetList();
        MessageInfoViewModel GetElement(int id);
        void AddElement(MessageInfoBindingModel model);
    }
}
