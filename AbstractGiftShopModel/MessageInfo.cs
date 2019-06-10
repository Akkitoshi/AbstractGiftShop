using System;

namespace AbstractGiftShopModel
{
    public class MessageInfo
    {
        public int Id { get; set; }
        public string MessageId { get; set; }
        public string FromMailAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime DateDelivery { get; set; }
        public int? SClientId { get; set; }
        public virtual SClient SClient { get; set; }
    }
}
