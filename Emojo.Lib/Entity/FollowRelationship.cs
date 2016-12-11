﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emojo.Lib {

    public class FollowRelationship {
        public int Id { get; set; }
        [Required]
        public virtual User Follower { get; set; }
        [Required]
        public virtual User Followed { get; set; }
    }
}