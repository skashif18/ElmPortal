using System;

namespace CoreBusiness
{
    public class Response
    {
        public int Id { get; set; }
        public bool IsSuccess { get; set; } = false;
        public string Message { get; set; }
        public object Objects { get; set; }
        public string guid { get; set; }

    }
}







