/*設定ファイル作成用メインクラス */
using System;
using System.Drawing;
using System.Windows.Forms;


class main
{
    static void Main()
    {
        Form01 m_f = new Form01();
        //logフォルダの作成
        if(!System.IO.Directory.Exists("log")){
            System.IO.Directory.CreateDirectory("log");
            m_f.logwrite("「log」がありません。\n作成します");
        }

        //判定用画像の保存フォルダの確認 無ければフォルダを作成する
        if(!System.IO.Directory.Exists("pic_folder")){
            m_f.logwrite_msgbox("「pic_folder」がありません。フォルダを作成します。");
            System.IO.Directory.CreateDirectory("pic_folder");
        }

        //設定ファイルの読み込みと確認 無ければプロセス終了
        if(!m_f.read_csv()){
            m_f.logwrite_msgbox("csvファイルがありません\n終了します");return;
        }

        //フォームを立ち上げる
        Application.Run(m_f);
    }
}


