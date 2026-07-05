using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using AmiJukeBoxService.Models;

namespace AmiJukeBoxService.Database;

public class DatabaseFunctions
{
    private readonly string _connStr;
    private readonly ILogger<DatabaseFunctions> _logger;

    public DatabaseFunctions(IConfiguration config, ILogger<DatabaseFunctions> logger)
    {
        _connStr = config.GetConnectionString("JukeboxDatabase")!;
        _logger = logger;
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to save selection {JbLetter}{JbNumberA}/{JbNumberB} to database", m.JbLetter, m.JbNumberA, m.JbNumberB);
            return -1;
        }
    }

    public bool UpdateImagePath(JbSelectionModel m)
    {
        try
        {
            using IDbConnection db = new MySqlConnection(_connStr);
            db.Execute("UPDATE amijukebox.jbselection SET ImageStripName=@ImageStripName WHERE Id=@Id", m);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update image path for selection {Id}", m.Id);
            return false;
        }
    }

    public bool ArchiveSelection(int id)
    {
        try
        {
            using IDbConnection db = new MySqlConnection(_connStr);
            db.Execute("UPDATE amijukebox.jbselection SET Archived=1 WHERE Id=@Id", new { Id = id });
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to archive selection {Id}", id);
            return false;
        }
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to reinstate selection {Id}", m.Id);
            return false;
        }
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update selection {Id}", m.Id);
            return false;
        }
    }
}
