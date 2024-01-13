using LocalizeR.Core.Identity;
using LocalizeR.Core.ServiceContracts;
using LocalizeR.Core.Services;
using LocalizeR.Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using RepositoryContracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
}
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactAppPolicy", policybuilder =>
    {
        policybuilder.WithOrigins("http://localhost:3000").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequiredLength = 5;

}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders().AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>().AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();
builder.Services.Add(new ServiceDescriptor(
    typeof(ISimilarityCalculator),
    typeof(SimilarityCalculator),
    ServiceLifetime.Transient

    ));
builder.Services.Add(new ServiceDescriptor(
    typeof(IJobSequencer),
    typeof(JobSequencer),
    ServiceLifetime.Transient));
builder.Services.Add(new ServiceDescriptor(
    typeof(IRequestClassifier),
    typeof(RequestClassifier), ServiceLifetime.Transient));
builder.Services.AddTransient<IJwtService, JwtService>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddAuthorization();
builder.Services.AddTransient<IRatingsStats, RatingStatistics>();
builder.Services.AddTransient<IUserRequestsRepository, UserRequestsRepository>();
builder.Services.AddTransient<IServiceProviderInfo, ServiceProviderInfoRepository>();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.UseCors("ReactAppPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
