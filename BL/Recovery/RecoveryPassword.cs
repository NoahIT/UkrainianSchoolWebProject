using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Auth;
using BL.Email;
using BL.Exceptions;
using BL.Helpers;
using DAL.Implementations.CRUDRepos;
using DAL.Interfaces;
using DAL.Models;

namespace BL.Recovery
{
    public class RecoveryPassword : IRecovery
    {
        private readonly INotificationService sender;
        private readonly IRecoveryPasswordDAL password;
        private readonly IEncrypt encrypt;

        public RecoveryPassword(INotificationService sender, IRecoveryPasswordDAL password, IEncrypt encrypt)
        {
            this.sender = sender;
            this.password = password;
            this.encrypt = encrypt;
        }

        public async Task SendToEmailPassVerifLink(string email)
        {
           var user = await password.GetByEmail(email);

           if (user != null)
           {
               await password.DeleteAll(user.Id);

               await password.Create(new PasswordCodes()
               {
                   User = user,
                   VerificationCode = Guid.NewGuid()
               });

               var passCode = await password.Get(email);

               if (passCode!=null)
               {
                  await sender.SendEmailSupport(new MailMessageCustom("Востановление пароля", $"https://uniqum.school/password-recovery?code={passCode.VerificationCode}", email));
               }
           }
           else
           {
               await sender.SendEmailSupport(new MailMessageCustom("Кто то пытается востановить аккаунт", "Если это вы, просто создайте новый аккаунт", email));
           }
        }

        public async Task<string> GetEmailByCode(string code)
        {
           return await password.GetEmailByCode(HelpersM.StringToGuidDef(code)??Guid.Empty);
        }

        public async Task ChangePassword(string p1, string p2, string code)
        {
            VerifyPassword(p1, p2);

            Guid? guidCode = HelpersM.StringToGuidDef(code);


            var user = await password.GetUserByCode(guidCode);

            if (user != null)
            {
                user.AppUser.Password = encrypt.HashPassword(p1, user.AppUser.Salt);
                await password.UpdatePassword(user);
            }

            await password.Delete(guidCode ?? Guid.Empty);
            
        }


        private void VerifyPassword(string p1, string p2)
        {
            if (p1!=p2)
            {
                throw new PasswordRepeatException();
            }

            if (p1.Length < 5)
            {
                throw new EasyPasswordException();
            }
        }
    }
}
