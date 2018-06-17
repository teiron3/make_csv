/*ログ出力用のメソッド */
using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

partial class Form01{
    //Console出力設定
    StreamWriter standard = new StreamWriter(Console.OpenStandardOutput(), System.Text.Encoding.GetEncoding("Shift_JIS"));

    //引数 msg をログファイルとコンソールに出力
    public void logwrite(string msg){
        //ログの出力先ファイルの設定(.\log\yyyyMMddHH.log)
        StreamWriter sw = new StreamWriter(".\\log\\makecsv_" + DateTime.Now.ToString("yyyyMMdd") + ".log",
                               true, System.Text.Encoding.GetEncoding("Shift_JIS"));

        //ログファイルに書き込み
        Console.SetOut(sw);
        Console.WriteLine(DateTime.Now.ToString("HH:mm ") + msg);
        sw.Flush();
        sw.Close();

        //コンソールに出力
        Console.SetOut(standard);
        Console.WriteLine(msg);
        standard.Flush();
    }

    //引数 msg をログファイルとコンソールとメッセージウィンドウに出力
    public void logwrite_msgbox(string msg){
        //ログの出力先ファイルの設定(.\log\yyyyMMddHH.log)
        StreamWriter sw = new StreamWriter(".\\log\\makecsv_" + DateTime.Now.ToString("yyyyMMddHH") + ".log",
                               true, System.Text.Encoding.GetEncoding("Shift_JIS"));

        //ログファイルに書き込み
        Console.SetOut(sw);
        Console.WriteLine(DateTime.Now.ToString("HH:mm ") + msg);
        sw.Flush();
        sw.Close();

        //コンソールに出力
        Console.SetOut(standard);
        Console.WriteLine(msg);
        standard.Flush();

        //メッセージウィンドウに出力
        MessageBox.Show(msg);
    }
}

