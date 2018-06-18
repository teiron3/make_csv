using System;
using System.Drawing;
using System.Windows.Forms;

partial class Form02 : Form{
    public Form02(pic_data_class p_args){
        this.Width = 200;
        this.Hidth = 200;
        this.Text = p_args.Name;

        Label lavel1 = new Lavel();
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
        btn1.Click += (obj, e) => {p_args.Name = textbox1.Text;return;}
    }
}