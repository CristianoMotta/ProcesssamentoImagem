using System;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Web.Util;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Metodo recebe a imagem original, salvar essa imagem em uma pasta do projeto e envia para equilização.
        /// </summary>
        /// <param>Imagem</param>
        /// <returns>Retorna 2 imagens, a imagem original e imagem equalizada.</returns>
        [HttpPost]
        public ActionResult Index(UploadViewModel model)
        {
            UploadViewModel modeloRetorno = null;

            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];

                    if (file.ContentLength > 0)
                    {
                        if (MvcUtil.ValidarArquivoImagem(file.InputStream))
                        {
                            var arquivoOriginal = Path.GetFileName(file.FileName);

                            modeloRetorno = new UploadViewModel()
                            {
                                ImagemOriginal = $"img_{DateTime.Now.Ticks}{Path.GetExtension(file.FileName).ToLower()}",
                                ImagemEquilizada = $"img_{DateTime.Now.Ticks}_equilizada{Path.GetExtension(file.FileName).ToLower()}"
                            };

                            string imagemOriginal = Path.Combine(Server.MapPath("~/Uploads"), modeloRetorno.ImagemOriginal);
                            string imagemEquilizada = Path.Combine(Server.MapPath("~/Uploads"), modeloRetorno.ImagemEquilizada);

                            //SALVAR IMAGEM ORIGINAL
                            file.SaveAs(imagemOriginal);

                            Bitmap imageOriginal = new Bitmap(file.InputStream);

                            //EQUILIZAR 
                            var imageEqualizada = AlgoritmoEqualizacao.EqualizarImagem(imageOriginal);

                            //SALVAR IMAGEM EQUILIZADA
                            imageEqualizada.Save(imagemEquilizada, MvcUtil.RecuperarFormatoImagem(Path.GetExtension(file.FileName).ToLower()));
                        }
                        else
                        {
                            modeloRetorno.Mensagem = "O arquivo não e do tipo imagem.";
                        }
                    }
                }
            }

            return View(modeloRetorno);
        }
    }
}