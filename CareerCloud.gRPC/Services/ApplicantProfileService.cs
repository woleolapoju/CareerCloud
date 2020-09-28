using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using static CareerCloud.gRPC.Protos.ApplicantProfile;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantProfileService : ApplicantProfileBase
    {
        public ApplicantProfileLogic _logic { get; private set; }
        public ApplicantProfileService()
        {
            _logic = new ApplicantProfileLogic(new EFGenericRepository<
           ApplicantProfilePoco>());
        }

        //get
        public override Task<ApplicantProfilereply> GetApplicantProfile(IdRequestApplicantProfile request, ServerCallContext context)
        {
            ApplicantProfilePoco poco = _logic.Get(Guid.Parse(request.Id));

            return Task.FromResult<ApplicantProfilereply>(FromPoco(poco));
        }

        //getAll
        public override Task<ApplicantProfiles> GetApplicantProfiles(Empty request, ServerCallContext context)
        {
            ApplicantProfiles collectionOfApplicantProfile = new ApplicantProfiles();
            List<ApplicantProfilePoco> pocos = _logic.GetAll();
            foreach (ApplicantProfilePoco poco in pocos)
            {
                collectionOfApplicantProfile.AppPro.Add(FromPoco(poco));
            }
            return Task.FromResult<ApplicantProfiles>(collectionOfApplicantProfile);

        }

        //create
        public override Task<Empty> AddApplicantProfile(ApplicantProfiles request, ServerCallContext context)
       {
            List<ApplicantProfilePoco> pocos = new List<ApplicantProfilePoco>();
            foreach (ApplicantProfilereply reply in request.AppPro)
            {
                pocos.Add(ToPoco(reply));
            }

            _logic.Add(pocos.ToArray());

            return Task.FromResult(new Empty()) ;
        }

        //update
        public override Task<Empty> UpdateApplicantProfile(ApplicantProfiles request, ServerCallContext context)
         {
            List<ApplicantProfilePoco> pocos = new List<ApplicantProfilePoco>();
            foreach (ApplicantProfilereply reply in request.AppPro)
            {
                pocos.Add(ToPoco(reply));
            }

            _logic.Update(pocos.ToArray());

            return Task.FromResult(new Empty()) ;
        }

        //Delete
        public override Task<Empty> DeleteApplicantProfile(ApplicantProfiles request, ServerCallContext context)
        {
            List<ApplicantProfilePoco> pocos = new List<ApplicantProfilePoco>();
            foreach (ApplicantProfilereply reply in request.AppPro)
            {
                pocos.Add(ToPoco(reply));
            }

            _logic.Delete(pocos.ToArray());

            return Task.FromResult(new Empty()) ;
        }

        private ApplicantProfilereply FromPoco(ApplicantProfilePoco poco)
        {
            return new ApplicantProfilereply()
            {
                Id = poco.Id.ToString(),
                Login = poco.Login.ToString(),
                CurrentSalary=(double)poco.CurrentSalary,
                CurrentRate = (double)poco.CurrentRate,
                Currency= poco.Currency,
                Country = poco.Country,
                Province = poco.Province,
                Street = poco.Street,
                City = poco.City,
                PostalCode = poco.PostalCode,
        };
        }

        private ApplicantProfilePoco ToPoco(ApplicantProfilereply reply)
        {
            return new ApplicantProfilePoco()
            {
               Id = Guid.Parse(reply.Id),
                Login =Guid.Parse(reply.Login),
                CurrentSalary = (decimal)reply.CurrentSalary,
                CurrentRate = (decimal)reply.CurrentRate,
                Currency = reply.Currency,
                Country = reply.Country,
                Province = reply.Province,
                Street = reply.Street,
                City = reply.City,
                PostalCode = reply.PostalCode,
            };
        }
    }
}


