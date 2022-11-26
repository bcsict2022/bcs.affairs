using infrastructure.bcs.affairs.Entities;
using infrastructure.bcs.affairs.Models;
using infrastructure.bcs.affairs.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.bcs.affairs.Factories
{
    public class FactoryUsers : IUsers
    {
        private readonly AffairsContext _basedContext;

        public FactoryUsers(AffairsContext basedContext)
        {
            _basedContext = basedContext;
        }

        private static void CreateHashWithSalt(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            try
            {
                //byte[] salt, hash;

                using (var h = new HMACSHA512())
                {
                    passwordSalt = h.Key;
                    passwordHash = h.ComputeHash(Encoding.UTF8.GetBytes(password));
                }

                //passwordHash = Convert.ToBase64String(hash);
                //passwordSalt = Convert.ToBase64String(salt);

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public async Task<bool> CreateUserAsync(vmUser vm)
        {
            try
            {
                CreateHashWithSalt("password", out byte[] passwordHash, out byte[] passwordSalt);

                var user = new Users
                {
                  EmailAddress = vm.UserId,
                  LastName = vm.LastName,
                  FirstName = vm.FirstName,
                  PasswordHashed = passwordHash,
                  SecurityStamp = passwordSalt,
                  UserStatus =   true,
                  IsFirstLogin = false,
                  CreatedUser = "", // vm.Createdby,
                  TransactionDate = DateTime.Today.Date
                };
                await _basedContext.User.AddAsync(user);
                await _basedContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool NewPassword(string userId, string newPassword)
        {
            try
            {
                CreateHashWithSalt(newPassword, out byte[] passwordHash, out byte[] passwordSalt);

                var user = _basedContext.User.Find(userId);
                if (user != null)
                {
                    user.PasswordHashed = passwordHash;
                    user.SecurityStamp = passwordSalt;
                    user.IsFirstLogin = true;
                    user.CreatedUser = userId;
                    user.TransactionDate = DateTime.Today.Date;
                    _basedContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task DeleteUserAsync(string id)
        {
            try
            {
                var user = _basedContext.User.Find(id);

                user.UserStatus = false; 
                await _basedContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task EditUserAsync(vmUserDetails vm)
        {
            try
            {
                var user = _basedContext.User.Find(vm.EmailAddress);
              
                user.FirstName = vm.FirstName;
                user.LastName = vm.LastName;
           
                await _basedContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<vmUserDetails>> GetUsersAsync()
        {
            try
            {
                var lists = await _basedContext.User.Select(p => new vmUserDetails()
                {
                    EmailAddress = p.EmailAddress,
                    LastName = p.LastName,  
                    FirstName = p.FirstName,
                    IsFirstLogin = p.IsFirstLogin,
                    UserStatus = p.UserStatus,
                    CreatedUser = p.CreatedUser,
                    TransactionDate = p.TransactionDate
                }).ToListAsync();

                return lists;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<String> GetLogin(vmLogin vm)
        {
            try
            {
                var user = await _basedContext.User.FindAsync(vm.UserId);


                if (user != null)
                {
                    if (user.IsFirstLogin == false)
                    {
                        bool done = NewPassword(userId:vm.UserId, newPassword: vm.Password);
                        if (done == true)
                        {
                            return user.EmailAddress;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    if (user.UserStatus == true)
                    {
                        var h = new HMACSHA512(user.SecurityStamp);
                        var computeHash = h.ComputeHash(Encoding.UTF8.GetBytes(vm.Password));


                        if (computeHash.SequenceEqual(user.PasswordHashed))
                        {
                            return user.EmailAddress;
                        }
                        else
                        {
                            return "Password Not matched";
                        }

                    }
                    else
                    {
                        return "User Status is INACTIVE";
                    }
                }
                else
                {
                    return "User Name not found";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
