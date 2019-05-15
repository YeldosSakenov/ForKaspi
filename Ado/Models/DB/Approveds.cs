using DataAccess.Ado.Models.Entitys;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Ado.Models.DB
{
    public class Approveds
    {
        private readonly string _connect = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
        private SqlConnection _con;
        private SqlCommand _cmd;
        private SqlDataReader _sqlReader;
        private IdeaBankBaseEntities db;
        public async Task<List<Entitys.Approved>> Get(Entitys.Approved approved)
        {
            List<Entitys.Approved> list = new List<Entitys.Approved>();
            db = new IdeaBankBaseEntities();

            using (_con = new SqlConnection(_connect))
            {
                try
                {
                    _cmd = new SqlCommand("spGetListOfApproved", _con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlParameter param = new SqlParameter
                    {
                        ParameterName = "@UserId",
                        Value = approved.UserId
                    };
                    _cmd.Parameters.Add(param);

                    SqlParameter paramOfferId = new SqlParameter
                    {
                        ParameterName = "@OfferId",
                        Value = approved.OfferId
                    };
                    _cmd.Parameters.Add(paramOfferId);

                    _con.Open();
                    _sqlReader = await _cmd.ExecuteReaderAsync();

                    if (_sqlReader.HasRows)
                    {
                        while (_sqlReader.Read())
                        {
                            Entitys.Approved approv = new Entitys.Approved
                            {
                                UserId = _sqlReader["UserId"].ToString(),
                                UserName= db.AspNetUsers.ToList().Where(u => u.Id == _sqlReader["UserId"].ToString()).First().UserName,
                                OfferId = (int)_sqlReader["OfferId"],                                
                                StatusId = (int)_sqlReader["StatusId"],
                                StatusName=db.Statuses.ToList().Where(s=>s.Id== (int)_sqlReader["StatusId"]).First().Name
                            };
                            if (!_sqlReader.IsDBNull(_sqlReader.GetOrdinal("DateApproved")))
                            {
                                approv.DateApproved = (DateTime)_sqlReader["DateApproved"];
                            }
                            list.Add(approv);
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
        public async Task<bool> Insert(Entitys.Approved approved)
        {
            if (CheckExistOfApprove(approved))
            {
                return false;
            }

            using (_con = new SqlConnection(_connect))
            {
                _cmd = new SqlCommand("spInsertApproved", _con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter param = new SqlParameter
                {
                    ParameterName = "@UserId",
                    Value = approved.UserId
                };
                _cmd.Parameters.Add(param);

                SqlParameter paramOfferId = new SqlParameter
                {
                    ParameterName = "@OfferId",
                    Value = approved.OfferId
                };
                _cmd.Parameters.Add(paramOfferId);

                SqlParameter paramStatusId = new SqlParameter
                {
                    ParameterName = "@StatusId",
                    Value = approved.StatusId
                };
                _cmd.Parameters.Add(paramStatusId);

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
        public async Task<bool> Update(Entitys.Approved approved)
        {
            using (_con = new SqlConnection(_connect))
            {
                _cmd = new SqlCommand("spUpdateApproved", _con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter param = new SqlParameter
                {
                    ParameterName = "@UserId",
                    Value = approved.UserId
                };
                _cmd.Parameters.Add(param);

                SqlParameter paramOfferId = new SqlParameter
                {
                    ParameterName = "@OfferId",
                    Value = approved.OfferId
                };
                _cmd.Parameters.Add(paramOfferId);

                SqlParameter paramStatusId = new SqlParameter
                {
                    ParameterName = "@StatusId",
                    Value = approved.StatusId
                };
                _cmd.Parameters.Add(paramStatusId);

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
        public async Task<bool> Delete(Entitys.Approved approved)
        {
            using (_con = new SqlConnection(_connect))
            {
                _cmd = new SqlCommand("spDeleteApproved", _con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter param = new SqlParameter
                {
                    ParameterName = "@UserId",
                    Value = approved.UserId
                };
                _cmd.Parameters.Add(param);

                SqlParameter paramOfferId = new SqlParameter
                {
                    ParameterName = "@OfferId",
                    Value = approved.OfferId
                };
                _cmd.Parameters.Add(paramOfferId);

                SqlParameter paramStatusId = new SqlParameter
                {
                    ParameterName = "@StatusId",
                    Value = approved.StatusId
                };
                _cmd.Parameters.Add(paramStatusId);

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
        private bool CheckExistOfApprove(Entitys.Approved approved)
        {
            using (_con = new SqlConnection(_connect))
            {
                _cmd = new SqlCommand("spCheckOfExistApprove", _con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter param = new SqlParameter
                {
                    ParameterName = "@UserId",
                    Value = approved.UserId
                };
                _cmd.Parameters.Add(param);

                SqlParameter paramOfferId = new SqlParameter
                {
                    ParameterName = "@OfferId",
                    Value = approved.OfferId
                };
                _cmd.Parameters.Add(paramOfferId);

                SqlParameter paramStatusId = new SqlParameter
                {
                    ParameterName = "@StatusId",
                    Value = approved.StatusId
                };
                _cmd.Parameters.Add(paramStatusId);

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
