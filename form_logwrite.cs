/*���O�o�͗p�̃��\�b�h */
using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

partial class Form01{
    //Console�o�͐ݒ�
    StreamWriter standard = new StreamWriter(Console.OpenStandardOutput(), System.Text.Encoding.GetEncoding("Shift_JIS"));

    //���� msg �����O�t�@�C���ƃR���\�[���ɏo��
    public void logwrite(string msg){
        //���O�̏o�͐�t�@�C���̐ݒ�(.\log\yyyyMMddHH.log)
        StreamWriter sw = new StreamWriter(".\\log\\makecsv_" + DateTime.Now.ToString("yyyyMMdd") + ".log",
                               true, System.Text.Encoding.GetEncoding("Shift_JIS"));

        //���O�t�@�C���ɏ�������
        Console.SetOut(sw);
        Console.WriteLine(DateTime.Now.ToString("HH:mm ") + msg);
        sw.Flush();
        sw.Close();

        //�R���\�[���ɏo��
        Console.SetOut(standard);
        Console.WriteLine(msg);
        standard.Flush();
    }

    //���� msg �����O�t�@�C���ƃR���\�[���ƃ��b�Z�[�W�E�B���h�E�ɏo��
    public void logwrite_msgbox(string msg){
        //���O�̏o�͐�t�@�C���̐ݒ�(.\log\yyyyMMddHH.log)
        StreamWriter sw = new StreamWriter(".\\log\\makecsv_" + DateTime.Now.ToString("yyyyMMddHH") + ".log",
                               true, System.Text.Encoding.GetEncoding("Shift_JIS"));

        //���O�t�@�C���ɏ�������
        Console.SetOut(sw);
        Console.WriteLine(DateTime.Now.ToString("HH:mm ") + msg);
        sw.Flush();
        sw.Close();

        //�R���\�[���ɏo��
        Console.SetOut(standard);
        Console.WriteLine(msg);
        standard.Flush();

        //���b�Z�[�W�E�B���h�E�ɏo��
        MessageBox.Show(msg);
    }
}

