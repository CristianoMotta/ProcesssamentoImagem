using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
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
                        var imageEqualizada = MvcUtil.EqualizarImagem(imageOriginal);

                        //SALVAR IMAGEM EQUILIZADA
                        imageEqualizada.Save(imagemEquilizada, MvcUtil.GetExtensionFromImageFormat(Path.GetExtension(file.FileName).ToLower()));
                    }
                }
            }

            return View(modeloRetorno);
        }
    }
}