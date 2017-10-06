﻿using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace alimentation
{
    internal class Role
    {
        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreationUser { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string ModificationUser { get; set; }

    }
}
