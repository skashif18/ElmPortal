namespace CoreBusiness.Base
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class Entity:IEntity
    {
        [Key]
        public int Id { get; set; }
        public string CreationUserName { get; set; }
        public DateTime CreationDate { get; set; }
        public string LastUpdateUserName { get; set; }
        public DateTime? LastUpdateDate { get; set; }

    }
}
