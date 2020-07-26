using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
   public class SystemCountryCodeLogic // : BaseLogicWithoutInterface<SystemCountryCodePoco>
	{
		IDataRepository<SystemCountryCodePoco> _repository;
		public SystemCountryCodeLogic(IDataRepository<SystemCountryCodePoco> repository) // : base(repository)
		{
			_repository = repository;
		}
		protected  void Verify(SystemCountryCodePoco[] pocos)
		{
			List<ValidationException> exceptions = new List<ValidationException>();
			foreach (SystemCountryCodePoco poco in pocos)
			{

				if (String.IsNullOrEmpty(poco.Code))
				{
					exceptions.Add(new ValidationException(900, "Code cannot be empty"));
				}

				if (String.IsNullOrEmpty(poco.Name))
				{
					exceptions.Add(new ValidationException(901, "Name cannot be empty"));
				}
			}
			if (exceptions.Count > 0)
			{
				throw new AggregateException(exceptions);
			}

		}


		public  void Add(SystemCountryCodePoco[] pocos)
		{
			Verify(pocos);
			//base.Add(pocos);
			_repository.Add(pocos);
		}

		public  void Update(SystemCountryCodePoco[] pocos)
		{
			Verify(pocos);
			//base.Update(pocos);
			_repository.Update(pocos);
		}
		public void Delete(SystemCountryCodePoco[] pocos)
		{
			_repository.Remove(pocos);
		}


		public  List<SystemCountryCodePoco> GetAll()
		{
			return _repository.GetAll().ToList();
		}

		public SystemCountryCodePoco Get(String code)
		{
			return _repository.GetSingle(c => c.Code == code);
		}

	}
}
