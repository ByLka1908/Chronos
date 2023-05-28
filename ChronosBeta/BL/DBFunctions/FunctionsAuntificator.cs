﻿using System.Linq;
using System.Net;
using ChronosBeta.DB;
using System.Windows;
using System.Configuration;

namespace ChronosBeta.BL
{
    class FunctionsAuntificator
    {
        /// <summary>
        /// Аунтификация пользователя
        /// </summary>
        /// <param name="credential">Пароль и логин</param>
        /// <returns></returns>
        public static bool Auntification(NetworkCredential credential)
        {
            CronosEntities entities = new CronosEntities();
            Users user = new Users();
            user = entities.Users.Where(x => x.Login == credential.UserName && x.Password == credential.Password).FirstOrDefault();

            if (user != null)
            {
                FunctionsCurrentUser.SetUser(user);
                return true;
            }
            else
                return false;
        }
    }
}