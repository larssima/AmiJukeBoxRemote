using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using AmiJukeBoxService.Models;

namespace AmiJukeBoxService.Images;

public class ImageStripService
{
    private readonly string _templatesPath;
    private readonly string _imagesPath;

    public ImageStripService(IConfiguration config, IWebHostEnvironment env)
    {
        // Default to wwwroot/assets relative to the exe, overridable via config
        var assetsPath = config["AppSettings:AssetsPath"];
        if (string.IsNullOrEmpty(assetsPath))
            assetsPath = Path.Combine(env.WebRootPath, "assets");

        _templatesPath = Path.Combine(assetsPath, "templates");
        _imagesPath = Path.Combine(assetsPath, "images");
    }

    public void CleanImagesDirectory()
    {
        foreach (var file in new DirectoryInfo(_imagesPath).GetFiles())
            file.Delete();
    }

    public string CreateStrip(JbSelectionModel m)
    {
        var templateFile = Path.Combine(_templatesPath, m.ImageStripTemplate);
        using var bitmap = new Bitmap(Image.FromFile(templateFile));
        using var g = Graphics.FromImage(bitmap);
        g.SmoothingMode = SmoothingMode.AntiAlias;

        var typeFont = new Font("Traveling _Typewriter", 12, FontStyle.Bold);
        var smallFont = new Font("Traveling _Typewriter", 10, FontStyle.Bold);

        if (m.Archived == 0)
        {
            int opacity = 168;
            g.DrawString(m.JbLetter + m.JbNumberA, smallFont,
                new SolidBrush(Color.FromArgb(opacity, Color.Black)), new Point(6, 31));
            g.DrawString(m.JbLetter + m.JbNumberB, smallFont,
                new SolidBrush(Color.FromArgb(opacity, Color.Black)), new Point(6, 51));
        }

        DrawCentered(g, typeFont, m.Artist1, 40);
        DrawCentered(g, typeFont, m.A1Song, 15);
        DrawCentered(g, typeFont, m.B1Song, 70);

        var fileEndName = m.Id + (m.Archived == 0 ? "" : "_arch");
        var baseName = Path.GetFileNameWithoutExtension(m.ImageStripTemplate);
        var fileName = $"{baseName}_{fileEndName}.png";

        bitmap.Save(Path.Combine(_imagesPath, fileName), ImageFormat.Png);
        return fileName;
    }

    private static void DrawCentered(Graphics g, Font font, string text, int y)
    {
        var size = g.MeasureString(text, font);
        g.DrawString(text, font, SystemBrushes.WindowText,
            new Point(150 - Math.Min(150, (int)Math.Round(size.Width * 0.5)), y));
    }
}
