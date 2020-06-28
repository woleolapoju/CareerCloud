using System;
using System.Collections.Generic;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyProfileRepository : IDataRepository<CompanyProfilePoco>
    {
        public void Add(params CompanyProfilePoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            conn.Open();
            foreach (CompanyProfilePoco poco in items)
            {
                cmd.CommandText = @"INSERT INTO [dbo].[Company_Profiles]
                                       ([Id]
                                       ,[Registration_Date]
                                       ,[Company_Website]
                                       ,[Contact_Phone]
                                       ,[Contact_Name]
                                       ,[Company_Logo])
                                 VALUES
                                       (@Id
                                       ,@Registration_Date
                                       ,@Company_Website
                                       ,@Contact_Phone
                                       ,@Contact_Name
                                       ,@Company_Logo)";

                cmd.Parameters.AddWithValue("@Id", poco.Id);
                cmd.Parameters.AddWithValue("@Registration_Date", poco.RegistrationDate);
                cmd.Parameters.AddWithValue("@Company_Website", poco.CompanyWebsite);
                cmd.Parameters.AddWithValue("@Contact_Phone", poco.ContactPhone);
                cmd.Parameters.AddWithValue("@Contact_Name", poco.ContactName);
                cmd.Parameters.AddWithValue("@Company_Logo", poco.CompanyLogo);
                cmd.ExecuteNonQuery();

            }
            conn.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Company_Profiles";
            conn.Open();
            cmd.Connection = conn;
            SqlDataReader rdr = cmd.ExecuteReader();
            CompanyProfilePoco[] pocos = new CompanyProfilePoco[1000];
            int x = 0;
            while (rdr.Read())
            {
                CompanyProfilePoco poco = new CompanyProfilePoco();
                poco.Id = rdr.GetGuid(0);
                poco.RegistrationDate = rdr.GetDateTime(1);
                poco.CompanyWebsite = rdr.IsDBNull(2)? null: (string) rdr.GetString(2);
                poco.ContactPhone = rdr.GetString(3);
                poco.ContactName = rdr.IsDBNull(4) ? null : (string)rdr.GetString(4);
                poco.CompanyLogo = rdr.IsDBNull(5)? null: (byte[])rdr[5];
                poco.TimeStamp = (byte[])rdr[6];

                 pocos[x] = poco; x++;

            }

            conn.Close();
            return pocos.Where(p => p != null).ToList();
        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyProfilePoco> poco = GetAll().AsQueryable();
            poco = poco.Where(p => p != null);
            return poco.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            conn.Open();
            foreach (CompanyProfilePoco poco in items)

            {
                cmd.CommandText = @"DELETE FROM [dbo].[Company_Profiles] WHERE Id=@Id";
                cmd.Parameters.AddWithValue("@Id", poco.Id);
                cmd.ExecuteNonQuery();

            }
            conn.Close();
        }

        public void Update(params CompanyProfilePoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            conn.Open();
            foreach (CompanyProfilePoco poco in items)
            {
                cmd.CommandText = @"UPDATE [dbo].[Company_Profiles]
                                   SET  [Registration_Date] = @Registration_Date
                                      ,[Company_Website] = @Company_Website
                                      ,[Contact_Phone] = @Contact_Phone
                                      ,[Contact_Name] = @Contact_Name
                                      ,[Company_Logo] = @Company_Logo
                                 WHERE[Id] = @Id";

                cmd.Parameters.AddWithValue("@Id", poco.Id);
                cmd.Parameters.AddWithValue("@Registration_Date", poco.RegistrationDate);
                cmd.Parameters.AddWithValue("@Company_Website", poco.CompanyWebsite);
                cmd.Parameters.AddWithValue("@Contact_Phone", poco.ContactPhone);
                cmd.Parameters.AddWithValue("@Contact_Name", poco.ContactName);
                cmd.Parameters.AddWithValue("@Company_Logo", poco.CompanyLogo);
                cmd.ExecuteNonQuery();

            }
            conn.Close();
        }
    }
}
