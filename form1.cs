using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

partial class Form01 : Form{
    bool stop_flg = false;
    ListView lv = new ListView();
    TextBox tb = new TextBox();
    Label label = new Label();
    CheckBox cb = new CheckBox();

    public Form01(){

        //フォームの設定
        this.Width = 650;
        this.Height = 350;


        //Listviewの設定
        lv.Parent = this;
        lv.Location = new Point(10,10);
        lv.Width = 600;
        lv.Height = 190;
        lv.View = View.Details;
        lv.FullRowSelect = true;
        lv.Columns.Add("Name", 100);
        lv.Columns.Add("Necessity", 70);
        lv.Columns.Add("X", 70);
        lv.Columns.Add("Y", 70);
        lv.Columns.Add("Width", 70);
        lv.Columns.Add("Height", 70);
        lv.Columns.Add("Pic_X", 70);
        lv.Columns.Add("Pic_Y", 70);
        lv.Columns.Add("Pic_Width", 70);
        lv.Columns.Add("Pic_Height", 70);
        lv.Columns.Add("Pic_CreateDate", 100);
        
        Button btn1 = new Button();
        btn1.Parent = this;
        btn1.Location = new Point(10, 210);
        btn1.Text = "読み込み";
        btn1.Click += (obj, e) => paint_list();

        Button btn2 = new Button();
        btn2.Parent = this;
        btn2.Location = new Point(10, 240);
        btn2.Text = "書き込み";
        btn2.Click += (obj, e) => write_csv();

        Button btn3 = new Button();
        btn3.Parent = this;
        btn3.Location = new Point(300, 210);
        btn3.Width = 80;
        btn3.Height = 40;
        btn3.Text = "データチェック";
        btn3.Click += (obj, e) => Data_Check();

        Button btn4 = new Button();
        btn4.Parent = this;
        btn4.Location = new Point(390, 210);
        btn4.Width = 80;
        btn4.Height = 40;
        btn4.Text = "画像取得";
        btn4.Click += new EventHandler(btn4Click_getpicture);

        Button btn5 = new Button();
        btn5.Parent = this;
        btn5.Location = new Point(480, 210);
        btn5.Width = 80;
        btn5.Height = 40;
        btn5.Text = "名前変更";
        btn5.Click += new EventHandler(btn5Click_namechange);


        Button btn6 = new Button();
        btn6.Parent = this;
        btn6.Location = new Point(480, 260);
        btn6.Width = 80;
        btn6.Height = 40;
        btn6.Text = "新規追加";
        btn6.Click += new EventHandler(btn6Click_newname);

        this.KeyPreview = true;
        KeyDown += new KeyEventHandler(testkeypress);

        label.Parent = this;
        label.Location = new Point(10, 270);
        label.Width = 300;
        label.Height = 100;
        label.Text = "範囲取得は左上は S 右下は E を押してください。\n画像の時は Picture のチェックを入れてください。\nTrue は T を False は F を押してください。";

        cb.Parent = this;
        cb.Location = new Point(100, 210);
        cb.Text = "Picture";
    }

    void btn4Click_getpicture(object sender, EventArgs e){
        if(lv.SelectedItems.Count < 1)return;

        Func<string, pic_data_class>find_p_class = (str_name) =>{
            foreach(pic_data_class pc in p_class){
                if(str_name == pc.Name)return pc;
            }
            return null;
        };
 
        pic_data_class p = find_p_class(lv.SelectedItems[0].SubItems[0].Text);

        bool a;
        a = (p.Width < 1 || p.Height < 1 );
        a = a || (p.Pic_Width < 1 || p.Pic_Height < 1 );
        if(a){MessageBox.Show("取得範囲の値を取り直してください");return;}

        if(p.Necessity)
        pic_make.pic_create(p);
    }

    void btn5Click_namechange(object sender, EventArgs e){
        Func<string, pic_data_class>find_p_class = (str_name) =>{
            foreach(pic_data_class pc in p_class){
                if(str_name == pc.Name)return pc;
            }
            return null;
        };
        pic_data_class p = find_p_class(lv.SelectedItems[0].SubItems[0].Text);

        show_Form02(p);
    }

