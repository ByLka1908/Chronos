﻿using ChronosBeta.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChronosBeta.BL
{
    public class ViewTask
    {
        public DB.Task Task { get; set; }
        public string Name { get; set; }
        public string UserCreateTask { get; set; }
        public string UserDoTask { get; set; }

        public ViewTask(DB.Task task) 
        {
            Task = task;
            Name = task.NameTask;
            UserCreateTask = task.Users.Name;
            UserDoTask = task.Users1.Name;
        }
    }
}