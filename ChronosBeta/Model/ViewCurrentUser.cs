﻿using ChronosBeta.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ChronosBeta.Model
{
    public class ViewCurrentUser
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public BitmapImage ImageUser { get; set; }
    }
}