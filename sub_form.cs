using System;
using System.Drawing;
using System.Windows.Forms;

partial class Form02 : Form{
    public Form02(pic_data_class p_args){
        this.Width = 200;
        this.Height = 200;
        this.Text = p_args.Name;

        Label lavel1 = new Label();
        lavel1.Parent = this;
        lavel1.Text = "Name";
        lavel1.Location = new Point(10, 10);

        TextBox textbox1 = new TextBox();
        textbox1.Parent = this;
        textbox1.Text = p_args.Name;
        textbox1.Location = new Point(10, 30);

        Button btn1 = new Button();
        btn1.Parent = this;
        btn1.Text = "•ÏX”½‰f";
        btn1.Location = new Point(10, 70);
        btn1.Click += (obj, e) => {p_args.Name = textbox1.Text;this.Close();};
    }
}
class Form03:Form{
    PictureBox PictureBox1 = new PictureBox();
    PictureBox PictureBox2 = new PictureBox();
    int scrY = Screen.PrimaryScreen.Bounds.Height;
    int scrX = Screen.PrimaryScreen.Bounds.Width;
    int x  = Cursor.Position.X - 15;
    int y  = Cursor.Position.Y - 15;
    public form(){
        this.Size = new Size(scrX, scrY);
        this.Location = new Point(0, 0);
        this.StartPosition = FormStartPosition.Manual;

        this.FormBorderStyle = FormBorderStyle.None;
        this.TransparencyKey = this.BackColor;
        
        Console.WriteLine("x:{0} y:{1}", scrX, scrY);
        PictureBox1.Parent = this;
        PictureBox1.Size = new Size(31, 31);
        PictureBox1.Location = new Point(x, y);
        this.draw();

        PictureBox2.Parent = this;
        PictureBox2.Size = new Size(scrX, scrY);
        PictureBox2.Location = new Point(0, 0);
        this.draw2();
        
        this.KeyPreview = true;
        KeyDown += new KeyEventHandler(keydown);
    }
    public void draw(){
        Bitmap bmp = new Bitmap(31, 31);
        PictureBox1.Image = bmp;
        Graphics g = Graphics.FromImage(bmp);
        g.DrawLine(Pens.Black, 15, 0, 15, 31);
        g.DrawLine(Pens.Black, 0, 15, 31, 15);
        g.Dispose();
    }
    public void draw2(){
        Bitmap bmp = new Bitmap(scrX, scrY);
        PictureBox2.Image = bmp;
        Graphics g = Graphics.FromImage(bmp);
        int x1 = 30, x2 = 60,y1 = 500, y2 = 400;
        g.DrawRectangle(Pens.Black, (x1 < x2)?x1:x2,(y1 < y2)?y1:y2,(x1<x2)?x2-x1:x1-x2,(y1<y2)?y2-y1:y1-y2 );
        g.Dispose();
    }

    void keydown(object sender, KeyEventArgs k){
        if(k.KeyCode == Keys.J)y += 10;
        if(k.KeyCode == Keys.K)y -= 10;
        if(k.KeyCode == Keys.H)x -= 10;
        if(k.KeyCode == Keys.L)x += 10;
        Console.WriteLine("x:{0} y:{1}", x, y);

        PictureBox1.Location = new Point(x, y);
    }
}