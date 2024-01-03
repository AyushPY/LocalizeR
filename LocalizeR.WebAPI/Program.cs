using LocalizeR.Core.Identity;
using LocalizeR.Core.ServiceContracts;
using LocalizeR.Core.Services;
using LocalizeR.Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.UseCors("ReactAppPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
