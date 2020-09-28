using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using static CareerCloud.gRPC.Protos.CompanyJob;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobService: CompanyJobBase
    {
        public CompanyJobLogic _logic { get; private set; }
        public CompanyJobService()
        {
            _logic = new CompanyJobLogic(new EFGenericRepository<
           CompanyJobPoco>());
        }

        //get
        public override Task<CompanyJobreply> GetCompanyJob(IdRequestCompanyJob request, ServerCallContext context)
        {
            CompanyJobPoco poco = _logic.Get(Guid.Parse(request.Id));

            return Task.FromResult<CompanyJobreply>(FromPoco(poco));
        }

        //getAll
        public override Task<CompanyJobs> GetCompanyJobs(Empty request, ServerCallContext context)
        {
            CompanyJobs collectionOfCompanyJob = new CompanyJobs();
            List<CompanyJobPoco> pocos = _logic.GetAll();
            foreach (CompanyJobPoco poco in pocos)
            {
                collectionOfCompanyJob.AppCoyjob.Add(FromPoco(poco));
            }
            return Task.FromResult<CompanyJobs>(collectionOfCompanyJob);

        }

        //create
        public override Task<Empty> AddCompanyJob(CompanyJobs request, ServerCallContext context)
        {
            List<CompanyJobPoco> pocos = new List<CompanyJobPoco>();
            foreach (CompanyJobreply reply in request.AppCoyjob)
            {
                pocos.Add(ToPoco(reply));
            }

            _logic.Add(pocos.ToArray());

            return Task.FromResult(new Empty()) ;
        }

        //update
        public override Task<Empty> UpdateCompanyJob(CompanyJobs request, ServerCallContext context)
        {
            List<CompanyJobPoco> pocos = new List<CompanyJobPoco>();
            foreach (CompanyJobreply reply in request.AppCoyjob)
            {
                pocos.Add(ToPoco(reply));
            }

            _logic.Update(pocos.ToArray());

            return Task.FromResult(new Empty()) ;
        }

        //Delete
        public override Task<Empty> DeleteCompanyJob(CompanyJobs request, ServerCallContext context)
        {
            List<CompanyJobPoco> pocos = new List<CompanyJobPoco>();
            foreach (CompanyJobreply reply in request.AppCoyjob)
            {
                pocos.Add(ToPoco(reply));
            }

            _logic.Delete(pocos.ToArray());

            return Task.FromResult(new Empty()) ;
        }

        private CompanyJobreply FromPoco(CompanyJobPoco poco)
        {
            return new CompanyJobreply()
            {
                Id = poco.Id.ToString(),
                Company = poco.Company.ToString(),
                ProfileCreated = poco.ProfileCreated == null ? null : Timestamp.FromDateTime((DateTime)poco.ProfileCreated),
                IsInactive= poco.IsInactive,
                IsCompanyHidden=poco.IsCompanyHidden
            };
        }

        private CompanyJobPoco ToPoco(CompanyJobreply reply)
        {
            return new CompanyJobPoco()
            {
                Id = Guid.Parse(reply.Id),
                Company = Guid.Parse(reply.Company),
                ProfileCreated = reply.ProfileCreated.ToDateTime(),
                IsInactive = reply.IsInactive,
                IsCompanyHidden = reply.IsCompanyHidden
            };
        }
    }
}
