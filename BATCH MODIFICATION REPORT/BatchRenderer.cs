using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;

namespace BATCH_MODIFICATION_REPORT
{
    public class BatchRenderer
    {
        private int width = 0, height = 0;
        private double totHeight = 1500;
        private Bitmap bmp = null;
        private Graphics gfx = null;

        private List<BatchData> data = null;
        Image logoImg = Image.FromFile(Path.Combine(Directory.GetCurrentDirectory(), "assets", "logo.png"));

        public BatchRenderer(int width, int height)
        {
            this.width = width;
            this.height = height;
        }


        public int getDataCount()
        {
            if (this.data == null) return 0;
            else return this.data.Count;
        }
        public List<BatchData> getData()
        {
            return this.data;
        }
        public void setData(List<BatchData> data)
        {
            this.data = data;
        }
        public void setRenderSize(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public Point convertCoord(Point a)
        {
            double px = height / totHeight;

            Point res = new Point();
            res.X = (int)(a.X * px);
            res.Y = (int)((totHeight - a.Y) * px);
            return res;
        }
        public PointF convertCoord(PointF p)
        {
            double px = height / totHeight;
            PointF res = new PointF();
            res.X = (int)(p.X * px);
            res.Y = (int)((totHeight - p.Y) * px);
            return res;
        }
        public Bitmap getBmp()
        {
            return this.bmp;
        }

        public void drawFilledCircle(Brush brush, Point o, Size size)
        {
            double px = height / totHeight;
            size.Width = (int)(size.Width * px);
            size.Height = (int)(size.Height * px);

            Rectangle rect = new Rectangle(convertCoord(o), size);

            gfx.FillEllipse(brush, rect);
        }
        public void fillRectangle(Color color, Rectangle rect)
        {
            rect.Location = convertCoord(rect.Location);
            double px = height / totHeight;
            rect.Width = (int)(rect.Width * px);
            rect.Height = (int)(rect.Height * px);

            Brush brush = new SolidBrush(color);
            gfx.FillRectangle(brush, rect);
            brush.Dispose();

        }
        public void drawRectangle(Pen pen, Rectangle rect)
        {
            rect.Location = convertCoord(rect.Location);
            double px = height / totHeight;
            rect.Width = (int)(rect.Width * px);
            rect.Height = (int)(rect.Height * px);
            gfx.DrawRectangle(pen, rect);
        }

        public void drawImg(Image img, Point o, Size size)
        {
            double px = height / totHeight;
            o = convertCoord(o);
            Rectangle rect = new Rectangle(o, new Size((int)(size.Width * px), (int)(size.Height * px)));
            gfx.DrawImage(img, rect);

        }
        public void drawString(Color color, Point o, string content, int font = 15)
        {

            o = convertCoord(o);

            // Create font and brush.
            Font drawFont = new Font("Arial", font);
            SolidBrush drawBrush = new SolidBrush(color);

            gfx.DrawString(content, drawFont, drawBrush, o.X, o.Y);

            drawFont.Dispose();
            drawBrush.Dispose();

        }

        public void drawCenteredString_withBorder(string content, Rectangle rect, Brush brush, Font font, Color borderColor)
        {

            //using (Font font1 = new Font("Arial", fontSize, FontStyle.Bold, GraphicsUnit.Point))

            // Create a StringFormat object with the each line of text, and the block
            // of text centered on the page.
            double px = height / totHeight;
            rect.Location = convertCoord(rect.Location);
            rect.Width = (int)(px * rect.Width);
            rect.Height = (int)(px * rect.Height);

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            // Draw the text and the surrounding rectangle.
            gfx.DrawString(content, font, brush, rect, stringFormat);

            Pen borderPen = new Pen(new SolidBrush(borderColor), 2);
            gfx.DrawRectangle(borderPen, rect);
            borderPen.Dispose();
        }
        public void drawCenteredImg_withBorder(Image img, Rectangle rect, Brush brush, Font font, Color borderColor)
        {
            double px = height / totHeight;
            rect.Location = convertCoord(rect.Location);
            rect.Width = (int)(px * rect.Width);
            rect.Height = (int)(px * rect.Height);

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            // Draw the text and the surrounding rectangle.
            //gfx.DrawString(content, font, brush, rect, stringFormat);
            //drawImg(logoImg, new Point(20, 60), new Size(150, 50));
            gfx.DrawImage(img, rect);
            Pen borderPen = new Pen(new SolidBrush(borderColor), 2);
            gfx.DrawRectangle(borderPen, rect);
            borderPen.Dispose();
        }
        public void drawCenteredString(string content, Rectangle rect, Brush brush, Font font)
        {

            //using (Font font1 = new Font("Arial", fontSize, FontStyle.Bold, GraphicsUnit.Point))

            // Create a StringFormat object with the each line of text, and the block
            // of text centered on the page.
            double px = height / totHeight;
            rect.Location = convertCoord(rect.Location);
            rect.Width = (int)(px * rect.Width);
            rect.Height = (int)(px * rect.Height);

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            // Draw the text and the surrounding rectangle.
            gfx.DrawString(content, font, brush, rect, stringFormat);
            //gfx.DrawRectangle(Pens.Black, rect);

        }
        private void fillPolygon(Brush brush, PointF[] points)
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = convertCoord(points[i]);
            }
            gfx.FillPolygon(brush, points);
        }
        public void drawLine(Point p1, Point p2, Color color, int linethickness = 1)
        {
            if (color == null)
                color = Color.Gray;

            p1 = convertCoord(p1);
            p2 = convertCoord(p2);
            gfx.DrawLine(new Pen(color, linethickness), p1, p2);

        }
        public void drawString(Font font, Color brushColor, string content, Point o)
        {
            o = convertCoord(o);
            SolidBrush drawBrush = new SolidBrush(brushColor);
            gfx.DrawString(content, font, drawBrush, o.X, o.Y);
        }
        public void drawString(Point o, string content, int font = 15)
        {

            o = convertCoord(o);

            // Create font and brush.
            Font drawFont = new Font("Arial", font);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            gfx.DrawString(content, drawFont, drawBrush, o.X, o.Y);

        }

