using Market.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Market.Services
{
    public class UsuarioService
    {
        //readonly IDispositivoConectadoService _DispositivoConectadoService;

        //public async Task GerarCookie(Usuario usuario, bool lembrarSenha = false)
        //{
        //    var dispositivoAtual = _DispositivoConectadoService.GetDispositivoAtual();
        //    var isConectado = _DispositivoConectadoService.IsConectado(usuario.UsuarioId);
        //    if (!isConectado)
        //        _DispositivoConectadoService.ConectaDispositivo(usuario.UsuarioId);
        //    if (usuario.Imagem == null)
        //        usuario.Imagem = new Imagem();
        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, usuario.NomeUsuario),
        //        new Claim(ClaimTypes.Email, usuario.Email),
        //        new Claim(ClaimTypes.DateOfBirth, usuario.DataNascimento.ToShortDateString()),
        //        new Claim("ImagemPerfil", string.IsNullOrEmpty(usuario.Imagem.Caminho) ? ImagemHelper.CaminnhoImagemPerfilDefault : usuario.Imagem.Caminho),
        //        new Claim("DispositivoConectado", dispositivoAtual.ToString()),
        //        new Claim(ClaimsHelper.UsuarioId, usuario.UsuarioId.ToString()),
        //        new Claim(ClaimTypes.Role, Enum.GetName(typeof(PerfilUsuarioEnum),usuario.PerfilUsuario))
        //    };

        //    var claimsIdentity = new ClaimsIdentity(
        //        claims, CookieAuthenticationDefaults.AuthenticationScheme);

        //    var authProperties = (lembrarSenha) ?
        //    new AuthenticationProperties
        //    {
        //        IsPersistent = true
        //    } : new AuthenticationProperties
        //    {
        //        IsPersistent = true,
        //        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
        //    };

        //    await _HttpContextAccessor.HttpContext.SignInAsync(
        //        CookieAuthenticationDefaults.AuthenticationScheme,
        //        new ClaimsPrincipal(claimsIdentity),
        //        authProperties);
        //}
    }
}
