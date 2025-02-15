using BusinessLogicLayer.AuthService;
using BusinessLogicLayer.BasketService;
using BusinessLogicLayer.CategoryService;
using BusinessLogicLayer.CreditCardService;
using BusinessLogicLayer.HostedService;
using BusinessLogicLayer.OrderService;
using BusinessLogicLayer.PaymentService;
using BusinessLogicLayer.ProductService;
using BusinessLogicLayer.RoleService;
using BusinessLogicLayer.UnitOfWork;
using BusinessLogicLayer.UserService;
using DataAccessLayer;
using DataAccessLayer.BasketItemRepository;
using DataAccessLayer.BasketRepository;
using DataAccessLayer.CategoryRepository;
using DataAccessLayer.CreditCardRepository;
using DataAccessLayer.Entities;
using DataAccessLayer.OrderItemRepository;
using DataAccessLayer.OrderRepository;
using DataAccessLayer.PaymentRepository;
using DataAccessLayer.ProductRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using Scalar.AspNetCore;
using System.Text;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Host.UseNLog();

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
}
).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration.GetSection("SignIn_Token").GetValue<string>("Issuer"),
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("SignIn_Token").GetValue<string>("SecretKey")!)),
        ValidateAudience = false,
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var token = context.Request.Headers["Authorization"].ToString();
            if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                token = token.Substring("Bearer ".Length).Trim();
            }

            // Token�n "SigninToken" i�in uygunlu�unu kontrol et
            //if (!context.HttpContext.Request.Headers.ContainsKey("AuthenticationScheme") ||
            //    context.HttpContext.Request.Headers["AuthenticationScheme"] == JwtBearerDefaults.AuthenticationScheme)
            //{
            //    context.Token = token;
            //}
            context.Token = token;
            return Task.CompletedTask;
        }
    };
}).AddJwtBearer("Client_Token", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration.GetSection("Client_Token").GetValue<string>("Issuer"),
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Client_Token").GetValue<string>("ClientSecretKey")!)),
        ValidateAudience = false,
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var token = context.Request.Headers["Authorization"].ToString();
            if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                token = token.Substring("Bearer ".Length).Trim();
            }

            // Tokenun "SigninToken" igin uygunlugunu kontrol et
            //if (context.HttpContext.Request.Headers.ContainsKey("AuthenticationScheme") &&
            //    context.HttpContext.Request.Headers["AuthenticationScheme"] == "Client_Token")
            //{
            //    context.Token = token;
            //}
            context.Token = token;
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(opt =>
    {
        opt.Title = "EcomPulse";
        opt.Theme = ScalarTheme.BluePlanet;
        opt.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
        opt.CustomCss = "";
        opt.ShowSidebar = true;
    });
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

