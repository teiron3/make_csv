/*�ݒ�t�@�C���쐬�p���C���N���X */
using System;
using System.Drawing;
using System.Windows.Forms;


class main
{
    static void Main()
    {
        Form01 m_f = new Form01();
        //log�t�H���_�̍쐬
        if(!System.IO.Directory.Exists("log")){
            System.IO.Directory.CreateDirectory("log");
            m_f.logwrite("�ulog�v������܂���B\n�쐬���܂�");
        }

        //����p�摜�̕ۑ��t�H���_�̊m�F ������΃t�H���_���쐬����
        if(!System.IO.Directory.Exists("pic_folder")){
            m_f.logwrite_msgbox("�upic_folder�v������܂���B�t�H���_���쐬���܂��B");
            System.IO.Directory.CreateDirectory("pic_folder");
        }

        //�ݒ�t�@�C���̓ǂݍ��݂Ɗm�F ������΃v���Z�X�I��
        if(!m_f.read_csv()){
            m_f.logwrite_msgbox("csv�t�@�C��������܂���\n�I�����܂�");return;
        }

        //�t�H�[���𗧂��グ��
        Application.Run(m_f);
    }
}


