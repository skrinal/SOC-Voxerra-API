﻿namespace Voxerra_API.Entities
{
    public class TblMessage
    {
        public int Id { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime SendDateTime { get; set; }
        public bool IsRead { get; set; }
        
        public virtual TblUser FromUser { get; set; }
        public virtual TblUser ToUser { get; set; }
    }
}
