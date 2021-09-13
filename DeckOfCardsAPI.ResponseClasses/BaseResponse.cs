using System;

namespace DeckOfCardsAPI.ResponseClasses
{
    public class BaseResponse
    {
        public bool success { get; set; }
        public string deck_id { get; set; }
        public int remaining { get; set; }
        public string error { get; set; }
    }
}
