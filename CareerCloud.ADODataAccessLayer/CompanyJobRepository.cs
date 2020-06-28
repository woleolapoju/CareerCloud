using System;
using System.Collections.Generic;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyJobRepository : IDataRepository<CompanyJobPoco>
    {
        public void Add(params CompanyJobPoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            conn.Open();
            foreach (CompanyJobPoco poco in items)
            {
                cmd.CommandText = @"INSERT INTO [dbo].[Company_Jobs]
                                       ([Id]
                                       ,[Company]
                                       ,[Profile_Created]
                                       ,[Is_Inactive]
                                       ,[Is_Company_Hidden])
                                 VALUES
                                       (@Id
                                       ,@Company
                                       ,@Profile_Created
                                       ,@Is_Inactive
                                       ,@Is_Company_Hidden)";

                cmd.Parameters.AddWithValue("@Id", poco.Id);
                cmd.Parameters.AddWithValue("@Company", poco.Company);
                cmd.Parameters.AddWithValue("@Profile_Created", poco.ProfileCreated);
                cmd.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);
                cmd.Parameters.AddWithValue("@Is_Company_Hidden", poco.IsCompanyHidden);
                cmd.ExecuteNonQuery();

            }
            conn.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyJobPoco> GetAll(params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Company_Jobs";
            conn.Open();
            cmd.Connection = conn;
            SqlDataReader rdr = cmd.ExecuteReader();
            CompanyJobPoco[] pocos = new CompanyJobPoco[1100];
            int x = 0;
             while (rdr.Read())
            {
                CompanyJobPoco poco = new CompanyJobPoco();
                poco.Id = rdr.GetGuid(0);
                poco.Company = rdr.GetGuid(1);
                poco.ProfileCreated = rdr.GetDateTime(2);
                poco.IsInactive = rdr.GetBoolean(3);
                poco.IsCompanyHidden = rdr.GetBoolean(4);
                poco.TimeStamp = (byte[])rdr[5];

                 pocos[x] = poco; x++;

            }

            conn.Close();
            return pocos.Where(p => p != null).ToList();
        }

        public IList<CompanyJobPoco> GetList(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobPoco GetSingle(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobPoco> poco = GetAll().AsQueryable();
            poco = poco.Where(p => p != null);
            return poco.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobPoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            conn.Open();
            foreach (CompanyJobPoco poco in items)

            {
                cmd.CommandText = @"DELETE FROM [dbo].[Company_Jobs] WHERE Id=@Id";
                cmd.Parameters.AddWithValue("@Id", poco.Id);

                cmd.ExecuteNonQuery();

            }
            conn.Close();
        }

        public void Update(params CompanyJobPoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            conn.Open();
            foreach (CompanyJobPoco poco in items)
            {
                cmd.CommandText = @"UPDATE [dbo].[Company_Jobs]
                                   SET [Company] = @Company
                                      ,[Profile_Created] = @Profile_Created
                                      ,[Is_Inactive] = @Is_Inactive
                                      ,[Is_Company_Hidden] = @Is_Company_Hidden
                                 WHERE [Id] = @Id";

                cmd.Parameters.AddWithValue("@Id", poco.Id);
                cmd.Parameters.AddWithValue("@Company", poco.Company);
                cmd.Parameters.AddWithValue("@Profile_Created", poco.ProfileCreated);
                cmd.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);
                cmd.Parameters.AddWithValue("@Is_Company_Hidden", poco.IsCompanyHidden);
                cmd.ExecuteNonQuery();

            }
            conn.Close();
        }
    }
}
