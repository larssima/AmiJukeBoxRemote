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

        public int GetNextId()
        {
            using (IDbConnection db = new MySqlConnection(ConfigurationManager.ConnectionStrings["JukeboxDatabase"]
                .ConnectionString))

            {
                db.Open();
                var id = db.Query<int>

                    ("Select Max(Id) From amijukebox.jbselection").ToString();
                return int.Parse(id + 1);
            }
        }

        public int SaveToDataBase(JbSelectionModel jbmodel)
        {
            var id = -1;
            try
            {
                using (IDbConnection db = new MySqlConnection(ConfigurationManager.ConnectionStrings["JukeboxDatabase"].ConnectionString))

                {
                    db.Open();

                    string sqlQuery = "INSERT INTO amijukebox.jbselection (jbletter,jbnumbera,jbnumberb,jbnumeric,a1song,a2song," +
                                      "b1song,b2song,artist1,artist2,imagestripname,musiccategory,archived,imagestriptemplate) VALUES (@jbletter,@jbnumbera," +
                                      "@jbnumberb,@jbnumeric,@a1song,@a2song,@b1song,@b2song,@artist1,@artist2,@imagestripname,@musiccategory,@archived,@imagestriptemplate)";
                    db.Execute(sqlQuery,
                        new
                        {
                            jbmodel.JbLetter,
                            jbmodel.JbNumberA,
                            jbmodel.JbNumberB,
                            jbmodel.JbNumeric,
                            jbmodel.A1Song,
                            jbmodel.A2Song,
                            jbmodel.B1Song,
                            jbmodel.B2Song,
                            jbmodel.Artist1,
                            jbmodel.Artist2,
                            jbmodel.ImageStripName,
                            jbmodel.MusicCategory,
                            jbmodel.Archived,
                            jbmodel.ImageStripTemplate
                        });

                    id = db.Query<int>("SELECT CAST(LAST_INSERT_ID() AS UNSIGNED INTEGER);").Single();

                    db.Close();
                }
            }
            catch (Exception e)
            {
                return id;
            }
            jbmodel.Id = id;
            return id;
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

        public JbSelectionModel GetJbSelection(int jbmodelId)
        {
            using (IDbConnection db = new MySqlConnection(ConfigurationManager.ConnectionStrings["JukeboxDatabase"].ConnectionString))

            {

                return db.Query<JbSelectionModel>("SELECT * FROM amijukebox.jbselection WHERE Id=@Id", new { Id = jbmodelId }).FirstOrDefault();
            }
        }
    

        public bool ArchiveSelection(int jbmodelId)
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(ConfigurationManager.ConnectionStrings["JukeboxDatabase"].ConnectionString))

                {

                    db.Open();

                    string sqlQuery = "Update amijukebox.jbselection Set Archived = 1 Where Id = @Id";
                    db.Execute(sqlQuery,
                        new
                        {
                            Id = jbmodelId
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

        public bool ReinstateSelection(JbSelectionModel jbmodel)
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(ConfigurationManager.ConnectionStrings["JukeboxDatabase"].ConnectionString))

                {

                    db.Open();

                    string sqlQuery = "Update amijukebox.jbselection Set JbLetter = @jbletter, JbNumberA = @jbnumbera, JbNumberB = @jbnumberb, JbNumeric = @jbnumeric, Archived = @archived Where Id = @Id";
                    db.Execute(sqlQuery,
                        new
                        {
                            jbmodel.JbLetter,
                            jbmodel.JbNumberA,
                            jbmodel.JbNumberB,
                            jbmodel.JbNumeric,
                            jbmodel.Archived,
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