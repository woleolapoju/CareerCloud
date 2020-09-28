using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using static CareerCloud.gRPC.Protos.ApplicantJobApplication;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantJobApplicationService : ApplicantJobApplicationBase
    {

        public ApplicantJobApplicationLogic _logic { get; private set; }
        public ApplicantJobApplicationService()
        {
            _logic = new ApplicantJobApplicationLogic(new EFGenericRepository<
           ApplicantJobApplicationPoco>());
        }

        //get
        public override Task<ApplicantJobApplicationreply> GetApplicantJobApplication(IdRequestApplicantJobApplication request, ServerCallContext context)
        
        {
            ApplicantJobApplicationPoco poco = _logic.Get(Guid.Parse(request.Id));

            return Task.FromResult<ApplicantJobApplicationreply>(FromPoco(poco));
        }

        //getAll
        public override Task<ApplicantJobApplications> GetApplicantJobApplications(Empty request, ServerCallContext context)
       {
            ApplicantJobApplications collectionOfApplicantJobApplication = new ApplicantJobApplications();
            List<ApplicantJobApplicationPoco> pocos = _logic.GetAll();
            foreach (ApplicantJobApplicationPoco poco in pocos)
            {
                collectionOfApplicantJobApplication.AppJob.Add(FromPoco(poco));
            }
            return Task.FromResult<ApplicantJobApplications>(collectionOfApplicantJobApplication);

        }

        //create
        public override Task<Empty> AddApplicantJobApplication(ApplicantJobApplications request, ServerCallContext context)
        {
            List<ApplicantJobApplicationPoco> pocos = new List<ApplicantJobApplicationPoco>();
            foreach (ApplicantJobApplicationreply reply in request.AppJob)
            {
                pocos.Add(ToPoco(reply));
            }

            _logic.Add(pocos.ToArray());

            return Task.FromResult(new Empty()) ;
        }

        //update
        public override Task<Empty> UpdateApplicantJobApplication(ApplicantJobApplications request, ServerCallContext context)
            {
            List<ApplicantJobApplicationPoco> pocos = new List<ApplicantJobApplicationPoco>();
            foreach (ApplicantJobApplicationreply reply in request.AppJob)
            {
                pocos.Add(ToPoco(reply));
            }

            _logic.Update(pocos.ToArray());

            return Task.FromResult(new Empty()) ;
        }

        //Delete
        public override Task<Empty> DeleteApplicantJobApplication(ApplicantJobApplications request, ServerCallContext context)
           {
            List<ApplicantJobApplicationPoco> pocos = new List<ApplicantJobApplicationPoco>();
            foreach (ApplicantJobApplicationreply reply in request.AppJob)
            {
                pocos.Add(ToPoco(reply));
            }

            _logic.Delete(pocos.ToArray());

            return Task.FromResult(new Empty()) ;
        }

        private ApplicantJobApplicationreply FromPoco(ApplicantJobApplicationPoco poco)
        {
            return new ApplicantJobApplicationreply()
            {
                Id = poco.Id.ToString(),
                Applicant = poco.Applicant.ToString(),
                Job = poco.Job.ToString(),
                ApplicationDate = poco.ApplicationDate == null ? null : Timestamp.FromDateTime((DateTime)poco.ApplicationDate),
            };
        }

        private ApplicantJobApplicationPoco ToPoco(ApplicantJobApplicationreply reply)
        {
            return new ApplicantJobApplicationPoco()
            {
                Id = Guid.Parse(reply.Id.ToString()),
                Applicant = Guid.Parse(reply.Applicant),
                Job = Guid.Parse(reply.Job),
                ApplicationDate = reply.ApplicationDate.ToDateTime()
            };
        }

    }
}
