using InsideTeste.Api;
using InsideTeste.CommandStore;
using InsideTeste.Database.Data;
using InsideTeste.Database.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase("inside"));
builder.Services.AddServiceDependencyGroup();
builder.Services.AddCommandStoreDependencyGroup();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    AddTestData(context);
};


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();

static void AddTestData(ApplicationDbContext context)
{
    List<Product> produtos =
    [
        new()
        {
            Id = new Guid("42D42D5C-3FF2-EF11-A1FB-4484C582882F"),
            Name = "Smartphone Samsung Galaxy S23",
            Description = "Smartphone com tela de 6,1 polegadas, processador Snapdragon 8 Gen 2, " +
                          "c�mera traseira de 50 MP e bateria de 3900 mAh.",
            Price = 5499M
        },
        new()
        {
            Id = new Guid("CC17796B-3FF2-EF11-AB9B-7E84C582882F"),
            Name = "Cafeteira El�trica Oster 127V",
            Description = "Cafeteira el�trica com capacidade de 1,2 L, filtro permanente, e design " +
                          "moderno para preparar caf� rapidamente.",
            Price = 199.90M
        },
        new()
        {
            Id = new Guid("92BD5875-3FF2-EF11-9008-DC84C582882F"),
            Name = "Cadeira Gamer DXRacer King Series",
            Description = "Cadeira ergon�mica com apoio de bra�o ajust�vel, suporte lombar e assento reclin�vel, " +
                          "ideal para longas sess�es de jogo.",
            Price = 2299M
        },
        new()
        {
            Id = new Guid("A0B0F07C-3FF2-EF11-9037-EF84C582882F"),
            Name = "Fone de Ouvido Bluetooth JBL Tune 500BT",
            Description = "Fone de ouvido Bluetooth com som de alta qualidade, at� 16 horas de bateria e design " +
                          "dobr�vel para f�cil transporte.",
            Price = 249M
        }
        ,
        new()
        {
            Id = new Guid("A84698A0-3FF2-EF11-9556-6285C582882F"),
            Name = "Notebook Dell Inspiron 15 5000",
            Description = "Notebook com processador Intel Core i5, 8GB de RAM, 512GB de SSD e tela Full HD de " +
                          "15,6 polegadas.",
            Price = 3299M
        }
    ];
    foreach (var product in produtos)
    {
        context.Products.Add(product);
    }    
    context.SaveChanges();
}