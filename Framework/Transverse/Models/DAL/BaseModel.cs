﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Transverse.Models.DAL
{
    public class BaseModel
    {
        public BaseModel()
        {
            CreatedDate = DateTime.Now;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}