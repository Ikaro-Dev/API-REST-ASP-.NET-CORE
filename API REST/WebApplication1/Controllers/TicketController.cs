using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
	[ApiController]
	[Route("api/Tickets")]
	public class TicketController : ControllerBase
	{
		#region Propriedades

		private readonly DataContext _context;

		#endregion

		#region Construtores
		public TicketController(DataContext context)
		{
			_context = context;
		}

		#endregion

		#region Métodos

		[HttpGet]
		[Authorize]
		public async Task<ActionResult<List<Ticket>>> Get()
		{
			var ticket = await _context.Tickets.ToListAsync();

			return ticket;
		}


		[HttpPut("{id}")]
		[Authorize]
		public async Task<IActionResult> Put(int id, Ticket ticket)
		{
			//Verifica se o ID passado por parametro é diferente do ID do Ticket
			if (id != ticket.Id)
			{
				return BadRequest();
			}
			_context.Entry(ticket).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return NoContent();
		}


		[HttpPost]
		[Authorize]
		public async Task<ActionResult<Ticket>> Post(
			[FromBody] Ticket model)
		{
			_context.Tickets.Add(model);
			await _context.SaveChangesAsync();
			return model;
		}


		[HttpDelete]
		[Authorize]
		public bool Delete(int id)
		{
			//Recebe o ticket cujo id foi requerido no parametro
			var obj = _context.Tickets.Where(T => T.Id == id).FirstOrDefault();

			try
			{
				//Verificando se o ID passado por parametro é válido
				if (obj.Id == id)
				{
					_context.Remove(obj);
					_context.SaveChanges();
				}
			}
			catch (Exception)
			{
				throw new Exception("ERROR: ID Inválido");
			}

			//Retorno do método caso o ID for igual ao requerido
			return true;
		}

		[HttpPost]
		[Route("login")]
		[AllowAnonymous]
		public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
		{
			var user = UserRepository.Get(model.UserName, model.Password);

			if (user == null)
				return NotFound(new { message = "Usuário ou senha inválido" });

			var token = TokenService.GerarToken(user);
			user.Password = "";
			return new
			{
				user = user,
				token = token
			};
		}

		#endregion
	}
}