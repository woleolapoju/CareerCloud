using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CareerCloud.BusinessLogicLayer
{
   public class CompanyProfileLogic : BaseLogic<CompanyProfilePoco>
	{
		public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository) : base(repository)
		{

		}
		protected override void Verify(CompanyProfilePoco[] pocos)
		{
			List<ValidationException> exceptions = new List<ValidationException>();
			
			string myTelPattern = @"^[2-9]\d{2}-\d{3}-\d{4}$";
			Regex rg = new Regex(myTelPattern);

			
			foreach (CompanyProfilePoco poco in pocos)
			{

				if (string.IsNullOrEmpty(poco.CompanyWebsite))
					exceptions.Add(new ValidationException(600, "Valid websites must end with the following extensions – .ca, .com, .biz"));
				else if(! (poco.CompanyWebsite == "*.ca" || poco.CompanyWebsite == "*.com" || poco.CompanyWebsite == "*.biz"))
				{
					exceptions.Add(new ValidationException(600, "Valid websites must end with the following extensions – .ca, .com, .biz"));
				}

				if (string.IsNullOrEmpty(poco.ContactPhone))
					exceptions.Add(new ValidationException(601, "ContactPhone must correspond to a valid phone number (e.g. 416-555-1234)"));
				else if (! rg.IsMatch(poco.ContactPhone))
				{
					exceptions.Add(new ValidationException(601, "ContactPhone must correspond to a valid phone number (e.g. 416-555-1234)"));
				}
			}
			if (exceptions.Count > 0)
			{
				throw new AggregateException(exceptions);
			}

		}
		public override void Add(CompanyProfilePoco[] pocos)
		{

			Verify(pocos);
			base.Add(pocos);
		}

		public override void Update(CompanyProfilePoco[] pocos)
		{
			Verify(pocos);
			base.Update(pocos);
		}
	}
}
