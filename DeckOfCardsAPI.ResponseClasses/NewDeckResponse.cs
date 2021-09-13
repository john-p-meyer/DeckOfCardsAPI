using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeckOfCardsAPI.ResponseClasses
{
    public class NewDeckResponse : BaseResponse
    {
        public bool shuffled { get; set; }
    }
}
