using EcomPulse.Repository;
using EcomPulse.Repository.BasketItemRepository;
using EcomPulse.Repository.BasketRepository;
using EcomPulse.Repository.CategoryRepository;
using EcomPulse.Repository.CreditCardRepository;
using EcomPulse.Repository.Entities;
using EcomPulse.Repository.OrderItemRepository;
using EcomPulse.Repository.OrderRepository;
using EcomPulse.Repository.PaymentRepository;
using EcomPulse.Repository.ProductRepository;
using EcomPulse.Service.AuthService;
using EcomPulse.Service.BasketService;
using EcomPulse.Service.CategoryService;
using EcomPulse.Service.CreditCardService;
using EcomPulse.Service.HostedService;
using EcomPulse.Service.OrderService;
using EcomPulse.Service.PaymentService;
using EcomPulse.Service.ProductService;
using EcomPulse.Service.RoleService;
using EcomPulse.Service.UnitOfWork;
using EcomPulse.Service.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//DATABASE CONFIGURATION
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

//IDENTITY CONFIGURATION
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 5;
}).AddEntityFrameworkStores<AppDbContext>();

//REPORSITORY DI CONFIGURATION
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IBasketItemRepository, BasketItemRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICreditCardRespository, CreditCardRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

//SERVICE DI CONFIGURATION
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ICreditCardService, CreditCardService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IAuthService, AuthService>();

//BACKSERVICE 
builder.Services.AddHostedService<ExpirationDateCheckerService>();

//JWT TOKEN CONFIGURATION
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration.GetSection("TokenOptions").GetValue<string>("Issuer"),
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("TokenOptions").GetValue<string>("SecretKey")!)),
        ValidateAudience = false,
    };
});


// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

app.Run();
