using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using static CareerCloud.gRPC.Protos.SecurityLogin;

namespace CareerCloud.gRPC.Services
{
    public class SecurityLoginService: SecurityLoginBase
    {

        public SecurityLoginLogic _logic { get; private set; }
        public SecurityLoginService()
        {
            _logic = new SecurityLoginLogic(new EFGenericRepository<
           SecurityLoginPoco>());
        }

        //get
        public override Task<SecurityLoginreply> GetSecurityLogin(IdRequestSecurityLogin request, ServerCallContext context)
         {
            SecurityLoginPoco poco = _logic.Get(Guid.Parse(request.Id));

            return Task.FromResult<SecurityLoginreply>(FromPoco(poco));
        }

        //getAll
        public override Task<SecurityLogins> GetSecurityLogins(Empty request, ServerCallContext context)
        {
            SecurityLogins collectionOfSecurityLogin = new SecurityLogins();
            List<SecurityLoginPoco> pocos = _logic.GetAll();
            foreach (SecurityLoginPoco poco in pocos)
            {
                collectionOfSecurityLogin.AppSecLogin.Add(FromPoco(poco));
            }
            return Task.FromResult<SecurityLogins>(collectionOfSecurityLogin);

        }




        //create
        public override Task<Empty> AddSecurityLogin(SecurityLogins request, ServerCallContext context)
          {
            List<SecurityLoginPoco> pocos = new List<SecurityLoginPoco>();
            foreach (SecurityLoginreply reply in request.AppSecLogin)
            {
                pocos.Add(ToPoco(reply));
            }

            _logic.Add(pocos.ToArray());

            return Task.FromResult(new Empty()) ;
        }

        //update
        public override Task<Empty> UpdateSecurityLogin(SecurityLogins request, ServerCallContext context)
     {
            List<SecurityLoginPoco> pocos = new List<SecurityLoginPoco>();
            foreach (SecurityLoginreply reply in request.AppSecLogin)
            {
                pocos.Add(ToPoco(reply));
            }

            _logic.Update(pocos.ToArray());

            return Task.FromResult(new Empty()) ;
        }

        //Delete
        public override Task<Empty> DeleteSecurityLogin(SecurityLogins request, ServerCallContext context)
         {
            List<SecurityLoginPoco> pocos = new List<SecurityLoginPoco>();
            foreach (SecurityLoginreply reply in request.AppSecLogin)
            {
                pocos.Add(ToPoco(reply));
            }

            _logic.Delete(pocos.ToArray());

            return Task.FromResult(new Empty()) ;
        }

        private SecurityLoginreply FromPoco(SecurityLoginPoco poco)
        {
            return new SecurityLoginreply()
            {
                Id = poco.Id.ToString(),
                Login = poco.Login.ToString(),
                Password = poco.Password,
                Created = poco.Created == null ? null : Timestamp.FromDateTime((DateTime)poco.Created),
                PasswordUpdate = poco.PasswordUpdate == null ? null : Timestamp.FromDateTime((DateTime)poco.PasswordUpdate),
                AgreementAccepted = poco.AgreementAccepted == null ? null : Timestamp.FromDateTime((DateTime)poco.AgreementAccepted),
                IsLocked=poco.IsLocked,
                IsInactive = poco.IsInactive,
                EmailAddress=poco.EmailAddress,
                PhoneNumber = poco.PhoneNumber,
                FullName = poco.FullName,
                ForceChangePassword=poco.ForceChangePassword,
                PrefferredLanguage=poco.PrefferredLanguage
        };
        }

        private SecurityLoginPoco ToPoco(SecurityLoginreply reply)
        {
            return new SecurityLoginPoco()
            {
                Id = Guid.Parse(reply.Id),
                Login = reply.Login,
                Password = reply.Password,
                Created = reply.Created.ToDateTime(),
                PasswordUpdate = reply.PasswordUpdate.ToDateTime(),
                AgreementAccepted = reply.AgreementAccepted.ToDateTime(),
                IsLocked = reply.IsLocked,
                IsInactive = reply.IsInactive,
                EmailAddress = reply.EmailAddress,
                PhoneNumber = reply.PhoneNumber,
                FullName = reply.FullName,
                ForceChangePassword = reply.ForceChangePassword,
                PrefferredLanguage = reply.PrefferredLanguage

            };
        }


    }
}
