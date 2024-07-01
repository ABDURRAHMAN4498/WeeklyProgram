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
                    StringFormat stringFormat = new StringFormat();
                    PrivateFontCollection privateFontCollection = new PrivateFontCollection();
                    privateFontCollection.AddFontFile("wwwroot/fonts/Cairo-VariableFont_slnt,wght.ttf");

                    Brush brush = new SolidBrush(Color.FromArgb(0, 82, 62));
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;
                    stringFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;


                    graphics.DrawString(projectViewModel.ProjectName, new Font(privateFontCollection.Families[0], 180, FontStyle.Bold, GraphicsUnit.World), brush, new Point(image.Width / 2, image.Height / 10), stringFormat);




                    Point pointf = new Point(image.Width - 350, 760);

                    for (int i = 0; i < projectViewModel.ObjectJson.Length; i++)
                    {
                        if (i % projectViewModel.ArrayColmun == 0 ||
                        i % projectViewModel.ArrayColmun < 6 &&
                         i < 6)
                        {
                            graphics.DrawString(projectViewModel.ObjectJson[i], new Font(privateFontCollection.Families[0], 60, FontStyle.Bold, GraphicsUnit.World), new SolidBrush(Color.White), pointf, stringFormat);
                        }

                        else
                        {
                            graphics.DrawString(projectViewModel.ObjectJson[i], new Font(privateFontCollection.Families[0], 60, FontStyle.Bold, GraphicsUnit.World), brush, pointf, stringFormat);

                        }




                        pointf.X -= 550;

                        if (pointf.X <= 140)
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