using Microsoft.AspNetCore.Http;
using System;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Market.Services;
using Market.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Market.Services
{
    public class ImagemService
    {
        public static async Task<string> SalvarImagemProduto(IFormFile imagem)
        {
            try
            {
                List<string> PermittedFileTypes = new List<string> {
                        "image/jpeg",
                        "image/png",
                        "image/jpg"
                };

                if (PermittedFileTypes.Contains(imagem.ContentType))
                {

                    var PathDirectory = $"images/ImagemProduto/";

                    Directory.CreateDirectory(string.Concat(@"wwwroot/", PathDirectory));

                    string nome = DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(imagem.FileName).ToLower();
                                        
                    using (var stream = new FileStream(string.Concat(@"wwwroot/", PathDirectory, nome), FileMode.Create))
                    {
                        await imagem.CopyToAsync(stream);
                    }

                    string PathFile;

                    PathFile = string.Concat(@"~/", PathDirectory, nome);
                    return PathFile;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }

        }

    }

}


