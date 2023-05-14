﻿using System;
using System.Collections.Generic;
using System.Linq;
using ChronosBeta.Model;
using System.Text;
using System.Threading.Tasks;
using System.Security.Policy;
using ChronosBeta.DB;
using System.Xml.Linq;
using System.Data.Entity.Migrations;
using System.Windows;

namespace ChronosBeta.BL
{
    class FunctionsTask
    {
        public static List<ViewTask> GetTasks()
        {
            CronosEntities entities = new CronosEntities();
            var project = entities.Task.ToList();
            List<ViewTask> view = new List<ViewTask>();
            foreach (var item in project)
                view.Add(new ViewTask(item));
            return view;
        }

        public static void AddTask( int UserDoTask,         int UserCreateTask ,
                                    string NameTask,        int Project, 
                                    string DeadLine,        string Description, 
                                    string SelectedItsOver, string EstimatedTime, 
                                    string AllSpentTime)
        {
            DB.Task task = new DB.Task();

            task.NameTask       = NameTask;
            task.UserDoTask     = UserDoTask;
            task.UserCreateTask = UserCreateTask;
            task.Project        = Project;
            task.Deadline       = DateTime.Parse(DeadLine);
            task.Description    = Description;
            task.EstimatedTime  = Convert.ToDouble(EstimatedTime);
            task.AllSpentTime   = Convert.ToDouble(AllSpentTime);

            if (SelectedItsOver == "Да")
                task.ItsOver = true;
            else
                task.ItsOver = false;

            if (task == null)
            {
                return;
            }

            CronosEntities entities = new CronosEntities();
            entities.Task.Add(task);
            entities.SaveChanges();
        }

        public static void SaveEditTask(int UserDoTask,         int UserCreateTask,
                                        string NameTask,        int Project,
                                        string DeadLine,        string Description,
                                        string SelectedItsOver, DB.Task currentTask,
                                        string EstimatedTime,   string AllSpentTime)
        {
            currentTask.NameTask       = NameTask;
            currentTask.UserDoTask     = UserDoTask;
            currentTask.UserCreateTask = UserCreateTask;
            currentTask.Project        = Project;
            currentTask.Deadline       = DateTime.Parse(DeadLine);
            currentTask.Description    = Description;
            currentTask.EstimatedTime  = Convert.ToDouble(EstimatedTime);
            currentTask.AllSpentTime   = Convert.ToDouble(AllSpentTime);

            if (SelectedItsOver == "Да")
                currentTask.ItsOver = true;
            else
                currentTask.ItsOver = false;

            if (currentTask == null)
            {
                return;
            }

            CronosEntities entities = new CronosEntities();
            entities.Task.AddOrUpdate(currentTask);
            entities.SaveChanges();
        }

        public static int GetIdTask(string task)
        {
            CronosEntities entities = new CronosEntities();
            return entities.Task.Where(x => x.NameTask == task).First().ID_Task;
        }

        public static List<string> GetViewTask()
        {
            CronosEntities entities = new CronosEntities();
            var task = entities.Task.Select(x => x.NameTask).ToList();
            return task;
        }

        public static void DeleteTask(DB.Task currentTask)
        {
            CronosEntities entities = new CronosEntities();
            entities.Task.Remove(entities.Task.Find(currentTask.ID_Task));
            entities.SaveChanges();
        }
    }
}