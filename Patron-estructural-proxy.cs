using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

// Modelo de Producto
public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

// Interfaz para el Repositorio de Productos
public interface IProductRepository
{
    List<Product> GetAllProducts();
}

// Implementación concreta del Repositorio de Productos
public class ProductRepository : IProductRepository
{
    public List<Product> GetAllProducts()
    {
        // Simulación de la obtención de datos desde una fuente de datos (base de datos, servicio, etc.)
        return new List<Product>
        {
            new Product { ProductId = 1, Name = "Product A", Price = 29.99m },
            new Product { ProductId = 2, Name = "Product B", Price = 39.99m },
            new Product { ProductId = 3, Name = "Product C", Price = 49.99m }
        };
    }
}

// Interfaz para el Proxy del Repositorio de Productos
public interface IProductRepositoryProxy : IProductRepository
{
    // Método adicional para invalidar la caché
    void InvalidateCache();
}

// Implementación concreta del Proxy del Repositorio de Productos
public class CachedProductRepository : IProductRepository, IProductRepositoryProxy
{
    private readonly IProductRepository _productRepository;
    private readonly IMemoryCache _cache;

    public ProductRepositoryProxy(IProductRepository productRepository, IMemoryCache cache)
    {
        _productRepository = productRepository;
        _cache = cache;
    }

    public List<Product> GetAllProducts()
    {
        const string cacheKey = "AllProducts";

        if (!_cache.TryGetValue(cacheKey, out List<Product> products))
        {
            // Si no está en caché, obtén los productos del repositorio y guárdalos en caché
            products = _productRepository.GetAllProducts();
            _cache.Set(cacheKey, products, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) // Cache válida por 5 minutos
            });
        }

        return products;
    }

    public void InvalidateCache()
    {
        const string cacheKey = "AllProducts";
        _cache.Remove(cacheKey);
    }
}

public class Startup
{

    public void Configure(IApplicationBuilder app)
    {
        app.MapGet("/", (IProductRepositoryProxy productRepository) =>
        {
            var products = productRepository.GetAllProducts();
            return Results.Ok(products);
        });


        // Equivalente a Dependency Injection (DI), instanciamos la implementación concreta que nos interesa (CachedProductRepository)
        app.MapGet("/", () =>
        {
            IProductRepositoryProxy productRepository = new CachedProductRepository();
            var products = productRepository.GetAllProducts();
            return Results.Ok(products);
        });

        app.MapGet("/invalidate-cache", (IProductRepositoryProxy productRepository) =>
        {
            productRepository.InvalidateCache();
            return Results.Ok("Cache invalidated");
        });
    }
}




// DI
builder.Services.Add(IProductRepositoryProxy, CachedProductRepository);
