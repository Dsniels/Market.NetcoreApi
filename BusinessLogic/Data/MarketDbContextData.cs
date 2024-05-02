using Core.Entities;
using Core.Entities.OrdenCompra;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusinessLogic.Data
{
    public class MarketDbContextData
    {

        public static async Task CargarDataAsync(MarketDbContext context, ILoggerFactory loggerFactory)
        {

            try
            {
                if (!context.Marca.Any())
                {
                    var marcaData = File.ReadAllText("../BusinessLogic/CargarData/marca.json");
                    var marcas = JsonSerializer.Deserialize<List<Marca>>(marcaData);

                    foreach (var marca in marcas)
                    {
                        context.Marca.Add(marca);
                    }
                    await context.SaveChangesAsync();

                }
                if (!context.Categoria.Any())
                {
                    var categoriaData = File.ReadAllText("../BusinessLogic/CargarData/categoria.json");
                    var categorias = JsonSerializer.Deserialize<List<Categoria>>(categoriaData);

                    foreach (var categoria in categorias)
                    {
                        context.Categoria.Add(categoria);
                    }
                    await context.SaveChangesAsync();
                }
                if (!context.Producto.Any())
                {
                    var productoData = File.ReadAllText("../BusinessLogic/CargarData/producto.json");
                    var productos = JsonSerializer.Deserialize<List<Producto>>(productoData);

                    foreach (var producto in productos)
                    {
                        context.Producto.Add(producto);
                    }
                    await context.SaveChangesAsync();
                }
                if (!context.TipoEnvios.Any())
                {
                    var TipoEnvioData = File.ReadAllText("../BusinessLogic/CargarData/TipoEnvio.json");
                    var TipoEnvios = JsonSerializer.Deserialize<List<TipoEnvio>>(TipoEnvioData);

                    foreach (var tipoEnvio in TipoEnvios)
                    {
                        context.TipoEnvios.Add(tipoEnvio);
                    }
                    await context.SaveChangesAsync();
                }

            }
            catch (Exception e)
            {

                var logger = loggerFactory.CreateLogger<MarketDbContextData>();
                logger.LogError(e.Message);
            }




        }
    }
}
