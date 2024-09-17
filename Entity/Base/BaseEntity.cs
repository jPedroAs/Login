using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Login.Entity;

public class BaseEntity
{
    // [Key]
    // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [BsonId]
    public ObjectId Id { get; set; }

    [IgnoreDataMember]
    public DateTime? CreateAt { get; set; }

    [IgnoreDataMember]
    public DateTime? LastUpdateAt { get; set; }

    [IgnoreDataMember]
    public DateTime? DeleteAt { get; set; }

    [IgnoreDataMember]
    public bool Actived { get; set; }
}