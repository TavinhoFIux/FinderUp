using System;
using System.Collections.Generic;
using System.Text;

namespace  Finder.Core.Model
{
   public class FeedStock
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public Guid UserId { get; set; }
    }
}
