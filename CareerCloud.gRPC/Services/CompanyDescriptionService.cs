using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using static CareerCloud.gRPC.Protos.CompanyDescription;

namespace CareerCloud.gRPC.Services
{
    public class CompanyDescriptionService : CompanyDescriptionBase
    {
        public CompanyDescriptionLogic _logic { get; private set; }
        public CompanyDescriptionService()
        {
            _logic = new CompanyDescriptionLogic(new EFGenericRepository<
           CompanyDescriptionPoco>());
        }

        //get
        public override Task<CompanyDescriptionreply> GetCompanyDescription(IdRequestCompanyDescription request, ServerCallContext context)
        {
            CompanyDescriptionPoco poco = _logic.Get(Guid.Parse(request.Id));

            return Task.FromResult<CompanyDescriptionreply>(FromPoco(poco));
        }

        //getAll
        public override Task<CompanyDescriptions> GetCompanyDescriptions(Empty request, ServerCallContext context)
        {
            CompanyDescriptions collectionOfCompanyDescription = new CompanyDescriptions();
            List<CompanyDescriptionPoco> pocos = _logic.GetAll();
            foreach (CompanyDescriptionPoco poco in pocos)
            {
                collectionOfCompanyDescription.AppCoyDesc.Add(FromPoco(poco));
            }
            return Task.FromResult<CompanyDescriptions>(collectionOfCompanyDescription);

        }

        //create
        public override Task<Empty> AddCompanyDescription(CompanyDescriptions request, ServerCallContext context)
        {
            List<CompanyDescriptionPoco> pocos = new List<CompanyDescriptionPoco>();
            foreach (CompanyDescriptionreply reply in request.AppCoyDesc)
            {
                pocos.Add(ToPoco(reply));
            }

            _logic.Add(pocos.ToArray());

            return Task.FromResult(new Empty()) ;
        }

        //update
        public override Task<Empty> UpdateCompanyDescription(CompanyDescriptions request, ServerCallContext context)
        {
            List<CompanyDescriptionPoco> pocos = new List<CompanyDescriptionPoco>();
            foreach (CompanyDescriptionreply reply in request.AppCoyDesc)
            {
                pocos.Add(ToPoco(reply));
            }

            _logic.Update(pocos.ToArray());

            return Task.FromResult(new Empty()) ;
        }

        //Delete
        public override Task<Empty> DeleteCompanyDescription(CompanyDescriptions request, ServerCallContext context)
        {
            List<CompanyDescriptionPoco> pocos = new List<CompanyDescriptionPoco>();
            foreach (CompanyDescriptionreply reply in request.AppCoyDesc)
            {
                pocos.Add(ToPoco(reply));
            }

            _logic.Delete(pocos.ToArray());

            return Task.FromResult(new Empty()) ;
        }

        private CompanyDescriptionreply FromPoco(CompanyDescriptionPoco poco)
        {
            return new CompanyDescriptionreply()
            {
                Id = poco.Id.ToString(),
                Company = poco.Company.ToString(),
                LanguageId = poco.LanguageId,
                CompanyName = poco.CompanyName,
                CompanyDescription = poco.CompanyDescription,

        };
        }

        private CompanyDescriptionPoco ToPoco(CompanyDescriptionreply reply)
        {
            return new CompanyDescriptionPoco()
            {
                Id =Guid.Parse(reply.Id),
                Company = Guid.Parse(reply.Company),
                LanguageId = reply.LanguageId,
                CompanyName = reply.CompanyName,
                CompanyDescription = reply.CompanyDescription,
            };
        }
    }
}
