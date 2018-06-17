using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

partial class Form01
{
    // csvファイルのデータを格納するデータ型クラス宣言
    public pic_data_class[] p_class;

    // csvファイル名の設定
    public string csv_file{get{return "csv_file.csv";}}

    // データの数(csvファイルの行数)設定 
    public int rows = 0;

    //csvファイルを読み込んでデータ型クラスに入れるメソッド
    //正常読み込みできなかったときに false を返す 
    public bool read_csv(){

        //正常に読み込みできたかどうかを判定用
        bool flg = true;
        this.rows = 0;
        this.p_class = new pic_data_class[200];

        string file_path = csv_file;
        if(!System.IO.File.Exists(file_path)) {
            logwrite_msgbox("error:設定用のcsvファイルがありません");
            stop_flg = true;
            return false;
        }

        System.IO.StreamReader text_strm = new System.IO.StreamReader(file_path, System.Text.Encoding.GetEncoding("shift_jis"));
        while(text_strm.Peek() >= 0){
            int i = this.rows;
            this.p_class[i] = new pic_data_class();
            string[] test_str;
            string s = text_strm.ReadLine();
            test_str = s.Split(',');
            
            this.p_class[i].Name = test_str[0];
            if(test_str.Length >= 2) this.p_class[i].Set_Necessity = test_str[1];
            if(test_str.Length >= 3) this.p_class[i].X = int.Parse(test_str[2]);
            if(test_str.Length >= 4) this.p_class[i].Y = int.Parse(test_str[3]);
            if(test_str.Length >= 5) this.p_class[i].Width = int.Parse(test_str[4]);
            if(test_str.Length >= 6) this.p_class[i].Height = int.Parse(test_str[5]);
            if(test_str.Length >= 7) this.p_class[i].Pic_X = int.Parse(test_str[6]);
            if(test_str.Length >= 8) this.p_class[i].Pic_Y = int.Parse(test_str[7]);
            if(test_str.Length >= 9) this.p_class[i].Pic_Width = int.Parse(test_str[8]);
            if(test_str.Length >= 10) this.p_class[i].Pic_Height = int.Parse(test_str[9]);
            if(test_str.Length >= 11) this.p_class[i].Pic_CreateDate = test_str[10];
            
            if(p_class[i].Necessity == true)
            {
                if(System.IO.File.Exists(p_class[i].Address))
                    this.p_class[i].Pic_data = new Bitmap(p_class[i].Address);
                else
                    logwrite("error:" + p_class[i].Name + "のbmpファイルがありません");
            }
            this.rows++;
        }

        this.rows = this.rows - 1;
        text_strm.Close();
        return flg;
    }

    /*クラス内のデータをcsvファイルに書き込むメソッド */
    public void write_csv(){
        //設定用csvファイルの有無の確認、有ればバックアップを作成する
        if(System.IO.File.Exists(csv_file) == true){
            logwrite("以前のファイルをバックアップフォルダに移します");
            if(System.IO.Directory.Exists(@".\csv_bk") == false)System.IO.Directory.CreateDirectory(@".\csv_bk");
            System.IO.File.Move(csv_file, @".\csv_bk\" + System.DateTime.Now.ToString("yyMMddHHmmss") + csv_file);
        }

        logwrite("新しい設定ファイルを作成します");

        //設定用csvファイルを作成
        System.IO.StreamWriter text_strm = new System.IO.StreamWriter(csv_file, false, System.Text.Encoding.GetEncoding("shift_jis"));
        int j = 0;
        for(int i = 0; i <= this.rows; i++){
            pic_data_class p = this.p_class[i -j];
            if(p.Name != null){
                text_strm.WriteLine(p.Name + "," + p.Necessity + "," + p.X + "," + p.Y + "," + p.Width + "," + p.Height + ","
                + p.Pic_X + "," + p.Pic_Y + "," + p.Pic_Width + "," + p.Pic_Height + "," + p.Pic_CreateDate);
            }
        }
        text_strm.Close();
    }


}

class pic_data_class
{
    bool need;

    public string Name{get;set;}
    public bool Necessity{get{return need;}set{need = value;}}
    public string Set_Necessity{get{return "";} set{ if(value == "True") need = true;else need = false;}}
    public int X{get;set;}
    public int Y{get;set;}
    public int Width{get;set;}
    public int Height{get;set;}
    public int Pic_X{get;set;}
    public int Pic_Y{get;set;}
    public int Pic_Width{get;set;}
    public int Pic_Height{get;set;}
    public string Pic_CreateDate{get;set;}
    public Bitmap Pic_data{get;set;}
    public string Address{get{return @".\pic_folder\" + this.Name + ".bmp";}}

}

class pic_make
{
    static public void pic_create(pic_data_class obj)
    {
        Bitmap bmp = new Bitmap( obj.Pic_Width, obj.Pic_Height);
        Graphics g = Graphics.FromImage(bmp);
        g.CopyFromScreen( new Point( obj.Pic_X, obj.Pic_Y), new Point( 0, 0), bmp.Size);

        g.Dispose();
        bmp.Save(obj.Address);
        
    }
}