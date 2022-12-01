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
        private UserNameDetails userName;

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
                  CreatedUser = vm.CreatedUser,
                  DepartmentId = vm.DepartmentId,
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
        public async Task EditUserAsync(vmEditUser vm)
        {
            try
            {
                var user = _basedContext.User.Find(vm.EmailAddress);
              
                user.FirstName = vm.FirstName;
                user.LastName = vm.LastName;
                user.DepartmentId = vm.DepartmentId;
           
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
                var lists = await _basedContext.UserDepartment.Select(p => new vmUserDetails()
                {
                    EmailAddress = p.EmailAddress,
                    LastName = p.LastName,  
                    FirstName = p.FirstName,
                    IsFirstLogin = p.IsFirstLogin,
                    UserStatus = p.UserStatus,
                    CreatedUser = p.CreatedUser,
                    TransactionDate = p.TransactionDate, DepartmentDescription = p.DepartmentDescription,
                    UserFullName = p.UserFullName, DepartmentId = p.DepartmentId
                }).ToListAsync();

                return lists;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<UserNameDetails> GetLogin(vmLogin vm)
        {
            try
            {
                var user = await _basedContext.User.FindAsync(vm.UserId);

                var userNameDetails = new UserNameDetails();
                if (user != null)
                {
                    if (user.IsFirstLogin == false)
                    {
                        bool done = NewPassword(userId:vm.UserId, newPassword: vm.Password);
                        if (done == true)
                        {
                            userNameDetails.UserFullName = user.FirstName + " " + user.LastName;

                            return userNameDetails;
                        }
                    }
                    if (user.UserStatus == true)
                    {
                        var h = new HMACSHA512(user.SecurityStamp);
                        var computeHash = h.ComputeHash(Encoding.UTF8.GetBytes(vm.Password));


                        if (computeHash.SequenceEqual(user.PasswordHashed))
                        {
                            userNameDetails.UserFullName = user.FirstName + " " + user.LastName;

                            return userNameDetails;
                        }
                        else
                        {
                            userNameDetails.UserFullName = "Password Not matched";
                            return userNameDetails;
                        }
                    }
                    else
                    {
                        userNameDetails.UserFullName = "User Status is INACTIVE";
                        return userNameDetails;
                    }
                }
                else
                {
                    userNameDetails.UserFullName = "User Name not found";
                    return userNameDetails;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> LoginTransactions(vmLoginTransaction vm)
        {
            try
            {
                var umodule = new LoginTransactions
                {
                    UserId = vm.UserId,
                    TransactionType = vm.TransactionType,
                    TransactionDate = DateTime.Today.Date,
                    TransactionTime = DateTime.Now.ToLongTimeString(),
                    TransactionTime1 = DateTime.Now.ToLocalTime()
                };
                await _basedContext.LoginTransaction.AddAsync(umodule);
                await _basedContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }
        public async Task<vmLoginUserProfile> GetUserProfile(string id)
        {
            try
            {
                var userProfile = new vmLoginUserProfile();
                var userDepartment = _basedContext.UserDepartment.Where(u => u.EmailAddress == id).FirstOrDefault();
                if (userDepartment != null)
                {
                    userProfile.DepartmentId = userDepartment.DepartmentDescription;
                }
                else
                {
                    userProfile.DepartmentId = null;
                    //userProfile.DepartmentId = "Registration";
                }
                var userGroup = await _basedContext.UserBandList.FindAsync(id);
                if(userGroup != null)
                {
                    if (userGroup.BandId > 0)
                    {
                        userProfile.BandId = userGroup.BandId;
                        userProfile.BandName = userGroup.BandName;
                    }                    
                }
                else
                {
                    userProfile.BandId = 0;
                    userProfile.BandName = "";
                }
                
                userProfile.UserId = id;

                return userProfile;
            }
            catch (Exception ex)
            {
                throw ex;
            }       
        }
        public async Task<bool> CreateUserIPDetails(UserIPDetails vm)
        {
            try
            {
                var ipdetails = new UserIPDetails
                {
                    UserId = vm.UserId,
                    IPAddress = vm.IPAddress,
                    City = vm.City,
                    AccuracyRadius = vm.AccuracyRadius,
                    StateName = vm.StateName,
                    Continent = vm.Continent,
                    Country = vm.Country,
                    Latitude = vm.Latitude,
                    Longitude = vm.Longitude,
                    TimeZone = vm.TimeZone,
                    RegisteredCountry = vm.RegisteredCountry,
                    TransactionDate = vm.TransactionDate
                };
                await _basedContext.UserIPDetail.AddAsync(ipdetails);
                await _basedContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> EditUserPasswords(vmPasswordChange vm)
        {
            try
            {
                byte[] RegistryAccount = null;
                           
                var user = await _basedContext.User.FindAsync(vm.UserId); 

                if (user != null)
                {
                    if (user.UserStatus == true)
                    {
                        RegistryAccount = user.SecurityStamp;
                        if (RegistryAccount != null)
                        {
                            var h = new HMACSHA512(user.SecurityStamp);
                            var computeHash = h.ComputeHash(Encoding.UTF8.GetBytes(vm.FormerPassword));


                            if (computeHash.SequenceEqual(user.PasswordHashed))
                            {
                                CreateHashWithSalt(vm.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);

                                user.PasswordHashed = passwordHash;
                                user.SecurityStamp = passwordSalt;
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
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

            return false;
        }
    }
}
