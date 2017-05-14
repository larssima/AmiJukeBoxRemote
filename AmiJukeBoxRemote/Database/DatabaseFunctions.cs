using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using AmiJukeBoxRemote.Models;
using Dapper;
using MySql.Data.MySqlClient;

namespace AmiJukeBoxRemote.Database
{
    public class DatabaseFunctions
    {

        public List<JbSelectionModel> GetAllSelections()
        {
            using (IDbConnection db = new MySqlConnection(ConfigurationManager.ConnectionStrings["JukeboxDatabase"]
                .ConnectionString))

            {
                db.Open();
                return db.Query<JbSelectionModel>

                    ("Select * From amijukebox.jbselection where archived!=1 order by JbNumeric").ToList();
            }
        }
        public List<JbSelectionModel> GetAllArchivedSelections()
        {
            using (IDbConnection db = new MySqlConnection(ConfigurationManager.ConnectionStrings["JukeboxDatabase"]
                .ConnectionString))

            {
                db.Open();
                return db.Query<JbSelectionModel>

                    ("Select * From amijukebox.jbselection where archived=1 order by JbNumeric").ToList();
            }
        }

        public bool SaveToDataBase(JbSelectionModel jbmopdel)
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(ConfigurationManager.ConnectionStrings["JukeboxDatabase"].ConnectionString))

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

        public bool UpdateImagePath(JbSelectionModel jbmodel)
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(ConfigurationManager.ConnectionStrings["JukeboxDatabase"].ConnectionString))

                {

                    db.Open();

                    string sqlQuery = "Update amijukebox.jbselection Set ImageStripName = @imagestripname Where Id = @Id";
                    db.Execute(sqlQuery,
                        new
                        {
                            jbmodel.ImageStripName,
                            jbmodel.Id
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