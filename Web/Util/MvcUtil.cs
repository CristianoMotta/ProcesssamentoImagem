using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Web.Util
{
    public static class MvcUtil
    {
        /// <summary>
        /// Metodo recebe a extensão de uma imagem e identifica o tipo ImageFormat da extensão.
        /// </summary>
        /// <param>String de extensao do arquivo</param>
        /// <returns>Retorna um ImagemFormat da bibloteca para salvar imagem corretamente.</returns>
        public static ImageFormat RecuperarFormatoImagem(string extensao)
        {
            switch (extensao)
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

        /// <summary>
        /// Metodo recebe o Stream da imagem e verifica se o conteudo do stream e uma imagem.
        /// </summary>
        /// <param>Stream de um arquivo</param>
        /// <returns>Retorna true ou false</returns>
        public static bool ValidarArquivoImagem(Stream arquivo)
        {
            try
            {
                var bitmap = Bitmap.FromStream(arquivo);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}