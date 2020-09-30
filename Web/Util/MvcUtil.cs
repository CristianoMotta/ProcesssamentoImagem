using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Web.Util
{
    public static class MvcUtil
    {
        //A entrada é um vetor com uma linha de pixels da imagem e a saída é a mesma linha já equalizada.
        public static Bitmap EqualizarImagem(Bitmap img)
        {
            int largura = img.Width;
            int altura = img.Height;

            BitmapData sd = img.LockBits(new Rectangle(0, 0, largura, altura), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            int bytes = sd.Stride * sd.Height;
            byte[] buffer = new byte[bytes];
            byte[] result = new byte[bytes];

            Marshal.Copy(sd.Scan0, buffer, 0, bytes);
            img.UnlockBits(sd);
            int current = 0;

            double[] pn = new double[256];

            for (int pi = 0; pi < bytes; pi += 4)
            {
                pn[buffer[pi]]++;
            }
            for (int prob = 0; prob < pn.Length; prob++)
            {
                pn[prob] /= (largura * altura);
            }
            for (int y = 0; y < altura; y++)
            {
                for (int x = 0; x < largura; x++)
                {
                    current = y * sd.Stride + x * 4;
                    double sum = 0;
                    for (int i = 0; i < buffer[current]; i++)
                    {
                        sum += pn[i];
                    }
                    for (int c = 0; c < 3; c++)
                    {
                        result[current + c] = (byte)Math.Floor(255 * sum);
                    }
                    result[current + 3] = 255;
                }
            }

            Bitmap res = new Bitmap(largura, altura);

            BitmapData rd = res.LockBits(new Rectangle(0, 0, largura, altura), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(result, 0, rd.Scan0, bytes);

            res.UnlockBits(rd);

            return res;
        }

        //A entrada é um vetor com uma linha de pixels da imagem e a saída é a mesma linha já equalizada.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        //private double[] EquilizarNoAlgoritmoDaP1(double[] vetorEntrada)
        //{
        //    int current = 0;

        //    for (int pi = 0; pi < bytes; pi += 4)
        //    {
        //        pn[buffer[pi]]++;
        //    }
        //    for (int prob = 0; prob < pn.Length; prob++)
        //    {
        //        pn[prob] /= (largura * altura);
        //    }
        //    for (int y = 0; y < altura; y++)
        //    {
        //        for (int x = 0; x < largura; x++)
        //        {
        //            current = y * sd.Stride + x * 4;
        //            double sum = 0;
        //            for (int i = 0; i < buffer[current]; i++)
        //            {
        //                sum += pn[i];
        //            }
        //            for (int c = 0; c < 3; c++)
        //            {
        //                result[current + c] = (byte)Math.Floor(255 * sum);
        //            }
        //            result[current + 3] = 255;
        //        }
        //    }

        //    //MESMA LINHA DE ENTRADA NA SAIDA, POREM EQUALIZADA
        //    return vetorEntrada;
        //}

        public static ImageFormat GetExtensionFromImageFormat(string extension)
        {
            switch (extension)
            {
                case ".jpg":
                    return ImageFormat.Jpeg;
                case ".png":
                    return ImageFormat.Png;
                case ".gif":
                    return ImageFormat.Gif;
                case ".bmp":
                    return ImageFormat.Bmp;
                case ".tif":
                    return ImageFormat.Tiff;
                case ".ico":
                    return ImageFormat.Icon;
                case ".emf":
                    return ImageFormat.Emf;
                case ".exif":
                    return ImageFormat.Wmf;
                case ".wmf":
                    return ImageFormat.Jpeg;
                default:
                    return null;
            }

        }
    }
}