    void btn6Click_newname(object sender, EventArgs e){
        this.rows ++;
        p_class[this.rows] = new pic_data_class();
        //p_class[this.rows].Name;

        show_Form02(p_class[this.rows]);
    }
    void testkeypress(object sender, KeyEventArgs e){
        if(lv.SelectedItems.Count > 0){
            //if(a != Keys.X | a != Keys.Y | a != Keys.P)return;
            if(e.KeyCode == Keys.P){
                cb.Checked = !cb.Checked;return;
            }

            Func<string, pic_data_class>find_p_class = (str_name) =>{
                foreach(pic_data_class pc in p_class){
                    if(str_name == pc.Name)return pc;
                }
                return null;
            };
            pic_data_class p = find_p_class(lv.SelectedItems[0].SubItems[0].Text);

            if(e.KeyCode == Keys.T){
                p.Necessity = true;
                lv.SelectedItems[0].SubItems[1].Text = "True";
                return;
            }

            if(e.KeyCode == Keys.F){
                p.Necessity = false;
                lv.SelectedItems[0].SubItems[1].Text = "False";
                return;
            }
            
            //チェックあり
            if(!cb.Checked){
                switch (e.KeyCode){
                    case Keys.S:
                        p.X = Cursor.Position.X;
                        lv.SelectedItems[0].SubItems[2].Text = p.X.ToString();
                        p.Y = Cursor.Position.Y;
                        lv.SelectedItems[0].SubItems[3].Text = p.Y.ToString();
                        break;

                    case Keys.E:
                        p.Width = Cursor.Position.X - p.X;
                        lv.SelectedItems[0].SubItems[4].Text = p.Width.ToString();
                        p.Height = Cursor.Position.Y - p.Y;
                        lv.SelectedItems[0].SubItems[5].Text = p.Height.ToString();
                        break;
                }
                return;
            }
            //チェックなし
            switch (e.KeyCode){
                case Keys.S:
                    p.Pic_X = Cursor.Position.X;
                    lv.SelectedItems[0].SubItems[6].Text = p.Pic_X.ToString();
                    p.Pic_Y = Cursor.Position.Y;
                    lv.SelectedItems[0].SubItems[7].Text = p.Pic_Y.ToString();
                    break;

                case Keys.E:
                    p.Pic_Width = Cursor.Position.X - p.Pic_X;
                    lv.SelectedItems[0].SubItems[8].Text = p.Pic_Width.ToString();
                    p.Pic_Height = Cursor.Position.Y - p.Pic_Y;
                    lv.SelectedItems[0].SubItems[9].Text = p.Pic_Height.ToString();
                    break;
            }
            return;
            
                    
        }
    } 
    
    void paint_list(){
        lv.Items.Clear();
        int j = 0;
        for(int k = 0; k <= rows; k++)
        {
            int i = k - j;
            if(p_class[k].Name != null){
                lv.Items.Add(p_class[k].Name); 
                lv.Items[i].SubItems.Add(p_class[k].Necessity.ToString());
                lv.Items[i].SubItems.Add(p_class[k].X.ToString());
                lv.Items[i].SubItems.Add(p_class[k].Y.ToString());
                lv.Items[i].SubItems.Add(p_class[k].Width.ToString());
                lv.Items[i].SubItems.Add(p_class[k].Height.ToString());
                lv.Items[i].SubItems.Add(p_class[k].Pic_X.ToString());
                lv.Items[i].SubItems.Add(p_class[k].Pic_Y.ToString());
                lv.Items[i].SubItems.Add(p_class[k].Pic_Width.ToString());
                lv.Items[i].SubItems.Add(p_class[k].Pic_Height.ToString());
                lv.Items[i].SubItems.Add(p_class[k].Pic_CreateDate); 
            }else{
                j++;
            }
        }
    }

    void Data_Check(){
        paint_list();
        bool a;

        Func<string, pic_data_class>find_p_class = (str_name) =>{
            foreach(pic_data_class pc in p_class){
                if(str_name == pc.Name)return pc;
            }
            return null;
        };

       
        for(int i = 0; i <= rows; i++){
            pic_data_class p = find_p_class(lv.Items[i].SubItems[0].Text);
            a = (p.Width < 1 || p.Height < 1 );
            a = a || (p.Pic_Width < 1 || p.Pic_Height < 1 );
            a = a || (p.Necessity && !System.IO.File.Exists(p.Address));
            if(a) lv.Items[i].BackColor = Color.Red;
        }
    }

    void show_Form02(pic_data_class p){
        string str = p.Name;
        bool flg = true;
        while(flg){
            Form02 fm2 = new Form02(p);
            fm2.ShowDialog(this);
            fm2.Dispose();
            if(p_class.Count(n => n != null && n.Name == p.Name) >= 2){
                p.Name = str;
            }else{
                flg = false;
            }
        }
        paint_list();
    }
}
