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
            Id = Guid.NewGuid(),
            Name = "Smartphone Samsung Galaxy S23",
            Description = "Smartphone com tela de 6,1 polegadas, processador Snapdragon 8 Gen 2, " +
                          "câmera traseira de 50 MP e bateria de 3900 mAh.",
            Price = 5499M
        },
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Cafeteira Elétrica Oster 127V",
            Description = "Cafeteira elétrica com capacidade de 1,2 L, filtro permanente, e design " +
                          "moderno para preparar café rapidamente.",
            Price = 199.90M
        },
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Cadeira Gamer DXRacer King Series",
            Description = "Cadeira ergonômica com apoio de braço ajustável, suporte lombar e assento reclinável, " +
                          "ideal para longas sessões de jogo.",
            Price = 2299M
        },
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Fone de Ouvido Bluetooth JBL Tune 500BT",
            Description = "Fone de ouvido Bluetooth com som de alta qualidade, até 16 horas de bateria e design " +
                          "dobrável para fácil transporte.",
            Price = 249M
        }
        ,
        new()
        {
            Id = Guid.NewGuid(),
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