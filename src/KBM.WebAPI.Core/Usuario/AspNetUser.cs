using Microsoft.AspNetCore.Http;

namespace KBM.WebAPI.Core.Usuario;

public class AspNetUser : IAspNetUser
{
	private readonly IHttpContextAccessor _accessor;

	public AspNetUser(IHttpContextAccessor accessor)
	{
		_accessor = accessor;
	}

	public bool EstaAutenticado() => _accessor.HttpContext!.User.Identity!.IsAuthenticated;
}
