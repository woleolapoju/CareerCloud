using System;
using System.Collections.Generic;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class SecurityRoleRepository : IDataRepository<SecurityRolePoco>
    {
        public void Add(params SecurityRolePoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            conn.Open();
            foreach (SecurityRolePoco poco in items)
            {
                cmd.CommandText = @"INSERT INTO [dbo].[Security_Roles]
                                       ([Id]
                                       ,[Role]
                                       ,[Is_Inactive])
                                 VALUES
                                       (@Id
                                       ,@Role
                                       ,@Is_Inactive)";

                cmd.Parameters.AddWithValue("@Id", poco.Id);
                cmd.Parameters.AddWithValue("@Role", poco.Role);
                cmd.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);
                cmd.ExecuteNonQuery();

            }
            conn.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityRolePoco> GetAll(params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Security_Roles";
            conn.Open();
            cmd.Connection = conn;
            SqlDataReader rdr = cmd.ExecuteReader();
            SecurityRolePoco[] pocos = new SecurityRolePoco[1000];
            int x = 0;
            while (rdr.Read())
            {
                SecurityRolePoco poco = new SecurityRolePoco();
                poco.Id = rdr.GetGuid(0);
                poco.Role = rdr.GetString(1);
                poco.IsInactive = rdr.GetBoolean(2);

                 pocos[x] = poco; x++;

            }

            conn.Close();
            return pocos.Where(p => p != null).ToList();
        }

        public IList<SecurityRolePoco> GetList(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityRolePoco GetSingle(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityRolePoco> poco = GetAll().AsQueryable();
            poco = poco.Where(p => p != null);
            return poco.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityRolePoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            conn.Open();
            foreach (SecurityRolePoco poco in items)

            {
                cmd.CommandText = @"DELETE FROM [dbo].[Security_Roles] WHERE Id=@Id";
                cmd.Parameters.AddWithValue("@Id", poco.Id);

                cmd.ExecuteNonQuery();

            }
            conn.Close();
        }

        public void Update(params SecurityRolePoco[] items)
        {
            SqlConnection conn = new SqlConnection(BaseAdo.connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            conn.Open();
            foreach (SecurityRolePoco poco in items)
            {
                cmd.CommandText = @"UPDATE [dbo].[Security_Roles]
                                   SET  [Role] = @Role
                                      ,[Is_Inactive] = @Is_Inactive
                                 WHERE [Id] = @Id";

                cmd.Parameters.AddWithValue("@Id", poco.Id);
                cmd.Parameters.AddWithValue("@Role", poco.Role);
                cmd.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);
                cmd.ExecuteNonQuery();

            }
            conn.Close();
        }
    }
}
