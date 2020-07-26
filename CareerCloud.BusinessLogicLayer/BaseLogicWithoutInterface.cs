using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
	public abstract  class BaseLogicWithoutInterface<TPoco> // where TPoco : IPoco
	{
		protected IDataRepository<TPoco> _repository;
		public string Code { get; set; }
		public BaseLogicWithoutInterface(IDataRepository<TPoco> repository)
		{
			_repository = repository;
		}

		 protected virtual void Verify(TPoco[] pocos)
		{
			return;
		}
        public virtual TPoco Get(String code)
        {
			return _repository.GetSingle(c => Code == code);
        }

        public virtual List<TPoco> GetAll()
		{
			return _repository.GetAll().ToList();
		}

        public virtual void Add(TPoco[] pocos)
        {
            //foreach (TPoco poco in pocos)
            //{
            //    if (poco.Id == Guid.Empty)
            //    {
            //        poco.Id = Guid.NewGuid();
            //    }
            //}

            _repository.Add(pocos);
        }

        public virtual void Update(TPoco[] pocos)
		{
			_repository.Update(pocos);
		}

		public void Delete(TPoco[] pocos)
		{
			_repository.Remove(pocos);
		}
	}
}
