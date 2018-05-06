using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardTrend.Common.Extensions;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;

namespace CardTrend.Common.Capcha
{
   public class Captcha
    {
        public byte[] Image { get; set; }
        public string Text { get; set; }
        public Captcha(bool noisy = true)
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            Text = Guid.NewGuid().ToString().Truncate(6);
            using (var mem = new MemoryStream())
            using (var bmp = new Bitmap(130, 30))
            using (var gfx = Graphics.FromImage((Image)bmp))
            {
                gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));

                //add noise
                if (noisy)
                {
                    int i, r, x, y;
                    var pen = new Pen(Color.Yellow);
                    for (i = 1; i < 10; i++)
                    {
                        pen.Color = Color.FromArgb(
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)));

                        r = rand.Next(0, (130 / 3));
                        x = rand.Next(0, 130);
                        y = rand.Next(0, 30);

                        gfx.DrawEllipse(pen, new Rectangle(x - r, y - r, r, r));
                    }
                }

                //add question
                gfx.DrawString(Text, new Font("Tahoma", 15), Brushes.Gray, 2, 3);

                //render as Jpeg
                bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
                Image = mem.GetBuffer();
            }
        }
    }
}
