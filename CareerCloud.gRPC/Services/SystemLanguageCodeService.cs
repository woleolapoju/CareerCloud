using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using static CareerCloud.gRPC.Protos.SystemLanguageCode;

namespace CareerCloud.gRPC.Services
{
    public class SystemLanguageCodeService: SystemLanguageCodeBase
    {
        public SystemLanguageCodeLogic _logic { get; private set; }
        public SystemLanguageCodeService()
        {
            _logic = new SystemLanguageCodeLogic(new EFGenericRepository<
           SystemLanguageCodePoco>());
        }

        //get
        public override Task<SystemLanguageCodereply> GetSystemLanguageCode(LanguageIDRequest request, ServerCallContext context)
         {
            SystemLanguageCodePoco poco = _logic.Get(request.LanguageID);

            return Task.FromResult<SystemLanguageCodereply>(FromPoco(poco));
        }

        //getAll
        public override Task<SystemLanguageCodes> GetSystemLanguageCodes(Empty request, ServerCallContext context)
        {
            SystemLanguageCodes collectionOfSystemLanguageCode = new SystemLanguageCodes();
            List<SystemLanguageCodePoco> pocos = _logic.GetAll();
            foreach (SystemLanguageCodePoco poco in pocos)
            {
                collectionOfSystemLanguageCode.AppSysLang.Add(FromPoco(poco));
            }
            return Task.FromResult<SystemLanguageCodes>(collectionOfSystemLanguageCode);

        }

        //create
        public override Task<Empty> AddSystemLanguageCode(SystemLanguageCodes request, ServerCallContext context)
        {
            List<SystemLanguageCodePoco> pocos = new List<SystemLanguageCodePoco>();
            foreach (SystemLanguageCodereply reply in request.AppSysLang)
            {
                pocos.Add(ToPoco(reply));
            }

            _logic.Add(pocos.ToArray());

            return Task.FromResult(new Empty()) ;
        }

        //update
        public override Task<Empty> UpdateSystemLanguageCode(SystemLanguageCodes request, ServerCallContext context)
       {
            List<SystemLanguageCodePoco> pocos = new List<SystemLanguageCodePoco>();
            foreach (SystemLanguageCodereply reply in request.AppSysLang)
            {
                pocos.Add(ToPoco(reply));
            }

            _logic.Update(pocos.ToArray());

            return Task.FromResult(new Empty()) ;
        }

        //Delete
        public override Task<Empty> DeleteSystemLanguageCode(SystemLanguageCodes request, ServerCallContext context)
         {
            List<SystemLanguageCodePoco> pocos = new List<SystemLanguageCodePoco>();
            foreach (SystemLanguageCodereply reply in request.AppSysLang)
            {
                pocos.Add(ToPoco(reply));
            }

            _logic.Delete(pocos.ToArray());

            return Task.FromResult(new Empty()) ;
        }

        private SystemLanguageCodereply FromPoco(SystemLanguageCodePoco poco)
        {
            return new SystemLanguageCodereply()
            {
                LanguageID = poco.LanguageID,
                Name = poco.Name,
                NativeName = poco.NativeName
        };
        }

        private SystemLanguageCodePoco ToPoco(SystemLanguageCodereply reply)
        {
            return new SystemLanguageCodePoco()
            {
                LanguageID = reply.LanguageID,
                Name = reply.Name,
                NativeName = reply.NativeName
            };
        }

    }
}
