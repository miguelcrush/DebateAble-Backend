

using DebateAble.Api;
using DebateAble.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Okta.AspNetCore;
using DebateAble.Api.Middleware;
using DebateAble.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DebateAbleDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddAuthentication(options =>
{
	options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
	string authorityEndpoint = $"{builder.Configuration["Okta:OktaDomain"]}/{builder.Configuration["Okta:AuthorizationServerId"]}";

	options.Authority = authorityEndpoint;
	options.Audience = builder.Configuration["Okta:Audience"];
	options.RequireHttpsMetadata = true;

});


builder.Services.AddAuthorization();

builder.Services.AddMvc(options =>
{
	var policy = new AuthorizationPolicyBuilder()
		.RequireAuthenticatedUser()
		.Build();
	options.Filters.Add(new AuthorizeFilter(policy));
});


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

/* CUSTOM SERVICES */

builder.Services.AddAutoMapper(System.Reflection.Assembly.GetExecutingAssembly());
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IAppUserService, AppUserService>();
builder.Services.AddScoped<IDebateService, DebateService>();
builder.Services.AddScoped<ISluggerService, SluggerService>();

/* END CUSTOM SERVICES */

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

if (builder.Environment.IsDevelopment())
{
	Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
}

app.UseAppUserCapture();

app.Run();
