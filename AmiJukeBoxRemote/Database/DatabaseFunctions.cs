using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using AmiJukeBoxRemote.Models;
using Dapper;

namespace AmiJukeBoxRemote.Database
{
    public class DatabaseFunctions
    {

        public List<JbSelectionModel> GetAllSelections()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["JukeboxDatabase"]
                .ConnectionString))

            {

                return db.Query<JbSelectionModel>

                    ("Select * From jbselection where archived!=1 order by JbNumeric").ToList();
            }
        }

        public bool SaveToDataBase(JbSelectionModel jbmopdel)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["JukeboxDatabase"].ConnectionString))

                {

                    db.Open();

                    string sqlQuery = "INSERT INTO []([RmEmpNo],[IdpsEmpNo]) VALUES (@RmEmpNo,@IdpsEmpNo)";
                    db.Execute(sqlQuery,
                        new
                        {
                            //rmIdpsModel.RmEmpNo,
                            //rmIdpsModel.IdpsEmpNo
                        });

                    db.Close();
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
    }
}