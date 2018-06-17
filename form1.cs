using System;
using System.Drawing;
using System.Windows.Forms;

partial class Form01 : Form
{
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
        btn1.Click += new EventHandler(btn1OnClick);

        Button btn2 = new Button();
        btn2.Parent = this;
        btn2.Location = new Point(10, 240);
        btn2.Text = "書き込み";
        btn2.Click += new EventHandler(btn2OnClick);

        Button btn3 = new Button();
        btn3.Parent = this;
        btn3.Location = new Point(300, 210);
        btn3.Width = 80;
        btn3.Height = 40;
        btn3.Text = "データチェック";
        btn3.Click += new EventHandler(btnClick_datacheck);

        Button btn4 = new Button();
        btn4.Parent = this;
        btn4.Location = new Point(390, 210);
        btn4.Width = 80;
        btn4.Height = 40;
        btn4.Text = "画像取得";
        btn4.Click += new EventHandler(btnClick_getpicture);

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

        //paint_list();

    }

    //描画
    void btn1OnClick(object sender, EventArgs e){
        paint_list();
    }

    //csvファイル書き込み
    void btn2OnClick(object sender, EventArgs e){
        write_csv();
    }

    void btnClick_getpicture(object sender, EventArgs e){
        if(lv.SelectedItems.Count < 1)return;

        bool a;
        a = (p_class[lv.SelectedItems[0].Index].Width < 1 || p_class[lv.SelectedItems[0].Index].Height < 1 );
        a = a || (p_class[lv.SelectedItems[0].Index].Pic_Width < 1 || p_class[lv.SelectedItems[0].Index].Pic_Height < 1 );
        if(a){MessageBox.Show("取得範囲の値を取り直してください");return;}

        if(p_class[lv.SelectedItems[0].Index].Necessity)
        pic_make.pic_create(p_class[lv.SelectedItems[0].Index]);
    }

    void btnClick_datacheck(object sender, EventArgs e){
        Data_Check();
    }

    void testkeypress(object sender, KeyEventArgs e){
        if(lv.SelectedItems.Count > 0){
            //if(a != Keys.X | a != Keys.Y | a != Keys.P)return;
            if(e.KeyCode == Keys.P){
                cb.Checked = !cb.Checked;return;
            }

            if(e.KeyCode == Keys.T){
                p_class[lv.SelectedItems[0].Index].Necessity = true;
                lv.SelectedItems[0].SubItems[1].Text = "True";
                return;
            }

            if(e.KeyCode == Keys.F){
                p_class[lv.SelectedItems[0].Index].Necessity = false;
                lv.SelectedItems[0].SubItems[1].Text = "False";
                return;
            }
            
            //チェックあり
            if(!cb.Checked){
                switch (e.KeyCode){
                    case Keys.S:
                        p_class[lv.SelectedItems[0].Index].X = Cursor.Position.X;
                        lv.SelectedItems[0].SubItems[2].Text = p_class[lv.SelectedItems[0].Index].X.ToString();
                        p_class[lv.SelectedItems[0].Index].Y = Cursor.Position.Y;
                        lv.SelectedItems[0].SubItems[3].Text = p_class[lv.SelectedItems[0].Index].Y.ToString();
                        break;

                    case Keys.E:
                        p_class[lv.SelectedItems[0].Index].Width = Cursor.Position.X - p_class[lv.SelectedItems[0].Index].X;
                        lv.SelectedItems[0].SubItems[4].Text = p_class[lv.SelectedItems[0].Index].Width.ToString();
                        p_class[lv.SelectedItems[0].Index].Height = Cursor.Position.Y - p_class[lv.SelectedItems[0].Index].Y;
                        lv.SelectedItems[0].SubItems[5].Text = p_class[lv.SelectedItems[0].Index].Height.ToString();
                        break;
                }
                return;
            }
            //チェックなし
            switch (e.KeyCode){
                case Keys.S:
                    p_class[lv.SelectedItems[0].Index].Pic_X = Cursor.Position.X;
                    lv.SelectedItems[0].SubItems[6].Text = p_class[lv.SelectedItems[0].Index].Pic_X.ToString();
                    p_class[lv.SelectedItems[0].Index].Pic_Y = Cursor.Position.Y;
                    lv.SelectedItems[0].SubItems[7].Text = p_class[lv.SelectedItems[0].Index].Pic_Y.ToString();
                    break;

                case Keys.E:
                    p_class[lv.SelectedItems[0].Index].Pic_Width = Cursor.Position.X - p_class[lv.SelectedItems[0].Index].Pic_X;
                    lv.SelectedItems[0].SubItems[8].Text = p_class[lv.SelectedItems[0].Index].Pic_Width.ToString();
                    p_class[lv.SelectedItems[0].Index].Pic_Height = Cursor.Position.Y - p_class[lv.SelectedItems[0].Index].Pic_Y;
                    lv.SelectedItems[0].SubItems[9].Text = p_class[lv.SelectedItems[0].Index].Pic_Height.ToString();
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
            if(p_class[i].Name != null){
                lv.Items.Add(p_class[i].Name); 
                lv.Items[i].SubItems.Add(p_class[i].Necessity.ToString());
                lv.Items[i].SubItems.Add(p_class[i].X.ToString());
                lv.Items[i].SubItems.Add(p_class[i].Y.ToString());
                lv.Items[i].SubItems.Add(p_class[i].Width.ToString());
                lv.Items[i].SubItems.Add(p_class[i].Height.ToString());
                lv.Items[i].SubItems.Add(p_class[i].Pic_X.ToString());
                lv.Items[i].SubItems.Add(p_class[i].Pic_Y.ToString());
                lv.Items[i].SubItems.Add(p_class[i].Pic_Width.ToString());
                lv.Items[i].SubItems.Add(p_class[i].Pic_Height.ToString());
                lv.Items[i].SubItems.Add(p_class[i].Pic_CreateDate); 
            }else{
                j++;
            }
        }
    }

    void Data_Check(){
        paint_list();
        bool a;
        for(int i = 0; i <= rows; i++){
            a = (p_class[i].Width < 1 || p_class[i].Height < 1 );
            a = a || (p_class[i].Pic_Width < 1 || p_class[i].Pic_Height < 1 );
            a = a || (p_class[i].Necessity && !System.IO.File.Exists(p_class[i].Address));
            if(a) lv.Items[i].BackColor = Color.Red;
        }
    }

}
