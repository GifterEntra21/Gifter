using DataAccessLayer.Interfaces;
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
    public class UserServiceDAL : IUserDAL
    {
        private readonly GifterContextDb _Db;
        public UserServiceDAL(GifterContextDb db)
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

        public async Task<SingleResponse<User>> Login(User model)
        {
            try
            {
                //pq n funciona o await e async? 

                User user = _Db.Users.FirstOrDefault(x => x.Username == model.Username && x.Password == model.Password);
                return ResponseFactory.CreateInstance().CreateSingleSuccessResponse(user);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateSingleFailedResponse<User>(ex, null);
            }
        }

        public async Task<Response> Insert(User user)
        {
            _Db.Users.Add(user);
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


        public async Task<Response> Update(User user)
        {
            User userDb =  await _Db.Users.FindAsync(user.Id);
            userDb.Username = user.Username;
            userDb.Password = user.Password;
            userDb.Email = user.Email;
            userDb.IsActive = user.IsActive;
            userDb.RefreshToken = user.RefreshToken;
            userDb.RefreshTokenExpiryTime = user.RefreshTokenExpiryTime;
           

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

        public async Task<SingleResponse<User>> GetByUsername(User model)
        {
            try
            {
                //pq n funciona o await e async? 

                User user = _Db.Users.FirstOrDefault(u => u.Username == model.Username);
                return ResponseFactory.CreateInstance().CreateSingleSuccessResponse(user);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateSingleFailedResponse<User>(ex, null);
            }
        }
    }
}
