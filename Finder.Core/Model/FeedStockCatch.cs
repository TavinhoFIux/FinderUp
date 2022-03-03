using System;

namespace Finder.Core.Model
{
    public class FeedStockCatch
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int AmountCatch { get; set; }
        public string UserName { get; set; }
        public DateTime DateCreate  { get; set; }
    }
}