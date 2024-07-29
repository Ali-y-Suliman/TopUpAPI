using Microsoft.EntityFrameworkCore;
using TopUpAPI.Data;
using TopUpAPI.Repositories.TopUpBeneficiaryRepository;
using TopUpAPI.Repositories.TopUpOptionRepository;
using TopUpAPI.Repositories.TransactionsRepository;
using TopUpAPI.Repositories.UserRepository;
using TopUpAPI.Repositories.UsersTopUpBeneficiariesRepository;
using TopUpAPI.Services.TopUpBeneficiaryService;
using TopUpAPI.Services.TopUpOptionService;
using TopUpAPI.Services.UserBalanceService;
using TopUpAPI.Services.UserService;
using TopUpAPI.Services.UsersTopUpBeneficiariesService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<IUserBalanceService, UserBalanceService>("balanceBaseUrl" ,client => {
    client.BaseAddress = new Uri("http://localhost:5195/api/UserBalance/");
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ITopUpBeneficiaryService, TopUpBeneficiaryService>();
builder.Services.AddScoped<ITopUpBeneficiaryRepository, TopUpBeneficiaryRepository>();

builder.Services.AddScoped<ITopUpOptionService, TopUpOptionService>();
builder.Services.AddScoped<ITopUpOptionRepository, TopUpOptionRepository>();

builder.Services.AddScoped<IUsersTopUpBeneficiariesService, UsersTopUpBeneficiariesService>();
builder.Services.AddScoped<IUsersTopUpBeneficiariesRepository, UsersTopUpBeneficiariesRepository>();

builder.Services.AddScoped<ITransactionsRepository, TransactionsRepository>();

builder.Services.AddScoped<IUserBalanceService, UserBalanceService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("MyPolicy");

app.MapControllers();

app.Run();


