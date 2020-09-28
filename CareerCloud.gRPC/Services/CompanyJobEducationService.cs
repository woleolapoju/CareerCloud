using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using static CareerCloud.gRPC.Protos.CompanyJobEducation;


namespace CareerCloud.gRPC.Services
{
    public class CompanyJobEducationService: CompanyJobEducationBase
    {
        public CompanyJobEducationLogic _logic { get; private set; }
        public CompanyJobEducationService()
        {
            _logic = new CompanyJobEducationLogic(new EFGenericRepository<
           CompanyJobEducationPoco>());
        }

        //get
        public override Task<CompanyJobEducationreply> GetCompanyJobEducation(IdRequestCompanyJobEducation request, ServerCallContext context)
        {
            CompanyJobEducationPoco poco = _logic.Get(Guid.Parse(request.Id));

            return Task.FromResult<CompanyJobEducationreply>(FromPoco(poco));
        }

        //getAll
        public override Task<CompanyJobEducations> GetCompanyJobEducations(Empty request, ServerCallContext context)
        {
            CompanyJobEducations collectionOfCompanyJobEducation = new CompanyJobEducations();
            List<CompanyJobEducationPoco> pocos = _logic.GetAll();
            foreach (CompanyJobEducationPoco poco in pocos)
            {
                collectionOfCompanyJobEducation.AppCoyJob.Add(FromPoco(poco));
            }
            return Task.FromResult<CompanyJobEducations>(collectionOfCompanyJobEducation);

        }

        //create
        public override Task<Empty> AddCompanyJobEducation(CompanyJobEducations request, ServerCallContext context)
           {
            List<CompanyJobEducationPoco> pocos = new List<CompanyJobEducationPoco>();
            foreach (CompanyJobEducationreply reply in request.AppCoyJob)
            {
                pocos.Add(ToPoco(reply));
            }

            _logic.Add(pocos.ToArray());

            return Task.FromResult(new Empty()) ;
        }

        //update
        public override Task<Empty> UpdateCompanyJobEducation(CompanyJobEducations request, ServerCallContext context)
        {
            List<CompanyJobEducationPoco> pocos = new List<CompanyJobEducationPoco>();
            foreach (CompanyJobEducationreply reply in request.AppCoyJob)
            {
                pocos.Add(ToPoco(reply));
            }

            _logic.Update(pocos.ToArray());

            return Task.FromResult(new Empty()) ;
        }

        //Delete
        public override Task<Empty> DeleteCompanyJobEducation(CompanyJobEducations request, ServerCallContext context)
         {
            List<CompanyJobEducationPoco> pocos = new List<CompanyJobEducationPoco>();
            foreach (CompanyJobEducationreply reply in request.AppCoyJob)
            {
                pocos.Add(ToPoco(reply));
            }

            _logic.Delete(pocos.ToArray());

            return Task.FromResult(new Empty()) ;
        }

        private CompanyJobEducationreply FromPoco(CompanyJobEducationPoco poco)
        {
            return new CompanyJobEducationreply()
            {
                Id = poco.Id.ToString(),
                Job = poco.Job.ToString(),
                Major = poco.Major,
                Importance = poco.Importance
             };
        }

        private CompanyJobEducationPoco ToPoco(CompanyJobEducationreply reply)
        {
            return new CompanyJobEducationPoco()
            {
                Id = Guid.Parse(reply.Id),
                Job = Guid.Parse(reply.Job),
                Major = reply.Major,
                Importance = (short)reply.Importance
            };
        }


    }
}
