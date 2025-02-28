using System.Net;
using System.Security.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Login.TokenServices;
using Login.ViewModels;
using Login.Entity;
using Login.Infra.Context;
using Login.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace Login.API.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    private readonly TokenService _tokenService; //Pablo isso aqui serve para gerar um dependência do obj
    private readonly PasswordHash _hash;
    private readonly IRepository<Register> _registerRepository;
    private readonly ParkingContext _Context;
    public AccountController(TokenService tokenService, PasswordHash hash, IRepository<Register> registerRepository, ParkingContext Context)
    {
        _tokenService = tokenService;
        _hash = hash;
        _registerRepository = registerRepository;
        _Context = Context;
    }

    [HttpPost("v1/accounts/register")]
    public async Task<ResponseViewModel> Register([FromBody] RegisterViewModel model)
    {
        var response = new ResponseViewModel();
        try
        {
            var user = await _registerRepository.Get().FirstOrDefaultAsync(x => x.RA == model.RA);
            if(user != null) return response.GetResponse("Usuário já registrado", HttpStatusCode.BadRequest);
            var pass = _hash.HashPassword(model.Password);

            var resgister = new Register
            {
                Name = model.Name,
                Email = model.Email,
                Password = pass,
                RA = model.RA
            };

            await _registerRepository.AddAsync(resgister);
            await _Context.SaveChangesAsync();
            return response.GetResponse("Registrado com sucesso.", HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return response.GetResponse(ex.Message, HttpStatusCode.BadRequest);
        }
    }

    //[AllowAnonymous]
    [HttpPost("v1/accounts/login")]
    public async Task<ResponseViewModel> SignIn([FromBody] LoginViewModel model)
    {
        var response = new ResponseViewModel();

        try
        {
            var user = await _registerRepository.Get().FirstOrDefaultAsync(f => f.RA == model.Sing || f.Email == model.Sing);

            if (user is null) return response.GetResponse("Usuário Não existe", HttpStatusCode.NoContent);

            var verif = _hash.VerifyPassword(model.Password, user.Password);
            if (verif == false) return response.GetResponse("Senha Inválida", HttpStatusCode.BadRequest);

            var token = _tokenService.GenerationToken(user.Email, user.Name);

            return response.GetResponse(new { Token = token }, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return response.GetResponse(ex.Message, HttpStatusCode.BadRequest);
        }
    }

    [HttpPost("v1/accounts/reset")]
    public async Task<ResponseViewModel> Reset([FromBody] ReseViewModel model)
    {
        var response = new ResponseViewModel();

        try
        {
            var user = await _registerRepository.Get().FirstOrDefaultAsync(f => f.Email == model.Email);

            if (user is null) return response.GetResponse("Usuário Não existe", HttpStatusCode.NoContent);

            var smtpClient = new SmtpClient("smtp.office365.com", 587)
            {
                Port = 587,
                Credentials = new NetworkCredential(),
                EnableSsl = true,
                UseDefaultCredentials = false,

            };
            var guid = Guid.NewGuid();
            var senha = guid.ToString("N").Substring(0, 10);
            var mailMessage = new MailMessage
            {
                From = new MailAddress("joaopedrodxz22@outlook.com", "ae"),
                Subject = "Gerador de Senha",
                Body = $"essa é sua senha:{senha}",
                IsBodyHtml = true,
            };
            mailMessage.To.Add(user.Email);
            user.Password = senha;

            smtpClient.Send(mailMessage);
            await _Context.SaveChangesAsync();

            return response.GetResponse("E-mail enviado", HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return response.GetResponse(ex.Message, HttpStatusCode.BadRequest);
        }
    }
}
