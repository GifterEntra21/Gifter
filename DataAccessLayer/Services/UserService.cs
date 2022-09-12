﻿using DataAccessLayer.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services
{
    public class UserService : IUserDAL
    {
        private readonly GifterContextDb _Db;
        public UserService(GifterContextDb db)
        {
            _Db = db;
        }

        public async Task<Response> Delete(User user)
        {          
            try
            {
                User userDb = await _Db.Users.FindAsync(user.Id);
                _Db.Users.Remove(userDb);
                _Db.SaveChanges();
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {

                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }

        }

        public async Task<DataResponse<User>> GetAll()
        {
            try
            {
                List<User> users = await _Db.Users.ToListAsync();
                if (users == null)
                {
                    return ResponseFactory.CreateInstance().CreateDataFailedResponse<User>(null);
                }
                return ResponseFactory.CreateInstance().CreateDataSuccessResponse(users);
            }
            catch (Exception ex)
            {

                return ResponseFactory.CreateInstance().CreateDataFailedResponse<User>(ex);
            }
        }

        public async Task<SingleResponse<User>> GetById(int id)
        {
            try
            {
                User user = await _Db.Users.FindAsync(id);
                return ResponseFactory.CreateInstance().CreateSingleSuccessResponse<User>(user);
                
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateSingleFailedResponse<User>(ex,null);
            }
        }

        public async Task<SingleResponse<User>> GetByUsername(string username)
        {
            try
            {
                User user = await _Db.Users.FindAsync(username);
                return ResponseFactory.CreateInstance().CreateSingleSuccessResponse(user);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateSingleFailedResponse<User>(ex, null);
            }
        }

        public async Task<Response> Insert(User user)
        {
            try
            {
                _Db.Users.Add(user);
                await _Db.SaveChangesAsync();
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }


        public async Task<Response> Update(User user)
        {
             User userDb =  await _Db.Users.FindAsync(user.Id);
            userDb.Username = user.Username;
            userDb.Password = user.Password;
            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.IsActive = user.IsActive;

            try
            {
                await _Db.SaveChangesAsync();
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {

                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }
    }
}
