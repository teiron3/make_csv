using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Linq;

class pic_hit{
    /* 引数 obj に記録された bmpデータ が指定の場所にあればtrueを返す */
    public bool pic_con(pic_data_class obj){
        //引数の obj が null の場合、false を返す
        if(obj == null)return false;

        Bitmap src = new Bitmap( obj.Pic_Width, obj.Pic_Height);
        
        Graphics g = Graphics.FromImage(src);
        g.CopyFromScreen( new Point( obj.Pic_X , obj.Pic_Y), new Point( 0, 0), src.Size);
        g.Dispose();

        BitmapData srcData = src.LockBits(new Rectangle( 0, 0, src.Width, src.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
        BitmapData bmpData = obj.Pic_data.LockBits(new Rectangle( 0, 0, obj.Pic_data.Width, obj.Pic_data.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

        byte[] srcPix, bmpPix;
        srcPix = new byte[ src.Width * src.Height * 4];
        bmpPix = new byte[ obj.Pic_data.Width * obj.Pic_data.Height * 4];

        Marshal.Copy( srcData.Scan0, srcPix, 0, srcPix.Length);
        Marshal.Copy( bmpData.Scan0, bmpPix, 0, bmpPix.Length);

        bool agree = false;

        if( srcPix.SequenceEqual( bmpPix) == true) agree = true;
        src.UnlockBits(srcData);
        obj.Pic_data.UnlockBits(bmpData);
        return agree;
    }

    /* 引数 obj に記録されたbmpデータが指定の範囲にあればtrueを返す */
    public bool pic_search(pic_data_class obj){
        //引数の obj が null の場合、false を返す
        if(obj == null)return false;
        
        Bitmap src = new Bitmap( obj.Width, obj.Height);
        
        Graphics g = Graphics.FromImage(src);
        g.CopyFromScreen( new Point( obj.X , obj.Y), new Point( 0, 0), src.Size);
        g.Dispose();

        BitmapData srcData = src.LockBits(new Rectangle( 0, 0, src.Width, src.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
        BitmapData bmpData = obj.Pic_data.LockBits(new Rectangle( 0, 0, obj.Pic_data.Width, obj.Pic_data.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

        byte[] srcPix, bmpPix, srcLine, bmpLine;
        srcPix = new byte[ src.Width * src.Height * 4];
        bmpPix = new byte[ obj.Pic_data.Width * obj.Pic_data.Height * 4];

        srcLine = new byte[ obj.Pic_data.Width * 4];
        bmpLine = new byte[ obj.Pic_data.Width * 4];

        Marshal.Copy( srcData.Scan0, srcPix, 0, srcPix.Length);
        Marshal.Copy( bmpData.Scan0, bmpPix, 0, bmpPix.Length);

        bool agree = true;

        for( int y = 0; y < src.Height - obj.Pic_data.Height; y++)
        {
            for( int x = 0; x < src.Width - obj.Pic_data.Width; x++)
            {
                agree = true;
                for( int yy = 0; yy < obj.Pic_data.Height; yy++)
                {
                    System.Array.Copy( srcPix, ( x + ( yy + y) * src.Width) * 4, srcLine, 0, ( srcLine.Length));
                    System.Array.Copy( bmpPix, yy * obj.Pic_data.Width * 4, bmpLine, 0, ( bmpLine.Length));

                    if( srcLine.SequenceEqual( bmpLine) == false) agree = false;
                    if( agree == false) break;
                }
                if( agree)
                {
                    break;
                }
            }
            if(agree) break;
        }

        src.UnlockBits(srcData);
        obj.Pic_data.UnlockBits(bmpData);
        return agree;
    }

    //画像を取得して参照したpic_data_class.Pic_dataに渡す
    public void pic_get(pic_data_class obj){
        obj.Pic_data = new Bitmap( obj.Pic_Width, obj.Pic_Height);
        Graphics g = Graphics.FromImage(obj.Pic_data);
        g.CopyFromScreen( new Point( obj.Pic_X , obj.Pic_Y), new Point( 0, 0),  obj.Pic_data.Size);
        g.Dispose();
    }

}

