using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using static CareerCloud.gRPC.Protos.ApplicantEducation;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantEducationService: ApplicantEducationBase
    {
        public ApplicantEducationLogic _logic { get; private set; }
        public ApplicantEducationService()
        {
            _logic = new ApplicantEducationLogic(new EFGenericRepository<
           ApplicantEducationPoco>());
        }

        //get
        public override Task<ApplicantEducationreply> GetApplicantEducation(IdRequest request, ServerCallContext context)
        {
            ApplicantEducationPoco poco=_logic.Get(Guid.Parse(request.Id));

           return Task.FromResult<ApplicantEducationreply>(FromPoco(poco));
        }

        //getAll
        public override Task<ApplicantEducations> GetApplicantEducations(Empty request, ServerCallContext context)
        {
            ApplicantEducations collectionOfApplicantEducation = new ApplicantEducations();
             List<ApplicantEducationPoco> pocos = _logic.GetAll();

          foreach (ApplicantEducationPoco poco in pocos)
            {
                collectionOfApplicantEducation.AppEdus.Add(FromPoco(poco));
            }
            return Task.FromResult<ApplicantEducations>(collectionOfApplicantEducation);

        }
        //create
        public override Task<Empty> AddApplicantEducations(ApplicantEducations request, ServerCallContext context)
        {
            List<ApplicantEducationPoco> pocos = new List<ApplicantEducationPoco>();
            foreach (ApplicantEducationreply reply in request.AppEdus)
            {
              pocos.Add(ToPoco(reply));
            }
          
            _logic.Add(pocos.ToArray());

         //   return Task.FromResult<Empty>(null);

            return Task.FromResult(new Empty()) ;
        }

        //update
        public override Task<Empty> UpdateApplicantEducations(ApplicantEducations request, ServerCallContext context)
        {
            List<ApplicantEducationPoco> pocos = new List<ApplicantEducationPoco>();
            foreach (ApplicantEducationreply reply in request.AppEdus)
            {
                pocos.Add(ToPoco(reply));
            }

            _logic.Update(pocos.ToArray());

            return Task.FromResult(new Empty()) ;
        }

        //Delete
        public override Task<Empty> DeleteApplicantEducations(ApplicantEducations request, ServerCallContext context)
        {
            List<ApplicantEducationPoco> pocos = new List<ApplicantEducationPoco>();
            foreach (ApplicantEducationreply reply in request.AppEdus)
            {
                pocos.Add(ToPoco(reply));
            }

            _logic.Delete(pocos.ToArray());

            return Task.FromResult(new Empty()) ;
        }
       
        private ApplicantEducationreply FromPoco(ApplicantEducationPoco poco)
        {
            return new ApplicantEducationreply()
            {
                Id = poco.Id.ToString(),
                Applicant = poco.Applicant.ToString(),
                CertificateDiploma = poco.CertificateDiploma,
                Major = poco.Major,
                StartDate = poco.StartDate == null ? null : Timestamp.FromDateTime((DateTime)poco.StartDate),
                CompletionDate = poco.CompletionDate == null ? null : Timestamp.FromDateTime((DateTime)poco.CompletionDate),
                CompletionPercent = poco.CompletionPercent == null ? 0 : (byte)poco.CompletionPercent

            };
        }

        private ApplicantEducationPoco ToPoco(ApplicantEducationreply reply)
        {
            return new ApplicantEducationPoco()
            {
                Id = Guid.Parse(reply.Id),
                Applicant = Guid.Parse(reply.Applicant),
                CertificateDiploma = reply.CertificateDiploma,
                Major = reply.Major,
                StartDate = reply.StartDate.ToDateTime(),
                CompletionDate = reply.CompletionDate.ToDateTime(),
                CompletionPercent = (byte?)reply.CompletionPercent,
              
            };
        }


    }
}
