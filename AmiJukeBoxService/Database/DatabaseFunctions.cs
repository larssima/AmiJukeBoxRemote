using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using AmiJukeBoxService.Models;

namespace AmiJukeBoxService.Database;

public class DatabaseFunctions
{
    private readonly string _connStr;

    public DatabaseFunctions(IConfiguration config)
    {
        _connStr = config.GetConnectionString("JukeboxDatabase")!;
    }

    public List<JbSelectionModel> GetAllSelections()
    {
        using IDbConnection db = new MySqlConnection(_connStr);
        return db.Query<JbSelectionModel>(
            "SELECT * FROM amijukebox.jbselection WHERE archived!=1 ORDER BY JbNumeric").ToList();
    }

    public List<JbSelectionModel> GetAllArchivedSelections()
    {
        using IDbConnection db = new MySqlConnection(_connStr);
        return db.Query<JbSelectionModel>(
            "SELECT * FROM amijukebox.jbselection WHERE archived=1 ORDER BY JbNumeric").ToList();
    }

    public int SaveToDataBase(JbSelectionModel m)
    {
        try
        {
            using IDbConnection db = new MySqlConnection(_connStr);
            db.Execute(
                "INSERT INTO amijukebox.jbselection (jbletter,jbnumbera,jbnumberb,jbnumeric,a1song,a2song," +
                "b1song,b2song,artist1,artist2,imagestripname,musiccategory,archived,imagestriptemplate,discogslink,spotifyuri) " +
                "VALUES (@JbLetter,@JbNumberA,@JbNumberB,@JbNumeric,@A1Song,@A2Song,@B1Song,@B2Song," +
                "@Artist1,@Artist2,@ImageStripName,@MusicCategory,@Archived,@ImageStripTemplate,@DiscogsLink,@SpotifyUri)", m);
            var id = db.Query<int>("SELECT CAST(LAST_INSERT_ID() AS UNSIGNED INTEGER);").Single();
            m.Id = id;
            return id;
        }
        catch { return -1; }
    }

    public bool UpdateImagePath(JbSelectionModel m)
    {
        try
        {
            using IDbConnection db = new MySqlConnection(_connStr);
            db.Execute("UPDATE amijukebox.jbselection SET ImageStripName=@ImageStripName WHERE Id=@Id", m);
            return true;
        }
        catch { return false; }
    }

    public bool ArchiveSelection(int id)
    {
        try
        {
            using IDbConnection db = new MySqlConnection(_connStr);
            db.Execute("UPDATE amijukebox.jbselection SET Archived=1 WHERE Id=@Id", new { Id = id });
            return true;
        }
        catch { return false; }
    }

    public bool ReinstateSelection(JbSelectionModel m)
    {
        try
        {
            using IDbConnection db = new MySqlConnection(_connStr);
            db.Execute(
                "UPDATE amijukebox.jbselection SET JbLetter=@JbLetter,JbNumberA=@JbNumberA,JbNumberB=@JbNumberB," +
                "JbNumeric=@JbNumeric,Archived=@Archived WHERE Id=@Id", m);
            return true;
        }
        catch { return false; }
    }

    public bool UpdateInDataBase(JbSelectionModel m)
    {
        try
        {
            using IDbConnection db = new MySqlConnection(_connStr);
            db.Execute(
                "UPDATE amijukebox.jbselection SET JbLetter=@JbLetter,JbNumberA=@JbNumberA,JbNumberB=@JbNumberB," +
                "JbNumeric=@JbNumeric,A1Song=@A1Song,A2Song=@A2Song,B1Song=@B1Song,B2Song=@B2Song," +
                "Artist1=@Artist1,Artist2=@Artist2,MusicCategory=@MusicCategory,ImageStripTemplate=@ImageStripTemplate," +
                "DiscogsLink=@DiscogsLink,SpotifyUri=@SpotifyUri,Archived=@Archived WHERE Id=@Id", m);
            return true;
        }
        catch { return false; }
    }
}
