using System.Drawing;

namespace HangFire.Web.BackgroundJobs
{
    public class DelayedJobs
    {
        public static string AddWatermarkJob(string file, string watermark)
        {
            return Hangfire.BackgroundJob.Schedule(() => ApplyWatermark(file, watermark), TimeSpan.FromSeconds(30));
        }
        public static void ApplyWatermark(string file, string watermark)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", file);
            using (var bitmap = Bitmap.FromFile(path))
            {
                using (Bitmap tempBitMap = new Bitmap(bitmap.Width, bitmap.Height))
                {
                    using (Graphics grp = Graphics.FromImage(tempBitMap))
                    {
                        grp.DrawImage(bitmap, 0, 0);
                        var font = new Font(FontFamily.GenericSansSerif, 25,FontStyle.Bold);
                        var color = Color.FromArgb(255, 0, 0);
                        var brush = new SolidBrush(color);
                        var point = new Point(20, bitmap.Height-50);
                        grp.DrawString(watermark, font, brush, point);
                        tempBitMap.Save(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/watermarks", file));
                    }
                }
            }
        }
    }
}