        public void drawPie(Color color, Point o, Size size, float startAngle, float sweepAngle)
        {
            // Create location and size of ellipse.
            double px = height / totHeight;
            size.Width = (int)(size.Width * px);
            size.Height = (int)(size.Height * px);

            Rectangle rect = new Rectangle(convertCoord(o), size);
            // Draw pie to screen.            
            Brush grayBrush = new SolidBrush(color);
            gfx.FillPie(grayBrush, rect, startAngle, sweepAngle);
        }


        public  GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {

            //Convert Rectangle Coord
            bounds.Location = convertCoord(bounds.Location);
            double px = height / totHeight;
            bounds.Width = (int)(bounds.Width * px);
            bounds.Height = (int)(bounds.Height * px);


            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // top left arc  
            path.AddArc(arc, 180, 90);

            // top right arc  
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // bottom right arc  
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left arc 
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

        public void fillRoundedRectangle(Brush brush, Rectangle rect, int borderRadius)
        {
            using (GraphicsPath path = RoundedRect(rect, borderRadius))
            {
                SmoothingMode initMode = gfx.SmoothingMode;
                gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                gfx.FillPath(brush, path);
                gfx.SmoothingMode = initMode;
            }
        }
        public void drawRoundedRectangle(Pen pen, Rectangle rect, int borderRadius)
        {

            using (GraphicsPath path = RoundedRect(rect, borderRadius))
            {
                SmoothingMode initMode = gfx.SmoothingMode;
                gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                gfx.DrawPath(pen, path);
                gfx.SmoothingMode = initMode;
            }
        }
        public void draw(int pageID = 1)
        {
            if (bmp == null)
                bmp = new Bitmap(width, height);
            else
            {
                if (bmp.Width != width || bmp.Height != height)
                {
                    bmp.Dispose();
                    bmp = new Bitmap(width, height);

                    gfx.Dispose();
                    gfx = Graphics.FromImage(bmp);
                    gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                }
            }


            if (gfx == null)
            {
                gfx = Graphics.FromImage(bmp);
                gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;                
                //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            }
            else
            {
                gfx.Clear(Color.Transparent);
            }

            if (data == null) return;

            double px = height / totHeight;
            //fillRectangle(Color.CornflowerBlue, new Rectangle(0, (int)totHeight, (int)(width / px), (int)totHeight));
            fillRectangle(Color.White, new Rectangle(0, (int)totHeight, (int)(width / px), (int)totHeight));

            int recWidth = 900, recHeight = 600, boxHeaderHeight = 120;
            int baseIndex = (pageID - 1) * 4;



            Pen blackBorderPen2 = new Pen(Color.Black, 2);

            Font textPercent = new Font("Arial", 50, FontStyle.Bold, GraphicsUnit.Point);
            Font textTitle = new Font("Arial", 32, FontStyle.Bold, GraphicsUnit.Point);
            Font textTitle2 = new Font("Arial", 22, FontStyle.Bold, GraphicsUnit.Point);
            Font headertitle = new Font("Arial", 16, FontStyle.Bold, GraphicsUnit.Point);
            Font headertitle2 = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Point);
            Font headerText = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Point);
            Font textFont10 = new Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point);
            Font textFont8 = new Font("Arial", 8, FontStyle.Regular, GraphicsUnit.Point);
            Font textFont7 = new Font("Arial", 8, FontStyle.Regular, GraphicsUnit.Point);
            Font textFont6 = new Font("Arial", 6, FontStyle.Regular, GraphicsUnit.Point);
            Font textFont5 = new Font("Arial", 5, FontStyle.Regular, GraphicsUnit.Point);

