using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using static CareerCloud.gRPC.Protos.SecurityLoginsLog;

namespace CareerCloud.gRPC.Services
{
    public class SecurityLoginsLogService: SecurityLoginsLogBase
    {
        public SecurityLoginsLogLogic _logic { get; private set; }
        public SecurityLoginsLogService()
        {
            _logic = new SecurityLoginsLogLogic(new EFGenericRepository<
           SecurityLoginsLogPoco>());
        }

        //get
        public override Task<SecurityLoginsLogreply> GetSecurityLoginsLog(IdRequestSecurityLoginsLog request, ServerCallContext context)
        {
            SecurityLoginsLogPoco poco = _logic.Get(Guid.Parse(request.Id));

            return Task.FromResult<SecurityLoginsLogreply>(FromPoco(poco));
        }

        //getAll
        public override Task<SecurityLoginsLogs> GetSecurityLoginsLogs(Empty request, ServerCallContext context)
        {
            SecurityLoginsLogs collectionOfSecurityLoginsLog = new SecurityLoginsLogs();
            List<SecurityLoginsLogPoco> pocos = _logic.GetAll();
            foreach (SecurityLoginsLogPoco poco in pocos)
            {
                collectionOfSecurityLoginsLog.AppSecLoginLog.Add(FromPoco(poco));
            }
            return Task.FromResult<SecurityLoginsLogs>(collectionOfSecurityLoginsLog);

        }

        //create
        public override Task<Empty> AddSecurityLoginsLog(SecurityLoginsLogs request, ServerCallContext context)
        {
            List<SecurityLoginsLogPoco> pocos = new List<SecurityLoginsLogPoco>();
            foreach (SecurityLoginsLogreply reply in request.AppSecLoginLog)
            {
                pocos.Add(ToPoco(reply));
            }

            _logic.Add(pocos.ToArray());

            return Task.FromResult(new Empty()) ;
        }

        //update
        public override Task<Empty> UpdateSecurityLoginsLog(SecurityLoginsLogs request, ServerCallContext context)
        {
            List<SecurityLoginsLogPoco> pocos = new List<SecurityLoginsLogPoco>();
            foreach (SecurityLoginsLogreply reply in request.AppSecLoginLog)
            {
                pocos.Add(ToPoco(reply));
            }

            _logic.Update(pocos.ToArray());

            return Task.FromResult(new Empty()) ;
        }

        //Delete
        public override Task<Empty> DeleteSecurityLoginsLog(SecurityLoginsLogs request, ServerCallContext context)
        {
            List<SecurityLoginsLogPoco> pocos = new List<SecurityLoginsLogPoco>();
            foreach (SecurityLoginsLogreply reply in request.AppSecLoginLog)
            {
                pocos.Add(ToPoco(reply));
            }

            _logic.Delete(pocos.ToArray());

            return Task.FromResult(new Empty()) ;
        }

        private SecurityLoginsLogreply FromPoco(SecurityLoginsLogPoco poco)
        {
            return new SecurityLoginsLogreply()
            {
                Id = poco.Id.ToString(),
                Login = poco.Login.ToString(),
                SourceIP = poco.SourceIP,
                LogonDate = poco.LogonDate == null ? null : Timestamp.FromDateTime((DateTime)poco.LogonDate),
                IsSuccesful=poco.IsSuccesful
            };
        }

        private SecurityLoginsLogPoco ToPoco(SecurityLoginsLogreply reply)
        {
            return new SecurityLoginsLogPoco()
            {
                Id = Guid.Parse(reply.Id),
                  Login = Guid.Parse(reply.Login),
                SourceIP = reply.SourceIP,
                LogonDate = reply.LogonDate.ToDateTime(),
                IsSuccesful = reply.IsSuccesful

            };
        }

    }
}
