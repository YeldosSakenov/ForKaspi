using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Ado.Models.DB
{
   public class AspNetUserRolesDb
    {
        private readonly string _connect = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
        private SqlConnection _con;
        private SqlCommand _cmd;
        private SqlDataReader _sqlReader;
        private IdeaBankBaseEntities db;

        public async Task<List<Entitys.AspNetUserRoles>> Get(Entitys.AspNetUserRoles approved)
        {
            List<Entitys.AspNetUserRoles> list = new List<Entitys.AspNetUserRoles>();
            db = new IdeaBankBaseEntities();

            using (_con = new SqlConnection(_connect))
            {
                try
                {
                    _cmd = new SqlCommand("spGetListOfUserRoles", _con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlParameter param = new SqlParameter
                    {
                        ParameterName = "@UserId",
                        Value = approved.UserId
                    };
                    _cmd.Parameters.Add(param);                  

                    _con.Open();

                    _sqlReader = await _cmd.ExecuteReaderAsync();

                    if (_sqlReader.HasRows)
                    {
                        while (_sqlReader.Read())
                        {
                            Entitys.AspNetUserRoles roles = new Entitys.AspNetUserRoles
                            {
                                UserId = _sqlReader["UserId"].ToString(),
                                RoleId= _sqlReader["RoleId"].ToString(),
                                RoleName= db.AspNetRoles.ToList().Where(u => u.Id == _sqlReader["RoleId"].ToString()).First().Name
                               
                            };                          
                            list.Add(roles);
                        }
                    }
                    _sqlReader.Close();
                }
                finally
                {
                    _con.Close();
                }
            }
            return list;
        }
        public async Task<bool> Insert(Entitys.AspNetUserRoles role)
        {
            if (CheckExistOfRoles(role))
            {
                return false;
            }

            using (_con = new SqlConnection(_connect))
            {
                _cmd = new SqlCommand("spInsertUserRoles", _con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter param = new SqlParameter
                {
                    ParameterName = "@UserId",
                    Value = role.UserId
                };
                _cmd.Parameters.Add(param);

                SqlParameter paramOfferId = new SqlParameter
                {
                    ParameterName = "@RoleId",
                    Value = role.RoleId
                };
                _cmd.Parameters.Add(paramOfferId);               

                try
                {
                    _con.Open();
                    await _cmd.ExecuteScalarAsync();
                }
                catch
                {
                    return false;
                }
                finally
                {
                    _con.Close();
                }
            }
            return true;
        }     
        public async Task<bool> Delete(Entitys.AspNetUserRoles role)
        {
            using (_con = new SqlConnection(_connect))
            {
                _cmd = new SqlCommand("spDeleteUserRoles", _con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter param = new SqlParameter
                {
                    ParameterName = "@UserId",
                    Value = role.UserId
                };
                _cmd.Parameters.Add(param);

                SqlParameter paramOfferId = new SqlParameter
                {
                    ParameterName = "@RoleId",
                    Value = role.RoleId
                };
                _cmd.Parameters.Add(paramOfferId);              

                try
                {
                    _con.Open();
                    await _cmd.ExecuteScalarAsync();
                }
                catch
                {
                    return false;
                }
                finally
                {
                    _con.Close();
                }
            }
            return true;
        }
        private bool CheckExistOfRoles(Entitys.AspNetUserRoles role)
        {
            using (_con = new SqlConnection(_connect))
            {
                _cmd = new SqlCommand("spCheckOfExistUserRole", _con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter param = new SqlParameter
                {
                    ParameterName = "@UserId",
                    Value = role.UserId
                };
                _cmd.Parameters.Add(param);

                SqlParameter paramOfferId = new SqlParameter
                {
                    ParameterName = "@RoleId",
                    Value = role.RoleId
                };
                _cmd.Parameters.Add(paramOfferId);              

                SqlParameter output = new SqlParameter("@Exists", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                _cmd.Parameters.Add(output);

                try
                {
                    _con.Open();
                    _cmd.ExecuteScalar();

                    if (output.Value != DBNull.Value)
                    {
                        return Convert.ToInt32(output.Value) > 0;
                    }

                    return false;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    _con.Close();
                }
            }
        }

    }
}
