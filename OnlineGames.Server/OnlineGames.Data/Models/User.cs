﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGames.Data.Models
{
    public class User:IdentityUser
    {
        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}