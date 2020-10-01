using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Web.Util
{
    public static class AlgoritmoEqualizacao
    {
        /// <summary>
        /// Metodo responsavel pela equalização da imagem
        /// </summary>
        /// <param>Imagem original</param>
        /// <returns>Retonar imagem de equalizada</returns>
        public static Bitmap EqualizarImagem(Bitmap img)
        {
            int largura = img.Width;
            int altura = img.Height;

            BitmapData sd = img.LockBits(new Rectangle(0, 0, largura, altura), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            int bytes = sd.Stride * sd.Height;
            byte[] vetorDePixels = new byte[bytes];

            Marshal.Copy(sd.Scan0, vetorDePixels, 0, bytes);
            img.UnlockBits(sd);

            //INICIO ALGORITMO
            int atual = 0;

            byte[] resultadoEqualizacao = new byte[bytes];
            double[] pn = new double[256];

            for (int pi = 0; pi < bytes; pi += 4)
            {
                pn[vetorDePixels[pi]]++;
            }

            for (int prob = 0; prob < pn.Length; prob++)
            {
                pn[prob] /= (largura * altura);
            }

            for (int y = 0; y < altura; y++)
            {
                for (int x = 0; x < largura; x++)
                {
                    atual = y * sd.Stride + x * 4;
                    double sum = 0;
                    for (int i = 0; i < vetorDePixels[atual]; i++)
                    {
                        sum += pn[i];
                    }
                    for (int c = 0; c < 3; c++)
                    {
                        resultadoEqualizacao[atual + c] = (byte)Math.Floor(255 * sum);
                    }
                    resultadoEqualizacao[atual + 3] = 255;
                }
            }

            //FIM ALGORITMO P1

            //GERAR IMAGEM NOVA
            Bitmap res = new Bitmap(largura, altura);
            BitmapData rd = res.LockBits(new Rectangle(0, 0, largura, altura), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(resultadoEqualizacao, 0, rd.Scan0, bytes);

            res.UnlockBits(rd);

            return res;
        }
    }
}