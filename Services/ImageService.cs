using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using Microsoft.AspNetCore.Routing.Template;
using WeeklyProgram.ViewModel;

namespace WeeklyProgram.Services
{
    public class ImageService
    {
        public byte[] ImagePrinter(ProjectViewModel projectViewModel)
        {
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "templates", $"{projectViewModel.ImageUrl}");


            if (!File.Exists(templatePath))
            {
                throw new FileNotFoundException($"Template file not found at path: {templatePath}");
            }
            using (Image image = Image.FromFile(templatePath))
            {
                using (Graphics graphics = Graphics.FromImage(image))
                {
                    Font? font = new Font("Arial", 24);

                    PrivateFontCollection privateFontCollection = new PrivateFontCollection();
                    privateFontCollection.AddFontFile("wwwroot/fonts/Amiri-Regular.ttf");
                    Brush brush = new SolidBrush(Color.FromArgb(0, 82, 62));

                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;
                    stringFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;

                    

                    PointF pointf = new PointF(image.Width - 350, 760);

                    for (int i = 0; i < projectViewModel.ObjectJson.Length; i++)
                    {
                        graphics.DrawString(projectViewModel.ObjectJson[i], font, brush, pointf, stringFormat);
                        pointf.X -= 550;
                        
                        if (i % projectViewModel.ArrayColmun == 0 )
                        {
                            pointf.Y += 210;
                            pointf.X = image.Width - 350;
                        }
                    }
                    using (MemoryStream ms = new MemoryStream())
                    {
                        image.Save(ms, ImageFormat.Png);
                        return ms.ToArray();
                    }
                }
            }
        }
    }
}