            drawCenteredString("BATCH MODIFICATION REPORT\nInstances of Adjudication", new Rectangle(0, 1500, 1800, 180),Brushes.Black, textTitle );
            for (int row = 0; row < 2; row ++)
            {
                for (int col = 0; col < 2; col++)
                {
                    int baseLeft = 20 + col * (recWidth + 10);
                    int baseTop = 1300 - row * (recHeight + 10);
                    int index = baseIndex + row * 2 + col;

                    if (index > data.Count - 1) break;
                    fillRectangle(Color.Black, new Rectangle(baseLeft, baseTop, recWidth, recHeight));
                    int boxHeight = recHeight - boxHeaderHeight * 2;
                    int boxWidth = (recWidth - 50) / 2;

                    fillRectangle(Color.White, new Rectangle(baseLeft + 10, baseTop - boxHeaderHeight, boxWidth, boxHeight));
                    double percent = 0;
                    if (data[index].ballots != 0)
                    {
                        percent = Math.Round(data[index].modified * 100 / (double)data[index].ballots, 2);
                    }
                    else percent = 0;

                    drawCenteredString(percent.ToString(), new Rectangle(baseLeft + 10, baseTop - boxHeaderHeight, boxWidth, boxHeight/ 2), Brushes.Red, textPercent);
                    drawCenteredString("Human/AI\nModified", new Rectangle(baseLeft + 10, baseTop - boxHeaderHeight - boxHeight / 2, boxWidth, boxHeight/2), Brushes.Black, textTitle);
                    drawCenteredString("%", new Rectangle(baseLeft + boxWidth - 80, baseTop - boxHeaderHeight, 80, 100), Brushes.Black, textTitle);

                    fillRectangle(Color.White, new Rectangle(baseLeft + recWidth / 2 + 15, baseTop - boxHeaderHeight, boxWidth, boxHeight));
                    string content1 = "1", content2 = "1";
                    if (percent == 0) {
                        content1 = "0"; content2 = "0"; 
                    } else
                    {
                        content2 = Math.Round(100 / percent, 0, MidpointRounding.AwayFromZero).ToString();
                    }

                    drawCenteredString(content1, new Rectangle(baseLeft + recWidth / 2 + 15, baseTop - boxHeaderHeight, boxWidth, boxHeight/3), Brushes.Red, textPercent);
                    drawCenteredString("OUT OF EVERY", new Rectangle(baseLeft + recWidth / 2 + 15, baseTop - boxHeaderHeight - boxHeight / 3, boxWidth, boxHeight / 6), Brushes.Black, textTitle2);

                    drawCenteredString(content2, new Rectangle(baseLeft + recWidth / 2 + 15, baseTop - boxHeaderHeight - boxHeight / 2, boxWidth, boxHeight / 3), Brushes.Red, textPercent);
                    drawCenteredString("BALLOTS MODIFIED", new Rectangle(baseLeft + recWidth / 2 + 15, baseTop - boxHeaderHeight - boxHeight * 5 / 6, boxWidth, boxHeight / 6), Brushes.Black, headertitle);







                    drawCenteredString("VTYPE", new Rectangle(baseLeft + 20, baseTop - 5, 120, 40), Brushes.White, headertitle);
                    drawRoundedRectangle(new Pen(Color.White, 5), new Rectangle(baseLeft + 20, baseTop - 45, 120, 60), 5);
                    drawCenteredString(data[index].votetype, new Rectangle(baseLeft + 20, baseTop - 45, 120, 60), Brushes.White, headertitle);

                    drawCenteredString("BATCH ID", new Rectangle(baseLeft + 180, baseTop - 5, 240, 40), Brushes.White, headertitle);
                    drawRoundedRectangle(new Pen(Color.White, 5), new Rectangle(baseLeft + 180, baseTop - 45, 240, 60), 5);
                    drawCenteredString(data[index].batchid.ToString(), new Rectangle(baseLeft + 180, baseTop - 45, 240, 60), Brushes.White, headertitle);


                    drawCenteredString("# BAL", new Rectangle(baseLeft + 480, baseTop - 5, 180, 40), Brushes.White, headertitle);
                    drawRoundedRectangle(new Pen(Color.White, 5), new Rectangle(baseLeft + 480, baseTop - 45, 180, 60), 5);
                    drawCenteredString(data[index].ballots.ToString(), new Rectangle(baseLeft + 480, baseTop - 45, 180, 60), Brushes.White, headertitle);


                    drawCenteredString("# MOD", new Rectangle(baseLeft + 700, baseTop - 5, 180, 40), Brushes.White, headertitle);
                    drawRoundedRectangle(new Pen(Color.White, 5), new Rectangle(baseLeft + 700, baseTop - 45, 180, 60), 5);
                    drawCenteredString(data[index].modified.ToString(), new Rectangle(baseLeft + 700, baseTop - 45, 180, 60), Brushes.White, headertitle);

                    drawCenteredString("UNIT", new Rectangle(baseLeft + 20, baseTop - recHeight + 115, 100, 40), Brushes.White, headertitle);
                    drawRoundedRectangle(new Pen(Color.White, 5), new Rectangle(baseLeft + 20, baseTop - recHeight + 75, 100, 50), 5);
                    drawCenteredString("H1", new Rectangle(baseLeft + 20, baseTop - recHeight + 75, 100, 50), Brushes.White, headertitle);


                    drawCenteredString("TYPE", new Rectangle(baseLeft + 160, baseTop - recHeight + 115, 280, 40), Brushes.White, headertitle);
                    drawRoundedRectangle(new Pen(Color.White, 5), new Rectangle(baseLeft + 160, baseTop - recHeight + 75, 280, 50), 5);
                    drawCenteredString(data[index].type, new Rectangle(baseLeft + 160, baseTop - recHeight + 75, 280, 50), Brushes.White, headertitle2);


                    drawCenteredString("LOC#", new Rectangle(baseLeft + 480, baseTop - recHeight + 115, 150, 40), Brushes.White, headertitle);
                    drawRoundedRectangle(new Pen(Color.White, 5), new Rectangle(baseLeft + 480, baseTop - recHeight + 75, 150, 50), 5);
                    drawCenteredString(data[index].votinglocationnumber.ToString(), new Rectangle(baseLeft + 480, baseTop - recHeight + 75, 150, 50), Brushes.White, headertitle);


                    drawCenteredString("LOC ID", new Rectangle(baseLeft + 680, baseTop - recHeight + 115, 200, 40), Brushes.White, headertitle);                    
                    drawRoundedRectangle(new Pen(Color.White, 5), new Rectangle(baseLeft + 680, baseTop - recHeight + 75, 200, 50), 5);
                    drawCenteredString(data[index].votinglocationname, new Rectangle(baseLeft + 680, baseTop - recHeight + 75, 200, 50), Brushes.White, headertitle);


                    
                }
            }

            drawImg(logoImg, new Point(100, 80), new Size(150, 60));
            string copyright = "©2021 Tesla Laboratories, llc & JHP";
            drawCenteredString(copyright, new Rectangle(1200, 80, 600, 80), Brushes.Black, headertitle2);
        }


    }
}
